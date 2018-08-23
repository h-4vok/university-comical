using Comical.Models;
using Comical.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public class UnitOfWork
    {
        private bool isConnected = false;
        private bool isTransactionRunning = false;

        private IDbTransaction transaction;
        private IDbConnection connection;

        public T Run<T>(Func<T> closure)
        {
            T output = default(T);

            this.BeginTransactionAndRun(() =>
            {
                output = closure();
            });

            return output;
        }

        public void Run(Action closure)
        {
            this.BeginTransactionAndRun(() =>
            {
                closure();
            });
        }

        protected void BeginTransactionAndRun(Action action)
        {
            using (this.connection = new SqlConnection(ComicalConfiguration.ConnectionString))
            {
                this.connection.Open();
                this.isConnected = true;

                using (this.transaction = this.connection.BeginTransaction())
                {
                    this.isTransactionRunning = true;

                    try
                    {
                        action();
                    }
                    catch
                    {
                        this.transaction.Rollback();
                        this.isTransactionRunning = false;

                        throw;
                    }
                    finally
                    {
                        if (this.isTransactionRunning) this.transaction.Commit();
                    }
                }

                this.connection.Close();
            }
        }

        public void Dispose()
        {
            if (this.transaction != null && this.isTransactionRunning)
            {
                this.transaction.Rollback();
                this.transaction.Dispose();
            }

            if (this.connection != null && this.isConnected)
            {
                this.connection.Close();
                this.connection.Dispose();
            }
        }

        public IEnumerable<T> Get<T>(string procedure, Func<IDataReader, T> fetchClosure, ParametersBuilder parametersBuilder = null)
        {
            var output = new List<T>();

            RunCommand(procedure, (command) =>
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = fetchClosure(reader);
                        output.Add(item);
                    }
                }
            }, parametersBuilder);

            return output;
        }

        public void Execute(string procedure, ParametersBuilder parametersBuilder = null)
        {
            this.RunCommand(
                procedure,
                (command) => command.ExecuteNonQuery(),
                parametersBuilder
            );
        }

        public object Scalar(string procedure, ParametersBuilder parametersBuilder = null)
        {
            object output = null;

            this.RunCommand(
                procedure,
                (command) => output = command.ExecuteScalar(),
                parametersBuilder
            );

            return output;
        }

        protected void RunCommand(string procedure, Action<IDbCommand> closure, ParametersBuilder parametersBuilder = null)
        {
            parametersBuilder = parametersBuilder ?? new ParametersBuilder();

            using (var command = this.connection.CreateCommand())
            {
                command.Transaction = this.transaction;
                command.CommandText = procedure;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = connection;

                parametersBuilder.SetupDbCommand(command);

                closure(command);
            }
        }

        public void ExecuteDirect(string procedure, ParametersBuilder parametersBuilder = null)
        {
            this.Run(() =>
            {
                this.Execute(procedure, parametersBuilder);
            });
        }

        public object ScalarDirect(string procedure, ParametersBuilder parametersBuilder = null)
        {
            var output = this.Run<object>(() =>
            {
                return this.Scalar(procedure, parametersBuilder);
            });

            return output;
        }

        public IEnumerable<T> GetDirect<T>(string procedure, Func<IDataReader, T> fetchClosure, ParametersBuilder parametersBuilder = null)
        {
            var output = this.Run<IEnumerable<T>>(() =>
            {
                return this.Get(procedure, fetchClosure, parametersBuilder);
            });

            return output;
        }

        public void FetchBaseModel(BaseModel model, IDataReader reader)
        {
            model.Id = reader.GetInt32("Id");
        }
    }
}

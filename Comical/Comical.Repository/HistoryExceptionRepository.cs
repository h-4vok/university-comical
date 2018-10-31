using Comical.Models;
using Comical.Models.Common;
using DBNostalgia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public class HistoryExceptionRepository : BaseRepository
    {
        public int New(HistoryException model)
        {
            var output = this.UnitOfWork.Run(() =>
            {
                var id = this.UnitOfWork.Scalar(
                    "HistoryException_new",
                    ParametersBuilder.With("Section", model.Section)
                    .And("ExceptionType", model.ExceptionType)
                    .And("ExceptionSource", model.ExceptionSource)
                    .And("ExceptionMessage", model.ExceptionMessage)
                    .And("ExceptionStackTrace", model.ExceptionStackTrace)
                    .And("UserId", model.UserId)
                ).AsInt();

                this.SetHorizontalVerifier(id);
                this.SetVerticalVerifierClosure();

                return id;
            });

            return output;
        }

        internal HistoryException FetchModel(IDataReader reader)
        {
            var item = new HistoryException
            {
                Id = reader.GetInt32("Id"),
                Section = reader.GetString("Section"),
                ExceptionType = reader.GetString("ExceptionType"),
                ExceptionSource = reader.GetString("ExceptionSource"),
                ExceptionMessage = reader.GetString("ExceptionMessage"),
                ExceptionStackTrace = reader.GetString("ExceptionStackTrace"),
                UserId = reader.GetInt32Nullable("UserId"),
                DateLogged = reader.GetDateTime("DateLogged")
            };

            return item;
        }

        public IEnumerable<HistoryException> Get()
        {
            var output = this.UnitOfWork.GetDirect(
                "HistoryException_get",
                this.FetchModel);

            return output;
        }
    }
}

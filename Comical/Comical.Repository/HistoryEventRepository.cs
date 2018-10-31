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
    public class HistoryEventRepository : BaseRepository
    {
        public int New(HistoryEvent model)
        {
            var output = this.UnitOfWork.Run(() =>
            {
                var id = this.UnitOfWork.Scalar(
                    "HistoryEvent_new",
                    ParametersBuilder.With("Section", model.Section)
                    .And("Message", model.Message)
                    .And("UserId", model.UserId)
                ).AsInt();

                this.SetHorizontalVerifier(id);
                this.SetVerticalVerifierClosure();

                return id;
            });

            return output;
        }

        internal HistoryEvent FetchModel(IDataReader reader)
        {
            var item = new HistoryEvent
            {
                Id = reader.GetInt32("Id"),
                Section = reader.GetString("Section"),
                Message = reader.GetString("Message"),
                DateLogged = reader.GetDateTime("DateLogged"),
                UserId = reader.GetInt32Nullable("UserId")
            };

            return item;
        }

        public IEnumerable<HistoryEvent> Get()
        {
            var output = this.UnitOfWork.GetDirect(
                "HistoryEvent_get",
                this.FetchModel
            ).ToList();

            return output;
        }
    }
}

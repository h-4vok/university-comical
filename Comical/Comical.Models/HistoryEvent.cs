using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models
{
    public class HistoryEvent : BaseModel
    {
        public string Section { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
        public DateTime DateLogged { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models
{
    public class Backup : BaseModel
    {
        public string FilePath { get; set; }
        public DateTime BackupDate { get; set; }
        public string CreatedBy { get; set; }
    }
}

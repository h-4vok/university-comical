using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public IEnumerable<string> GetPropertiesList()
        {
            var properties = this.GetType().GetProperties();
            var propertyNames = properties.Select(x => x.Name).ToList();

            return propertyNames;
        }
    }
}

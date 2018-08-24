using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public abstract class BaseRepository
    {
        public BaseRepository()
        {
            this.UnitOfWork = new UnitOfWork();
        }

        protected UnitOfWork UnitOfWork { get; set; }
    }
}

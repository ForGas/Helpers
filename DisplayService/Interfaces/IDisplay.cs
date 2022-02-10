using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayService.Interfaces
{
    public interface IDisplay
    {
        public void Print<T>(ICollection<T> entities) where T : class;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayService.Interfaces
{
    public interface IDisplay
    {
        void Print<T>(ICollection<T> entities) where T : class;
        void Print<T>(T model) where T : class;
    }
}

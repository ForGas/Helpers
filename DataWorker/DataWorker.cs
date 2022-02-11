using Service.Interfaces;
using Service.DirectoryService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DataWorker
    {
        private readonly IRestSerializer<RestFormat> _restSerializer;

        public DataWorker(IRestSerializer<RestFormat> restSerializer)
        {
            _restSerializer = restSerializer;
        }
    }
}

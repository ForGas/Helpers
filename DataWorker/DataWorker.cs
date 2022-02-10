using DataWorker.Interfaces;
using DirectoryService.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorker
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

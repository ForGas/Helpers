using Service.Interfaces;
using Service.DirectoryService.Base;
using Service.DirectoryService.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Service.Serialize
{
    public abstract class RestSerializer<Format> : IRestSerializer<Format>
        where Format : Enum
    {
        private readonly BaseDirectoryService<RestFormat> _directoryService;

        public RestSerializer(BaseDirectoryService<RestFormat> directoryService)
        {
            _directoryService = directoryService;
        }

        public string ConvertXmlToJsonInFolder<T>(string fileName)
        {
            var obj = DeserializeXmlToObjectInFolder<T>(_directoryService.GetFilePath(fileName));

            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy
                {
                    OverrideSpecifiedNames = false,
                    ProcessDictionaryKeys = false,
                    ProcessExtensionDataNames = false,
                }
            };

            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Newtonsoft.Json.Formatting.Indented,
                TypeNameHandling = TypeNameHandling.None,
                ObjectCreationHandling = ObjectCreationHandling.Auto
            });
        }

        public string? ConvertXmlToJsonInFolder(string fileName)
        {
            return !_directoryService.IsFileExists(fileName) && !_directoryService.VerifyFileNameExtension(fileName)
                ? null
                : ((Func<string>)(() => 
                {
                    var document = new XmlDocument();
                    var filePath = _directoryService.GetFilePath(fileName);

                    document.Load(filePath);

                    return JsonConvert.SerializeXmlNode(document, Newtonsoft.Json.Formatting.Indented, true);
                })).Invoke();
        }

        public string? ConvertXmlToJsonStreamInFolder(string fileName)
        {
            return !_directoryService.IsFileExists(fileName) && !_directoryService.VerifyFileNameExtension(fileName)
               ? null
               : ((Func<string>)(() =>
               {
                   var filePath = _directoryService.GetFilePath(fileName);
                   var document = new XmlDocument();

                   using var stream = new StreamReader(filePath);

                   document.LoadXml(stream.ReadToEnd());

                   return JsonConvert.SerializeXmlNode(document, Newtonsoft.Json.Formatting.Indented, true);
               })).Invoke();
        }

        public T DeserializeXmlToObjectInFolder<T>(string filepath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var reader = new StreamReader(filepath);

            return (T)serializer.Deserialize(reader);
        }
    }
}

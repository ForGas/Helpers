using DataWorker.Interfaces;
using System.Text.RegularExpressions;

namespace DataWorker.Services.Base
{
    public class BaseDirectoryService<FileExtension> : IFileService<FileExtension>
        where FileExtension : Enum
    {
        protected readonly string _directoryPath;
        protected readonly string _filesDirectoryName;

        public BaseDirectoryService(string directory, string filesDirectoryName)
        {
            _directoryPath = directory;
            _filesDirectoryName = filesDirectoryName;
        }

        public string GetDirectoryPath()
        {
            return _directoryPath;
        }

        public string? GetFilePath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            var filesPath = Path.Combine(_filesDirectoryName, fileName);

            return Path.GetFullPath(filesPath, _directoryPath);
        }

        public bool IsFileExists(string fileName)
        {
            var path = GetFilePath(fileName);

            return File.Exists(path);
        }

        public bool VerifyFileNameExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            var regex = new Regex(@"^\w*.");
            string fileExtension = regex.Replace(fileName, "");

            var extensions = Enum.GetNames(typeof(FileExtension));

            foreach (var item in extensions)
            {
                if (item.ToLower() == fileExtension)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

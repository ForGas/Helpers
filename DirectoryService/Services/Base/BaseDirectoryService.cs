using DirectoryService.Interfaces;
using System.Text.RegularExpressions;

namespace DirectoryService.Services.Base
{
    public abstract class BaseDirectoryService<FileExtension> : IFileService<FileExtension>
        where FileExtension : Enum
    {
        protected readonly string _directoryPath;
        protected readonly string _filesDirectoryName;

        protected BaseDirectoryService(string directory, string filesDirectoryName)
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
            return string.IsNullOrEmpty(fileName) 
                ? null 
                : (_filesDirectoryName == new DirectoryInfo(_directoryPath).Name
                    ? string.Concat(_directoryPath, '\\', fileName)
                    : Path.GetFullPath(Path.Combine(_filesDirectoryName, fileName), _directoryPath));
        }

        public string GetExtensionByFileName(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public bool IsFileExists(string fileName)
        {
            var path = GetFilePath(fileName);
            return File.Exists(path);
        }

        public bool VerifyFileNameExtension(string fileName)
        {
            return !string.IsNullOrEmpty(fileName) && ((Func<string, bool>)(static (fileName) =>
            {
                var regex = new Regex(@"^\w*.");
                string fileExtension = regex.Replace(fileName, "");

                var extensions = Enum.GetNames(typeof(FileExtension));
                return extensions.Any(item => item.ToLower() == fileExtension);
            })).Invoke(fileName);
        }

        public List<string>? FindFilesNameByExtension(string approximateFileName)
        {
            return !string.IsNullOrEmpty(approximateFileName) 
                ? null
                : ((Func<List<string>>)(() =>
                {
                    var filesNames = Directory
                            .GetFiles(Path.Combine(_directoryPath, _filesDirectoryName))
                            .Select(x => Path.GetFileName(x))
                            .ToList();

                    var extension = GetExtensionByFileName(approximateFileName);

                    return filesNames.Where(x => extension == GetExtensionByFileName(x)).ToList();
                })).Invoke();
        }
    }
}

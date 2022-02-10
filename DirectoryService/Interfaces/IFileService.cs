namespace DirectoryService.Interfaces
{
    public interface IFileService<FileExtension> : IPath, ICheckFile
        where FileExtension : Enum
    {
        public List<string>? FindFilesNameByExtension(string approximateFileName);
        public string GetExtensionByFileName(string fileName);
    }
}

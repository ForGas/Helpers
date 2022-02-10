namespace DirectoryService.Interfaces
{
    public interface IPath
    {
        public string? GetFilePath(string fileName);
        public string GetDirectoryPath();
    }
}

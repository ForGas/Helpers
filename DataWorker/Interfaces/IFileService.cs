namespace DataWorker.Interfaces
{
    public interface IFileService<FileExtension> : IPath, ICheckFile
        where FileExtension : Enum
    {
    }
}

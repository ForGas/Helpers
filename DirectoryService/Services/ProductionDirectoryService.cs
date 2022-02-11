using Service.Directory.Base;

namespace Service.Directory
{
    public class ProductionDirectoryService<Format> : BaseDirectoryService<Format>
        where Format : Enum
    {
        public ProductionDirectoryService(string fileDirectoryName)
            : base(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory), fileDirectoryName) { }
    }
}
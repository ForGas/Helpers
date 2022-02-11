using Service.Directory.Base;

namespace Service.Directory
{
    public class ProductionDirectoryService<Format> : BaseDirectoryService<Format>
        where Format : Enum
    {
        public ProductionDirectoryService(string filesDirectoryName)
            : base(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory), filesDirectoryName) { }
    }
}
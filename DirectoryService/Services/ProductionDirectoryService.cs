using DirectoryService.Services.Base;

namespace DirectoryService.Services
{
    public class ProductionDirectoryService<Format> : BaseDirectoryService<Format>
        where Format : Enum
    {
        public ProductionDirectoryService(string filesDirectoryName)
            : base(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory), filesDirectoryName)
        {

        }
    }
}

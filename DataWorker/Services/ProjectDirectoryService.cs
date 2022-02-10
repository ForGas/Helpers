using DirectoryService.Services.Base;
using System.Reflection;

namespace DirectoryService.Services
{
    public class ProjectDirectoryService<Format> : BaseDirectoryService<Format>
        where Format : Enum
    {
        public ProjectDirectoryService()
             : base(Path.GetFullPath(
                     Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")),
                  new DirectoryInfo(Path.GetFullPath(
                     Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."))).Name)
        { }
        public ProjectDirectoryService(string filesDirectoryName)
            : base(Path.GetFullPath(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")),
                  filesDirectoryName)
        { }

        public ProjectDirectoryService(Assembly assembly, string filesDirectoryName)
            : base(Path.GetFullPath(
                    Path.Combine(Path.GetDirectoryName(assembly.Location), @"..\..\..")),
                  filesDirectoryName)
        { }
    }
}

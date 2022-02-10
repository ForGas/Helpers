using DataWorker.Services.Base;
using DataWorker.Services.Enums;
using System.Reflection;

namespace DataWorker.Services
{
    public class ProjectDirectoryService : BaseDirectoryService<RestFormat>
    {
        public ProjectDirectoryService(string filesDirectoryName)
            : base(Path.GetFullPath(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..")),
                  filesDirectoryName)
        {

        }

        public ProjectDirectoryService(Assembly assembly, string filesDirectoryName)
            : base(Path.GetFullPath(
                    Path.Combine(Path.GetDirectoryName(assembly.Location), @"..\..\..")),
                  filesDirectoryName)
        {

        }
    }
}

using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BuilderOrganizer
{
    public class BuilderOrganizer
    {
        private static readonly Lazy<BuilderOrganizer> Lazy = new(new BuilderOrganizer());

        private BuilderOrganizer() { }

        public static BuilderOrganizer Instance => Lazy.Value;

        public TEntity CreateEntity<TEntity>() where TEntity : new()
        {
            return (TEntity)Activator.CreateInstance(typeof(TEntity), new object[] { });
        }

        public TEntity CreateEntityUsingInitParams<TEntity>(params object[] properties) where TEntity : new()
        {
            var obj = CreateEntity<TEntity>();
            var paramObjectNames = GetEnteredParametersName(new StackTrace(true));
            var indexCollection = GetCollateIndexDictionary(obj!.GetType(), paramObjectNames);
            var typeProperties = obj.GetType().GetProperties();

            foreach (var item in indexCollection.Where(x => typeProperties[x.Key].PropertyType.Name == properties[x.Value].GetType().Name))
            {
                typeProperties[item.Key].SetValue(obj, properties[item.Value], null);
            }

            return obj;
        }

        public string[] GetEnteredParametersName(StackTrace stackTrace)
        {
            var frame = stackTrace.GetFrame(1);
            var line = File.ReadAllLines(frame.GetFileName())[frame.GetFileLineNumber() - 1].Trim();
            return Regex.Match(line, @"\((.+?)\)").Groups[1].Value
                        .Split(',')
                        .Select(x => x.Trim())
                        .ToArray();
        }

        public Dictionary<int, int> GetCollateIndexDictionary(Type objectType, string[] paramObjectNames)
        {
            var indexDictionary = new Dictionary<int, int>();
            var properties = objectType.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                if (IsContain(properties[i], paramObjectNames))
                {
                    indexDictionary.Add(i, GetIndexByPropertyName(properties[i], paramObjectNames));
                }
            }

            return indexDictionary;
        }

        public int GetIndexByPropertyName(PropertyInfo property, string[] verifiableArr)
        {
            verifiableArr = verifiableArr.Select(x => x.ToUpper()).ToArray() ?? new string[0];
            return verifiableArr.ToList().IndexOf(property.Name.ToUpper());
        }

        public bool IsContain(PropertyInfo property, string[] verifiableArr)
        {
            verifiableArr = verifiableArr.Select(x => x.ToUpper()).ToArray() ?? new string[0];
            return verifiableArr.AsEnumerable().Any(x => x.ToUpper() == property.Name.ToUpper());
        }
    }
}
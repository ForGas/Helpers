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

        public TEntity CreateEntityUsingParams<TEntity>(params object[] properties) where TEntity : new()
        {
            var obj = CreateEntity<TEntity>();
            var paramObjectNames = GetParametrsName(new StackTrace(true));
            var indexCollection = GetIndexDictionary(obj!.GetType(), paramObjectNames);
            var typeProperties = obj.GetType().GetProperties();

            foreach (var item in indexCollection)
            {
                if (typeProperties[item.Key].PropertyType.Name == properties[item.Value].GetType().Name)
                {
                    typeProperties[item.Key].SetValue(obj, properties[item.Value], null);
                }
            }

            return obj;
        }

        public string[] GetParametrsName(StackTrace stackTrace)
        {
            var frame = stackTrace.GetFrame(1);
            var line = File.ReadAllLines(frame.GetFileName())[frame.GetFileLineNumber() - 1].Trim();
            return Regex.Match(line, @"\((.+?)\)").Groups[1].Value
                        .Split(',')
                        .Select(x => x.Trim())
                        .ToArray();
        }

        private Dictionary<int, int> GetIndexDictionary(Type objectType, string[] paramObjectNames)
        {
            var indexDictionary = new Dictionary<int, int>();
            var properties = objectType.GetProperties();
            var valueUpArr = paramObjectNames.Select(x => x.ToUpper()).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                if (IsContain(properties[i], valueUpArr))
                {
                    indexDictionary.Add(i, GetIndexByPropertyName(properties[i], valueUpArr));
                }
            }

            return indexDictionary;
        }

        public int GetIndexByPropertyName(PropertyInfo property, string[] verifiableArr)
        {
            return verifiableArr.ToList().IndexOf(property.Name.ToUpper());
        }

        public bool IsContain(PropertyInfo property, string[] verifiableArr)
        {
            return verifiableArr.AsEnumerable().Any(x => x.ToUpper() == property.Name.ToUpper());
        }
    }
}
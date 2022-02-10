namespace DataWorker.Interfaces
{
    public interface IRestSerializer<Format> : ISerialize
        where Format: Enum
    {
        string ConvertXmlToJsonInFolder<T>(string fileName);

        string? ConvertXmlToJsonInFolder(string fileName);

        string? ConvertXmlToJsonStreamInFolder(string fileName);

        T DeserializeXmlToObjectInFolder<T>(string filepath);
    }
}

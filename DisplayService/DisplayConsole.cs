using DisplayService.Interfaces;

namespace DisplayService
{
    public class DisplayConsole : IDisplay
    {
        public void Print<T>(ICollection<T> models) where T : class
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\t\t\t Start");
            Console.ForegroundColor = ConsoleColor.Blue;

            var properties = models.AsQueryable().ElementType.GetProperties().ToList();
            var entities = models.ToList();

            entities.ForEach(entry =>
            {
                properties.ForEach(property =>
                {
                    Console.Write($"{property.Name} - ");
                    Console.WriteLine($"{entry.GetType().GetProperty(property.Name)!.GetValue(entry, null) ?? "null"}    ");
                });

                Console.WriteLine();
            });
        }

        public void Print<T>(T model) where T : class
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\t\t\t Start");
            Console.ForegroundColor = ConsoleColor.Blue;

            var properties = typeof(T).GetProperties().ToList();

            properties.ForEach(property =>
            {
                Console.Write($"{property.Name} - ");
                Console.WriteLine($"{model.GetType().GetProperty(property.Name)!.GetValue(model, null) ?? "null"}   ");
            });

            Console.WriteLine();
        }
    }
}
using System.Threading.Tasks;

namespace AsyncParallelForEachExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            await Task.Run(() =>
            {
                //Parallel.ForEach(numbers, async number =>
                //{
                //    await Task.Delay(1000); // wait a second
                //    Console.WriteLine($"Number: {number}");
                //});

                Task1Async();
                Task2Async();
            });
            await Task.Run(() => Task2Async());


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        public static async Task Task1Async()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Parallel.ForEach(numbers, async number =>
            {
                await Task.Delay(1000); // wait a second
                Console.WriteLine($"Number: {number}");
            });
        }
        public static async Task Task2Async()
        {
            await Task.Delay(200);
            Console.WriteLine("Task2Async executed after 0.2s!");
        }
    }
}
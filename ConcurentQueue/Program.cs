using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConcurentQueue
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            ConcurrentQueue<int> coll = new ConcurrentQueue<int>();


            Task t1 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; ++i)
                {
                    coll.Enqueue(i);
                    Thread.Sleep(100);
                }
            });

            Task t2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(300);
                foreach (var item in coll)
                {
                    Console.WriteLine(item);
                    Thread.Sleep(150);
                }
            });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ex) // No exception
            {
                Console.WriteLine(ex.Flatten().Message);
            }


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        
    }
}
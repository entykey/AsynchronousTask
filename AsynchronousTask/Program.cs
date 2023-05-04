using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

public class Example
{
    // Asynchronous Main (since vstudio 2017):
    static async Task Main(string[] args)
    {
        #region "example 1 https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskstatus?view=net-7.0"
        /*
        var tasks = new List<Task<int>>();
        var source = new CancellationTokenSource();
        var token = source.Token;
        int completedIterations = 0;

        for (int n = 0; n <= 19; n++)
            tasks.Add(Task.Run(() => {
                int iterations = 0;
                for (int ctr = 1; ctr <= 2000000; ctr++)
                {
                    token.ThrowIfCancellationRequested();
                    iterations++;
                }
                Interlocked.Increment(ref completedIterations);
                if (completedIterations >= 10)
                    source.Cancel();
                return iterations;
            }, token));

        Console.WriteLine("Waiting for the first 10 tasks to complete...\n");
        try
        {
            Task.WaitAll(tasks.ToArray());
        }
        catch (AggregateException)
        {
            Console.WriteLine("Status of tasks:\n");
            Console.WriteLine("{0,10} {1,20} {2,14:N0}", "Task Id",
                              "Status", "Iterations");
            foreach (var t in tasks)
                Console.WriteLine("{0,10} {1,20} {2,14}",
                                  t.Id, t.Status,
                                  t.Status != TaskStatus.Canceled ? t.Result.ToString("N0") : "n/a");
        }
        */
        #endregion

        
        //Task t1 = Accept();
        //t1.Wait();
        Console.WriteLine("Concurrently method:");
        await Task.Run(() => Task1Async());
        await Task.Run(() => Task2Async());
        await Task.Run(() => Task3Async());



        Console.WriteLine("\nTasks list using WhenAll(IEnumerable<Task>) method:");
        var tasks = new List<Task>();
        tasks.Add(Task.Run(() => Task1Async()));
        tasks.Add(Task.Run(() => Task2Async()));
        tasks.Add(Task.Run(() => Task3Async()));
        Task t = Task.WhenAll(tasks);
        
        try
        {
            t.Wait();
        }
        catch { }
        if (t.Status == TaskStatus.RanToCompletion)
            Console.WriteLine("All t tasks succeeded.\n");


        Console.WriteLine("\nTasks array using WhenAll(IEnumerable<Task>) method:");
        Task[] templates = new Task[3]
        {
            Task.Run(() => Task1Async()),
            Task.Run(() => Task2Async()),
            Task.Run(() => Task3Async())
        };
        do
        {
            var doneTask = await Task.WhenAny(tasks);

            // This will return any result, but also throw any exception that
            // might have happened inside the task.
            await doneTask;

            tasks.Remove(doneTask);

        } while (tasks.Count > 0);
        Task t2 = Task.WhenAll(templates);
        try
        {
            t2.Wait();
        }
        catch { }
        if (t2.Status == TaskStatus.RanToCompletion)
            Console.WriteLine("All t2 tasks succeeded.");




        Console.WriteLine("\nParallel using WhenAll(IEnumerable<Task>) method:");
        await Task.Run(() => ParellelTasksAsync());
        Console.WriteLine("All tasks done! Main finished");
        

        #region "WhenAll(IEnumerable<Task>)  https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.whenall?redirectedfrom=MSDN&view=net-7.0#overloads"
        // example 1:
        /*
        int failed = 0;
        var tasks = new List<Task>();
        String[] urls = { "www.facebook.com", "www.cohovineyard.com",
                        "www.cohowinery.com", "www.northwindtraders.com",
                        "www.contoso.com" };

        Console.WriteLine("Pinging...");
        foreach (var value in urls)
        {
            var url = value;
            tasks.Add(Task.Run(() => {
                var png = new Ping();
                try
                {
                    var reply = png.Send(url);
                    if (!(reply.Status == IPStatus.Success))
                    {
                        Interlocked.Increment(ref failed);
                        throw new TimeoutException("Unable to reach " + url + ".");
                    }
                }
                catch (PingException)
                {
                    Interlocked.Increment(ref failed);
                    throw;
                }
            }));
        }
        Task t = Task.WhenAll(tasks);
        try
        {
            t.Wait();
        }
        catch { }

        if (t.Status == TaskStatus.RanToCompletion)
            Console.WriteLine("All ping attempts succeeded.");
        else if (t.Status == TaskStatus.Faulted)
            Console.WriteLine("{0} ping attempts failed", failed);
        
        */

        // example 2:
        /*
        var tasks = new List<Task<long>>();
        for (int ctr = 1; ctr <= 10; ctr++)
        {
            int delayInterval = 18 * ctr;
            tasks.Add(Task.Run(async () => {
                long total = 0;
                await Task.Delay(delayInterval);
                var rnd = new Random();
                // Generate 1,000 random numbers.
                for (int n = 1; n <= 1000; n++)
                    total += rnd.Next(0, 1000);
                return total;
            }));
        }
        var continuation = Task.WhenAll(tasks);
        try
        {
            continuation.Wait();
        }
        catch (AggregateException)
        { }

        if (continuation.Status == TaskStatus.RanToCompletion)
        {
            long grandTotal = 0;
            foreach (var result in continuation.Result)
            {
                grandTotal += result;
                Console.WriteLine("Mean: {0:N2}, n = 1,000", result / 1000.0);
            }

            Console.WriteLine("\nMean of Means: {0:N2}, n = 10,000",
                              grandTotal / 10000);
        }
        // Display information on faulted tasks.
        else
        {
            foreach (var t in tasks)
            {
                Console.WriteLine("Task {0}: {1}", t.Id, t.Status);
            }
        }
        */
        #endregion
    }

    // reference link: https://stackoverflow.com/questions/38634376/running-async-methods-in-parallel
    public static Task ParellelTasksAsync()
    {
        // Start all three tasks
        Task first = Task1Async();
        var second = Task2Async();   // or Task second = Task2Async();
        //var third = Task3Async();
        var third = Task.Run(() =>
        {
            return Task3Async();
        });

        // Then wait for them all to finish
        return Task.WhenAll(first, second, third);
    }

    public static async Task Task1Async()
    {
        await Task.Delay(1000);
        Console.WriteLine("Task1Async executed after 1s!");
    }
    public static async Task Task2Async()
    {
        await Task.Delay(200);
        Console.WriteLine("Task2Async executed after 0.2s!");
    }
    public static async Task Task3Async()
    {
        await Task.Delay(800);
        Console.WriteLine("Task3Async executed after 0.8s!");
    }
    public static async Task Accept()
    {
        while (true)
        {
            await Task.Delay(1000);
        }

        Console.WriteLine("Stoppped");
    }

    /*
    // https://stackoverflow.com/questions/41953102/using-async-await-or-task-in-web-api-controller-net-core
    [HttpGet]
    public async Task<IActionResult> myControllerAction()
    {
        var t1 = service.getdata1Async();
        var t2 = service.getdata2Async();
        var t3 = service.getdata3Async();
        await Task.WhenAll(t1, t2, t3);

        var data = new returnObject
        {
            d1 = await t1,
            d2 = await t2,
            d3 = await t3
        };

        return Ok(data);
    }
    */
}

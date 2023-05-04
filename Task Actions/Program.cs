// https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.-ctor?source=recommendations&view=net-7.0
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

//The following example uses the Task(Action) constructor
//to create tasks that retrieve the filenames in specified
//directories. All tasks write the file names to a single
//ConcurrentBag<T> object. The example then calls the
//WaitAll(Task[]) method to ensure that all tasks have completed,
//and then displays a count of the total number of file names
//written to the ConcurrentBag<T> object.
public class Example
{
    public static async Task Main()
    {
        // method 1: new Task():
        //var list = new ConcurrentBag<string>();
        //string[] dirNames = { ".", ".." };
        //List<Task> tasks = new List<Task>();
        //foreach (var dirName in dirNames)
        //{
        //    Task t = new Task(() => {
        //        foreach (var path in Directory.GetFiles(dirName))
        //            list.Add(path);
        //    });
        //    tasks.Add(t);
        //    t.Start();
        //}
        //await Task.WhenAll(tasks.ToArray());
        //foreach (Task t in tasks)
        //    Console.WriteLine("Task {0} Status: {1}", t.Id, t.Status);

        //Console.WriteLine("Number of files read: {0}", list.Count);


        // method 2: Task.Run():
        var list = new ConcurrentBag<string>();
        string[] dirNames = { ".", ".." };
        List<Task> tasks = new List<Task>();
        foreach (var dirName in dirNames)
        {
            Task t = Task.Run(() =>
            {
                foreach (var path in Directory.GetFiles(dirName))
                    list.Add(path);
            });
            tasks.Add(t);
        }
        Task.WaitAll(tasks.ToArray());
        foreach (Task t in tasks)
            Console.WriteLine("Task {0} Status: {1}", t.Id, t.Status);

        Console.WriteLine("Number of files read: {0}", list.Count);
    }
}
// The example displays output like the following:
//       Task 1 Status: RanToCompletion
//       Task 2 Status: RanToCompletion
//       Number of files read: 23
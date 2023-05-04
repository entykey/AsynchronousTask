// https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.-ctor?source=recommendations&view=net-7.0
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


//The following example defines an array of 6-letter words.
//Each word is then passed as an argument to the T
//ask(Action<Object>, Object) constructor, whose Action<t>
//delegate scrambles the characters in the word, then displays
//the original word and its scrambled version.
public class Example
{
    public static async Task Main()
    {
        var tasks = new List<Task>();
        Random rnd = new Random();
        Object lockObj = new Object();
        String[] words6 = { "reason", "editor", "rioter", "rental",
                          "senior", "regain", "ordain", "rained" };

        foreach (var word6 in words6)
        {
            Task t = new Task((word) => {
                Char[] chars = word.ToString().ToCharArray();
                double[] order = new double[chars.Length];
                lock (lockObj)
                {
                    for (int ctr = 0; ctr < order.Length; ctr++)
                        order[ctr] = rnd.NextDouble();
                }
                Array.Sort(order, chars);
                Console.WriteLine("{0} --> {1}", word,
                                  new String(chars));
            }, word6);
            t.Start();
            tasks.Add(t);
        }
        await Task.WhenAll(tasks.ToArray());
    }
}
// The example displays output like the following:
//    regain --> irnaeg
//    ordain --> rioadn
//    reason --> soearn
//    rained --> rinade
//    rioter --> itrore
//    senior --> norise
//    rental --> atnerl
//    editor --> oteird
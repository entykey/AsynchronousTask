

// correct use bit and pages but wrong arrow pointers
using System;
using System.Collections.Generic;

public class ClockAlgorithm
{
    public static string[][] Run(int numFrames, int[] pages)
    {
        List<string[]> output = new List<string[]>();

        int pointer = 0;
        int[] frames = new int[numFrames];
        bool[] useBits = new bool[numFrames];

        for (int i = 0; i < pages.Length; i++)
        {
            int page = pages[i];
            bool pageFault = true;

            for (int j = 0; j < numFrames; j++)
            {
                if (frames[j] == page)
                {
                    pageFault = false;
                    useBits[j] = true;
                    break;
                }
            }

            if (pageFault)
            {
                while (true)
                {
                    if (!useBits[pointer])
                    {
                        frames[pointer] = page;
                        useBits[pointer] = true;
                        pointer = (pointer + 1) % numFrames;
                        break;
                    }
                    else
                    {
                        useBits[pointer] = false;
                        pointer = (pointer + 1) % numFrames;
                    }
                }
            }

            string[] frameStatus = new string[numFrames];
            for (int j = 0; j < numFrames; j++)
            {
                if (frames[j] == 0)
                {
                    frameStatus[j] = null;
                }
                else if (useBits[j])
                {
                    frameStatus[j] = "->" + frames[j] + "*";
                }
                else
                {
                    frameStatus[j] = "" + frames[j];
                }
            }

            output.Add(frameStatus);
        }

        return output.ToArray();
    }
}

class Program
{
    static void Main(string[] args)
    {
        int numFrames = 3;
        int[] pages = { 2, 9, 6, 8, 2, 4, 3, 7, 5, 3, 9 };

        string[][] output = ClockAlgorithm.Run(numFrames, pages);

        foreach (string[] frameStatus in output)
        {
            Console.WriteLine("{" + string.Join(", ", frameStatus) + "}");
        }
    }
}








/*
// missing '*' 
public class Program
{
    public static void Main()
    {
        int[] referenceString = new int[] { 2, 9, 6, 8, 2, 4, 3, 7, 5 };
        int numFrames = 3;

        int[] frames = new int[numFrames];
        int[] step = new int[numFrames];
        int pointer = 0;
        int faults = 0;

        for (int i = 0; i < numFrames; i++)
        {
            frames[i] = -1;
        }

        for (int i = 0; i < referenceString.Length; i++)
        {
            bool isFault = true;
            for (int j = 0; j < numFrames; j++)
            {
                if (frames[j] == referenceString[i])
                {
                    isFault = false;
                    step[j] = 1;
                    break;
                }
            }

            if (isFault)
            {
                int replaceIndex = -1;
                int replaceStep = 0;
                for (int j = 0; j < numFrames; j++)
                {
                    if (frames[j] == -1)
                    {
                        replaceIndex = j;
                        break;
                    }

                    if (step[j] > replaceStep)
                    {
                        replaceIndex = j;
                        replaceStep = step[j];
                    }
                }

                frames[replaceIndex] = referenceString[i];
                step[replaceIndex] = 1;
                faults++;
            }

            for (int j = 0; j < numFrames; j++)
            {
                if (frames[j] != -1)
                {
                    Console.Write(frames[j] + (step[j] == 1 ? "*" : "") + "\t");
                }
                else
                {
                    Console.Write("null\t");
                }
            }
            Console.WriteLine();

            for (int j = 0; j < numFrames; j++)
            {
                if (frames[j] != -1)
                {
                    step[j]++;
                }
            }
        }

        Console.WriteLine("Number of page faults = " + faults);
    }
}
*/








/*
public class ClockAlgorithm
{
    public static string Run(int numFrames, int[] pages)
    {
        List<int> frameList = new List<int>(numFrames);
        int[] referenceBits = new int[numFrames];
        int currentIndex = 0;
        string output = "";

        for (int i = 0; i < pages.Length; i++)
        {
            int currentPage = pages[i];
            bool pageFound = false;

            for (int j = 0; j < frameList.Count; j++)
            {
                if (frameList.Count > j && frameList[j] == currentPage)
                {
                    referenceBits[j] = 1;
                    pageFound = true;
                    break;
                }
            }

            if (!pageFound)
            {
                while (referenceBits[currentIndex] == 1)
                {
                    referenceBits[currentIndex] = 0;
                    currentIndex = (currentIndex + 1) % numFrames;
                }

                if (referenceBits[currentIndex] == 0)
                {
                    if (frameList.Count <= currentIndex)
                    {
                        frameList.Add(currentPage);
                    }
                    else
                    {
                        frameList[currentIndex] = currentPage;
                    }
                    referenceBits[currentIndex] = 1;
                    currentIndex = (currentIndex + 1) % numFrames;
                }

                pageFound = false;
            }

            for (int j = 0; j < frameList.Count; j++)
            {
                output += frameList[j];

                if (referenceBits[j] == 1)
                {
                    output += "* ";
                }
                else
                {
                    output += " ";
                }
            }

            output += "\n";
        }

        return output;
    }
}

public class Program
{
    public static void Main()
    {
        int numFrames = 3;
        int[] pages = { 2, 9, 6, 8, 2, 4, 3, 7, 5, 3, 9 };
        string output = ClockAlgorithm.Run(numFrames, pages);
        Console.WriteLine(output);
    }
}
*/






/*using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        const int NUM_PAGES = 11;
        const int NUM_FRAMES = 3;   // 4

        //int[] pages = new int[NUM_PAGES] { 1, 2, 3, 4, 1, 2, 5, 1, 2, 3 };
        int[] pages = new int[NUM_PAGES] { 2, 9, 6, 8, 2, 4, 3, 7, 5, 3, 9 };

        Console.WriteLine("FIFO Algorithm:");
        RunAlgorithm(new FifoAlgorithm(NUM_FRAMES), pages);

        Console.WriteLine("LRU Algorithm:");
        RunAlgorithm(new LruAlgorithm(NUM_FRAMES), pages);

        Console.WriteLine("MFU Algorithm:");
        RunAlgorithm(new MfuAlgorithm(NUM_FRAMES), pages);

        Console.WriteLine("Optimal Algorithm:");
        RunAlgorithm(new OptimalAlgorithm(NUM_FRAMES), pages);

        Console.WriteLine("Clock Algorithm:");
        RunAlgorithm(new ClockAlgorithm(NUM_FRAMES), pages);

        Console.ReadLine();
    }

    static void RunAlgorithm(PageReplacementAlgorithm algorithm, int[] pages)
    {
        Console.Write("Page sequence: ");
        foreach (int page in pages)
        {
            Console.Write(page + " ");
            algorithm.AccessPage(page);
        }
        Console.WriteLine();

        Console.WriteLine("Page faults: " + algorithm.PageFaultCount);
    }
}

abstract class PageReplacementAlgorithm
{
    protected int[] Frames;
    protected int[] ReferenceBits;
    protected int PageFaults;
    protected List<string> Steps;

    public int PageFaultCount { get { return PageFaults; } }

    public PageReplacementAlgorithm(int numFrames)
    {
        Frames = new int[numFrames];
        ReferenceBits = new int[256]; // Assuming 256 pages
        for (int i = 0; i < numFrames; i++)
            Frames[i] = -1; // -1 indicates an empty frame

        PageFaults = 0;
    }

    public abstract void AccessPage(int page);
    public int GetPageFaults()
    {
        return PageFaults;
    }
    public List<string> GetSteps()
    {
        return Steps;
    }
}

class FifoAlgorithm : PageReplacementAlgorithm
{
    private int Index;

    public FifoAlgorithm(int numFrames) : base(numFrames)
    {
        Index = 0;
    }

    public override void AccessPage(int page)
    {
        bool found = false;
        for (int i = 0; i < Frames.Length; i++)
        {
            if (Frames[i] == page)
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            Frames[Index] = page;
            Index = (Index + 1) % Frames.Length;
            PageFaults++;
        }
    }
}

class LruAlgorithm : PageReplacementAlgorithm
{
    private List<int> Pages;

    public LruAlgorithm(int numFrames) : base(numFrames)
    {
        Pages = new List<int>();
    }

    public override void AccessPage(int page)
    {
        bool found = false;
        for (int i = 0; i < Frames.Length; i++)
        {
            if (Frames[i] == page)
            {
                found = true;
                Pages.Remove(page);
                Pages.Add(page);
                break;
            }
        }

        if (!found)
        {
            if (Frames[Frames.Length - 1] != -1)
            {
                Pages.Remove(Frames[Frames.Length - 1]);
            }

            for (int i = Frames.Length - 1; i > 0; i--)
            {
                Frames[i] = Frames[i - 1];
            }

            Frames[0] = page;
            Pages.Add(page);

            PageFaults++;
        }
    }
}

class MfuAlgorithm : PageReplacementAlgorithm
{
    private Dictionary<int, int> Counts;

    public MfuAlgorithm(int numFrames) : base(numFrames)
    {
        Counts = new Dictionary<int, int>();
    }

    public override void AccessPage(int page)
    {
        bool found = false;
        for (int i = 0; i < Frames.Length; i++)
        {
            if (Frames[i] == page)
            {
                found = true;
                if (!Counts.ContainsKey(page))
                {
                    Counts[page] = 1;
                }
                else
                {
                    Counts[page]++;
                }
                break;
            }
        }

        if (!found)
        {
            int index = -1;
            int minCount = int.MaxValue;
            for (int i = 0; i < Frames.Length; i++)
            {
                if (Frames[i] == -1)
                {
                    index = i;
                    break;
                }

                if (Counts.ContainsKey(Frames[i]) && Counts[Frames[i]] < minCount)
                {
                    index = i;
                    minCount = Counts[Frames[i]];
                }
            }

            Frames[index] = page;
            Counts[page] = 1;

            PageFaults++;
        }
    }
}

class OptimalAlgorithm : PageReplacementAlgorithm
{
    private List<int> Pages;

    public OptimalAlgorithm(int numFrames) : base(numFrames)
    {
        Pages = new List<int>();
    }

    public override void AccessPage(int page)
    {
        bool found = false;
        for (int i = 0; i < Frames.Length; i++)
        {
            if (Frames[i] == page)
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            if (Frames[Frames.Length - 1] != -1)
            {
                Pages.Remove(Frames[Frames.Length - 1]);
            }

            for (int i = Frames.Length - 1; i > 0; i--)
            {
                Frames[i] = Frames[i - 1];
            }

            Frames[0] = page;
            Pages.Add(page);

            PageFaults++;
        }
    }
}

class ClockAlgorithm : PageReplacementAlgorithm
{
    private int pointer;

    public ClockAlgorithm(int numFrames) : base(numFrames)
    {
        pointer = 0;
    }

    public override void AccessPage(int page)
    {
        bool found = false;
        while (!found)
        {
            for (int i = 0; i < Frames.Length; i++)
            {
                if (Frames[i] == page)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                if (Frames[pointer] == -1)
                {
                    Frames[pointer] = page;
                    pointer = (pointer + 1) % Frames.Length;
                    PageFaults++;
                    break;
                }
                else if (ReferenceBits[pointer] == 0)
                {
                    Frames[pointer] = page;
                    pointer = (pointer + 1) % Frames.Length;
                    PageFaults++;
                    break;
                }
                else
                {
                    ReferenceBits[pointer] = 0;
                    pointer = (pointer + 1) % Frames.Length;
                }
            }
        }

        ReferenceBits[page] = 1;
    }
}
*/
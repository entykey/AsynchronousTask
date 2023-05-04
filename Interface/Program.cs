// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface?source=recommendations
// https://stackoverflow.com/questions/3670057/does-console-writeline-block


class Program
{
    private interface ISampleInterface
    {
        void SampleMethod();    // Task SampleMethod();
    }


    // eg 2:
    public interface Drawable
    {
        void draw();
    }
    public class Rectangle : Drawable
    {
        public void draw()
        {
            Console.WriteLine("drawing rectangle...");
        }
    }
    public class Circle : Drawable
    {
        public void draw()
        {
            Console.WriteLine("drawing circle...");
        }
    }

    interface IPoint
    {
        // Property signatures:
        int X { get; set; }

        int Y { get; set; }

        double Distance { get; }
    }

    static void PrintPoint(IPoint p)
    {
        Console.WriteLine("x={0}, y={1}", p.X, p.Y);
    }

    class Point : IPoint
    {
        // Constructor:
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Property implementation:
        public int X { get; set; }

        public int Y { get; set; }

        // Property implementation
        public double Distance =>
           Math.Sqrt(X * X + Y * Y);
    }


    class ImplementationClass : ISampleInterface
    {
        // Explicit interface member implementation:
        async void ISampleInterface.SampleMethod()
        {
            // Method implementation:
            Console.WriteLine("SampleMethod executed!");
        }

        static async Task Main()
        {
            // Declare an interface instance.
            ISampleInterface obj = new ImplementationClass();

            // Call the member.
            obj.SampleMethod();



            // eg 2:
            Drawable d;
            d = new Rectangle();
            d.draw();
            d = new Circle();
            d.draw();

            IPoint p = new Point(2, 3);
            Console.Write("My Point: ");
            PrintPoint(p);
        }
    }

}
/* output:
SampleMethod executed!
drawing rectangle...
drawing circle...
*/
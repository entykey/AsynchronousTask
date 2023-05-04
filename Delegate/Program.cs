// C# program to illustrate Delegates

class GFG
{

    // Declaring the delegates (interface is better since it create at compile time, not run time like delegate)
    public delegate void AddVal(int x, int y);

    public void AddMedthod(int x, int y)
    {
        Console.WriteLine("[{0} + {1}] = [{2}]", x, y, x + y);
    }
    public void SubtractMethod(int x, int y)
    {
        Console.WriteLine("[{0} - {1}] = [{2}]", x, y, x - y);
    }

    // Main Method
    public static async Task Main(String[] args)
    {

        // Creating the object of GFG class
        GFG o = new GFG();

        // Creating object of delegate
        AddVal obj = new AddVal(o.AddMedthod);

        // Pass the values to the method
        // Using delegate object
        obj(190, 70);
        obj(10, 100);


        // Creating object of delegate
        AddVal obj1 = new AddVal(o.SubtractMethod);

        // Pass the values to the method
        // Using delegate object
        obj1(190, 70);
        obj1(10, 100);
    }
}
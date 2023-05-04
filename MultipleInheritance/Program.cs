// https://www.geeksforgeeks.org/c-sharp-multiple-inheritance-using-interfaces/

using System.Collections;

class Program
{
    interface ILanguages
    {
        void languages();
    }

    // Parent class 1
    class Languages : ILanguages
    {

        // Providing the implementation
        // of languages() method
        public void languages()
        {

            // Creating ArrayList
            ArrayList My_list = new ArrayList();

            // Adding elements in the
            // My_list ArrayList
            My_list.Add("C");
            My_list.Add("C++");
            My_list.Add("C#");
            My_list.Add("Java");

            Console.WriteLine("Languages used by GeeksforGeeks:");
            foreach (var elements in My_list)
            {
                Console.WriteLine(elements);
            }
        }
    }

    // Interface 2
    interface ICourses
    {
        void courses();
    }

    // Parent class 2
    class Courses : ICourses
    {

        // Providing the implementation
        // of courses() method
        public void courses()
        {

            // Creating ArrayList
            ArrayList My_list = new ArrayList();

            // Adding elements in the
            // My_list ArrayList
            My_list.Add("System Design");
            My_list.Add("Fork Python");
            My_list.Add("Geeks Classes DSA");
            My_list.Add("Fork Java");

            Console.WriteLine("\nCourses provided by GeeksforGeeks:");
            foreach (var elements in My_list)
            {
                Console.WriteLine(elements);
            }
        }
    }

    // Child class
    class GeeksforGeeks : ILanguages, ICourses    // derived from ILanguages, ICourses
    {

        // Creating objects of Languages and Courses class
        Languages GeekLangs = new Languages();
        Courses GeekCours = new Courses();

        public void languages()
        {
            GeekLangs.languages();
        }

        public void courses()
        {
            GeekCours.courses();
        }
    }

    // Driver Class
    public class GFG
    {

        // Main method
        static public void Main()
        {

            // Creating object of GeeksforGeeks class
            GeeksforGeeks obj = new GeeksforGeeks();
            obj.languages();
            obj.courses();
        }
    }

}

/* output:
Languages provided by GeeksforGeeks:
C
C++
C#
Java

Courses provided by GeeksforGeeks:
System Design
Fork Python
Geeks Classes DSA
Fork Java
*/
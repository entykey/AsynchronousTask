using System;
using System.IO;

class FileStream
{
    public static void Main()
    {
        try
        {
            // Only get files that begin with the letter "c".
            string[] dirs = Directory.GetFiles(@"C:\Users\TuanMacbookPro\Documents", "A*");
            Console.WriteLine("The number of files starting with A is {0}.", dirs.Length);
            foreach (string dir in dirs)
            {
                Console.WriteLine($"Found: {dir}");
            }



            Console.WriteLine("\n\nList of .txt files in Documents:");

            DirectoryInfo d = new DirectoryInfo(@"C:\Users\TuanMacbookPro\Documents"); //Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files

            foreach (FileInfo file in Files)
            {
                Console.WriteLine(file.Name); // list all .txt files
            }


            Console.WriteLine("\n\n");



            Console.WriteLine("\nList of .mp4 files in Downloads:");

            DirectoryInfo video = new DirectoryInfo(@"C:\Users\TuanMacbookPro\Downloads\saved videos"); //Assuming Test is your Folder

            FileInfo[] videoFiles = video.GetFiles("*.mp4"); //Getting Text files

            Console.WriteLine($"Num of files: {videoFiles.Length}");

            foreach (FileInfo videofile in videoFiles)
            {
                Console.WriteLine($"    {videofile.Name}"); // list all .mp4 files
                //str = str + ", " + file.Name;
            }



            Console.WriteLine("\n\n");



            Console.WriteLine("\nList of all child folders name from a direcotry:");
            var subdirs = Directory.GetDirectories(@"C:\Users\TuanMacbookPro\Documents")
                            .Select(p => new {
                                Path = p,
                                Name = Path.GetFileName(p)
                            })
                            .ToArray();
            foreach(var subdir in subdirs)
            {
                Console.WriteLine(subdir.Name + "      (" + subdir.Path  + ")");

                // it may not be allowed to get child folders, so handle with try catch block:
                try
                {
                    var childsubdirs = Directory.GetDirectories((subdir.Path))
                    .Select(p => new {
                        Path = p,
                        Name = Path.GetFileName(p)
                    })
                            .ToArray();

                    foreach (var childsubdir in childsubdirs)
                    {
                        Console.WriteLine($"    {childsubdir.Name}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to get {subdir}'s child directories, access is denied");
                }
            }


            Console.WriteLine("\n\n");



            // 2nd way:
            Console.WriteLine("\nList of all direcotries name:");
            // Make a reference to a directory.
            DirectoryInfo di1 = new DirectoryInfo("C:\\Users\\TuanMacbookPro\\Documents");

            // Get a reference to each directory in that directory.
            DirectoryInfo[] diArr = di1.GetDirectories();

            // Display the names of the directories.
            foreach (DirectoryInfo dri in diArr)
                Console.WriteLine(dri.Name);



        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }
}

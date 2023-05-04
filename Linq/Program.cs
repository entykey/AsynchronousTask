using System.Text;

public class LinqExample
{
    // Asynchronous Main (since vstudio 2017):
    public static async Task Main(string[] args)
    {
        //IList<Student> studentList = new List<Student>() {
        //    new Student() { StudentID = 1, StudentName = "John", Age = 13, Subjects = new List<string> { "Mathematics", "Physics" }} ,
        //    new Student() { StudentID = 2, StudentName = "Moin", Age = 21, Subjects = new List<string> { "Social Studies", "Chemistry" } } ,
        //    new Student() { StudentID = 3, StudentName = "Bill", Age = 18, Subjects = new List<string> { "Biology", "History", "Geography" } } ,
        //    new Student() { StudentID = 4, StudentName = "Ram" , Age = 20, Subjects = new List<string> { "English", "Zoology", "Botany" }} ,
        //    new Student() { StudentID = 5, StudentName = "Ron" , Age = 15, Subjects = new List<string> { "Civics", "Drawing" } },
        //    new Student() { StudentID = 6, StudentName = "Seth", Age = 19, Subjects = new List<string> { "Civics", "History" } },
        //};
        IList<Student> studentList = new List<Student>() {
                    new Student() { StudentID = 1, StudentName = "John", Age = 13 },
            new Student() { StudentID = 2, StudentName = "Moin", Age = 21 },
            new Student() { StudentID = 3, StudentName = "Bill", Age = 18 },
            new Student() { StudentID = 4, StudentName = "Ram", Age = 20 },
            new Student() { StudentID = 5, StudentName = "Ron", Age = 15 },
            new Student() { StudentID = 6, StudentName = "Seth", Age = 19 },
        };

        IList<Subject> subjectList = new List<Subject>() {
    new Subject() { SubjectID = 1, SubjectName = "Mathematics" },
    new Subject() { SubjectID = 2, SubjectName = "Physics" },
    new Subject() { SubjectID = 3, SubjectName = "Social Studies" },
    new Subject() { SubjectID = 4, SubjectName = "Chemistry" },
    new Subject() { SubjectID = 5, SubjectName = "Biology" },
    new Subject() { SubjectID = 6, SubjectName = "History" },
    new Subject() { SubjectID = 7, SubjectName = "Geography" },
    new Subject() { SubjectID = 8, SubjectName = "English" },
    new Subject() { SubjectID = 9, SubjectName = "Zoology" },
    new Subject() { SubjectID = 10, SubjectName = "Botany" },
    new Subject() { SubjectID = 11, SubjectName = "Civics" },
    new Subject() { SubjectID = 12, SubjectName = "Drawing" },
    };

        IList<Enrollment> enrollmentList = new List<Enrollment>() {
        new Enrollment() { StudentID = 1, SubjectID = 1 },
        new Enrollment() { StudentID = 1, SubjectID = 2 },
        new Enrollment() { StudentID = 2, SubjectID = 3 },
        new Enrollment() { StudentID = 2, SubjectID = 4 },
        new Enrollment() { StudentID = 3, SubjectID = 5 },
        new Enrollment() { StudentID = 3, SubjectID = 6 },
        new Enrollment() { StudentID = 3, SubjectID = 7 },
        new Enrollment() { StudentID = 4, SubjectID = 8 },
        new Enrollment() { StudentID = 4, SubjectID = 9 },
        new Enrollment() { StudentID = 4, SubjectID = 10 },
        new Enrollment() { StudentID = 5, SubjectID = 11 },
        new Enrollment() { StudentID = 5, SubjectID = 12 },
        new Enrollment() { StudentID = 6, SubjectID = 11 },
        new Enrollment() { StudentID = 6, SubjectID = 6 },
        };


        // Lambda (Method) syntax:
        //var filteredResult = studentList
        //                .Where(x => x.StudentName.StartsWith("R"))
        //                .Select(x => x.StudentName);

        // query syntax:
        var filteredResult = from x in studentList
                             where x.StudentName.StartsWith("R")
                             where x.StudentName.EndsWith("m")
                             select x.StudentName;

        var filteredResult1 = from s in studentList
                              where IsTeenAger(s)    // where s.Age > 12 && s.Age < 20
                              orderby s.Age descending, s.StudentName descending
                              select s;  // select s

        foreach (var studentName in filteredResult)
        {
            Console.WriteLine(studentName);
        }
        Console.WriteLine("");
        foreach (var s in filteredResult1)
        {
            Console.WriteLine($"Id: {s.StudentID}, StudentName: {s.StudentName}, Age: {s.Age}");
        }





        var studentListwithEnrolledSubjects =
              from student in studentList
              join enrollment in enrollmentList on student.StudentID equals enrollment.StudentID
              join subject in subjectList on enrollment.SubjectID equals subject.SubjectID
              group subject.SubjectName by new { student.StudentID, student.StudentName, student.Age } into g
              //select new
              //{
              //    g.Key.StudentID,
              //    g.Key.StudentName,
              //    g.Key.Age,
              //    SubjectsEnrolled = g.ToList()
              //};
              select $"{{ StudentId: {g.Key.StudentID}, StudentName: {g.Key.StudentName}, Age: {g.Key.Age}, SubjectsEnrolled: {string.Join(", ", g.ToList())} }}";

        Console.WriteLine("\n");
        foreach (var student in studentListwithEnrolledSubjects)
        {
            Console.WriteLine(student);
            Console.WriteLine("\n");
        }





        #region Projection operator
        // get subject list of all students (distinct):
        // SelectMany() (Lambda / Method syntax)
        Console.WriteLine("");
        //var Subjects = studentList.SelectMany(x => x.Subjects);

        // remove duplicated records
        //var Subjects = studentList.SelectMany(x => x.Subjects).Distinct();

        var Subjects = studentList
               .Join(enrollmentList, s => s.StudentID, e => e.StudentID, (s, e) => new { s, e })
               .Join(subjectList, se => se.e.SubjectID, sub => sub.SubjectID, (se, sub) => sub.SubjectName)
               .Distinct();


        foreach (var Subject in Subjects)
        {
            Console.WriteLine(Subject);
        }

        // Query (Comprehension) syntax
        Console.WriteLine("");
        //var Subjects1 = from student in studentList
        //                from subject in student.Subjects
        //                select subject;

        // remove duplicated records:
        var Subjects1 = (from enrollment in enrollmentList
                         join subject in subjectList
                         on enrollment.SubjectID equals subject.SubjectID
                         select subject.SubjectName).Distinct();


        foreach (var Subject in Subjects1)
        {
            Console.WriteLine(Subject);
        }
        Console.WriteLine("");
        Subjects1.ToList().ForEach(s =>
        {
            Console.WriteLine(s);
        });
        #endregion



        Console.WriteLine("\nJoin operator:\n");
        #region Join
        IList<string> strList1 = new List<string>() {
            "One",
            "Two",
            "Three",
            "Four"
        };

        IList<string> strList2 = new List<string>() {
            "One",
            "Two",
            "Five",
            "Six"
        };
        var innerJoin = strList1.Join(strList2,
                      str1 => str1,
                      str2 => str2,
                      (str1, str2) => str1);
        foreach(var n in innerJoin)
        {
            Console.WriteLine(n);
        }
        #endregion
    }

    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
    public class Enrollment
    {
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
    }
    public class Subject
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }

    public class Post
    {
        public int StudentID;
    }
    public static bool IsTeenAger(Student stud)
    {
        return stud.Age > 12 && stud.Age < 20;
    }
}

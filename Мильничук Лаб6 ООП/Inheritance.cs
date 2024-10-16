using System;
using System.Text;

public class Book
{
    private string author;
    private string title;
    private decimal price;

    public Book(string author, string title, decimal price)
    {
        this.Author = author;
        this.Title = title;
        this.Price = price;
    }

    public string Author
    {
        get { return this.author; }
        set
        {
            if (char.IsDigit(value[0]))
                throw new ArgumentException("Автор не дійсний!");
            this.author = value;
        }
    }

    public string Title
    {
        get { return this.title; }
        set
        {
            if (value.Length < 3)
                throw new ArgumentException("Title not valid!");
            this.title = value;
        }
    }

    public virtual decimal Price
    {
        get { return this.price; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Ціна не дійсна!");
            this.price = value;
        }
    }

    public override string ToString()
    {
        var resultBuilder = new StringBuilder();
        resultBuilder.AppendLine($"Type: {this.GetType().Name}")
                     .AppendLine($"Title: {this.Title}")
                     .AppendLine($"Author: {this.Author}")
                     .AppendLine($"Price: {this.Price:f2}");
        return resultBuilder.ToString().TrimEnd();
    }
}

public class GoldenEditionBook : Book
{
    public GoldenEditionBook(string author, string title, decimal price)
        : base(author, title, price)
    {
    }

    public override decimal Price
    {
        get { return base.Price * 1.3m; }
    }
}

public class Human
{
    private string firstName;
    private string lastName;

    public Human(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public string FirstName
    {
        get { return this.firstName; }
        set
        {
            if (char.IsLower(value[0]))
                throw new ArgumentException("Expected upper case letter! Argument: firstName");
            if (value.Length < 4)
                throw new ArgumentException("Expected length at least 4 symbols! Argument: firstName");
            this.firstName = value;
        }
    }

    public string LastName
    {
        get { return this.lastName; }
        set
        {
            if (char.IsLower(value[0]))
                throw new ArgumentException("Expected upper case letter! Argument: lastName");
            if (value.Length < 3)
                throw new ArgumentException("Expected length at least 3 symbols! Argument: lastName");
            this.lastName = value;
        }
    }
}

public class Student : Human
{
    private string facultyNumber;

    public Student(string firstName, string lastName, string facultyNumber)
        : base(firstName, lastName)
    {
        this.FacultyNumber = facultyNumber;
    }

    public string FacultyNumber
    {
        get { return this.facultyNumber; }
        set
        {
            if (value.Length < 5 || value.Length > 10)
                throw new ArgumentException("Invalid faculty number!");
            this.facultyNumber = value;
        }
    }
}

public class Worker : Human
{
    private decimal weekSalary;
    private int workHoursPerDay;

    public Worker(string firstName, string lastName, decimal weekSalary, int workHoursPerDay)
        : base(firstName, lastName)
    {
        this.WeekSalary = weekSalary;
        this.WorkHoursPerDay = workHoursPerDay;
    }

    public decimal WeekSalary
    {
        get { return this.weekSalary; }
        set
        {
            if (value <= 10)
                throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
            this.weekSalary = value;
        }
    }

    public int WorkHoursPerDay
    {
        get { return this.workHoursPerDay; }
        set
        {
            if (value < 1 || value > 12)
                throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
            this.workHoursPerDay = value;
        }
    }

    public decimal SalaryPerHour()
    {
        return this.WeekSalary / (5 * WorkHoursPerDay);
    }
}

public class Program
{
    public static void Main()
    {
        // Завдання 1
        try
        {
            Console.WriteLine("Введіть автора, назву та ціну книги:");
            string author = Console.ReadLine();
            string title = Console.ReadLine();
            decimal price = decimal.Parse(Console.ReadLine());
            Book book = new Book(author, title, price);
            GoldenEditionBook goldenEditionBook = new GoldenEditionBook(author, title, price);
            Console.WriteLine(book + Environment.NewLine);
            Console.WriteLine(goldenEditionBook);
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
        }

        // Завдання 2
        try
        {
            Console.WriteLine("Введіть інформацію про студента (ім'я, прізвище, номер факультету):");
            string[] studentInfo = Console.ReadLine().Split();
            string studentFirstName = studentInfo[0];
            string studentLastName = studentInfo[1];
            string studentFacultyNumber = studentInfo[2];
            Student student = new Student(studentFirstName, studentLastName, studentFacultyNumber);

            Console.WriteLine("Введіть інформацію про працівника (ім'я, прізвище, зарплата, години на день):");
            string[] workerInfo = Console.ReadLine().Split();
            string workerFirstName = workerInfo[0];
            string workerLastName = workerInfo[1];
            decimal workerSalary = decimal.Parse(workerInfo[2]);
            int workerHours = int.Parse(workerInfo[3]);
            Worker worker = new Worker(workerFirstName, workerLastName, workerSalary, workerHours);

            Console.WriteLine($"Ім'я студента: {student.FirstName}");
            Console.WriteLine($"Прізвище студента: {student.LastName}");
            Console.WriteLine($"Номер факультету: {student.FacultyNumber}");
            Console.WriteLine();
            Console.WriteLine($"Ім'я працівника: {worker.FirstName}");
            Console.WriteLine($"Прізвище працівника: {worker.LastName}");
            Console.WriteLine($"Зарплата за тиждень: {worker.WeekSalary:f2}");
            Console.WriteLine($"Години на день: {worker.WorkHoursPerDay}");
            Console.WriteLine($"Зарплата за годину: {worker.SalaryPerHour():f2}");
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
        }
    }
}

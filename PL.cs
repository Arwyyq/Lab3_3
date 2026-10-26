using System;
using System.Collections.Generic;
using BLL; //Підключає BLL, щоб бачити EntityService
using DAL; //Підключає DAL, щоб бачити клас Student

//Клас Menu для рівня PL
public class Menu
{
    private readonly EntityService _service;

    public Menu()
    {
        _service = new EntityService();
    }

    public void MainMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("1. Додати нового студента");
            Console.WriteLine("2. Вивести всіх студентів");
            Console.WriteLine("3. Порахувати студентів чоловіків");
            Console.WriteLine("4. Редагувати студента");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    AddStudentDialog();
                    break;
                case "2":
                    ShowAllStudents();
                    break;
                case "3":
                    CountingMaleStudents();
                    break;
                case "4":
                    EditStudentDialog(); // НОВИЙ ВИКЛИК
                    break;
                case "0":
                    isRunning = false;
                    Console.WriteLine("Програму завершено.");
                    break;
                default:
                    Console.WriteLine("Невірний вибір!");
                    break;
            }
        }
    }

    private void AddStudentDialog()
    {
        Console.WriteLine("\n--- Додавання студента ---");

        Console.Write("Прізвище: ");
        string lastName = Console.ReadLine() ?? "";

        Console.Write("Ім'я: ");
        string firstName = Console.ReadLine() ?? "";

        Console.Write("Курс: ");
        if (!int.TryParse(Console.ReadLine(), out int course))
            course = 1;

        Console.Write("Студентський квиток: ");
        string studentId = Console.ReadLine() ?? "";

        Console.Write("Стать (Чоловіча/Жіноча): ");
        string gender = Console.ReadLine() ?? "";

        Console.Write(
            "Місце проживання (якщо гуртожиток, введіть 'номер-кімната', наприклад '4-105'): "
        );
        string residence = Console.ReadLine() ?? "";

        Student newStudent = new Student(lastName, firstName, course, studentId, gender, residence);

        //Передає студента на рівень BLL для збереження
        _service.AddStudent(newStudent);
        Console.WriteLine("Студента успішно збережено!");
    }

    private void ShowAllStudents()
    {
        Console.WriteLine("\n--- Всі студенти ---");
        List<Student> students = _service.GetAllStudents();

        if (students.Count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        foreach (var s in students)
        {
            Console.WriteLine(s.ToString());
        }
    }

    private void CountingMaleStudents()
    {
        Console.WriteLine(
            "Обчислити кількість студентів чоловіків 3-го курсу, які проживають у гуртожитку."
        );

        //Звертаємося до BLL за логікою
        int count = _service.CountThirdCourseMaleStudentsInDorm();
        List<Student> targetStudents = _service.GetThirdCourseMaleStudentsInDorm();

        Console.WriteLine($"\nЗнайдено студентів: {count}");

        if (count > 0)
        {
            Console.WriteLine("Їхні дані:");
            foreach (var s in targetStudents)
            {
                Console.WriteLine(s.ToString());
            }
        }
    }

    private void EditStudentDialog()
    {
        Console.WriteLine("\n--- Редагування студента ---");
        Console.Write("Введіть номер студентського квитка для пошуку: ");
        string idToSearch = Console.ReadLine() ?? "";

        //Щоб не ускладнювати, просто збирає нові дані так само, як при додаванні
        Console.WriteLine("\nВведіть нові дані для цього студента:");

        Console.Write("Прізвище: ");
        string lastName = Console.ReadLine() ?? "";

        Console.Write("Ім'я: ");
        string firstName = Console.ReadLine() ?? "";

        Console.Write("Курс: ");
        if (!int.TryParse(Console.ReadLine(), out int course))
            course = 1;

        Console.Write("Стать (Чоловіча/Жіноча): ");
        string gender = Console.ReadLine() ?? "";

        Console.Write("Місце проживання: ");
        string residence = Console.ReadLine() ?? "";

        //Створює об'єкт з новими даними. Квиток залишає старим
        Student updatedStudent = new Student(
            lastName,
            firstName,
            course,
            idToSearch,
            gender,
            residence
        );

        //Викликає метод з BLL
        bool success = _service.UpdateStudent(idToSearch, updatedStudent);

        if (success)
        {
            Console.WriteLine("Дані студента успішно оновлено!");
        }
        else
        {
            Console.WriteLine("Помилка: Студента з таким квитком не знайдено.");
        }
    }
}

//Головний клас програми
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        //В методі Main() повинен бути тільки виклик методу MainMenu() з класу Menu()
        Menu appMenu = new Menu();
        appMenu.MainMenu();
    }
}

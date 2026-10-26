using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

//Створює логічний простір імен для нашого рівня доступу до даних
namespace DAL
{
    //Клас сутності (Entity) - Студент
    [Serializable]
    public class Student
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public int Course { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        //Місце проживання: якщо гуртожиток, то "номер гуртожитку-кімната"
        public string Residence { get; set; } = string.Empty;

        //Порожній конструктор обов'язковий для JSON-серіалізації
        public Student() { }

        public Student(
            string lastName,
            string firstName,
            int course,
            string studentId,
            string gender,
            string residence
        )
        {
            LastName = lastName;
            FirstName = firstName;
            Course = course;
            StudentId = studentId;
            Gender = gender;
            Residence = residence;
        }

        //Перевизначає ToString для зручного виводу
        public override string ToString()
        {
            return $"{LastName} {FirstName}, {Course} курс, Квиток: {StudentId}, Стать: {Gender}, Проживання: {Residence}";
        }
    }

    //Клас для роботи з даними (базою)
    public class EntityContext
    {
        //Шлях до файлу "бази даних"
        private readonly string _filePath = "students_db.json";

        //Метод для збереження списку студентів у файл
        public void SaveData(List<Student> students)
        {
            //Використовує JSON серіалізацію
            string jsonString = JsonSerializer.Serialize(
                students,
                new JsonSerializerOptions { WriteIndented = true }
            );
            File.WriteAllText(_filePath, jsonString);
        }

        //Метод для зчитування списку студентів з файлу
        public List<Student> LoadData()
        {
            //Якщо файлу ще немає, повертає порожній список, щоб не було помилки
            if (!File.Exists(_filePath))
            {
                return new List<Student>();
            }

            string jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Student>>(jsonString) ?? new List<Student>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DAL; //Підключає наш рівень доступу до даних

namespace BLL
{
    //Клас-сервіс, який містить логіку роботи зі студентами
    public class EntityService
    {
        private readonly EntityContext _context;

        //Конструктор створює об'єкт DAL для роботи з файлом
        public EntityService()
        {
            _context = new EntityContext();
        }

        //Метод для додавання нового студента (і збереження у файл)
        public void AddStudent(Student newStudent)
        {
            List<Student> students = _context.LoadData();
            students.Add(newStudent);
            _context.SaveData(students);
        }

        //Метод для отримання всіх студентів
        public List<Student> GetAllStudents()
        {
            return _context.LoadData();
        }

        public bool UpdateStudent(string studentId, Student updatedData)
        {
            List<Student> students = _context.LoadData();

            //Шукає студента за номером квитка
            Student studentToUpdate = students.FirstOrDefault(s => s.StudentId == studentId);

            if (studentToUpdate != null)
            {
                //Оновлює дані
                studentToUpdate.LastName = updatedData.LastName;
                studentToUpdate.FirstName = updatedData.FirstName;
                studentToUpdate.Course = updatedData.Course;
                studentToUpdate.Gender = updatedData.Gender;
                studentToUpdate.Residence = updatedData.Residence;

                //Зберігає оновлений список у файл
                _context.SaveData(students);
                return true; //Редагування успішне
            }

            return false; //Студента не знайдено
        }

        //Обчислення кількількості студентів чоловіків 3-го курсу, які проживають у гуртожитку.
        public int CountThirdCourseMaleStudentsInDorm()
        {
            List<Student> students = _context.LoadData();

            //Використовує LINQ для зручного фільтрування
            int count = students.Count(s =>
                s.Gender.ToLower() == "чоловіча"
                && // Чоловік
                s.Course == 3
                && // 3-й курс
                s.Residence.Contains("-") //Проживає в гуртожитку (формат "номер гуртожитку-кімната")
            );

            return count;
        }

        //Метод для отримання самих студентів, які підпадають під критерії (щоб вивести їх на екран)
        public List<Student> GetThirdCourseMaleStudentsInDorm()
        {
            List<Student> students = _context.LoadData();

            return students
                .Where(s =>
                    s.Gender.ToLower() == "чоловіча" && s.Course == 3 && s.Residence.Contains("-")
                )
                .ToList();
        }
    }
}

using System;
using System.Collections.Generic;

namespace CourseManagement
{
    internal class Student
    {
        public string FullName { get; set; }
        public int Id { get; set; }
        public string Group { get; set; }

        public Student(string fullName, int id, string group) { FullName = fullName; Id = id; Group = group; }
        public override bool Equals(object obj) { Student other = obj as Student; return other != null && Id == other.Id; }
        public override int GetHashCode() { return Id.GetHashCode(); }
    }

    internal class CourseProgress
    {
        public string CourseName { get; set; }
        public int Score { get; set; }
        public int CompletedTopics { get; set; }
        public string CurrentTopic { get; set; }

        public CourseProgress(string courseName, int score, int completedTopics, string currentTopic)
        { CourseName = courseName; Score = score; CompletedTopics = completedTopics; CurrentTopic = currentTopic; }
    }

    internal class Program
    {
        private static Dictionary<Student, List<CourseProgress>> students = new Dictionary<Student, List<CourseProgress>>();

        private static void Main()
        {
            // Задание 4
            // Разработайте систему управления учебными курсами и студентами.
            // Храните данные с помощью Dictionary<Student, List<CourseProgress>>.
            // Student: ФИО, ID, группа. CourseProgress: название курса, текущий балл,
            // завершенные темы, текущая тема.
            // Реализуйте: добавление нового студента или обновление существующего;
            // добавление курса и обновление прогресса; поиск и фильтрацию по группе,
            // статусу прогресса и баллу; сортировку студентов по успеваемости.
            // Студенты учатся на программистов в группах ПВ411 и ПВ412.

            AddExampleData();
            while (true)
            {
                Console.WriteLine("\n1 - добавить/изменить студента");
                Console.WriteLine("2 - добавить/изменить курс");
                Console.WriteLine("3 - список студентов");
                Console.WriteLine("4 - фильтр по группе");
                Console.WriteLine("5 - фильтр по баллу");
                Console.WriteLine("6 - сортировка по успеваемости");
                Console.WriteLine("0 - выход");
                Console.Write("Действие: ");
                string action = Console.ReadLine();
                if (action == "1") AddOrUpdateStudent();
                else if (action == "2") AddOrUpdateProgress();
                else if (action == "3") ShowAll();
                else if (action == "4") FilterByGroup();
                else if (action == "5") FilterByScore();
                else if (action == "6") SortByPerformance();
                else if (action == "0") return;
                else Console.WriteLine("Неверный выбор.");
            }
        }

        private static void AddExampleData()
        {
            Student first = new Student("Анна Коваль", 1, "ПВ411");
            Student second = new Student("Максим Бондар", 2, "ПВ412");
            students.Add(first, new List<CourseProgress> { new CourseProgress("C#", 92, 8, "Generics") });
            students.Add(second, new List<CourseProgress> { new CourseProgress("C#", 76, 6, "Интерфейсы") });
        }

        private static Student FindStudent(int id)
        {
            foreach (Student student in students.Keys) if (student.Id == id) return student;
            return null;
        }

        private static void AddOrUpdateStudent()
        {
            Console.Write("ID: "); int id;
            if (!int.TryParse(Console.ReadLine(), out id)) { Console.WriteLine("ID должен быть числом."); return; }
            Console.Write("ФИО: "); string name = Console.ReadLine();
            Console.Write("Группа (ПВ411 или ПВ412): "); string group = Console.ReadLine().ToUpper();
            if (group != "ПВ411" && group != "ПВ412") { Console.WriteLine("Допустимы только группы ПВ411 и ПВ412."); return; }
            Student existing = FindStudent(id);
            if (existing == null) students.Add(new Student(name, id, group), new List<CourseProgress>());
            else { existing.FullName = name; existing.Group = group; }
            Console.WriteLine("Данные студента сохранены.");
        }

        private static void AddOrUpdateProgress()
        {
            Console.Write("ID студента: "); int id;
            if (!int.TryParse(Console.ReadLine(), out id)) { Console.WriteLine("ID должен быть числом."); return; }
            Student student = FindStudent(id);
            if (student == null) { Console.WriteLine("Студент не найден."); return; }
            Console.Write("Название курса: "); string course = Console.ReadLine();
            Console.Write("Текущий балл: "); int score;
            if (!int.TryParse(Console.ReadLine(), out score)) { Console.WriteLine("Балл должен быть числом."); return; }
            Console.Write("Завершено тем: "); int topics;
            if (!int.TryParse(Console.ReadLine(), out topics)) { Console.WriteLine("Количество тем должно быть числом."); return; }
            Console.Write("Текущая тема: "); string currentTopic = Console.ReadLine();
            CourseProgress found = null;
            foreach (CourseProgress progress in students[student]) if (progress.CourseName == course) found = progress;
            if (found == null) students[student].Add(new CourseProgress(course, score, topics, currentTopic));
            else { found.Score = score; found.CompletedTopics = topics; found.CurrentTopic = currentTopic; }
            Console.WriteLine("Прогресс сохранён.");
        }

        private static void ShowAll()
        {
            foreach (KeyValuePair<Student, List<CourseProgress>> pair in students)
            {
                Console.WriteLine(pair.Key.Id + ". " + pair.Key.FullName + ", " + pair.Key.Group);
                foreach (CourseProgress progress in pair.Value) Console.WriteLine("   " + progress.CourseName + ": " + progress.Score + " баллов, тем: " + progress.CompletedTopics + ", сейчас: " + progress.CurrentTopic);
            }
        }

        private static void FilterByGroup()
        {
            Console.Write("Группа: "); string group = Console.ReadLine().ToUpper();
            foreach (Student student in students.Keys) if (student.Group == group) Console.WriteLine(student.FullName + " (" + student.Group + ")");
        }

        private static void FilterByScore()
        {
            Console.Write("Минимальный балл: "); int score;
            if (!int.TryParse(Console.ReadLine(), out score)) { Console.WriteLine("Введите число."); return; }
            foreach (KeyValuePair<Student, List<CourseProgress>> pair in students)
                foreach (CourseProgress progress in pair.Value)
                    if (progress.Score >= score) Console.WriteLine(pair.Key.FullName + " — " + progress.CourseName + ": " + progress.Score);
        }

        private static void SortByPerformance()
        {
            List<Student> sorted = new List<Student>();
            foreach (Student student in students.Keys) sorted.Add(student);
            sorted.Sort(delegate (Student first, Student second) { return BestScore(second).CompareTo(BestScore(first)); });
            Console.WriteLine("Студенты по успеваемости:");
            foreach (Student student in sorted) Console.WriteLine(student.FullName + " — " + BestScore(student) + " баллов");
        }

        private static int BestScore(Student student)
        {
            int best = 0;
            foreach (CourseProgress progress in students[student]) if (progress.Score > best) best = progress.Score;
            return best;
        }
    }
}

using System;
using System.Collections.Generic;

namespace EmployeePasswords
{
    internal class Program
    {
        private static Dictionary<string, string> employees = new Dictionary<string, string>();
        private static void Main()
        {
            // Задание 1
            // Создайте приложение для менеджмента сотрудников и паролей.
            // Необходимо хранить следующую информацию:
            // - Логины сотрудников;
            // - Пароли сотрудников.
            // Для хранения информации используйте Dictionary<T>.
            // Приложение должно предоставлять такую функциональность:
            // - Добавление логина и пароля сотрудника;
            // - Удаление логина сотрудника;
            // - Изменение информации о логине и пароле сотрудника;
            // - Получение информации о пароле по логину сотрудника.
            employees.Add("admin", "Admin123");
            while (true)
            {
                Console.WriteLine("\n1-Добавить  2-Удалить  3-Изменить  4-Показать пароль  0-Выход");
                Console.Write("Действие: ");
                switch (Console.ReadLine())
                {
                    case "1": Add(); break; case "2": Delete(); break; case "3": Change(); break;
                    case "4": ShowPassword(); break; case "0": return; default: Console.WriteLine("Неверный выбор."); break;
                }
            }
        }
        private static void Add() { Console.Write("Логин: "); string login = Console.ReadLine(); Console.Write("Пароль: "); string password = Console.ReadLine(); if (employees.ContainsKey(login)) Console.WriteLine("Такой логин уже есть."); else { employees.Add(login, password); Console.WriteLine("Сотрудник добавлен."); } }
        private static void Delete() { Console.Write("Логин: "); Console.WriteLine(employees.Remove(Console.ReadLine()) ? "Логин удалён." : "Логин не найден."); }
        private static void Change()
        {
            Console.Write("Старый логин: "); string oldLogin = Console.ReadLine();
            if (!employees.ContainsKey(oldLogin)) { Console.WriteLine("Логин не найден."); return; }
            Console.Write("Новый логин: "); string newLogin = Console.ReadLine(); Console.Write("Новый пароль: "); string newPassword = Console.ReadLine();
            if (newLogin != oldLogin && employees.ContainsKey(newLogin)) { Console.WriteLine("Новый логин уже занят."); return; }
            employees.Remove(oldLogin); employees[newLogin] = newPassword; Console.WriteLine("Информация изменена.");
        }
        private static void ShowPassword() { Console.Write("Логин: "); string login = Console.ReadLine(); Console.WriteLine(employees.ContainsKey(login) ? "Пароль: " + employees[login] : "Логин не найден."); }
    }
}

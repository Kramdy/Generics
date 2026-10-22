using System;
using System.Collections.Generic;

namespace EnglishFrenchDictionary
{
    internal class Program
    {
        private static Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        private static void Main()
        {
            // Задание 2
            // Создайте приложение «Англо-французский словарь».
            // Храните слово на английском языке и варианты его перевода на французский.
            // Для хранения информации используйте Dictionary<T>.
            // Приложение должно предоставлять такую функциональность:
            // - Добавление слова и вариантов перевода;
            // - Удаление слова;
            // - Удаление варианта перевода;
            // - Изменение слова;
            // - Изменение варианта перевода;
            // - Поиск перевода слова.
            dictionary.Add("healthy", new List<string> { "sain", "salutaire" });
            while (true)
            {
                Console.WriteLine("\n1-Добавить 2-Удалить слово 3-Удалить перевод 4-Изменить слово 5-Изменить перевод 6-Поиск 0-Выход");
                Console.Write("Действие: ");
                switch (Console.ReadLine()) { case "1": Add(); break; case "2": DeleteWord(); break; case "3": DeleteTranslation(); break; case "4": ChangeWord(); break; case "5": ChangeTranslation(); break; case "6": Search(); break; case "0": return; default: Console.WriteLine("Неверный выбор."); break; }
            }
        }
        private static void Add() { Console.Write("Английское слово: "); string word = Console.ReadLine(); Console.Write("Французский перевод: "); string translation = Console.ReadLine(); if (!dictionary.ContainsKey(word)) dictionary[word] = new List<string>(); dictionary[word].Add(translation); Console.WriteLine("Добавлено."); }
        private static void DeleteWord() { Console.Write("Слово: "); Console.WriteLine(dictionary.Remove(Console.ReadLine()) ? "Удалено." : "Слово не найдено."); }
        private static void DeleteTranslation() { Console.Write("Слово: "); string word = Console.ReadLine(); Console.Write("Перевод: "); string translation = Console.ReadLine(); if (dictionary.ContainsKey(word) && dictionary[word].Remove(translation)) Console.WriteLine("Перевод удалён."); else Console.WriteLine("Перевод не найден."); }
        private static void ChangeWord() { Console.Write("Старое слово: "); string oldWord = Console.ReadLine(); if (!dictionary.ContainsKey(oldWord)) { Console.WriteLine("Слово не найдено."); return; } Console.Write("Новое слово: "); string newWord = Console.ReadLine(); dictionary[newWord] = dictionary[oldWord]; dictionary.Remove(oldWord); Console.WriteLine("Слово изменено."); }
        private static void ChangeTranslation() { Console.Write("Слово: "); string word = Console.ReadLine(); if (!dictionary.ContainsKey(word)) { Console.WriteLine("Слово не найдено."); return; } Console.Write("Старый перевод: "); string oldTranslation = Console.ReadLine(); int index = dictionary[word].IndexOf(oldTranslation); if (index == -1) { Console.WriteLine("Перевод не найден."); return; } Console.Write("Новый перевод: "); dictionary[word][index] = Console.ReadLine(); Console.WriteLine("Перевод изменён."); }
        private static void Search() { Console.Write("Английское слово: "); string word = Console.ReadLine(); if (!dictionary.ContainsKey(word)) { Console.WriteLine("Слово не найдено."); return; } Console.WriteLine("Переводы: " + string.Join(", ", dictionary[word])); }
    }
}

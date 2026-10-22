using System;
using System.Collections.Generic;

namespace CafeQueue
{
    internal class Visitor
    {
        public string Name { get; set; }
        public bool HasReservation { get; set; }

        public Visitor(string name, bool hasReservation)
        {
            Name = name;
            HasReservation = hasReservation;
        }
    }

    internal class Program
    {
        private static Queue<Visitor> queue = new Queue<Visitor>();
        private static int freeTables = 2;

        private static void Main()
        {
            // Задание 3
            // Создайте приложение, эмулирующее очередь в популярное кафе.
            // Посетители кафе приходят и попадают в очередь, если нет свободного места.
            // При освобождении столика в кафе первый посетитель очереди занимает его.
            // Если приходит клиент, который резервировал столик на определенное время,
            // он получает зарезервированный столик вне очереди.
            // При разработке приложения используйте класс Queue<T>.

            while (true)
            {
                Console.WriteLine("\nСвободных столиков: " + freeTables);
                Console.WriteLine("1 - пришёл посетитель  2 - освободился столик  3 - показать очередь  0 - выход");
                Console.Write("Действие: ");
                string action = Console.ReadLine();
                if (action == "1") AddVisitor();
                else if (action == "2") FreeTable();
                else if (action == "3") ShowQueue();
                else if (action == "0") return;
                else Console.WriteLine("Неверный выбор.");
            }
        }

        private static void AddVisitor()
        {
            Console.Write("Имя посетителя: ");
            string name = Console.ReadLine();
            Console.Write("Есть резерв столика? (да/нет): ");
            bool reservation = Console.ReadLine().ToLower() == "да";
            Visitor visitor = new Visitor(name, reservation);

            if (visitor.HasReservation)
                Console.WriteLine(visitor.Name + " занимает зарезервированный столик вне очереди.");
            else if (freeTables > 0)
            {
                freeTables--;
                Console.WriteLine(visitor.Name + " занимает свободный столик.");
            }
            else
            {
                queue.Enqueue(visitor);
                Console.WriteLine(visitor.Name + " добавлен в очередь.");
            }
        }

        private static void FreeTable()
        {
            if (queue.Count > 0)
            {
                Visitor visitor = queue.Dequeue();
                Console.WriteLine("Столик занимает первый в очереди: " + visitor.Name);
            }
            else
            {
                freeTables++;
                Console.WriteLine("Столик свободен.");
            }
        }

        private static void ShowQueue()
        {
            if (queue.Count == 0) { Console.WriteLine("Очередь пуста."); return; }
            Console.WriteLine("Очередь:");
            foreach (Visitor visitor in queue) Console.WriteLine("- " + visitor.Name);
        }
    }
}

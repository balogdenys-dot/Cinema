using System;
using System.Text;

namespace Program;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        bool running = true;
        while (running)
        {
            Console.WriteLine("=== Сервісний центр ===");
            Console.WriteLine("1 — про програму");
            Console.WriteLine("2 — таблиця вартості квитків");
            Console.WriteLine("3 — продаж квитків за тиждень");
            Console.WriteLine("0 — вихід");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();
            Console.WriteLine();
            switch (choice)
            {
                case "1":
                    ShowAbout();
                    Pause();
                    break;
                case "2":
                    ShowCostTable();
                    Pause();
                    break;
                case "3":
                    // ShowWorkloadTable(); ← ваш метод із таблицею на два входи
                    Pause();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("До побачення.");
                    break;
                default:
                    Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                    Pause();
                    break;

            }
        }
    }
    static void Pause()
    {
        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey(true);
        Console.Clear();
    }
    static void ShowAbout()
    {
        Console.WriteLine("Тема: Кінотеатр");
        Console.WriteLine("Головна сутність: Сеанс");
        Console.WriteLine("Перелік полів та їх типи даних:");
        Console.WriteLine("  - int код сеансу");
        Console.WriteLine("  - string назва фільму");
        Console.WriteLine("  - int код — зал, з довідника");
        Console.WriteLine("  - DateTime дата й час");
        Console.WriteLine("  - double ціна квитка");
        Console.WriteLine("  - int продано місць");
        Console.WriteLine("  - int усього місць");
        Console.WriteLine("  - Підсумкова величина: заповненість залів у відсотках і касовий збір за день");
    }
    static void ShowCostTable()
    {
        Console.WriteLine("--- Табуляція вартості (не менше 8 рядків, дробовий крок) ---");
        double basePrice = 120.5;

        Console.WriteLine($"{"Кількість"} | {"Вартість (грн)"}");

        for (double x = 0.5; x <= 4.0; x += 0.5)
        {
            double y = x * basePrice;
            Console.WriteLine($"{x,:F1} | {y,:F2}");
        }
    }
    static void ShowWorkloadTable()
    {
        Console.WriteLine("Таблиця завантаження залів з двома входами та підсумками");
        Console.Write("Введіть кількість залів: ");
        int rows = Convert.ToInt32(Console.ReadLine());

    }
}

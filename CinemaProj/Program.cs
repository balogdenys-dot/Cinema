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
            Console.WriteLine("=== Кінотеатр ===");
            Console.WriteLine("1 — про програму");
            Console.WriteLine("2 — Таблиця вартості квитків");
            Console.WriteLine("3 — Завантаження залів та касовий збір за тиждень");
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
                    // ShowCostTable();

                    Console.WriteLine("Поки не зроблено.");

                    Pause();
                    break;
                case "3":
                    // ShowWorkloadTable();
                    Console.WriteLine("Поки не зроблено.");
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
        Console.WriteLine("");
        Console.WriteLine("");
    }
}

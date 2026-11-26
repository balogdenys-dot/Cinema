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
                case "1": ShowAbout(); Pause(); break;
                case "2": ShowCostTable(); Pause(); break;
                case "3": ShowWorkloadTable(); Pause(); break;
                case "0": running = false; Console.WriteLine("До побачення."); break;
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

        Console.WriteLine("Введіть початкову кількість квитків: ");
        double start = Convert.ToDouble(Console.ReadLine("Введіть початкову кількість квитків: "));
        Console.WriteLine("Введіть кінцеву кількість квитків: ");
        double end = Convert.ToDouble(Console.ReadLine("Введіть кінцеву кількість квитків: "));
        Console.WriteLine("Введіть крок табуляції: ");
        double step = Convert.ToDouble(Console.ReadLine("Введіть крок табуляції: "));

        Console.WriteLine($"{"Кількість"} | {"Вартість"}");

        for (double x = start; x <= end; x += step)
        {
            double y = x * basePrice;
            Console.WriteLine($"{x} | {y}");
        }
    }
    static void ShowWorkloadTable()
    {
        const int maxSeats = 80;
        const double ticketPrice = 120.0;
        int totalTickets = 0;

        Console.WriteLine("Таблиця завантаження залів з двома входами та підсумками");

        Console.Write("Введіть кількість залів: ");
        if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0)
        {
            Console.WriteLine("Некоректне значення");
            return;
        }

        Console.Write("Введіть кількість днів показу фільмів: ");
        if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
        {
            Console.WriteLine("Некоректне значення");
            return;
        }


        Console.WriteLine("\nВведіть кількість проданих квитків, максимум 80 у залі:");

        int[,] soldData = new int[rows, cols];
        int[] rowTotals = new int[rows];


        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int ticketsSold;
                while (true)
                {
                    Console.Write($"[Зал {i + 1}, День {j + 1}] квитків (0-{maxSeats}): ");
                    ticketsSold = Convert.ToInt32(Console.ReadLine());
                    if (ticketsSold >= 0 && ticketsSold <= maxSeats)
                    {
                        break;
                    }
                    Console.WriteLine($"Помилка! Введіть число від 0 до {maxSeats}.");
                }
                soldData[i, j] = ticketsSold;
                rowTotals[i] += ticketsSold;
                totalTickets += ticketsSold;
            }
        }

        Console.WriteLine("\n--- Таблиця ---");
        Console.Write("Зал / День\t");
        for (int j = 1; j <= cols; j++)
        {
            Console.Write($"День {j}\t");
        }
        Console.WriteLine("Підсумок");
        for (int i = 0; i < rows; i++)
        {
            Console.Write($"Зал {i + 1}\t\t"); 
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{soldData[i, j]}\t");
            }
            Console.WriteLine($"{rowTotals[i]}");
        }
    }
    static double CalculateRevenue(double price, int soldSeats)
    {
        return price * soldSeats;
    }
    static double ReadDouble(string prompt)
    {
        double value = 0;
        while (true)
        {
            Console.WriteLine(prompt);
            if (double.TryParse(Console.ReadLine(), out value))
            {
                return value;
            }
            Console.WriteLine("Помилка, введіть число.");
        }
    }
    static int ReadInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
            {
                return value;
            }
            Console.WriteLine("Помилка! Введіть ціле число.");
        }
    }
}

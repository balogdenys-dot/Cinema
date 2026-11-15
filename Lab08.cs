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

        Console.WriteLine("Введіть початкову кількість квитків: ");
        double start = Convert.ToDouble(Console.ReadLine("Введіть початкову кількість квитків: "));
        Console.WriteLine("Введіть кінцеву кількість квитків: ");
        double end = Convert.ToDouble(Console.ReadLine("Введіть кінцеву кількість квитків: "));
        Console.WriteLine("Введіть крок табуляції: ");
        double step = Convert.ToDouble(Console.ReadLine("Введіть крок табуляції: "));


        double basePrice = 120.5;

        Console.WriteLine($"{"Кількість"} | {"Вартість (грн)"}");

        for (double x = start; x <= end; x += step)
        {
            double y = x * basePrice;
            Console.WriteLine($"{x:F1} | {y:F2}");
        }
    }
    static void ShowWorkloadTable()
    {
        Console.WriteLine("Таблиця завантаження залів з двома входами та підсумками\n");

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

        int Total = 0;
        int[] colTotals = new int[cols];


        Console.Write("Зал / День\t");
        for (int j = 1; j <= cols; j++)
        {
            Console.Write($"День {j}\t");
        }
        Console.WriteLine("Підсумок");

        for (int i = 1; i <= rows; i++)
        {
            Console.Write($"Зал {i}\t\t");
            int rowSum = 0;
            for (int j = 1; j <= cols; j++)
            {
                int ticketsSold = (i * 6 + j * 11) % 70 + 10;
                Console.Write($"{ticketsSold}\t");
                rowSum += ticketsSold;
                colTotals[j - 1] += ticketsSold;
            }

            Console.WriteLine($"{rowSum}");
            Total += rowSum;
        }


        Console.Write("Підсумок\t");
        for (int j = 0; j < cols; j++)
        {
            Console.Write($"{colTotals[j]}\t");
        }
        Console.WriteLine($"{Total}");
    }

    static double CalculateRevenue(double price, int soldSeats)
    {
        return price * soldSeats;
    }
}


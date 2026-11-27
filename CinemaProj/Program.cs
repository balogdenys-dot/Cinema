using System;
using System.Text;
namespace Program;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        double[] values = new double[100];
        int count = 0;

        bool running = true;
        while (running)
        {
            Console.WriteLine("=== Кінотеатр ===");
            Console.WriteLine("1 — Про програму");
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
                case "3": ShowWorkloadTable(values, ref count); Pause(); break;
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
        Console.WriteLine("---Табуляція вартості---");

        double basePrice = 120;
        Console.WriteLine($"Ціна одного квитка: {basePrice}");
        double start = ReadDouble("Введіть початкову кількість квитків: ");
        double end = ReadDouble("Введіть кінцеву кількість квитків: ");
        double step = ReadDouble("Введіть крок табуляції: ");

        Console.WriteLine($"{"Кількість"} | {"Вартість"}");

        for (double x = start; x <= end; x += step)
        {
            double y = x * basePrice;
            Console.WriteLine($"{x} | {y}");
        }
    }
    static void ShowWorkloadTable(double[] values, ref int count)
    {
        const int maxSeats = 80;
        int totalTickets = 0;

        Console.WriteLine("Таблиця завантаження залів з двома входами та підсумками");

        double ticketPrice = ReadDouble("Введіть ціну квитка: ");
        if (ticketPrice <= 0)
        {
            Console.WriteLine("Некоректне значення ціни");
            return;
        }

        int rows = ReadInt("Введіть кількість залів: ");
        if (rows <= 0)
        {
            Console.WriteLine("Некоректне значення");
            return;
        }

        int cols = ReadInt("Введіть кількість днів показу фільмів: ");
        if (cols <= 0)
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
                    ticketsSold = ReadInt($"[Зал {i + 1}, День {j + 1}] квитків (0-{maxSeats}): ");
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

        double[] revenues = new double[rows];

        Console.WriteLine("Підсумок");
        for (int i = 0; i < rows; i++)
        {
            Console.Write($"Зал {i + 1}\t\t"); 
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{soldData[i, j]}\t");
            }
            revenues[i] = CalculateRevenue(ticketPrice, rowTotals[i]);
            Console.WriteLine($"{rowTotals[i]}\t\t{revenues[i]} грн");

        }
        AddValue(values, ref count, revenues);
        Console.WriteLine($"\nКасові збори залів додано в масив. Значень у масиві: {count}");

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
    static void AddValue(double[] values, ref int count, double[] newValues)
    {
        if (values == null || newValues == null)
        {
            return;
        }

        for (int i = 0; i < newValues.Length; i++)
        {
            if (count >= values.Length)
            {
                break;
            }

            values[count] = newValues[i];
            count++;
        }
    }
    static void AddValues(double[] values, ref int count)
    {
        int addAmount = ReadInt("Скільки значень додати? ");
        if (addAmount < 0)
        {
            Console.WriteLine("Кількість не може бути від'ємною.");
            return;
        }

        if (count + addAmount > values.Length)
        {
            Console.WriteLine($"Недостатньо місця в масиві. Вільно: {values.Length - count} значення");
            return;
        }

        double[] input = new double[addAmount];
        for (int i = 0; i < addAmount; i++)
        {
            input[i] = ReadDouble($"Введіть значення {count + i + 1}: ");
        }

        int beforeCount = count;
        AddValue(values, ref count, input);
        Console.WriteLine($"Лічильник змінився: з {beforeCount} значень => {count} значення");
    }

    static void ChangeInt(int number)
    {
        number = 999;
    }

    static void ChangeValue(double[] values, int index, double newValue)
    {
        values[index] = newValue;
    }
}

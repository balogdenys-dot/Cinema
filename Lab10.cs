using System;
using System.Text;

namespace CinemaApp;

internal class Program
{
    const int Capacity = 100;

    static int[] ids = new int[Capacity];
    static string[] movieTitles = new string[Capacity];
    static int[] hallIds = new int[Capacity];
    static DateTime[] sessionDates = new DateTime[Capacity];
    static double[] ticketPrices = new double[Capacity];
    static int[] soldSeats = new int[Capacity];
    static int[] totalSeats = new int[Capacity];
    static int count = 0;
    static int nextId = 1;
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        double[] values = new double[100];
        int count = 0;
        bool running = true;



        while (running)
        {
            Console.WriteLine("=== Сервісний центр ===");
            Console.WriteLine("1 — про програму");
            Console.WriteLine("2 — таблиця вартості квитків");
            Console.WriteLine("3 — продаж квитків за тиждень");
            Console.WriteLine("4 — додати значення");
            Console.WriteLine("5 — видалити за номером");
            Console.WriteLine("6 — показати статистику");
            Console.WriteLine("7 — додати сеанс");
            Console.WriteLine("0 — вихід");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();
            Console.WriteLine();
            switch (choice)
            {
                case "1": ShowAbout(); Pause(); break;
                case "2": ShowCostTable(); Pause(); break;
                case "3": ShowWorkloadTable(); Pause(); break;
                case "4": AddValues(values, ref count); Pause(); break;
                case "5": RemoveNumber(values, ref count); Pause(); break;
                case "6": ShowStatistics(values, count); Pause(); break;
                case "7": AddSession(); Pause(); break;
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
        Console.WriteLine("  - Підсумкова величина: День і прибуток за день у кожній залі ");
    }
    static void ShowCostTable()
    {
        Console.WriteLine("--- Табуляція вартості (не менше 8 рядків, дробовий крок) ---");

        double start = ReadDouble("Введіть початкову кількість квитків: ");
        double end = ReadDouble("Введіть кінцеву кількість квитків: ");
        double step = ReadDouble("Введіть крок табуляції: ");


        double basePrice = 120.5;

        Console.WriteLine($"{"Кількість"} | {"Вартість (грн)"}");

        for (double x = start; x <= end; x += step)
        {
            double y = x * basePrice;
            Console.WriteLine($"{x} | {y}");
        }
    }
    static void ShowWorkloadTable()
    {
        Console.WriteLine("Таблиця завантаження залів з двома входами та підсумками\n");

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
        PrintTable(values, count);
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

    static void RemoveNumber(double[] values, ref int count)
    {
        if (count == 0)
        {
            Console.WriteLine("Масив порожній.");
            return;
        }

        PrintTable(values, count);
        int number = ReadInt($"Введіть номер елемента для видалення (1-{count}): ");
        RemoveAt(values, ref count, number - 1);
        PrintTable(values, count);
    }

    static void RemoveAt(double[] values, ref int count, int index)
    {
        if (values == null || index < 0 || index >= count)
        {
            Console.WriteLine("Неправильний номер елемента.");
            return;
        }

        for (int i = index; i < count - 1; i++)
        {
            values[i] = values[i + 1];
        }

        values[count - 1] = 0;
        count--;
        Console.WriteLine("Елемент видалено.");
    }


    static void PrintTable(double[] values, int count)
    {
        Console.WriteLine("--- Таблиця значень ---");

        if (count <= 0)
        {
            Console.WriteLine("     | (порожньо)");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"No {i + 1} | {values[i]}");
        }
    }

    static bool FindMax(double[] values, int count, out double maxValue, out int maxNumber)
    {
        if (values == null || count <= 0)
        {
            maxValue = 0;
            maxNumber = -1;
            return false;
        }

        maxValue = values[0];
        maxNumber = 1;

        for (int i = 1; i < count; i++)
        {
            if (values[i] > maxValue)
            {
                maxValue = values[i];
                maxNumber = i + 1;
            }
        }

        return true;
    }
    static void ChangeInt(int number)
    {
        number = 999;
    }

    static void ChangeValue(double[] values, int index, double newValue)
    {
        values[index] = newValue;
    }

    static void ShowStatistics(double[] values, int count)
    {
        PrintTable(values, count);

        if (count <= 0)
        {
            Console.WriteLine("Статистика недоступна: набір значень порожній.");
            return;
        }

        double sum = 0;
        double minValue = values[0];
        for (int i = 0; i < count; i++)
        {
            sum += values[i];
            if (values[i] < minValue)
            {
                minValue = values[i];
            }
        }

        double avg = sum / count;
        Console.WriteLine($"Кількість елементів: {count}");
        Console.WriteLine($"Сума: {sum}");
        Console.WriteLine($"Середнє: {avg}");
        Console.WriteLine($"Найменше значення: {minValue}");

        if (FindMax(values, count, out double maxVal, out int maxNum))
        {
            Console.WriteLine($"Найбільше значення: {maxVal} (номер {maxNum})");
        }
    }

    static void AddSession()
    {
        string title;
        int hall;
        DateTime date;
        double price;
        int total;
        int sold;

        if (count == Capacity)
        {
            Console.WriteLine("Сховище заповнене, видаліть щось зайве.");
            return;
        }

        title = ReadText("Назва фільму: ");

        while (true)
        {
            hall = ReadInt("Код залу (1–10): ");
            if (hall < 1 || hall > 10)
            {
                Console.WriteLine("Невірне значення! Код залу має бути від 1 до 10.");
                continue;
            }
            break;
        }

        while (true)
        {
            Console.WriteLine("Дата й час (дд.мм.рррр гг:хх): ");
            if (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Невірна дата! Спробуйте ще раз має бути така дата 24.11.2026 18:30");
                continue;
            }
            break;
        }

        while (true)
        {
            price = ReadDouble("Ціна квитка (грн): ");
            if (price < 1 || price > 10000)
            {
                Console.WriteLine("Невірне значення! Ціна має бути від 1 до 10000 грн.");
                continue;
            }
            break;
        }

        while (true)
        {
            total = ReadInt("Усього місць у залі (1–500): ");
            if (total < 1 || total > 500)
            {
                Console.WriteLine("Невірне значення! Кількість місць має бути від 1 до 500.");
                continue;
            }
            break;
        }

        while (true)
        {
            sold = ReadInt($"Продано місць (0–{total}): ");
            if (sold < 0 || sold > total)
            {
                Console.WriteLine($"Невірне значення! Проданих місць має бути від 0 до {total}.");
                continue;
            }
            break;
        }

        ids[count] = nextId;
        movieTitles[count] = title;
        hallIds[count] = hall;
        sessionDates[count] = date;
        ticketPrices[count] = price;
        soldSeats[count] = sold;
        totalSeats[count] = total;

        count++;
        nextId++;

        Console.WriteLine($"Додано сеанс №{ids[count - 1]}.");
    }
    static void AddReadySessions()
    {
        if (count + 6 > Capacity)
        {
            Console.WriteLine("Недостатньо місця у сховищі.");
            return;
        }

        ids[count] = nextId;
        movieTitles[count] = "Backrooms";
        hallIds[count] = 1;
        sessionDates[count] = DateTime.Parse("24.11.2026 14:00");
        ticketPrices[count] = 150;
        soldSeats[count] = 85;
        totalSeats[count] = 100;
        count++;
        nextId++;

        ids[count] = nextId;
        movieTitles[count] = "Інтерстеллар";
        hallIds[count] = 2;
        sessionDates[count] = DateTime.Parse("24.11.2026 12:30");
        ticketPrices[count] = 180;
        soldSeats[count] = 60;
        totalSeats[count] = 80;
        count++;
        nextId++;

        ids[count] = nextId;
        movieTitles[count] = "Дуже страшне кіно 3";
        hallIds[count] = 1;
        sessionDates[count] = DateTime.Parse("24.11.2026 19:15");
        ticketPrices[count] = 200;
        soldSeats[count] = 95;
        totalSeats[count] = 100;
        count++;
        nextId++;

        ids[count] = nextId;
        movieTitles[count] = "Матриця 2";
        hallIds[count] = 3;
        sessionDates[count] = DateTime.Parse("25.11.2026 15:00");
        ticketPrices[count] = 140;
        soldSeats[count] = 40;
        totalSeats[count] = 60;
        count++;
        nextId++;

        ids[count] = nextId;
        movieTitles[count] = "Бігущий по лабіринту";
        hallIds[count] = 2;
        sessionDates[count] = DateTime.Parse("25.11.2026 18:00");
        ticketPrices[count] = 160;
        soldSeats[count] = 75;
        totalSeats[count] = 80;
        count++;
        nextId++;

        ids[count] = nextId;
        movieTitles[count] = "Аватар 2";
        hallIds[count] = 1;
        sessionDates[count] = DateTime.Parse("25.11.2026 21:00");
        ticketPrices[count] = 220;
        soldSeats[count] = 90;
        totalSeats[count] = 100;
        count++;
        nextId++;

        Console.WriteLine("Додано 6 готових сеансів.");
    }
}
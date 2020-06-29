using System;
using System.Diagnostics;
using System.Globalization;

namespace SDIZO
{
    class Program
    {
        public float[] importData;

        static void printInterface(string[] options, int select)
        {

            int max_length = 0;
            int swt = 2;
            char LG = '\u2554';
            char PG = '\u2557';
            char LD = '\u255A';
            char PD = '\u255D';
            char LK = '\u2560';
            char PK = '\u2563';
            char poz = '\u2550';
            char pion = '\u2551';
            foreach (string s in options)
            {

                if (s.Length > max_length) max_length = s.Length;
            }
            if (max_length < 4)
            {
                Console.Write("2. Nadluższa z opcji musi mieć przynajmniej 4 znaki");
                return;
            }
            if (max_length % 2 == 0) swt = 1;
            Console.Write(LG);
            for (int i = 0; i < max_length + 2; i++) Console.Write(poz);
            Console.Write(PG + "\n" + pion);
            for (int i = 0; i < (max_length - 4) / 2 + 1; i++) Console.Write(" ");
            Console.Write("OPCJE");
            for (int i = 0; i < (max_length - 4) / 2 + swt; i++) Console.Write(" ");
            Console.Write(pion + "\n" + LK);
            for (int i = 0; i < max_length + 2; i++) Console.Write(poz);
            Console.WriteLine(PK);
            int x = 0;
            foreach (string s in options)
            {
                Console.Write(pion);
                for (int i = 0; i < (max_length - s.Length) / 2 + 1; i++) Console.Write(" ");
                if (x == select) Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(s);
                Console.ForegroundColor = ConsoleColor.White;
                if (s.Length % 2 == 0)
                {
                    for (int i = 0; i < (max_length - s.Length) / 2 + swt; i++) Console.Write(" ");
                }
                else
                {
                    for (int i = 0; i < (max_length - s.Length) / 2 + Math.Abs(swt - 3); i++) Console.Write(" ");
                }
                Console.WriteLine(pion);
                x++;
            }
            Console.Write(LD);
            for (int i = 0; i < max_length + 2; i++) Console.Write(poz);
            Console.Write(PD);

        }
        static void Main(string[] args)
        {
            Functions fnc = new Functions();
            int select = 0;
            char pick;
            float[] data = null;
            string[] options = { "Wgraj plik wejściowy w formacie .csv",
                "Sortowanie algorytmem bąbelkowym",
                "Sortowanie algorytmem szybkim",
                "Sortowanie przez zliczanie",
                "Sortowanie kopcowe",
                "Zamknij program"};
            while (true)
            {
                Console.Clear();
                printInterface(options, select);
                pick = Console.ReadKey(true).KeyChar;
                if (pick == 'w')
                {
                    if (select > 0) select--;
                    else select = options.Length - 1;
                }
                else if (pick == 's')
                {
                    if (select < options.Length - 1) select++;
                    else select = 0;
                }
                else if (pick == 'e')
                {
                    Console.Clear();
                    Console.WriteLine("Wybrano opcję: " + options[select]);
                    switch (select)
                    {
                        case 0: //ImportData
                            data = fnc.ReadInput();
                            break;
                        case 1: //Bubblesort
                            if (data != null)
                            {
                                fnc.Bubblesorts(data);
                            }
                            else
                            {
                                Console.WriteLine("Nie udało sie wczytać danych");
                            }
                            break;
                        case 2: //Quicksort
                            if (data != null)
                            {
                                var timer = Stopwatch.StartNew();
                                var export = fnc.Quicksort(data, 0, data.Length - 1);
                                timer.Stop();
                                Console.WriteLine("Czas wykonanego sortowania: " + (timer.ElapsedMilliseconds / 1000.0f).ToString(CultureInfo.InvariantCulture) + "sekund.");
                                Functions.Export(export, "SortowanieSzybkie");
                                Console.WriteLine("Zapis danych został wykonany poprawnie");
                                Console.WriteLine("Jeśli chcesz wyjść do menu wciśnij e");
                            }
                            else
                            {
                                Console.WriteLine("Nie udało sie wczytać danych");
                            }
                            break;
                        case 3: //Countingsort
                            if (data != null)
                            {
                                fnc.CountingSort(data);
                            }
                            else
                            {
                                Console.WriteLine("Nie udało sie wczytać danych");
                            }
                            break;
                        case 4: //Heapsort
                            if (data != null)
                            {
                                var time = Stopwatch.StartNew();
                                fnc.Heapsort(data);
                                time.Stop();
                                Console.WriteLine("Czas wykonanego sortowania: " + (time.ElapsedMilliseconds / 1000.0f).ToString(CultureInfo.InvariantCulture) + "sekund.");
                                Functions.Export(data, "SortowanieKopcowe");
                                Console.WriteLine("Jeśli chcesz wyjść do menu wciśnij e");
                            }
                            else
                            {
                                Console.WriteLine("Nie udało sie wczytać danych");
                            }
                            break;
                        case 5://Close
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                    Console.ReadKey(true);

                }
            }
        }
    }
}
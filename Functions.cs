using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SDIZO
{
    class Functions
    {
        #region Alghoritms

        #region Bubblesort
        public void Bubblesorts(float[] array)
        {
            var timer = Stopwatch.StartNew();
            float[] data = array;
            float temp;
            for (int p = 0; p <= data.Length - 2; p++)
            {
                for (int i = 0; i <= data.Length - 2; i++)
                {
                    if (data[i] > data[i + 1])
                    {
                        temp = data[i + 1];
                        data[i + 1] = data[i];
                        data[i] = temp;
                    }
                }
            }
            Export(data, "SortowanieBabelkowe");
            Console.WriteLine("Zapis danych został wykonany poprawnie");
            timer.Stop();
            Console.WriteLine("Czas wykonanego sortowania: " + (timer.ElapsedMilliseconds / 1000.0f).ToString(CultureInfo.InvariantCulture) + "sekund.");
            Console.WriteLine("Jeśli chcesz wyjść do menu wciśnij e");
        }

        #endregion
        #region Quicksort
        public float[] Quicksort(float[] array, int left, int right)
        {
            
            var i = left;
            var j = right;
            float[] data = array;
            var pivot = data[(left + right) / 2];
            while (i < j)
            {
                while (data[i] < pivot) i++;
                while (data[j] > pivot) j--;
                if (i <= j)
                {
                    var tmp = data[i];
                    data[i++] = data[j];
                    data[j--] = tmp;
                }
            }
            if (left < j) Quicksort(data, left, j);
            if (i < right) Quicksort(data, i, right);
            return data;
            
        }

        #endregion
        #region Countingsort
        public void CountingSort(float[] array)
        {
            var timer = Stopwatch.StartNew();
            var data = (float[])array.Clone();
            int minVal = (int)array.Min();
            int maxVal = (int)array.Max();
            int[] counts = new int[maxVal - minVal + 1];

            for (int i = 0; i < array.Length; i++)
            {
                counts[(int)array[i] - minVal]++;
            }
            counts[0]--;
            for (int i = 1; i < counts.Length; i++)
            {
                counts[i] = counts[i] + counts[i - 1];
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                data[counts[(int)array[i] - minVal]--] = (int)array[i];
            }
            Export(data, "SortowaniePrzez Zliczanie");
            Console.WriteLine("Zapis danych został wykonany poprawnie");
            timer.Stop();
            Console.WriteLine("Czas wykonanego sortowania: " + (timer.ElapsedMilliseconds / 1000.0f).ToString(CultureInfo.InvariantCulture) + "sekund.");
        }

        #endregion
        #region Heapsort
        public float[] Heapsort(float[] array)
        {
            var data = (float[])array.Clone();
            for (int i = data.Length / 2 - 1; i >= 0; i--)
            {
                Heapify(data, data.Length, i);
            }
            for (int i = data.Length - 1; i >= 0; i--)
            {
                float temp = data[0];
                data[0] = data[i];
                data[i] = temp;
                Heapify(data, i, 0);
            }
            
            return data;

        }
        static void Heapify(float[] array, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < n && array[left] > array[largest])
            {
                largest = left;
            }
            if (right < n && array[right] > array[largest])
            {
                largest = right;
            }
            if (largest != i)
            {
                float temp = array[i];
                array[i] = array[largest];
                array[largest] = temp;
                Heapify(array, n, largest);
            }
        }
            #endregion

            #endregion

            #region ImportExport
            #region Import
            public float[] ReadInput()
        {
            Program pr = new Program();
            var reader = new StreamReader("dane.csv");
            int i = 0;
            var data = false;
            var options = false;

            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                if (line.StartsWith("#"))
                {
                    continue;
                }
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    switch (line)
                    {
                        case "[SDIZO]":
                            options = true;
                            continue;

                        case "[DATA]":
                            options = false;
                            data = true;
                            continue;
                    }
                }
                if (options)
                {
                    line = line.Remove(line.IndexOf('#'));
                    var subject = line.Split('=')[0];
                    var value = line.Split('=')[1];
                    if (subject == "READ_RECORDS")
                    {
                        pr.importData = new float[int.Parse(value)];
                    }
                    else if (subject == "READ_AS")
                    {
                        //Brak funckjonalności
                    }

                }
                if (data)
                {
                    pr.importData[i++] = float.Parse(line, CultureInfo.InvariantCulture.NumberFormat);
                }
                if (i >= pr.importData.Length)
                {
                    break;
                }

            }
            if (pr.importData != null)
            {
                Console.WriteLine("Dane zostały wczytane");
                Console.WriteLine("Jeśli chcesz wyjść do menu wciśnij e");
            }
            else
            {
                Console.WriteLine("Błąd podczas wczytywania danych");

            }
            return pr.importData;
        }


        #endregion
        #region Export
        public static void Export(float[] data, string name)
        {
            var csvpath = name + ".csv";
            if (File.Exists(csvpath))
            {
                File.WriteAllText(csvpath, string.Empty);
                StringBuilder csvcontent = new StringBuilder();
                foreach (float v in data)
                {
                    csvcontent.AppendLine(v.ToString());
                }
                File.AppendAllText(csvpath, csvcontent.ToString());
            }
            else
            {
                File.Create(csvpath).Close();
                StringBuilder csvcontent = new StringBuilder();
                foreach (float v in data)
                {
                    csvcontent.AppendLine(v.ToString());
                }
                File.AppendAllText(csvpath, csvcontent.ToString());
            }
        }
        #endregion

        #endregion
    }
}

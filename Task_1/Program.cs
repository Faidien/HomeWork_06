using HomeWork_05;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;

namespace Task_1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            UtilityClass.PrintHeader("* * * * * Работа с числом N * * * * *");
            string filePath = OperationSelect();
            string newFilePath = "";
            long numN = CheckFile(filePath);
            if (numN > 0)
            {
                Console.WriteLine($"Число N: {numN.ToString("# ### ### ###")}");
                newFilePath = OperationSelect(numN, 1, filePath);
            }
            if (newFilePath != "")
            {
                OperationSelect(0, 2, newFilePath);

            }


            Console.ReadLine();
        }
        /// <summary>
        /// Выбор пользователем операции
        /// </summary>
        /// <param name="numN"></param>
        /// <param name="typeOper">Тип операции. По умолчанию - 0(первый выбор операций), 1 - второй выбор. </param>
        private static string OperationSelect(long numN = 0, byte typeOper = 0, string path = "")
        {
            string[] operations = new string[] { "Открыть файл", "Выход из программы", "Расчет количества групп",
                "Полный расчет групп", "Поместить файл в архив (возможно восстание машин...)", "Не помещать файл в архив"};
            byte result;
            string res;
            int minOut = typeOper * 2;
            int maxOut = minOut + 2;
            int count = 0;
            do
            {
                Console.WriteLine("\nВыбор операции: ");
                for (int i = minOut; i < maxOut; i++)
                {
                    Console.WriteLine($"{++count}. {operations[i]}");
                }
                byte.TryParse(Console.ReadLine(), out result);
                count = 0;
            } while (result + minOut > maxOut || result + minOut <= minOut);


            switch (result + minOut)
            {
                case 1://Открыть 
                    return GetFilePath();
                //break;
                case 2://Выход из программы
                    Console.WriteLine("До свидания!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    return "-1";
                //break;
                case 3://Расчет количества групп
                    CountGroupCalculate(numN);
                    return "";
                case 4://Полный расчет групп
                    res = FullCalculate(numN, path);
                    return res;

                case 5://Поместить файл в архив (возможно восстание машин...)
                    ArchiveFile(path);
                    return "1";

                case 6://Не помещать файл в архив
                    Console.WriteLine("До свидания!");
                    Console.ReadLine();
                    Environment.Exit(0);
                    return "-1";
                default:
                    return "-1";
            }
        }

        /// <summary>
        /// Архивировать файл с группами
        /// </summary>
        private static void ArchiveFile(string sourceFile)
        {
            //string pathD = Path.GetDirectoryName(path);
            string compressFile = Path.GetDirectoryName(sourceFile) + @"\N_Group.zip";
            string targetFile = "N_Group_new.txt";
            var f1 = new FileInfo(compressFile);
            var f2 = new FileInfo(sourceFile);
            if (File.Exists(compressFile))
            {
                File.Delete(compressFile);
            }
            using (ZipArchive zipArchive = ZipFile.Open(compressFile, ZipArchiveMode.Create))
            {
                zipArchive.CreateEntryFromFile(sourceFile, targetFile);

            }
            Console.WriteLine($"Сжатие файла {sourceFile} завершено. Исходный размер: {f2.Length.ToString("# ### ### ###")} " +
                $" сжатый размер: {f1.Length.ToString("# ### ### ###")}.");


        }

        /// <summary>
        /// Полный расчет с группировкой цифр по неделимости друг на друга, запись в файл
        /// </summary>
        /// <param name="numN"></param>
        private static string FullCalculate(long numN, string filePath)
        {
            string newFilepath = filePath.Insert(filePath.IndexOf('.'), "_inGroup");

            Stopwatch st = new Stopwatch();
            st.Start();
            using (StreamWriter sw = new StreamWriter(new FileStream(newFilepath, FileMode.Create, FileAccess.Write, FileShare.None, 10 * 1024 * 1024, false)))
            {
                int count = 1;
                long degree = 1;

                for (long i = 1; i <= numN; i++)
                {
                    if (i == degree)
                    {
                        sw.Write($"Группа {count++}: ");
                        degree *= 2;
                    }
                    sw.Write(i.ToString());
                    if (i == degree - 1)
                        sw.WriteLine(".");
                    else
                        sw.Write(" ");
                }
            }
            st.Stop();
            TimeSpan ts = st.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Расчет произведен, файл сохранен по пути: " + newFilepath);
            Console.WriteLine($"Время операции: {elapsedTime}");
            return newFilepath;
        }

        /// <summary>
        /// Расчет количества групп, вывод кол-ва на экран.
        /// </summary>
        /// <param name="numN"></param>
        private static void CountGroupCalculate(long numN)
        {
            int groupCount = (int)Math.Ceiling(Math.Log(numN, 2));
            Console.WriteLine($"Количество групп для числа {numN.ToString("# ### ### ###")}: {groupCount}");


        }

        /// <summary>
        /// Проверка файла на соответсвие условиям( число, не меньше 1, не больше 1_000_000_000). Если все ок - вернет число, нет - вернет "-1"
        /// </summary>
        /// <returns></returns>
        private static long CheckFile(string path)
        {
            StreamReader reader;
            if (path != "")
            {
                using (reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        if (long.TryParse(reader.ReadLine(), out long N))
                        {
                            if (N < 1 || N > 1_000_000_000)
                            {
                                Console.WriteLine("Число выходит за пределы границ (1 - 1.000.000.000)!");
                                return -1;
                            }
                            else
                            {
                                return N;
                            }

                        }
                        else
                        {
                            Console.WriteLine("Невозможно интерпертировать строку в файле как число!");
                            return -1;
                        }

                    }
                }
                return -1;
            }
            else
            {
                Console.WriteLine("Файл не выбран!");
                return -1;
            }
        }

        // Не знаю, где лежит файл, потому подумал будет логичнее сделать его выбор. 
        /// <summary>
        /// Получение пути к файлу через диалоговое окно
        /// </summary>
        /// <param name="isFileChoose"></param>
        /// <returns></returns>
        private static string GetFilePath()
        {
            string s;
            using (var ofDlg = new OpenFileDialog())
            {
                ofDlg.Filter = "Все файлы (*.*)|*.*";
                ofDlg.CheckFileExists = true;
                ofDlg.InitialDirectory = @"D:\";
                ofDlg.Title = "Выбор файла с числом N";
                ofDlg.ShowDialog();
                s = ofDlg.FileName;
            }
            return s;
        }
    }
}

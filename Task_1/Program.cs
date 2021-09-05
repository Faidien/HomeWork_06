using HomeWork_05;
using System;
using System.IO;
using System.Windows.Forms;

namespace Task_1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            UtilityClass.PrintHeader("* * * * * Работа с числом N * * * * *");
            long numN = CheckFile();

            if (numN > 0)
            {
                Console.WriteLine($"Число N: {numN.ToString("# ### ### ###")}");

                //Console.WriteLine("\nВыбор операции расчета: \n 1. Полный расчет групп\n 2. Расчет количества групп \n 3. Выход из программы");
                OperationSelect(numN);
            }

            Console.ReadLine();
        }
        /// <summary>
        /// Выбор пользователем операции
        /// </summary>
        /// <param name="numN"></param>
        /// <param name="typeOper">Тип операции. По умолчанию - 0(первый выбор операций), 1 - второй выбор. </param>
        private static void OperationSelect(long numN, byte typeOper = 0)
        {
            string[] operations1 = new string[] { "1. Полный расчет групп", "2. Расчет количества групп", "3. Выход из программы" };
            string[] operations2 = new string[] { "1. Поместить файл в архив (возможно восстание машин...)", "2. Не помещать файл в архив" };
            byte result;
            if (typeOper == 0)
            {
                Console.WriteLine("\nВыбор операции расчета: ");
                foreach (var item in operations1)
                {
                    Console.WriteLine($"{item}");
                }

                do
                {
                    byte.TryParse(Console.ReadLine(), out result);
                    switch (result)
                    {
                        case 1:
                            FullCalculate(numN);
                            break;
                        case 2:
                            CountCalculate(numN);
                            break;
                        case 3:

                            break;
                        default:
                            break;
                    }
                }
                while (result > operations1.Length & result < 1);
            }
            else
            {
                Console.WriteLine("\nВыбор операции архивирования: ");
                foreach (var item in operations2)
                {
                    Console.WriteLine($"{item}");
                }

                do
                {
                    byte.TryParse(Console.ReadLine(), out result);
                    switch (result)
                    {
                        case 1:
                            ArchiveFile();
                            break;
                        case 2:

                            break;
                        default:
                            break;
                    }
                }
                while (result > operations2.Length & result < 1);
            }
        }
        /// <summary>
        /// Архивировать файл с группами
        /// </summary>
        private static void ArchiveFile()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Полный расчет с группировкой цифр по неделимости друг на друга, запись в файл
        /// </summary>
        /// <param name="numN"></param>
        private static void FullCalculate(long numN)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Расчет количества групп, вывод кол-ва на экран.
        /// </summary>
        /// <param name="numN"></param>
        private static void CountCalculate(long numN)
        {
            int groupCount = (int)Math.Ceiling(Math.Log(numN, 2));
            Console.WriteLine($"Количество групп для числа {numN.ToString("# ### ### ###")}: {groupCount}");
        }

        /// <summary>
        /// Проверка файла на соответсвие условиям( число, не меньше 1, не больше 1_000_000_000). Если все ок - вернет число, нет - вернет "-1"
        /// </summary>
        /// <returns></returns>
        private static long CheckFile()
        {
            StreamReader reader;
            string path = GetFilePath(out bool isFileChoose);
            if (isFileChoose)
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
        private static string GetFilePath(out bool isFileChoose)
        {
            string s;
            using (var ofDlg = new OpenFileDialog())
            {
                ofDlg.Filter = "Все файлы (*.*)|*.*";
                ofDlg.CheckFileExists = true;
                ofDlg.InitialDirectory = @"D:\";
                ofDlg.Title = "Выбор файла с числом N";
                isFileChoose = (ofDlg.ShowDialog() == DialogResult.OK) ? true : false;
                s = ofDlg.FileName;
            }
            return s;
        }
    }
}

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
            OperationSelect(typeOper: 1);
            //long numN = CheckFile();

            //if (numN > 0)
            //{
            //Console.WriteLine($"Число N: {numN.ToString("# ### ### ###")}");
            //OperationSelect(numN);
            //}
            Console.ReadLine();
        }
        /// <summary>
        /// Выбор пользователем операции
        /// </summary>
        /// <param name="numN"></param>
        /// <param name="typeOper">Тип операции. По умолчанию - 0(первый выбор операций), 1 - второй выбор. </param>
        private static void OperationSelect(long numN = 0, byte typeOper = 0)
        {
            string[] operations = new string[] { "Открыть файл", "Выход из программы", "Расчет количества групп",
                "Полный расчет групп", "Поместить файл в архив (возможно восстание машин...)", "Не помещать файл в архив"};
            byte result;
            int minOut = typeOper * 2;
            int maxOut = minOut + 2;
            int count = 0;
            do
            {
                Console.WriteLine("\nВыбор операции: ");
                for (int i = minOut; i < maxOut; i++)
                {
                    Console.WriteLine($"{count + 1}. {operations[i]}");
                }
                byte.TryParse(Console.ReadLine(), out result);
            } while (result > maxOut || result <= minOut);


            switch (result)
            {
                case 1://Открыть файл

                    break;
                case 2://Выход из программы
                    Console.WriteLine("До свидания!");
                    break;
                case 3://Расчет количества групп
                    CountGroupCalculate(numN);

                    break;
                case 4://Полный расчет групп
                    FullCalculate(numN);
                    break;

                case 5://Поместить файл в архив (возможно восстание машин...)
                    ArchiveFile();
                    break;

                case 6://Не помещать файл в архив

                    break;
                default:
                    break;
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
        private static void CountGroupCalculate(long numN)
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

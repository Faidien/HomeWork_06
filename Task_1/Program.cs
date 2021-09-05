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
            if (CheckFile() > 0)
            {

            }

            Console.ReadLine();
        }

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
                                Console.WriteLine($"Число N - {N}");
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

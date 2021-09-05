using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string path = "";
            path = GetFilePath();            // Не знаю, где лежит файл, потому подумал будет логичнее сделать его выбор. 
            

            Console.ReadLine();
        }

        private static string  GetFilePath()
        {
            string s;
            using (var ofDlg = new OpenFileDialog())
            {
                ofDlg.Filter = "Текстовые файлы (*.txt, *.csv)|*.txt";
                ofDlg.CheckFileExists = true;
                ofDlg.InitialDirectory = @"C:\";
                if (ofDlg.ShowDialog() == DialogResult.OK)
                {
                    s = ofDlg.FileName;
                }
                else
                {
                    Console.WriteLine("Файл не выбран!");
                    s = "";

                }

            }
            return s;
        }
    }
}

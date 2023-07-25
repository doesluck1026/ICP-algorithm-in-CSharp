using ICP_Algorithm.WFA.Properties;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ICP_Algorithm.WFA
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GetLanguages();
            ShowMainForm();
        }

        static private void GetLanguages()
        {
            if (Settings.Default.Language == Languages.Languages.TurkishTR)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Languages.Languages.TurkishCodeTR);
            }
            else if (Settings.Default.Language == Languages.Languages.EnglishUS)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Languages.Languages.EnglishCodeUS);
            }
            else if (Settings.Default.Language == Languages.Languages.EnglishUK)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
            }
        }

        static private void ShowMainForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
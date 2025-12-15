using DatabaseAccessSem1;
using DatabaseAccessSem1.Repository;
using EksamenSemEt.UI;
using Sem1BackupForms;
namespace EksamenSemEt
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {

            Console.WriteLine("Hello Eggplant lovers!");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Hovedmenu());

            Application.Run(new MainForm());
        }
    }
}
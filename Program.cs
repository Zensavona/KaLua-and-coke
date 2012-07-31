using System;
using System.Collections.Generic;
using System.Text;
using Emulator.Forms;
using System.Windows.Forms;

namespace Emulator 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

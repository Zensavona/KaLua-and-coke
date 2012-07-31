using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace Emulator.Forms 
{
    public partial class MainForm : Form 
    {
        private Server server;
        private Thread serverThread;
        
        public static MainForm Instance;
    
        public MainForm() 
        {
            InitializeComponent();
            Application.ApplicationExit += new EventHandler(Application_Exit);
            MainForm.Instance = this;

            this.consoleTextBox.GotFocus += new EventHandler(consoleTextBox_GotFocus);
            this.consoleTextBox.Click += new EventHandler(consoleTextBox_Click);
        }

        void consoleTextBox_Click(object sender, EventArgs e)
        {
            HideCaret();
        }

        void consoleTextBox_GotFocus(object sender, EventArgs e)
        {
            HideCaret();
        }
        
        [DllImport("User32.dll")]
        static extern Boolean HideCaret(System.IntPtr hWnd);
        
        public void HideCaret()
        {
            HideCaret(this.consoleTextBox.Handle);
        }
        
        private void MainForm_Load(object sender, EventArgs e) 
        {
            ServerConsole.WriteLine("Starting Server");
            serverThread = new Thread(new ParameterizedThreadStart(
                delegate(object target) {
                    server = new Server();
                }
            ));
            serverThread.Start();            
        }

        private void exitMenuItem_Click(object sender, EventArgs e) 
        {
            Application.Exit();
        }
        
        private void Application_Exit(object sender, EventArgs e) 
        {
            try {
                server.Close(); 
            } catch(Exception) {}
            Environment.Exit(0);
        }

        public delegate void UIDelegate();
        
        public void InvokeDelegate(UIDelegate method) {
            try {
                Invoke(method);
            } catch(Exception) {}
        } 
        
    }
}
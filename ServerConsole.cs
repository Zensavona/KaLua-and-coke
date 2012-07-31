using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using Emulator.Forms;

namespace Emulator 
{
    class ServerConsole 
    {	
        public static void Write(string format, params object[] arg0)
        {
            MainForm.Instance.InvokeDelegate(delegate() 
            {
                MainForm.Instance.consoleTextBox.AppendText(String.Format(format, arg0));
                MainForm.Instance.consoleTextBox.Focus();
                MainForm.Instance.consoleTextBox.SelectionStart = MainForm.Instance.consoleTextBox.Text.Length;
                MainForm.Instance.HideCaret();
            });
        }
        
        public static void Write(Color color,string format, params object[] arg0)
        {
            MainForm.Instance.InvokeDelegate(delegate() 
            {
                MainForm.Instance.consoleTextBox.SelectionColor = color;
                
                MainForm.Instance.consoleTextBox.AppendText(String.Format(format, arg0));
                MainForm.Instance.consoleTextBox.Focus();
                MainForm.Instance.consoleTextBox.SelectionStart = MainForm.Instance.consoleTextBox.Text.Length;
                MainForm.Instance.HideCaret();
                
                MainForm.Instance.consoleTextBox.SelectionColor = Color.Black;
            });
        }
        
        public static void WriteLine(string format, params object[] arg0)
        {
            MainForm.Instance.InvokeDelegate(delegate() 
            {
                MainForm.Instance.consoleTextBox.AppendText(
                    String.Format(format, arg0) + 
                    System.Environment.NewLine   
                );    
                MainForm.Instance.consoleTextBox.Focus();
                MainForm.Instance.consoleTextBox.SelectionStart = MainForm.Instance.consoleTextBox.Text.Length;
                MainForm.Instance.HideCaret();
            });
        }

        public static void WriteLine(Color color,string format,params object[] arg0)
        {
            MainForm.Instance.InvokeDelegate(delegate() 
            {
                MainForm.Instance.consoleTextBox.SelectionColor = color;
                
                MainForm.Instance.consoleTextBox.AppendText(
                    String.Format(format, arg0) + 
                    System.Environment.NewLine   
                );    
                MainForm.Instance.consoleTextBox.Focus();
                MainForm.Instance.consoleTextBox.SelectionStart = MainForm.Instance.consoleTextBox.Text.Length;
                MainForm.Instance.HideCaret();
                
                MainForm.Instance.consoleTextBox.SelectionColor = Color.Black;
            });
        }
    }
}

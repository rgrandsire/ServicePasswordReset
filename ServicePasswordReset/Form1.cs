using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServicePasswordReset
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists("C:\\temp\\ResetServicePassword.cmd"))
            {
                try
                {
                    File.Delete("C:\\temp\\ResetServicePassword.cmd");
                }
                catch { }
            }
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] lines = {"@echo off","title Getting User status","rem Creating the sql file",
                              "echo UPDATE dbo.Core_UserAccounts set CustomPassword='4xQvZNBTuO9dZGcWJPqFkw==' where CustomUser='Service'; > temp.sql",
                              "echo Running the command", "sqlcmd -S localhost\\highqa17 -d HQA_CDB -U hqaservice5 -P WRD3!4x6K8CmfL#c@rBA -o Results.txt -i temp.sql",
                              "del temp.sql", "@exit"};
            //check if C:\temp exist
            if (!Directory.Exists("C:\\temp")) Directory.CreateDirectory("C:\\temp)");
            File.WriteAllLines("C:\\temp\\ResetServicePassword.cmd", lines);
            var startInfo = new ProcessStartInfo("C:\\temp\\ResetServicePassword.cmd");
            startInfo.UseShellExecute = true;
            startInfo.CreateNoWindow = true;
            startInfo.LoadUserProfile = true;
            Process.Start(startInfo);
            String ZrES = Path.GetDirectoryName(Application.ExecutablePath) + "\\Results.txt";
          if (File.Exists(ZrES) == true)
            {
                string[] zResult = File.ReadAllLines(ZrES);
              if (zResult.Contains("(1 rows affected)"))
                {
                    MessageBox.Show("The Service account's password has been reset", "Password Resewt Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace PPSWarmUp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<String> urls = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            System.IO.StreamReader file = new System.IO.StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\URLsToWarm.txt");
            string line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                urls.Add(line);
            }

            file.Close();

            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 200;
            timer1.Start();
        }
        
        int curItem = 0;
        void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Stop();
            timer1.Interval = 12000;
            if ((curItem + 1) > urls.Count)
            {
                Application.Exit();
                return;
            }

            webBrowser1.Navigate(urls[curItem]);
            curItem++;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            timer1.Start();
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
        }
    }
}

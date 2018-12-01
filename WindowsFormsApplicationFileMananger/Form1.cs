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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
        }

        private void RefreshList()
        {
            checkedListBox1.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
            DirectoryInfo[] diArray = di.GetDirectories();
            
            foreach (DirectoryInfo item in diArray)
            {
                checkedListBox1.Items.Add(item);
            }
            FileInfo[] fiArray = di.GetFiles();
            foreach (FileInfo item in fiArray)
            {
                checkedListBox1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath == "")
                return;
            RefreshList();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            FileInfo[] array = new FileInfo[checkedListBox1.CheckedItems.Count];
            checkedListBox1.CheckedItems.CopyTo(array, 0);
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            foreach (FileInfo item in array)
            {
                item.CopyTo(fbd.SelectedPath + @"\" + item.Name);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            FileInfo[] array = new FileInfo[checkedListBox1.CheckedItems.Count];
            checkedListBox1.CheckedItems.CopyTo(array, 0);
            try
            {
                foreach (FileInfo item in array)
                {
                    item.Delete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            RefreshList();
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            FileInfo[] array = new FileInfo[checkedListBox1.CheckedItems.Count];
            checkedListBox1.CheckedItems.CopyTo(array, 0);
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            try
            {
                foreach (FileInfo item in array)
                {
                    item.MoveTo(fbd.SelectedPath + @"\" + item.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            RefreshList();
        }

        private void buttonCreateFile_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter name of file (*.*)");
                return;
            }

            File.Create(folderBrowserDialog1.SelectedPath + @"\" + textBox1.Text);
            textBox1.Text = "";
            RefreshList();
        }

        private void buttonEditFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = folderBrowserDialog1.SelectedPath;
            openFileDialog1.ShowDialog();
            MessageBox.Show(openFileDialog1.FileName);
            EditForm ef = new EditForm(openFileDialog1.FileName);
            ef.ShowDialog();
        }

        private void checkedListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (checkedListBox1.SelectedItem is FileInfo)
                return;

            folderBrowserDialog1.SelectedPath = (checkedListBox1.SelectedItem as DirectoryInfo).FullName;
            RefreshList();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
            folderBrowserDialog1.SelectedPath = di.Parent.FullName;
            RefreshList();
        }
    }
}

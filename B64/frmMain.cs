using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace B64
{
    public partial class Form1 : Form
    {
        string filePath = "";
        string ext;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                txtPath.Text = filePath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                ext = ddlFileType.Text;
                
                if (ext == "--Select--")
                {
                    MessageBox.Show("Please select document type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Please select source file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                saveFileDialog1.Filter = ext + " files (*." + ext + ")|*." + ext + "|All files (*.*)|*.*";

                string savePath = "";
                saveFileDialog1.FileName = "OutputFile";
                saveFileDialog1.DefaultExt = ddlFileType.SelectedText;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    savePath = saveFileDialog1.FileName;
                }

                string str = new StreamReader(filePath).ReadToEnd().ToString();

                byte[] bytes = Convert.FromBase64String(str);

                System.IO.FileStream stream = new FileStream(savePath, FileMode.CreateNew);
                System.IO.BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();

                MessageBox.Show("Successfully generated the file", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {                
                ddlFileType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}

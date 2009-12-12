using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SABSyncGUI
{
    public partial class SABSyncGUI : Form
    {
        public SABSyncGUI()
        {
            InitializeComponent();

            string tvRootTextConfig = Program.TestMethod();
            tvRootText.Text = tvRootTextConfig;


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog" ;
            fdlg.InitialDirectory = @"c:\" ;
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*" ;
            fdlg.FilterIndex = 2 ;
            fdlg.RestoreDirectory = true ;

            if(fdlg.ShowDialog() == DialogResult.OK)
            {
                tvRootText.Text = fdlg.FileName ;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}

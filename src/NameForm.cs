using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFtpClient
{
    public partial class NameForm : Form
    {
        private bool isEditName;
        public string FileName { get; set; }

        public NameForm()
        {
            InitializeComponent();
            this.Load += NameForm_Load;
            this.FormClosing += NameForm_FormClosing;
        }

        public NameForm(string n) : this()
        {
            FileName = n;
            isEditName = true;
        }

        private void NameForm_Load(object sender, EventArgs e)
        {
            if (isEditName) { tbName.Text = FileName; }
        }

        private void NameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (tbName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Введите имя", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                this.FileName = this.tbName.Text.Trim();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFtpClient
{
    public partial class Connection : Form
    {
        public IPAddress ServerIp { get; private set; }
        public int Port { get; private set; }
        public string UserLogin { get; private set; }
        public string UserPassword { get; private set; }

        public Connection()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                IPAddress ip;
                if (IPAddress.TryParse(this.tbServerIp.Text, out ip) == false)
                {
                    this.errorProvider1.SetError(this.tbServerIp, "Некорректный адрес");
                    e.Cancel = true;
                }
                else
                {
                    this.errorProvider1.SetError(this.tbServerIp, "");
                    this.ServerIp = ip;
                }

                int port;
                if (Int32.TryParse(this.tbServerPort.Text, out port) == false)
                {
                    this.errorProvider1.SetError(this.tbServerPort, "Некорректный порт");
                    e.Cancel = true;
                }
                else if (port <= 0 || 65536 < port)
                {
                    this.errorProvider1.SetError(this.tbServerPort, "Допустимый диапазон 1-65536");
                    e.Cancel = true;
                }
                else
                {
                    this.errorProvider1.SetError(this.tbServerPort, "");
                    this.Port = port;
                }

                string login = this.tbLogin.Text.Trim();
                if (login.Length == 0 || login.Length > 255)
                {
                    this.errorProvider1.SetError(this.tbLogin, "Некорректный логин");
                    e.Cancel = true;
                }
                else
                {
                    this.errorProvider1.SetError(this.tbLogin, "");
                    this.UserLogin = login;
                }

                string password = this.tbPassword.Text.Trim();
                if (login.Length == 0 || login.Length > 255)
                {
                    this.errorProvider1.SetError(this.tbPassword, "Некорректный пароль");
                    e.Cancel = true;
                }
                else
                {
                    this.errorProvider1.SetError(this.tbPassword, "");
                    this.UserPassword = password;
                }
            }
        }
    }
}
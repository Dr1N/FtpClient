using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFtpClient
{
    public partial class MyFtpMain : Form
    {
        private string _currentPath;
        public string LocalPath
        {
            get
            {
                return _currentPath;
            }
        }

        private string _ftpPath;
        public string FtpPath
        {
            get 
            {
                return _ftpPath;
            }
        }

        private string _user;
        private string _password;
        private IPAddress _ftpServerIp;
        private int _ftpServerPort;

        private List<FileDirectoryInfo> _ftpFiles;

        public MyFtpMain()
        {
            InitializeComponent();
            this.Load += Form1_Load;

            this._ftpFiles = new List<FileDirectoryInfo>();
            this._ftpPath = "";
        }

        #region ControlEvents

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetFtpSettings();

            this._currentPath = "Компьютер";
            this.sbLocalPath.Text = this._currentPath;
            this.GetLocalDrives();
            this.SetColumnsForFiles(this.lvFtpFiles);

            this.lvLocalFiles.GotFocus += lvFiles_GotFocus;
            this.lvLocalFiles.LostFocus += lvFiles_LostFocus;
            this.lvFtpFiles.GotFocus += lvFiles_GotFocus;
            this.lvFtpFiles.LostFocus += lvFiles_LostFocus;
        }

        private void tsbUp_Click(object sender, EventArgs e)
        {
            if (this.lvLocalFiles.Focused == true)
            {
                this.UpLocal();
            }
            else if (this.lvFtpFiles.Focused == true)
            {
                this.UpFtp();
            }
        }

        #region ListView
        
        private void lvLocalFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv == null) { return; }

            if (lv.SelectedItems.Count != 1) { return; }

            ListViewItem currentItem = lv.SelectedItems[0];

            if (currentItem.Tag is DriveInfo)
            {
                DriveInfo drive = currentItem.Tag as DriveInfo;
                this.GetDirectoriesAndFiles(drive.RootDirectory.FullName);
            }
            else if (currentItem.Tag is DirectoryInfo)
            {
                DirectoryInfo dir = currentItem.Tag as DirectoryInfo;
                this.GetDirectoriesAndFiles(dir.FullName);
            }
        }

        private void lvFtpFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv == null) { return; }

            if (lv.SelectedItems.Count != 1) { return; }

            ListViewItem currentItem = lv.SelectedItems[0];
            FileDirectoryInfo fdi = currentItem.Tag as FileDirectoryInfo;

            if (fdi == null) { return; }

            if (fdi.Type == "Каталог")
            {
                this._ftpPath += "/" + fdi.Name;
                this.sbFtpPath.Text = this._ftpPath;
                this.GetFilesFromFTPServer(this._ftpPath);
                this.UpdateFtpFilesList();
            }
        }

        private void lvFiles_GotFocus(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv == null) { return; }

            if (lv.Name == this.lvLocalFiles.Name)
            {
                this.splitContainer1.Panel1.BackColor = Color.Yellow;
            }
            else if (lv.Name == this.lvFtpFiles.Name)
            {
                this.splitContainer1.Panel2.BackColor = Color.Yellow;
            }
        }

        private void lvFiles_LostFocus(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv == null) { return; }

            if (lv.Name == this.lvLocalFiles.Name)
            {
                this.splitContainer1.Panel1.BackColor = SystemColors.Control;
            }
            else
            {
                this.splitContainer1.Panel2.BackColor = SystemColors.Control;
            }
        }

        #endregion

        #region Menu
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.GetFilesFromFTPServer(this._ftpPath);
        }

        private void cmnFtpMakeDirectory_Click(object sender, EventArgs e)
        {
            NameForm nameForm = new NameForm();
            if (nameForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.FtpOperations(nameForm.FileName, WebRequestMethods.Ftp.MakeDirectory);
            }
        }

        private void cmnFtpDeleteDirectory_Click(object sender, EventArgs e)
        {
            if (this.lvFtpFiles.SelectedItems.Count != 1) { return; }

            FileDirectoryInfo fdi = this.lvFtpFiles.SelectedItems[0].Tag as FileDirectoryInfo;
            
            if (fdi == null) { return; }

            string method = (fdi.Type == "Каталог") ? WebRequestMethods.Ftp.RemoveDirectory : WebRequestMethods.Ftp.DeleteFile;
            this.FtpOperations(fdi.Name, method);
        }

        private void cmnFtpRename_Click(object sender, EventArgs e)
        {
            if (this.lvFtpFiles.SelectedItems.Count != 1) { return; }

            FileDirectoryInfo fdi = this.lvFtpFiles.SelectedItems[0].Tag as FileDirectoryInfo;

            if (fdi == null) { return; }

            NameForm nameForm = new NameForm(fdi.Name);
            if (nameForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (fdi.Name == nameForm.FileName) { return; }
                this.FtpRename(fdi.Name, nameForm.FileName, fdi.Type == "Файл");
            }
        }

        private void cmnLocalUploadFtp_Click(object sender, EventArgs e)
        {
            if (this.lvLocalFiles.SelectedItems.Count != 1) { return; }

            FileInfo fi = this.lvLocalFiles.SelectedItems[0].Tag as FileInfo;
            if (fi == null)
            {
                MessageBox.Show("Выберете файл", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.UploadFileToFtp(fi);
        }

        private void cmnFtpDownloadFile_Click(object sender, EventArgs e)
        {
            if (this.lvFtpFiles.SelectedItems.Count != 1) { return; }

            FileDirectoryInfo fdi = this.lvFtpFiles.SelectedItems[0].Tag as FileDirectoryInfo;

            if (fdi == null) { return; }

            if(fdi.Type != "Файл")
            {
                MessageBox.Show("Выберете файл", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(this._currentPath == "Компьютер")
            {
                MessageBox.Show("Выберете путь для скачивания", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DownloadFileFromFtp(fdi.Name);
        }

        #endregion

        #endregion

        #region FTP

        private void GetFilesFromFTPServer(string path)
        {
            FtpWebResponse ftpResp = null;
            try
            {
                this._ftpFiles.Clear();

                FtpWebRequest ftpReq = this.GetFtpRequest(path, WebRequestMethods.Ftp.ListDirectoryDetails, false);
                ftpResp = (FtpWebResponse)ftpReq.GetResponse();

                if (ftpResp.StatusCode == FtpStatusCode.OpeningData)
                {
                    string responce;
                    using (Stream ftpStream = ftpResp.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(ftpStream, Encoding.Default))
                        {
                            responce = reader.ReadToEnd();
                        }
                    }

                    using (StringReader sReader = new StringReader(responce))
                    {
                        string str;
                        while (true)
                        {
                            try
                            {
                                str = sReader.ReadLine();
                                if (str == null) { break; }
                                this.AddFileToFileList(str);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось прочитать список файлов на сервере", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.UpdateFtpFilesList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ftpResp != null) { ftpResp.Close(); }
            }
        }

        private void SetFtpSettings()
        {
            Connection cnForm = new Connection();
            if (cnForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._ftpServerIp = cnForm.ServerIp;
                this._ftpServerPort = cnForm.Port;
                this._user = cnForm.UserLogin;
                this._password = cnForm.UserPassword;
            }
        }

        private FtpWebRequest GetFtpRequest(string path, string method, bool isFile)
        {
            string uri = "ftp://" + this._ftpServerIp.ToString() + path;
            if (isFile != true)
            {
                uri += "/";
            }
            FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(uri);
            ftpRequest.Credentials = new NetworkCredential(this._user, this._password);
            ftpRequest.Method = method;
            ftpRequest.Proxy = null;

            return ftpRequest;
        }

        private void FtpOperations(string name, string method)
        {
            FtpWebResponse ftpResp = null;
            try
            {
                FtpWebRequest ftpRequest = null;
                string path = this._ftpPath + "/" + name;
                if (method == WebRequestMethods.Ftp.MakeDirectory || method == WebRequestMethods.Ftp.RemoveDirectory)
                {
                    ftpRequest = this.GetFtpRequest(path, method, false);
                }
                else if (method == WebRequestMethods.Ftp.DeleteFile)
                {
                    ftpRequest = this.GetFtpRequest(path, method, true);
                }

                ftpResp = (FtpWebResponse)ftpRequest.GetResponse();
                if (ftpResp.StatusCode != FtpStatusCode.PathnameCreated && ftpResp.StatusCode != FtpStatusCode.FileActionOK)
                {
                    string message = String.Format("{0}\nКод: {1}", "Не удалось выполнить операцию", ftpResp.StatusCode);
                    MessageBox.Show(message, "Операция на FTP сервере", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.GetFilesFromFTPServer(this._ftpPath);
                }
            }
            catch (WebException we)
            {
                MessageBox.Show(we.Message, "Операция на FTP сервере", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка в приложении", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ftpResp != null) { ftpResp.Close(); }
            }
        }

        private void FtpRename(string oldName, string newName, bool isFile)
        {
            FtpWebResponse ftpResp = null;
            try
            {
                string path = this._ftpPath + "/" + oldName;
                FtpWebRequest ftpRequest = this.GetFtpRequest(path, WebRequestMethods.Ftp.Rename, true);
                ftpRequest.RenameTo = newName;
                ftpResp = (FtpWebResponse)ftpRequest.GetResponse();

                if (ftpResp.StatusCode == FtpStatusCode.FileActionOK)
                {
                    this.GetFilesFromFTPServer(this._ftpPath);
                }
                else
                {
                    string message = String.Format("{0}\nКод: {1}", "Не удалось выполнить операцию", ftpResp.StatusCode);
                    MessageBox.Show(message, "Операция на FTP сервере", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (WebException we)
            {
                MessageBox.Show(we.Message, "Операция на FTP сервере", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка в приложении", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ftpResp != null) { ftpResp.Close(); }
            }
        }

        private void UploadFileToFtp(FileInfo file)
        {
            try
            {
                //Запрос

                string fileName = this._ftpPath + "/" + file.Name;
                FtpWebRequest ftpRequest = this.GetFtpRequest(fileName, WebRequestMethods.Ftp.UploadFile, true);
                ftpRequest.UseBinary = true;
                ftpRequest.KeepAlive = true;

                //Запись файла

                using (FileStream fileStream = file.OpenRead())
                {
                    ftpRequest.ContentLength = fileStream.Length;
                    using (Stream ftpRequestStream = ftpRequest.GetRequestStream())
                    {
                        byte[] buffer = new byte[4096];
                        int cnt = 0;
                        do
                        {
                            cnt = fileStream.Read(buffer, 0, buffer.Length);
                            if (cnt == 0) { break; }
                            ftpRequestStream.Write(buffer, 0, cnt);
                        } while (true);
                    }
                }

                //Запрос

                using (FtpWebResponse ftpResp = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    if (ftpResp.StatusCode == FtpStatusCode.ClosingData)
                    {
                        this.GetFilesFromFTPServer(this._ftpPath);
                    }
                    else
                    {
                        string message = String.Format("{0}\nКод: {1}", "Не удалось выполнить операцию", ftpResp.StatusCode);
                        MessageBox.Show(message, "Операция на FTP сервере", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (WebException we)
            {
                MessageBox.Show(we.Message, "Операция на FTP сервере", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка в приложении", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DownloadFileFromFtp(string file)
        {
            try
            {
                string srcName = this._ftpPath + "/" + file;
                FtpWebRequest ftpRequest = this.GetFtpRequest(srcName, WebRequestMethods.Ftp.DownloadFile, true);
                ftpRequest.UseBinary = true;
                ftpRequest.KeepAlive = true;

                string dstName = Path.Combine(this._currentPath, file);
               
                using (FtpWebResponse ftpResp = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    if (ftpResp.StatusCode == FtpStatusCode.OpeningData)
                    {
                        using (FileStream fileStream = new FileStream(dstName, FileMode.Create, FileAccess.Write))
                        {
                            Stream responseStream = ftpResp.GetResponseStream();
                            byte[] buffer = new byte[4096];
                            int cnt = 0;
                            while (true)
                            {
                                cnt = responseStream.Read(buffer, 0, buffer.Length);
                                if (cnt == 0) { break; }
                                fileStream.Write(buffer, 0, cnt);
                            }
                        }
                        this.GetDirectoriesAndFiles(this._currentPath);
                    }
                    else
                    {
                        string message = String.Format("{0}\nКод: {1}", "Не удалось выполнить операцию", ftpResp.StatusCode);
                        MessageBox.Show(message, "Операция на FTP сервере", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (WebException we)
            {
                MessageBox.Show(we.Message, "Операция на FTP сервере", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка в приложении", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion

        #region Helpers

        private void GetLocalDrives()
        {
            DriveInfo[] localDrives;
            try
            {
                localDrives = DriveInfo.GetDrives();
                if (localDrives.Length == 0)
                {
                    MessageBox.Show("Диски не обнаружены", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "Получение списка дисков", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (UnauthorizedAccessException uaae)
            {
                MessageBox.Show(uaae.Message, "Доступ к дискам", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Что-то сломалось", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.lvLocalFiles.Items.Clear();
            this.SetColumnsForDrives();

            ListViewItem lvi;
            foreach (DriveInfo drive in localDrives)
            {
                try
                {
                    string totalSize = String.Format("{0:F2} Гб", (double)drive.TotalSize / (1024 * 1024 * 1024));
                    string freeSpace = String.Format("{0:F2} Гб", (double)drive.TotalFreeSpace / (1024 * 1024 * 1024));

                    lvi = new ListViewItem(drive.Name);
                    lvi.SubItems.Add(drive.DriveType.ToString());
                    lvi.SubItems.Add(totalSize);
                    lvi.SubItems.Add(freeSpace);
                    lvi.Tag = drive;
                    this.lvLocalFiles.Items.Add(lvi);
                }
                catch { }
            }

            foreach (ColumnHeader col in this.lvLocalFiles.Columns)
            {
                col.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

            this._currentPath = "Компьютер";
            this.sbLocalPath.Text = this._currentPath;
        }

        private void SetColumnsForDrives()
        {
            this.lvLocalFiles.Columns.Clear();

            ColumnHeader ch = new ColumnHeader();
            ch.Text = "Имя";
            this.lvLocalFiles.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = "Тип"; 
            this.lvLocalFiles.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = "Общий размер";
            this.lvLocalFiles.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = "Свободно";
            this.lvLocalFiles.Columns.Add(ch);

        }

        private void GetDirectoriesAndFiles(string path)
        {
            try
            {
                this.lvLocalFiles.Items.Clear();
                this.SetColumnsForFiles(this.lvLocalFiles);

                ListViewItem lvi;
                DateTime lastTime;

                var directories = from dir in Directory.GetDirectories(path) select (new DirectoryInfo(dir));
                foreach (DirectoryInfo dir in directories)
                {
                    try
                    {
                        lastTime = dir.LastWriteTime;
                        lvi = new ListViewItem(dir.Name);
                        lvi.SubItems.Add("Каталог");
                        lvi.SubItems.Add("");
                        lvi.SubItems.Add(lastTime.ToLongDateString() + " " + lastTime.ToLongTimeString());
                        lvi.Tag = dir;
                        this.lvLocalFiles.Items.Add(lvi);
                    }
                    catch { }
                }

                string fileSize;
                var files = from file in Directory.GetFiles(path) select (new FileInfo(file));
                foreach (FileInfo file in files)
                {
                    try
                    {
                        fileSize = file.Length / 1024 + " Кб";
                        lastTime = file.LastWriteTime;
                        lvi = new ListViewItem(file.Name);
                        lvi.SubItems.Add("Файл");
                        lvi.SubItems.Add(fileSize);
                        lvi.SubItems.Add(lastTime.ToLongDateString() + " " + lastTime.ToLongTimeString());
                        lvi.Tag = file;
                        this.lvLocalFiles.Items.Add(lvi);
                    }
                    catch { }
                }

                foreach (ColumnHeader col in this.lvLocalFiles.Columns)
                {
                    col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }

                this._currentPath = path;
                this.sbLocalPath.Text = this._currentPath;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void SetColumnsForFiles(ListView lv)
        {
            lv.Columns.Clear();

            string dateCol = (lv.Name == "lvFtpFiles") ? "Дата создания" : "Дата изменения";

            ColumnHeader ch = new ColumnHeader();
            ch.Text = "Имя";
            lv.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = "Тип";
            lv.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = "Размер";
            lv.Columns.Add(ch);

            ch = new ColumnHeader();
            ch.Text = dateCol;
            lv.Columns.Add(ch);
        }

        private void AddFileToFileList(string ftpString)
        {
            Console.WriteLine(ftpString);

            if (ftpString[ftpString.Length - 1] == '.') { return; }

            //Парсинг данных

            Regex rex = new Regex(@"\s+");
            string repStr = rex.Replace(ftpString, " ");
            string[] splStr = repStr.Split(' ');

            string type = (splStr[0][0] == 'd') ? "Каталог" : "Файл";
            int size = Int32.Parse(splStr[4]);
            string date = String.Format("{0} {1} {2}", splStr[5], splStr[6], splStr[7]);

            //Парсинг имени

            int start = ftpString.IndexOf(splStr[7]) + splStr[7].Length + 1;
            string name = ftpString.Substring(start, ftpString.Length - start);

            this._ftpFiles.Add(new FileDirectoryInfo(name, type, size, date));
        }

        private void UpdateFtpFilesList()
        {
            this.lvFtpFiles.Items.Clear();

            var fileList = from f in this._ftpFiles orderby f.Type, f.Name select f;
            string size;
            ListViewItem lvi;
            foreach (FileDirectoryInfo item in fileList)
            {
                size = (item.Size == -1) ? "" : item.Size.ToString() + " Кб";
                lvi = new ListViewItem(item.Name);
                lvi.SubItems.Add(item.Type);
                lvi.SubItems.Add(size);
                lvi.SubItems.Add(item.Date);
                lvi.Tag = item;
                lvFtpFiles.Items.Add(lvi);
            }

            foreach (ColumnHeader item in this.lvFtpFiles.Columns)
            {
                item.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        private void UpLocal()
        {
            if (this._currentPath == "Компьютер") { return; }

            DirectoryInfo currentDirectory = new DirectoryInfo(this._currentPath);
            DirectoryInfo parrentDirectory = currentDirectory.Parent;

            if (parrentDirectory == null)
            {
                this.GetLocalDrives();
            }
            else
            {
                this.GetDirectoriesAndFiles(parrentDirectory.FullName);
            }
        }

        private void UpFtp()
        {
            if (this._ftpPath == "") { return; }

            int tmp = this._ftpPath.LastIndexOf('/');
            this._ftpPath = this._ftpPath.Substring(0, tmp);
            this.sbFtpPath.Text = this._ftpPath;

            this.GetFilesFromFTPServer(this._ftpPath);
            this.UpdateFtpFilesList();
        }

        #endregion
    }
}
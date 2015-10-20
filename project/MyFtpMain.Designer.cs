namespace MyFtpClient
{
    partial class MyFtpMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyFtpMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbUp = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvLocalFiles = new System.Windows.Forms.ListView();
            this.cmnFileOperation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnLocalUploadFtp = new System.Windows.Forms.ToolStripMenuItem();
            this.lvFtpFiles = new System.Windows.Forms.ListView();
            this.cmnFtpOperation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnFtpDownloadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnFtpMakeDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnFtpRename = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnFtpDeleteDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbLocalPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbFtpPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmnFileOperation.SuspendLayout();
            this.cmnFtpOperation.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(984, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbUp
            // 
            this.tsbUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbUp.Image")));
            this.tsbUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUp.Name = "tsbUp";
            this.tsbUp.Size = new System.Drawing.Size(23, 22);
            this.tsbUp.Text = "На уровень вверх";
            this.tsbUp.Click += new System.EventHandler(this.tsbUp_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 52);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvLocalFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvFtpFiles);
            this.splitContainer1.Size = new System.Drawing.Size(984, 585);
            this.splitContainer1.SplitterDistance = 477;
            this.splitContainer1.TabIndex = 2;
            // 
            // lvLocalFiles
            // 
            this.lvLocalFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLocalFiles.ContextMenuStrip = this.cmnFileOperation;
            this.lvLocalFiles.FullRowSelect = true;
            this.lvLocalFiles.GridLines = true;
            this.lvLocalFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvLocalFiles.HideSelection = false;
            this.lvLocalFiles.Location = new System.Drawing.Point(3, 3);
            this.lvLocalFiles.MultiSelect = false;
            this.lvLocalFiles.Name = "lvLocalFiles";
            this.lvLocalFiles.Size = new System.Drawing.Size(471, 579);
            this.lvLocalFiles.TabIndex = 0;
            this.lvLocalFiles.UseCompatibleStateImageBehavior = false;
            this.lvLocalFiles.View = System.Windows.Forms.View.Details;
            this.lvLocalFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvLocalFiles_MouseDoubleClick);
            // 
            // cmnFileOperation
            // 
            this.cmnFileOperation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnLocalUploadFtp});
            this.cmnFileOperation.Name = "contextMenuStrip1";
            this.cmnFileOperation.Size = new System.Drawing.Size(163, 26);
            // 
            // cmnLocalUploadFtp
            // 
            this.cmnLocalUploadFtp.Name = "cmnLocalUploadFtp";
            this.cmnLocalUploadFtp.Size = new System.Drawing.Size(162, 22);
            this.cmnLocalUploadFtp.Text = "Загрузить на ftp";
            this.cmnLocalUploadFtp.Click += new System.EventHandler(this.cmnLocalUploadFtp_Click);
            // 
            // lvFtpFiles
            // 
            this.lvFtpFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFtpFiles.ContextMenuStrip = this.cmnFtpOperation;
            this.lvFtpFiles.FullRowSelect = true;
            this.lvFtpFiles.GridLines = true;
            this.lvFtpFiles.HideSelection = false;
            this.lvFtpFiles.Location = new System.Drawing.Point(3, 3);
            this.lvFtpFiles.MultiSelect = false;
            this.lvFtpFiles.Name = "lvFtpFiles";
            this.lvFtpFiles.Size = new System.Drawing.Size(497, 579);
            this.lvFtpFiles.TabIndex = 0;
            this.lvFtpFiles.UseCompatibleStateImageBehavior = false;
            this.lvFtpFiles.View = System.Windows.Forms.View.Details;
            this.lvFtpFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFtpFiles_MouseDoubleClick);
            // 
            // cmnFtpOperation
            // 
            this.cmnFtpOperation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnFtpDownloadFile,
            this.toolStripSeparator3,
            this.cmnFtpMakeDirectory,
            this.cmnFtpRename,
            this.cmnFtpDeleteDirectory});
            this.cmnFtpOperation.Name = "cmnFtpOperation";
            this.cmnFtpOperation.Size = new System.Drawing.Size(172, 98);
            // 
            // cmnFtpDownloadFile
            // 
            this.cmnFtpDownloadFile.Name = "cmnFtpDownloadFile";
            this.cmnFtpDownloadFile.Size = new System.Drawing.Size(171, 22);
            this.cmnFtpDownloadFile.Text = "Скачать файл";
            this.cmnFtpDownloadFile.Click += new System.EventHandler(this.cmnFtpDownloadFile_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(168, 6);
            // 
            // cmnFtpMakeDirectory
            // 
            this.cmnFtpMakeDirectory.Name = "cmnFtpMakeDirectory";
            this.cmnFtpMakeDirectory.Size = new System.Drawing.Size(171, 22);
            this.cmnFtpMakeDirectory.Text = "Создать каталог...";
            this.cmnFtpMakeDirectory.Click += new System.EventHandler(this.cmnFtpMakeDirectory_Click);
            // 
            // cmnFtpRename
            // 
            this.cmnFtpRename.Name = "cmnFtpRename";
            this.cmnFtpRename.Size = new System.Drawing.Size(171, 22);
            this.cmnFtpRename.Text = "Переименовать...";
            this.cmnFtpRename.Click += new System.EventHandler(this.cmnFtpRename_Click);
            // 
            // cmnFtpDeleteDirectory
            // 
            this.cmnFtpDeleteDirectory.Name = "cmnFtpDeleteDirectory";
            this.cmnFtpDeleteDirectory.Size = new System.Drawing.Size(171, 22);
            this.cmnFtpDeleteDirectory.Text = "Удалить";
            this.cmnFtpDeleteDirectory.Click += new System.EventHandler(this.cmnFtpDeleteDirectory_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.sbLocalPath,
            this.toolStripStatusLabel2,
            this.sbFtpPath});
            this.statusStrip1.Location = new System.Drawing.Point(0, 640);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(3);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(106, 16);
            this.toolStripStatusLabel1.Text = "Локальный путь:";
            // 
            // sbLocalPath
            // 
            this.sbLocalPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbLocalPath.Margin = new System.Windows.Forms.Padding(0, 3, 50, 2);
            this.sbLocalPath.Name = "sbLocalPath";
            this.sbLocalPath.Size = new System.Drawing.Size(33, 17);
            this.sbLocalPath.Text = "Путь";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(3);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(58, 16);
            this.toolStripStatusLabel2.Text = "FTP путь:";
            // 
            // sbFtpPath
            // 
            this.sbFtpPath.Name = "sbFtpPath";
            this.sbFtpPath.Size = new System.Drawing.Size(0, 17);
            // 
            // MyFtpMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MyFtpMain";
            this.Text = "MyFtp";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmnFileOperation.ResumeLayout(false);
            this.cmnFtpOperation.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbUp;
        private System.Windows.Forms.ListView lvLocalFiles;
        private System.Windows.Forms.ListView lvFtpFiles;
        private System.Windows.Forms.ToolStripStatusLabel sbLocalPath;
        private System.Windows.Forms.ContextMenuStrip cmnFileOperation;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel sbFtpPath;
        private System.Windows.Forms.ToolStripMenuItem cmnLocalUploadFtp;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmnFtpOperation;
        private System.Windows.Forms.ToolStripMenuItem cmnFtpDownloadFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cmnFtpMakeDirectory;
        private System.Windows.Forms.ToolStripMenuItem cmnFtpDeleteDirectory;
        private System.Windows.Forms.ToolStripMenuItem cmnFtpRename;

    }
}


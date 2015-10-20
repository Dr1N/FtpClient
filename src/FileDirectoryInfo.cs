using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFtpClient
{
    class FileDirectoryInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }

        private int size;
        public int Size
        {
            get
            {
                if (Type == "Файл")
                {
                    return size / 1024;
                }
                return -1;
            }
            private set
            {
                this.size = value;
            }
        }
        public string Date { get; set; }

        public FileDirectoryInfo(string name, string type, int size, string date)
        {
            this.Name = name;
            this.Type = type;
            this.Size = size;
            this.Date = date;
        }
    }
}
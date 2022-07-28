using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Services.Model
{
    public class SaveFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtention { get; set; }
        public string FileType { get; set; }
        public byte[] DocumentData { get; set; }
        public string Message { get; set; }
    }
}

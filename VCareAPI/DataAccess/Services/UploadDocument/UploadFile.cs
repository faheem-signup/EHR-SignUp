using DataAccess.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.UploadDocument
{
    public class UploadFile : IUploadFile
    {
        public async Task<SaveFile> Upload(string file, string moduleName, string folderName, string fileName)
        {
            SaveFile data = new SaveFile();
            if (string.IsNullOrEmpty(file))
            {
                return null;
            }

            string fileData = file.Split(',')[1];
            string fileExtention = GetFileExtension.GetBase64FileExtension(fileData);
            fileName = fileName + "-" + DateTime.Now.Ticks + "." + fileExtention;
            byte[] bytes = Convert.FromBase64String(fileData);
            string mainFolder = "UploadedFiles";
            string containerName = moduleName;
            string folderpath = Path.GetFullPath("~/" + mainFolder + "/" + folderName).Replace("~\\", string.Empty);

            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            string filePath = "/" + mainFolder + "/" + folderName + "/" + fileName;
            string fullpath = Path.GetFullPath("~" + filePath).Replace("~\\", string.Empty);
            //using (FileStream fs = File.Create(fullpath, 1024))
            //{
            //    fs.Write(bytes, 0, bytes.Length);
            //}

            data.FileName = fileName;
            data.FileExtention = fileExtention;
            data.FilePath = null;//fullpath;
            data.DocumentData = bytes;
            data.Message = "Saved";

            return data;
        }

        public static class GetFileExtension
        {
            public static string GetBase64FileExtension(string base64String)
            {
                var data = base64String.Substring(0, 5);

                switch (data.ToUpper())
                {
                    case "IVBOR":
                        return "png";
                    case "/9J/4":
                        return "jpg";
                    case "JVBER":
                        return "pdf";
                    case "AAABA":
                        return "ico";
                    case "UMFYI":
                        return "rar";
                    case "E1XYD":
                        return "rtf";
                    case "U1PKC":
                        return "txt";
                    case "MQOWM":
                    case "77U/M":
                        return "srt";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}

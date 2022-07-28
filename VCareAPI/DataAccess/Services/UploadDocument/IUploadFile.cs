using DataAccess.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.UploadDocument
{
    public interface IUploadFile
    {
        Task<SaveFile> Upload(string file, string moduleName, string folderName, string fileName);
    }
}

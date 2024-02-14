using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Domain.Common
{
    public class FileHelper
    {
        public static void SaveFileInFolder(string document, string filePath)
        {

            string bData = document.Substring(document.IndexOf(",") + 1);
            byte[] binaryData = Convert.FromBase64String(bData);
            MemoryStream ms = new MemoryStream(binaryData);

            FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            ms.WriteTo(file);
            file.Close();
            ms.Close();
        }

        public static void CreateFolder(string folderPath)
        {
            bool exists = Directory.Exists(folderPath);
            if (!exists)
            {
                Directory.CreateDirectory(folderPath);
            }
        }
        

    }
}

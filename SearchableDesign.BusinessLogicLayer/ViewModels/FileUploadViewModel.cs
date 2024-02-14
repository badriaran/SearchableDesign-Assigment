using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchableDesign.Repository.ViewModels
{
    public class FileUploadViewModel
    {
        public string Document { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentFullPath { get; set; }
        public int DocumentSize { get; set; }
        public string Remarks { get; set; }

    }
    public class ImageUploadViewModel
    {
        public string Image { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public string ImageFullPath { get; set; }
        public int ImageSize { get; set; }
        public string Remarks { get; set; }
    }
}

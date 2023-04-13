using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class FileModel : BaseModel
    {
        private string fileName;

        public string FileName { get => fileName; set { fileName = value; OnPropertyChanged(); } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class WordWrapperModel : BaseModel
    {
        private string _word;        
        public string Word { get => _word; set { _word = value; OnPropertyChanged(); } }

        private int _count;
        public int Count { get => _count; set { _count = value; OnPropertyChanged(); } }
    }
}

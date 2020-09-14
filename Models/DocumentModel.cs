using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EZDocStorage.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }

        public string DocName { get; set; }
        public int Btyes { get; set; }
        public DateTime CreationDate { get; set; }
        public string Extention { get; set; }
    }
}

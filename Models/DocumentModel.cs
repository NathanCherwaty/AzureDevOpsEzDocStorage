using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EZDocStorage.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        [Required]
        [JsonProperty(PropertyName = "name")]
        public string DocName { get; set; }
        [Required]
        public string Bytes { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public string Extension { get; set; }
    }

    public class DocumentModelDTO
    {
        public int Id { get; set; }

        public string DocName { get; set; }
    }
}

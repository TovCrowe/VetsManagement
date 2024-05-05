using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Patient
    {
        [Key]
        public int cliente_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public DateTime fecha_de_alta { get; set; } 
        public string sintomas { get; set; } = string.Empty;



    }
}
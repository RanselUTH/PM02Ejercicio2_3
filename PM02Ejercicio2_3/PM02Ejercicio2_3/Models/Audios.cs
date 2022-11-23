using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM02Ejercicio2_3.Models
{
    public class Audios
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string url { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
    }
}

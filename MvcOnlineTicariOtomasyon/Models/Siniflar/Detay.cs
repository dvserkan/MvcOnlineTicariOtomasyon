using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Detay
    {
        [Key]
        public int DetayID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string urunad { get; set; }
        public string urunbilgi { get; set; }
    }
}
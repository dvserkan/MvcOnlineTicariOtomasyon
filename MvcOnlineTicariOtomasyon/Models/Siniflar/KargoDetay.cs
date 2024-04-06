using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayid { get; set; }
        public string Aciklama { get; set; }
        public string TakipKodu { get; set; }
        public string Personel { get; set; }
        public string Alici { get; set; }
        public DateTime Tarih { get; set; }
        public bool Status { get; set; }
    }
}
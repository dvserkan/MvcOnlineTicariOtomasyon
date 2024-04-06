﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Cariler
    {
        [Key]
        public int Cariid { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50,ErrorMessage ="En Fazla 50 Karakter Yazabilirsiniz.")]
        [Required(ErrorMessage ="Bu Alan Boş Geçilemez.")]
        public string CariAd { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CariSoyad { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CariUnvan { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(15)]
        public string CariSehir { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CariMail { get; set; }
        public string CariSifre { get; set; }
        public bool CariStatus { get; set; }
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}
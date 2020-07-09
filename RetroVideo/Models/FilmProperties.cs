using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetroVideo.Models
{
    public class FilmProperties
    {
        [DisplayFormat(DataFormatString = "{0:€ #,##0.00}")]
        public decimal prijs { get; set; }
    }
}
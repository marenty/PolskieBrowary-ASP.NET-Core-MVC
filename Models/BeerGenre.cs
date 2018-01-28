using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PolskieBrowary.Models
{
    public class BeerGenre
    {
        public int BeerGenreID { get; set; }

        [Required(ErrorMessage = "Podaj zawartość alkoholu.")]
        [Remote(
            action: "VerifyBeerGenre",
            controller: "BeerGenres"
            )]
        [StringLength(50)]
        [Display(Name = "Nazwa Gatunku")]
        public string GenreName { get; set; }

        [Required(ErrorMessage = "Podaj zawartość alkoholu.")]
        [StringLength(1500, MinimumLength = 10, ErrorMessage ="Minimalna długość opisu to 10 znaków")]
        [Display(Name = "Opis Gatunku")]
        public string GenreDescription { get; set; }
    }
}

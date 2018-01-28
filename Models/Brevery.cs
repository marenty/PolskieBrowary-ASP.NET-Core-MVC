using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PolskieBrowary.Models
{
    public class Brevery
    {
        public int BreveryID { get; set; }

        [Required(ErrorMessage = "Musisz wypełnić to pole")]
        [Remote(
            action: "VerifyBreveryName",
            controller: "Breveries"
            )]
        [StringLength(40)]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z ]+$", ErrorMessage = "Podaj właściwą nazwę browaru!")]
        [Display(Name = "Nazwa Browaru")]
        public string BreveryName { get; set; }

        [Required(ErrorMessage = "Musisz wypełnić to pole")]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z]+[a-zA-Z ]+$", ErrorMessage = "Podaj właściwe miasto!")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Musisz wypełnić to pole")]
        [StringLength(2000, MinimumLength = 10)]
        [Display(Name = "Opis Browaru")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Musisz podać rok założenia browaru!")]
        [Range(1300, 2018, ErrorMessage = "Podaj odpowiedni rok założenia browaru")]
        [Display(Name = "Rok założenia")]
        public int Established { get; set; }

        [Required(ErrorMessage = "Musisz wypełnić to pole")]
        [Range(1, int.MaxValue, ErrorMessage = "Podaj wartość większą od zera!")]
        [Display(Name = "Możliwości produkcyjne [hl]")]
        public int ProductionCapacity { get; set; }
    }
}

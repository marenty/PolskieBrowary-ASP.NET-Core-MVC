using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PolskieBrowary.Models
{
    public class Beer
    {
        public int BeerID { get; set; }
        [Required(ErrorMessage = "Musisz wypełnić to pole")]
        [Display(Name = "Nazwa piwa")]
        public string BreveryName { get; set; }

        [Required(ErrorMessage = "Podaj zawartość alkoholu.")]
        [RegularExpression(@"^[0-9]{1,2}(\,[0-9]{1,2})$", ErrorMessage = "Podałeś błędną zawartość alkoholu")]
        [Display(Name = "Zawartość alkoholu[%]")]
        public decimal AlcoholContent { get; set; }

        [Required(ErrorMessage = "Podaj zawartość ekstraktu.")]
        [RegularExpression(@"^([0-9]{1,2})(\,[0-9]{1,2})$", ErrorMessage = "Podałeś błędną zawartość ekstraktu")]
        [Display(Name = "Zawartość ekstraktu[%]")]
        public decimal ExtractContent { get; set; }

        [Range(0, 9999.99)]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[0-9]*(\,[0-9]{1,2})$", ErrorMessage = "Podałeś błędną cenę")]
        [Required(ErrorMessage = "Podaj cenę.")]
        [Display(Name = "Cena[zł]")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Musisz podać browar pochodzenia!")]
        [Display(Name = "Browar pochodzenia")]
        public int BreveryID { get; set; }

        [Required(ErrorMessage = "Musisz podać gatunek piwa!")]
        [Display(Name = "Gatunek")]
        public int BeerGenreID { get; set; }

        public Brevery Brevery { get; set; }
        public BeerGenre BeerGenre { get; set; }
    }
}

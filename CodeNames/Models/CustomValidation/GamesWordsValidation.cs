using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Models
{
    public class GamesWordsValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object GamesWordsObj, ValidationContext validationContext)
        {
            IEnumerable<Gameswords> gamesWords = GamesWordsObj as IEnumerable<Gameswords>;

            if (gamesWords == null)
            {
                return new ValidationResult("Error occured");
            }


            return ValidationResult.Success;
        }
    }
}

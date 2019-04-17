using CodeNames.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Models
{
    public class GamesMetadata : IValidatableObject
    {
        public GamesMetadata()
        {
            Gameswords = new HashSet<Gameswords>();
        }

        [Display(Name = "Start team")]
        [Required(ErrorMessage = "Start team required.")]
        [Range(1, 2)]
        public short StartTeamId { get; set; }

        [Required(ErrorMessage = "Words required.")]
        [GamesWordsValidation]
        public ICollection<Gameswords> Gameswords { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<int> wordsId = new List<int>();
            
            if (Gameswords == null || Gameswords.Count() != 25)
            {
                yield return new ValidationResult("The game needs 25 differents words.", new[] { "Gameswords" });
            }

            foreach (var GameWord in Gameswords)
            {
                if (wordsId.Contains(GameWord.WordId))
                {
                    yield return new ValidationResult("The game needs 25 differents words.", new[] { "Gameswords" });
                }

                wordsId.Add(GameWord.WordId);
            }
        }
    }
}

using lab_1_Nyesteban.Exceptions;
using lab_1_Nyesteban.Models;

namespace lab_1_Nyesteban.Validators
{
    public class GameValidator
    {
        public static void ValidateGame(Game game)
        {
            if (game.GameRating < 0 || game.GameRating > 5)
            {
                throw new ValidationException("The game's rating must be between 0 and 5!");
            }
            if (game.GameSize <= 0)
            {
                throw new ValidationException("The game's size must be greater than 0!");
            }
            if (game.GameLength <= 0)
            {
                throw new ValidationException("The game's length must be greater than 0!");
            }
        }
    }
}

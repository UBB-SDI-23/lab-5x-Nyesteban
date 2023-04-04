using lab_1_Nyesteban.Exceptions;
using lab_1_Nyesteban.Models;

namespace lab_1_Nyesteban.Validators
{
    public class AppValidator
    {
        public static void ValidateApp(App app)
        {
            if (app.AppPrice < 0)
            {
                throw new ValidationException("The app's price must be greater or equal than 0!");
            }
            if (app.AppSize <0)
            {
                throw new ValidationException("The app's size must be greater or equal than 0!");
            }
        }
    }
}

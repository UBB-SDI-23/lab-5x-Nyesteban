using lab_1_Nyesteban.Models;
using System.ComponentModel.DataAnnotations;

namespace lab_1_Nyesteban.Validators
{
    public class CompanyValidator
    {
        public static void ValidateCompany(Company company)
        {
            if(company.CompanyRating < 0 || company.CompanyRating > 5)
            {
                throw new ValidationException("The company's rating must be between 0 and 5!");
            }
        }
    }
}

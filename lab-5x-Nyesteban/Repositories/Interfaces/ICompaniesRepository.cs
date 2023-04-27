using lab_1_Nyesteban.DTOs;
using lab_1_Nyesteban.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab_1_Nyesteban.Repositories.Interfaces
{
    public interface ICompaniesRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<IEnumerable<Company>> GetCompaniesAllData();
        public Task<ActionResult<Company>> GetCompany(int id);
        public Task<IEnumerable<Company>> GetCompaniesPaginated(int skip, int take);
        public Task<Company> PutCompany(int id, Company company);
        public Task<Company> PutCompanyByNewGame(int id, Game game);
        public Task<Company> PutCompanyGameId(int id, int gameid);
        public Task<Company> PutCompanyGameIds(int id, List<int> gameids);
        public Task PostCompany(Company company);
        public Task DeleteCompany(int id);
        public  Task<IEnumerable<CompanyGamesDTO>> GetCompaniesOrderedByAverageGameRating();
        public Task<IEnumerable<CompanyAppsDTO>> GetCompaniesOrderedByAppCount();

    }
}

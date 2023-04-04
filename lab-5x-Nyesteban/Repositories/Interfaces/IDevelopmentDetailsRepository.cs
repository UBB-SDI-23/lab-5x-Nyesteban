using lab_1_Nyesteban.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab_1_Nyesteban.Repositories.Interfaces
{
    public interface IDevelopmentDetailsRepository
    {
        public Task<IEnumerable<DevelopmentDetail>> GetDevelopmentDetails();
        public Task<ActionResult<DevelopmentDetail>> GetDevelopmentDetail(int id1, int id2);
        public Task<DevelopmentDetail> PutDevelopmentDetail(int id1, int id2, DevelopmentDetail developmentDetail);
        public Task<ActionResult<DevelopmentDetail>> PostDevelopmentDetail(DevelopmentDetail developmentDetail);
        public Task<IEnumerable<App>> GetAppsByCompanyId(int companyId);
        public Task<IEnumerable<Company>> GetCompaniesByAppId(int appId);
        public Task DeleteDevelopmentDetail(int id1, int id2);
    }
}

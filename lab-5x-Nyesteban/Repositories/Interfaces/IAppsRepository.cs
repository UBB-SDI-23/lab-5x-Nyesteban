using lab_1_Nyesteban.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab_1_Nyesteban.Repositories.Interfaces
{
    public interface IAppsRepository
    {
        public Task AddApp(App app);

        public Task<ActionResult<IEnumerable<App>>> GetAppsWithSizeLargerThan(int appSize);

        public Task<IEnumerable<App>> GetAppsPaginated(int skip, int take);

        public Task RemoveApp(int id);

        public Task<App> UpdateApp(int id, App app);

        public Task<IEnumerable<App>> GetApps();

        public Task<App> GetAppById(int id);
    }
}

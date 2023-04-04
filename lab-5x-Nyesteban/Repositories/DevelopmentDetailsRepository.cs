using lab_1_Nyesteban.DAL;
using lab_1_Nyesteban.Models;
using lab_1_Nyesteban.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab_1_Nyesteban.Repositories
{
    public class DevelopmentDetailsRepository : IDevelopmentDetailsRepository
    {
        private readonly StoreContext _context;

        public DevelopmentDetailsRepository(StoreContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<DevelopmentDetail>> GetDevelopmentDetails()
        {
            if (_context.DevelopmentDetails == null)
            {
                throw new Exception($"Entity set 'StoreContext.DevelopmentDetails'  is null.");
            }
            var result = _context.Set<DevelopmentDetail>().ToList() as IEnumerable<DevelopmentDetail>;
            return Task.FromResult(result);
        }

        public async Task<ActionResult<DevelopmentDetail>> GetDevelopmentDetail(int id1, int id2)
        {
            if (_context.DevelopmentDetails == null)
            {
                throw new Exception($"Entity set 'StoreContext.DevelopmentDetails'  is null.");
            }
            var developmentDetail = await _context.DevelopmentDetails.FindAsync(id1, id2);

            if (developmentDetail == null)
            {
                throw new Exception($"DevelopmentDetail with Ids {id1}, {id2} does not exist!");
            }

            return developmentDetail;
        }

        public async Task<DevelopmentDetail> PutDevelopmentDetail(int id1, int id2, DevelopmentDetail developmentDetail)
        {
            if (id1 != developmentDetail.CompanyId || id2 != developmentDetail.AppId)
            {
                throw new Exception("The ids are different!");
            }

            _context.Entry(developmentDetail).State = EntityState.Modified;


            await _context.SaveChangesAsync();

            return developmentDetail;
        }

        public async Task<ActionResult<DevelopmentDetail>> PostDevelopmentDetail(DevelopmentDetail developmentDetail)
        {
            if (_context.DevelopmentDetails == null)
            {
                throw new Exception($"Entity set 'StoreContext.DevelopmentDetails'  is null.");
            }
            _context.DevelopmentDetails.Add(developmentDetail);
            await _context.SaveChangesAsync();
            return developmentDetail;
            /*
            try
            {
               await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateException)
            {
                if (DevelopmentDetailExists(developmentDetail.CompanyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }*/

            //return CreatedAtAction("GetDevelopmentDetail", new { id = developmentDetail.CompanyId }, developmentDetail);
        }

        public Task<IEnumerable<App>> GetAppsByCompanyId(int companyId)
        {
            var result = (
                from DD in _context.DevelopmentDetails
                where DD.CompanyId == companyId
                join A in _context.Apps on DD.AppId equals A.ID
                select A
            ) as IEnumerable<App>;

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Company>> GetCompaniesByAppId(int appId)
        {
            var result = (
                from DD in _context.DevelopmentDetails
                where DD.AppId == appId
                join C in _context.Companies on DD.CompanyId equals C.ID
                select C
            ) as IEnumerable<Company>;
            return Task.FromResult(result);
        }

        public async Task DeleteDevelopmentDetail(int id1, int id2)
        {
            if (_context.DevelopmentDetails == null)
            {
                throw new Exception($"Entity set 'StoreContext.DevelopmentDetails'  is null.");
            }
            var developmentDetail = await _context.DevelopmentDetails.FindAsync(id1, id2);
            if (developmentDetail == null)
            {
                throw new Exception($"DevelopmentDetail with Ids {id1}, {id2} does not exist!");
            }

            _context.DevelopmentDetails.Remove(developmentDetail);
            await _context.SaveChangesAsync();
        }

        private bool DevelopmentDetailExists(int id)
        {
            return (_context.DevelopmentDetails?.Any(e => e.CompanyId == id)).GetValueOrDefault();
        }
    }
}

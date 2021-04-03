using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Broker.System.Covers.Data;
using Broker.System.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Broker.System.Covers.Services
{
    public class CoverService : ICoverService
    {
        
        private readonly BrokerCoversDbContext _brokerDbContext;
        
        public CoverService(BrokerCoversDbContext brokerDbContext)
        {
            _brokerDbContext = brokerDbContext;
        }
        
        public async Task<List<Cover>> GetCoversAsync(string brokerId)
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(
                _brokerDbContext.Covers.Where(l => l.BrokerId.Equals(brokerId)));
        }
        
        public async Task<Cover> GetByIdAsync(int coverId)
        {
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(
                _brokerDbContext.Covers.Where(l => l.CoverId.Equals(coverId)));
        }
        
        public async Task<bool> UpdateAsync(Cover coverToUpdate)
        {
            _brokerDbContext.Covers.Update(coverToUpdate);
            var update = await _brokerDbContext.SaveChangesAsync();
        
            return update > 0;
        }
        
        public async Task<bool> DeleteAsync(int coverId)
        {
            var coverFromList = await GetByIdAsync(coverId);
        
            if (coverFromList != null)
            {
                _brokerDbContext.Covers.Remove(coverFromList);
                var deleted = await _brokerDbContext.SaveChangesAsync();
                return deleted > 0;
            }
        
            return false;
        }
        
        public async Task<EntityEntry<Cover>> CreateAsync(Cover cover)
        {
            bool coverAlreadyExists = false;
            var covers = await GetCoversAsync(cover.BrokerId);
            foreach (var c in covers)
            {
                if (c.Type.Equals(cover.Type))
                {
                    coverAlreadyExists = true;
                    break;
                } 
            }

            if (!coverAlreadyExists)
            {
                var res = await _brokerDbContext.Covers.AddAsync(cover);
                var created = await _brokerDbContext.SaveChangesAsync();
                return res;
            }

            return null;
        }

        public async Task<bool> UserOwnsCover(int coverId, string userId)
        {
            var limit = await _brokerDbContext.Covers.AsNoTracking().SingleOrDefaultAsync(x => x.CoverId == coverId);
        
            if (limit == null) return false;
        
            if (limit.BrokerId == userId) return true;
        
            return false;
        }
    }
}
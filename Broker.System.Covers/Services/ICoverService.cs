using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Broker.System.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Broker.System.Covers.Services
{
    public interface ICoverService
    {
        Task<List<Cover>> GetCoversAsync(string brokerId);
        Task<Cover> GetByIdAsync(int coverId);
        Task<bool> UpdateAsync(Cover coverToUpdate);
        Task<bool> DeleteAsync(int coverId);
        Task<EntityEntry<Cover>> CreateAsync(Cover cover);
        Task<bool> UserOwnsCover(int coverId, string userId);
    }
}
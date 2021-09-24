using System;
using System.Threading.Tasks;
using PickEmLeagueModels.Models;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IMiscAdminService
    {
        Task<MiscAdmin> GetMiscAdminAsync();
        Task<bool> UpdateMiscAdminAsync(MiscAdmin miscAdmin);
    }
}

using System;
using System.IO;
using System.Threading.Tasks;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IAwsS3Service
    {
        Task<byte[]> GetUserImageAsync(long userId);
    }
}

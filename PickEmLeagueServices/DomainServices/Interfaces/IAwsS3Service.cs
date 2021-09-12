using System;
using System.IO;
using System.Threading.Tasks;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IAwsS3Service
    {
        Task<byte[]> GetUserImageAsync(long userId);
        Task<byte[]> GetDefaultUserImageAsync();

        Task SetUserImage(long userId, Stream file, string key);
    }
}

using System;
using System.IO;
using System.Threading.Tasks;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IAwsS3Service
    {
        Task<byte[]> GetUserImageAsync(long userId, string access, string secret);
        Task<byte[]> GetDefaultUserImageAsync(string access, string secret);

        Task SetUserImage(long userId, Stream file, string key, string access, string secret);
    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.DomainServices.Implementations
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly IUserRepository _userRepository;

        public AwsS3Service(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task GetUserImageAsync(MemoryStream ms)
        {
            var s3Client = new AmazonS3Client();

            var request = new GetObjectRequest()
            {
                BucketName = "2021pickemleagueresources",
                Key = "Me.jpeg",
            };

            using (GetObjectResponse response = await s3Client.GetObjectAsync(request))
            {
                using (Stream stream = response.ResponseStream)
                {
                    stream.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                }
            }
        }

        public async Task<byte[]> GetUserImageAsync(long userId)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var s3Client = new AmazonS3Client();
                var user = await _userRepository.GetById(userId);

                var request = new GetObjectRequest()
                {
                    BucketName = "2021pickemleagueresources",
                    Key = string.IsNullOrEmpty(user.ProfilePictureKey) ?
                            "DefaultLogo.svg" : user.ProfilePictureKey,
                };

                using (GetObjectResponse response = await s3Client.GetObjectAsync(request))
                {
                    using (Stream stream = response.ResponseStream)
                    {
                        stream.CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                }

                return ms.ToArray();
            }
        }
    }
}

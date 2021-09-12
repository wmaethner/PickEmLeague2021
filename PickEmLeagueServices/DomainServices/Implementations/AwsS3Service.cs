using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.DomainServices.Implementations
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        const string BUCKET_NAME = "2021pickemleagueresources";
        const string DEFAULT_LOGO = "DefaultLogo.jpg";

        public AwsS3Service(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<byte[]> GetDefaultUserImageAsync(string access, string secret)
        {
            return await GetImage(new GetObjectRequest()
            {
                BucketName = BUCKET_NAME,
                Key = DEFAULT_LOGO,
            }, access, secret);
        }

        public async Task<byte[]> GetUserImageAsync(long userId, string access, string secret)
        {
            var user = await _userRepository.GetById(userId);
            if (string.IsNullOrEmpty(user.ProfilePictureKey))
            {
                return new byte[0];
            }

            return await GetImage(new GetObjectRequest()
            {
                BucketName = BUCKET_NAME,
                Key = string.IsNullOrEmpty(user.ProfilePictureKey) ?
                            DEFAULT_LOGO : user.ProfilePictureKey,
            }, access, secret);
        }

        public async Task SetUserImage(long userId, Stream file, string key, string access, string secret)
        {
            var user = await _userRepository.GetById(userId);
            var putRequest = new PutObjectRequest()
            {
                BucketName = BUCKET_NAME,
                InputStream = file,
                Key = key,
            };

            var s3Client = new AmazonS3Client(access, secret);

            var response = await s3Client.PutObjectAsync(putRequest);
            user.ProfilePictureKey = key;
            await _userRepository.SaveAsync();
        }

        private async Task<byte[]> GetImage(GetObjectRequest request, string access, string secret)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var s3Client = new AmazonS3Client(access, secret);

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

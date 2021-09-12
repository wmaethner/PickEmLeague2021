using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueServices.DomainServices.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IAwsS3Service _awsS3Service;

        public ImageController(IAwsS3Service awsS3Service)
        {
            _awsS3Service = awsS3Service;
        }

        [HttpGet("getUsersImage")]
        public async Task<byte[]> GetUsersImage(long userId, string access, string secret)
        {
            return await _awsS3Service.GetUserImageAsync(userId, access, secret);
        }

        [HttpPost("setUsersImage")]
        public async Task SetUserImage(long userId, IFormFile formFile, string access, string secret)
        {
            await _awsS3Service.SetUserImage(userId, formFile.OpenReadStream(), formFile.FileName, access, secret);
        }
    }
}

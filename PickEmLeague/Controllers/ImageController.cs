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

        [HttpGet]
        public async Task<byte[]> GetUsersImage(long userId = 0)
        {
            return await _awsS3Service.GetUserImageAsync(userId);
        }

        [HttpPost]
        public async Task SetUserImage(long userId, IFormFile formFile)
        {
            await _awsS3Service.SetUserImage(userId, formFile.OpenReadStream(), formFile.FileName);
        }
    }
}

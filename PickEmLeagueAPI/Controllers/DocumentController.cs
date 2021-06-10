using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IS3Service _s3Service;

        public DocumentController(IS3Service s3Service)
        {
            _s3Service = s3Service;
        }

        [HttpPost]
        public async Task<Document> AddDocument([FromForm] DocumentUploadRequest request)
        {
            if (request.File == null)
            {
                throw new ArgumentNullException();
            }

            using (var ms = new MemoryStream())
            {
                request.File.CopyTo(ms);
                return await _s3Service.CreateDocumentAsync(ms, request.File.FileName);
            }
        }
    }

    public class DocumentUploadRequest
    {
        public IFormFile? File { get; set; }
    }
}

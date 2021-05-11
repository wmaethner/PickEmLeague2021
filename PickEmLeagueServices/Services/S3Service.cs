using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using PickEmLeague.Global.Shared;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueServices.Services
{
    [DIServiceScope(typeof(IS3Service), typeof(S3Service))]
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucketName;


        public S3Service(IConfiguration config, IAmazonS3 s3)
        {
            _s3 = s3;
            _bucketName = config.GetValue<string>("Services:DocumentService:BucketName");
        }

        public async Task<Document> CreateDocumentAsync(Stream data, string name)
        {
            Document document = new Document()
            {
                Name = name
            };

            var s3Response = await _s3.PutObjectAsync(new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = name,
                InputStream = data
            });

            if (s3Response.HttpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(
                    $"AWS S3 document upload of a document with target uri failed with status code {s3Response.HttpStatusCode}");
                //throw new DocumentUploadException("AWS S3 did not report success uploading the given Document.");
            }

            return document;
        }

        public Task<Stream> GetDocumentAsync(Document document)
        {
            throw new NotImplementedException();
        }
    }
}

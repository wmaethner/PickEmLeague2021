using System;
using System.IO;
using System.Threading.Tasks;
using PickEmLeagueDomain.Models;

namespace PickEmLeagueServices.Interfaces
{
    public interface IS3Service
    {
        Task<Document> CreateDocumentAsync(Stream data, string name);
        Task<Stream> GetDocumentAsync(Document document);
        //Task
    }
}

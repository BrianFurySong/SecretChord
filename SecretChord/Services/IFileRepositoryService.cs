using System.Collections.Generic;
using SecretChord.Models.Domain;
using SecretChord.Models.Requests;

namespace SecretChord.Services
{
    public interface IFileRepositoryService
    {
        int Delete(int id);
        int Insert(FileRepositoryAddRequest model);
        IEnumerable<FileRepository> SelectAll();
        FileRepository SelectById(int id);
        void Update(FileRepositoryUpdateRequest model);
    }
}
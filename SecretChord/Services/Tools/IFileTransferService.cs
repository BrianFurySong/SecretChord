using System.IO;

namespace SecretChord.Services.Tools
{
    public interface IFileTransferService
    {
        string FileUpload(Stream stream, string fileName, string fileRepositoryIdRequest = "false");
    }
}
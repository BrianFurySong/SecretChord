using SecretChord.Services.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SecretChord.Controllers.Api
{
    [RoutePrefix("api/FileUpload")]
    public class FileTransferController : ApiController
    {
        IFileTransferService _fileTransferService;
        public FileTransferController(IFileTransferService fileTransferService)
        {
            _fileTransferService = fileTransferService;
        }

        [Route(), HttpPost]
        public HttpResponseMessage Post()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                string statusMessage = "";

                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files["file"];

                    string fileRepositoryIdRequest = httpRequest.Form["fileRepositoryIdRequest"];
                    if (fileRepositoryIdRequest == "undefined")
                        fileRepositoryIdRequest = "false";

                    statusMessage = _fileTransferService.FileUpload(file.InputStream, file.FileName, fileRepositoryIdRequest);
                }

                //ItemResponse<string> response = new ItemResponse<string>();
                string response = statusMessage;

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}

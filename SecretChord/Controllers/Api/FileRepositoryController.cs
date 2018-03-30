using SecretChord.Models.Domain;
using SecretChord.Models.Requests;
using SecretChord.Models.Responses;
using SecretChord.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SecretChord.Controllers.Api
{
    [RoutePrefix("api/FileRepositories")]
    public class FileRepositoryController : ApiController
    {
        IFileRepositoryService _fileRepositoryService;

        public FileRepositoryController(IFileRepositoryService fileRepositoryService)
        {
            _fileRepositoryService = fileRepositoryService;
        }

        [Route(), HttpGet]
        public HttpResponseMessage SelectAll()
        {
            try
            {
                IEnumerable<FileRepository> fileRepository = _fileRepositoryService.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, fileRepository);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage SelectById(int id)
        {
            try
            {
                FileRepository fileRepository = _fileRepositoryService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, fileRepository);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }

        [Route(), HttpPost]
        public HttpResponseMessage Post(FileRepositoryAddRequest model)
        {
            try
            {
                int id = _fileRepositoryService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(FileRepositoryUpdateRequest model)
        {
            try
            {
                _fileRepositoryService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _fileRepositoryService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }

}

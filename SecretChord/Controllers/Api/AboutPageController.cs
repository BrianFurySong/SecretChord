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
    [RoutePrefix("api/AboutPages")]
    public class AboutPageController : ApiController
    {
        IAboutPageService _aboutPageService;

        public AboutPageController(IAboutPageService aboutPageService)
        {
            _aboutPageService = aboutPageService;
        }

        [Route(), HttpGet]
        public HttpResponseMessage SelectAll()
        {
            try
            {
                IEnumerable<AboutPage> aboutPage = _aboutPageService.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, aboutPage);
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
                AboutPage aboutPage = _aboutPageService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, aboutPage);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }

        [Route("Page/{page:int}"), HttpGet]
        public HttpResponseMessage SelectAllByPage(int page)
        {
            try
            {
                IEnumerable<AboutPage> aboutPage = _aboutPageService.SelectAllByPage(page);
                return Request.CreateResponse(HttpStatusCode.OK, aboutPage);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        [Route(), HttpPost]
        public HttpResponseMessage Post(AboutPageAddRequest model)
        {
            try
            {
                int id = _aboutPageService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(AboutPageUpdateRequest model)
        {
            try
            {
                _aboutPageService.Update(model);
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
                _aboutPageService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}

using SecretChord.Models.Requests;
using SecretChord.Models.Domain;
using SecretChord.Models;
using SecretChord.Models.Responses;
using SecretChord.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecretChord.Filters;

namespace SecretChord.Controllers.Api
{
    //[AuthorizationRequired]
    [RoutePrefix("api/FaqItems")]
    public class FaqItemController : ApiController
    {
        IFaqItemService _faqItemService;

        public FaqItemController(IFaqItemService faqItemService)
        {
            _faqItemService = faqItemService;
        }

        [Route(), HttpGet]
        public HttpResponseMessage SelectAll()
        {
            try
            {
                IEnumerable<FaqItem> faqItem = _faqItemService.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, faqItem);
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
                FaqItem faqItem = _faqItemService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, faqItem);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }

        [Route(), HttpPost]
        public HttpResponseMessage Post(FaqItemAddRequest model)
        {
            try
            {
                int id = _faqItemService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(FaqItemUpdateRequest model)
        {
            try
            {
                _faqItemService.Update(model);
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
                _faqItemService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}

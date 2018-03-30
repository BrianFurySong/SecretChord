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
    [RoutePrefix("api/AppConfigs")]
    public class AppConfigController : ApiController
    {
        IAppConfigService _appConfigService;

        public AppConfigController(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }

        [Route("ConfigKey/{configKey}"), HttpGet]
        public HttpResponseMessage SelectByKey(string configKey)
        {
            try
            {
                IEnumerable<string> appConfig = _appConfigService.SelectByKey(configKey);
                return Request.CreateResponse(HttpStatusCode.OK, appConfig);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        [Route(), HttpGet]
        public HttpResponseMessage SelectAll()
        {
            try
            {
                IEnumerable<AppConfig> appConfig = _appConfigService.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, appConfig);
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
                AppConfig appConfig = _appConfigService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, appConfig);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }

        [Route(), HttpPost]
        public HttpResponseMessage Post(AppConfigAddRequest model)
        {
            try
            {
                int id = _appConfigService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(AppConfigUpdateRequest model)
        {
            try
            {
                _appConfigService.Update(model);
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
                _appConfigService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}

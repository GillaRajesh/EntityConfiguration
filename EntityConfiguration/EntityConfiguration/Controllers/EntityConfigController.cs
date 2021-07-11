using EntityConfiguration.Models;
using EntityConfigurationBLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace EntityConfiguration.Controllers
{
    public class EntityConfigController : ApiController
    {
        protected IEntityBLL objentityBLL;

        public EntityConfigController()
        {
            objentityBLL = new EntityBLL();
        }

        public EntityConfigController(IEntityBLL entityBLL)
        {
            objentityBLL = entityBLL;
        }
       

        // GET api/values
        public HttpResponseMessage GetFields(string entityName,bool isDefault)
        {
            try
            {
                HttpResponseMessage _responseMessage = new HttpResponseMessage();
                var formatter = new JsonMediaTypeFormatter();               
                string entityInfo = objentityBLL.GetFields(entityName, isDefault);
                JObject jObject = new JObject();
                if (string.IsNullOrEmpty(entityInfo))
                {
                    return BuildErrorResponse("No details found",HttpStatusCode.NotFound);
                }
                jObject = JObject.Parse(entityInfo.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }));
                _responseMessage.StatusCode = HttpStatusCode.OK;
                _responseMessage.Content = new ObjectContent<JObject>(jObject, formatter, "application/json");

                return _responseMessage;
            }
            
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetDefaultFields(string entityName)
        {
            try
            {               
                if (!string.IsNullOrEmpty(entityName))
                {
                    return GetFields(entityName, true);
                }
                else
                {
                    return BuildErrorResponse("EntityName should not be empty", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
           
        }


        [HttpGet]
        public HttpResponseMessage GetCustomFields(string entityName)
        {
            try
            {
               
                if (!string.IsNullOrEmpty(entityName))
                {
                    return GetFields(entityName, false);
                }
                else
                {
                    return BuildErrorResponse("EntityName should not be empty",HttpStatusCode.BadRequest);
                    
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        
        [HttpPost]
        public HttpResponseMessage SaveFields(JObject jObject)
        {
            try
            {
                HttpResponseMessage _responseMessage = new HttpResponseMessage();
                var formatter = new JsonMediaTypeFormatter();
                JObject jObjectInfo = new JObject();
                
                string entityInfo = objentityBLL.SaveFields(Convert.ToString(jObject));
                jObjectInfo = JObject.Parse(entityInfo.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }));

                if (jObjectInfo["Error"] != null)
                {
                    return BuildErrorResponse(jObjectInfo["Error"][0]["Message"].ToString(), HttpStatusCode.BadRequest);
                }

                _responseMessage.StatusCode = HttpStatusCode.OK;
                _responseMessage.Content = new ObjectContent<JObject>(jObject, formatter, "application/json");

                return _responseMessage;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        
        private HttpResponseMessage BuildErrorResponse(string message,HttpStatusCode httpStatusCode)
        {
            HttpResponseMessage _responseMessage = new HttpResponseMessage();
            var formatter = new JsonMediaTypeFormatter();            

            JObject jObject = new JObject
                    {
                        { "Error", message }
                    };
            _responseMessage.StatusCode = httpStatusCode;
            _responseMessage.Content = new ObjectContent<JObject>(jObject, formatter, "application/json");
            return _responseMessage;
        }
    }
}

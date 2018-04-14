using FeedbackCba.Core.ViewModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Cors;
using FeedbackCba.Core;
using FeedbackCba.Core.Dtos;

namespace FeedbackCba.Controllers
{
    public class FeedbacksController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbacksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // POST api/<controller>
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/customers/{customerId}/feedbacks")]
        public IHttpActionResult Post(string customerId, FeedbackDto feedBack)
        {
            var customer = _unitOfWork.Customers.GetCustomer(customerId);
            if (customer.IsValid())
            {
                if (_unitOfWork.Feedbacks.Create(customerId, feedBack))
                {
                    _unitOfWork.Complete();
                }
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("");
            response.WriteCorsHeaders(new CorsResult { AllowedOrigin = "*", AllowedHeaders = { "Content-Type" } });
            
            return ResponseMessage(response);
        }

        public IHttpActionResult Options(string customerId, FeedbackDto feedBack)
        {
            var hostName = System.Web.HttpContext.Current.Request.Headers["Origin"].ToLower();
            var validDomains = "http://domain1.com;http://domain2.com"; //load from Db using customerId
            var isValidHost = validDomains.Split(';').Any(p => p.ToLower().Equals(hostName));

            var response = Request.CreateResponse();
            if (isValidHost)
            {
                response.WriteCorsHeaders(new CorsResult { AllowedOrigin = hostName, AllowedHeaders = { "Content-Type" } });
            }

            return ResponseMessage(response);
            
            //return Ok();
            //<add name="Access-Control-Allow-Origin" value="*" />
            //    <add name="Access-Control-Allow-Headers" value="Content-Type" />

        }
    }
}
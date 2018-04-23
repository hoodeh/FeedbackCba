using FeedbackCba.Core;
using FeedbackCba.Core.Dtos;
using FeedbackCba.Persistence;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FeedbackCba.Controllers.Api
{
    public class FeedbacksController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFeedbackRecorder _feedbackRecorder;
        private readonly ICustomerDomainValidator _domainValidator;


        public FeedbacksController(IUnitOfWork unitOfWork, IFeedbackRecorder feedbackRecorder, ICustomerDomainValidator domainValidator)
        {
            _unitOfWork = unitOfWork;
            _feedbackRecorder = feedbackRecorder;
            _domainValidator = domainValidator;
        }

        // POST api/<controller>
        [Route("api/customers/{customerId}/feedbacks")]
        public IHttpActionResult Post(string customerId, FeedbackDto feedBack)
        {
            if (!_feedbackRecorder.CanProvideFeedback(customerId, feedBack.PageUrl))
            {
                return BadRequest();
            }

            var customer = _unitOfWork.Customers.GetCustomer(customerId);
            if (customer == null || !customer.IsValid())
            {
                return NotFound();
            }

            if (_unitOfWork.Feedbacks.Create(customerId, feedBack))
            {
                _unitOfWork.Complete();
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);

            _feedbackRecorder.RecordFeedback(customerId, feedBack.PageUrl);

            response.Content = new StringContent("{\"type\":\"success\"}", Encoding.UTF8, "application/json");
            
            WriteCorsHeader(customerId, response);
            
            return ResponseMessage(response);
        }

        public IHttpActionResult Options(string customerId, FeedbackDto feedBack)
        {
            
            var response = Request.CreateResponse();
            WriteCorsHeader(customerId, response);
            return ResponseMessage(response);
        }

        private void WriteCorsHeader(string customerId, HttpResponseMessage response)
        {
            string hostName;
            if (_domainValidator.IsValidHostName(customerId,out hostName))
            {
                response.WriteCorsHeaders(new CorsResult
                {
                    AllowedOrigin = hostName,
                    AllowedHeaders = {"Content-Type"},
                    SupportsCredentials = true
                });
            }
        }
    }

}
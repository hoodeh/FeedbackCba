using FeedbackCba.Controllers.Api;
using FeedbackCba.Core;
using FeedbackCba.Core.Dtos;
using FeedbackCba.Core.Models;
using FeedbackCba.Core.Repositories;
using FeedbackCba.Persistence;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace FeedbackCbaTest.Controllers.Api
{
    [TestClass]
    public class FeedbackControllerTests
    {
        private FeedbacksController _feedbacksController;
        private Mock<IFeedbackRepository> _mockFeedbackRepository;
        private Mock<ICustomerReporitory> _mockCustomerRepository;
        private Mock<IFeedbackRecorder> _mockFeedbackRecorder;
        //private Mock<ICustomerDomainValidator> _mockDomainValidator;
        private string _pageUrl = "https://cba.com.au";
        private string _customerId = "dbb2db69-917f-4989-90a6-48ec7562ee39";

        [TestInitialize]
        public void TestInitialize()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockFeedbackRepository = new Mock<IFeedbackRepository>();
            _mockCustomerRepository = new Mock<ICustomerReporitory>();
            _mockFeedbackRecorder = new Mock<IFeedbackRecorder>();
            var mockDomainValidator = new Mock<ICustomerDomainValidator>();
            mockUnitOfWork.SetupGet(u => u.Feedbacks).Returns(_mockFeedbackRepository.Object);
            mockUnitOfWork.SetupGet(u => u.Customers).Returns(_mockCustomerRepository.Object);

            _feedbacksController = new FeedbacksController(mockUnitOfWork.Object, _mockFeedbackRecorder.Object, mockDomainValidator.Object);
        }

        [TestMethod]
        public void CreateFeedback_WithRecentFeedback_ShouldReturnBadRequest()
        {
            _mockFeedbackRecorder.Setup(r => r.CanProvideFeedback(_customerId, _pageUrl)).Returns(false);

            var result = _feedbacksController.Post(_customerId, new FeedbackDto {PageUrl = _pageUrl});

            result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public void CreateFeedback_ForNotRegisteredCustomer_ShouldReturnBadRequest()
        {
            _mockFeedbackRecorder.Setup(r => r.CanProvideFeedback(_customerId, _pageUrl)).Returns(true);
            Customer customer = null;
            _mockCustomerRepository.Setup(c => c.GetCustomer(_customerId)).Returns(customer);

            var result = _feedbacksController.Post(_customerId, new FeedbackDto { PageUrl = _pageUrl });

            result.Should().BeOfType<NotFoundResult>();
        }

        //[TestMethod]
        //public void CreateFeedback_ValidRequest_ShouldReturnOk()
        //{
        //    _mockFeedbackRecorder.Setup(r => r.CanProvideFeedback(_customerId, _pageUrl)).Returns(true);
        //    var customer = new Customer{IsEnabled = true, ExpireDate = DateTime.Now.AddDays(1)};
        //    _mockCustomerRepository.Setup(c => c.GetCustomer(_customerId)).Returns(customer);

        //    var result = _feedbacksController.Post(_customerId, new FeedbackDto { PageUrl = _pageUrl });

        //    result.Should().BeOfType<OkResult>();
        //}

    }
}

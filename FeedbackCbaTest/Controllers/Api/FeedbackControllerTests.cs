using FeedbackCba.Controllers;
using FeedbackCba.Controllers.Api;
using FeedbackCba.Core;
using FeedbackCba.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FeedbackCbaTest.Controllers.Api
{
    [TestClass]
    public class FeedbackControllerTests
    {
        //private FeedbacksController _feedbacksController;
        //private Mock<IFeedbackRepository> _mockFeedbackRepository;
        //private string _pageUrl = "https://cba.com.au";
        //private string _customerId = "dbb2db69-917f-4989-90a6-48ec7562ee39";

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    _mockFeedbackRepository = new Mock<IFeedbackRepository>();
        //    mockUnitOfWork.SetupGet(u => u.Feedbacks).Returns(_mockFeedbackRepository.Object);

        //    _feedbacksController = new FeedbacksController(mockUnitOfWork.Object);
        //}

        [TestMethod]
        public void CreateFeedback_ValidRequest_ShouldReturnOK()
        {

        }
    }
}

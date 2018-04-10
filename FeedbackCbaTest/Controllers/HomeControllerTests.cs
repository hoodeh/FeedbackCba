using FeedbackCba.Controllers;
using FeedbackCba.Core;
using FeedbackCba.Core.Models;
using FeedbackCba.Core.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Mvc;

namespace FeedbackCbaTest.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _homeController;
        private Mock<ICustomerReporitory> _mockCustomerRepository;
        private string _pageUrl = "test.com";
        private string _customerId = "dbb2db69-917f-4989-90a6-48ec7562ee39";

        public HomeControllerTests()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCustomerRepository = new Mock<ICustomerReporitory>();
            mockUnitOfWork.SetupGet(u => u.Customers).Returns(_mockCustomerRepository.Object);

            _homeController = new HomeController(mockUnitOfWork.Object);
        }

        [TestMethod]
        public void Feedback_NoGivenCustomerId_ShouldReturnNotfound()
        {
            var result = _homeController.Feedback("", _pageUrl);

            result.Should().BeOfType<HttpNotFoundResult>();
        }

        [TestMethod]
        public void Feedback_NoCustomerWithGivenId_ShouldReturnNotfound()
        {
            var result = _homeController.Feedback(_customerId, _pageUrl);

            result.Should().BeOfType<HttpNotFoundResult>();
        }

        [TestMethod]
        public void Feedback_CustomerWithDisabledAccount_ShouldReturnExpiredPage()
        {
            var customer = new Customer {Id = new Guid(_customerId), IsEnabled = false};

            _mockCustomerRepository.Setup(c => c.GetCustomer(_customerId)).Returns(customer);

            var result = _homeController.Feedback(_customerId, _pageUrl);

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public void Feedback_CustomerWithExpiredAccount_ShouldReturnExpiredPage()
        {
            var customer = new Customer
            {
                Id = new Guid(_customerId),
                IsEnabled = true,
                ExpireDate = DateTime.Now.AddDays(-1)
            };

            _mockCustomerRepository.Setup(c => c.GetCustomer(_customerId)).Returns(customer);

            var result = _homeController.Feedback(_customerId, _pageUrl) as ViewResult;

            result.ViewName.Should().BeSameAs("ExpiredPackage");
        }
        
    }
}

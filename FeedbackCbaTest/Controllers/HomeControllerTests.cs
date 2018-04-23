using FeedbackCba.Controllers;
using FeedbackCba.Core;
using FeedbackCba.Core.Models;
using FeedbackCba.Core.Repositories;
using FeedbackCba.Persistence;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Web.Mvc;

namespace FeedbackCbaTest.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _homeController;
        private Mock<ICustomerReporitory> _mockCustomerRepository;
        private Mock<ICustomerDomainValidator> _mockDomainValidator;

        private string _pageUrl = "https://cba.com.au";
        private string _customerId = "dbb2db69-917f-4989-90a6-48ec7562ee39";

        [TestInitialize]
        public void TestInitialize()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCustomerRepository = new Mock<ICustomerReporitory>();
            mockUnitOfWork.SetupGet(u => u.Customers).Returns(_mockCustomerRepository.Object);

            _mockDomainValidator = new Mock<ICustomerDomainValidator>();
            var mockFeedbackRecorder = new Mock<IFeedbackRecorder>();
            _homeController = new HomeController(mockUnitOfWork.Object, mockFeedbackRecorder.Object, _mockDomainValidator.Object);
        }

        [TestMethod]
        public void Feedback_NoGivenCustomerId_ShouldReturnNotfound()
        {
            var result = _homeController.Feedback("", _pageUrl) as HttpStatusCodeResult;

            result.StatusCode.Should().Be(400);
        }

        [TestMethod]
        public void Feedback_NoCustomerWithGivenId_ShouldReturnNotfound()
        {
            var result = _homeController.Feedback(_customerId, _pageUrl) as HttpStatusCodeResult;

            result.StatusCode.Should().Be(403);
        }

        [TestMethod]
        public void Feedback_CustomerWithDisabledAccount_ShouldReturnExpiredPage()
        {
            var customer = new Customer { Id = new Guid(_customerId), IsEnabled = false };
            _mockCustomerRepository.Setup(c => c.GetCustomer(_customerId)).Returns(customer);

            var result = _homeController.Feedback(_customerId, _pageUrl) as ViewResult;

            result.ViewName.Should().Be("ExpiredPackage");
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

            result.ViewName.Should().Be("ExpiredPackage");
        }

        [TestMethod]
        public void Feedback_CustomerWithInvalidDomain_ShouldThrowUnauthorizedException()
        {
            var customer = new Customer
            {
                Id = new Guid(_customerId),
                IsEnabled = true,
                ExpireDate = DateTime.Now.AddDays(1),
                ValidDomains = _pageUrl + "-"
            };

            _mockCustomerRepository.Setup(c => c.GetCustomer(_customerId)).Returns(customer);
            var hostname = string.Empty;
            _mockDomainValidator.Setup(d => d.IsValidHostName(_customerId, out hostname)).Returns(false);

            var result = _homeController.Feedback(_customerId, _pageUrl) as HttpStatusCodeResult;

            result.StatusCode.Should().Be(403);
        }

    }
}

using FeedbackCba.Core.Models;
using FeedbackCba.Persistence;
using FeedbackCba.Persistence.Repositories;
using FeedbackCbaTest.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using System.Linq;

namespace FeedbackCbaTest.Persistence.Repositories
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private CustomerReporitory _repoitory;
        private DbSet<Customer> _mockCustomers;
        private string _customerId = "dbb2db69-917f-4989-90a6-48ec7562ee39";

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCustomers = new TestDbSet<Customer>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Customers).Returns(_mockCustomers);

            _repoitory = new CustomerReporitory(mockContext.Object);
        }

        [TestMethod]
        public void IsValidCustomer_DisableCustomer_ShouldReturnFalse()
        {
            var customer = new Customer { Id = new Guid(_customerId), IsEnabled = false, ExpireDate = DateTime.Now.AddDays(1) };
            _mockCustomers.Add(customer);

            var result = _repoitory.IsValidCustomer(_customerId);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidCustomer_ExpiredCustomer_ShouldReturnFalse()
        {
            var customer = new Customer { Id = new Guid(_customerId), IsEnabled = true, ExpireDate = DateTime.Now.AddDays(-1) };
            _mockCustomers.Add(customer);

            var result = _repoitory.IsValidCustomer(_customerId);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void GetValidDomains_NoDomains_ShouldReturnEmptyArray()
        {
            var customer = new Customer { Id = new Guid(_customerId)};
            _mockCustomers.Add(customer);

            var result = _repoitory.GetValidDomains(_customerId);

            result.Should().Equal(new[] {""});
        }

        [TestMethod]
        public void GetValidDomains_WithTwoDomains_ShouldReturnTwoLengthArray()
        {
            var customer = new Customer { Id = new Guid(_customerId), ValidDomains = "https://cba.com;https://commsec.com" };
            _mockCustomers.Add(customer);

            var result = _repoitory.GetValidDomains(_customerId);

            result.Should().HaveCount(2);
        }
    }
}

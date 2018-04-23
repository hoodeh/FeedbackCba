using FeedbackCba.Core.Dtos;
using FeedbackCba.Core.Models;
using FeedbackCba.Persistence;
using FeedbackCba.Persistence.Repositories;
using FeedbackCbaTest.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace FeedbackCbaTest.Persistence.Repositories
{
    [TestClass]
    public class FeedbackReposirtoryTests
    {
        private FeedbackRepository _repoitory;
        private DbSet<Feedback> _mockFeedbacks;
        private string _customerId = "dbb2db69-917f-4989-90a6-48ec7562ee39";
        private string _pageUrl = "https://cba.com.au";
        private bool _isMainPage = true;
        private string _userId = "1";

        [TestInitialize]
        public void TestInitialize()
        {
            _mockFeedbacks = new TestDbSet<Feedback>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Feedbacks).Returns(_mockFeedbacks);

            _repoitory = new FeedbackRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetFeedback_DifferentPageUrl_ShouldReturnNull()
        {
            var feedback = new Feedback
            {
                Id = 1,
                CustomerId = new Guid(_customerId),
                PageUrl = _pageUrl,
                IsMainPage = _isMainPage,
                SubmitDate = DateTime.Now
            };

            _mockFeedbacks.Add(feedback);

            var result = _repoitory.GetFeedback(_customerId, _pageUrl + "-", _isMainPage);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetFeedback_DifferentPageType_ShouldReturnNull()
        {
            var feedback = new Feedback
            {
                Id = 1,
                CustomerId = new Guid(_customerId),
                PageUrl = _pageUrl,
                IsMainPage = _isMainPage,
                SubmitDate = DateTime.Now
            };

            _mockFeedbacks.Add(feedback);

            var result = _repoitory.GetFeedback(_customerId, _pageUrl, !_isMainPage);

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetFeedback_DifferentUserId_ShouldReturnNull()
        {
            var feedback = new Feedback
            {
                Id = 1,
                CustomerId = new Guid(_customerId),
                PageUrl = _pageUrl,
                IsMainPage = _isMainPage,
                UserId = _userId,
                SubmitDate = DateTime.Now
            };

            _mockFeedbacks.Add(feedback);

            var result = _repoitory.GetFeedback(_customerId, _pageUrl, !_isMainPage, _userId + "1");

            result.Should().BeNull();
        }

        [TestMethod]
        public void GetFeedback_ValidRequest_ShouldReturnFeedback()
        {
            var feedback = new Feedback
            {
                Id = 1,
                CustomerId = new Guid(_customerId),
                PageUrl = _pageUrl,
                IsMainPage = _isMainPage,
                SubmitDate = DateTime.Now
            };

            _mockFeedbacks.Add(feedback);

            var result = _repoitory.GetFeedback(_customerId, _pageUrl, _isMainPage);

            result.Should().Be(feedback);
        }

        [TestMethod]
        public void Create_ValidRequest_ShouldAddFeedback()
        {
            var feedback = new FeedbackDto
            {
                Rate = 8,
                PageUrl = _pageUrl,
                IsMainPage = _isMainPage,
                QuestionId = 1
            };

            var result = _repoitory.Create(_customerId, feedback);

            result.Should().Be(true);
            _mockFeedbacks.Should().HaveCount(1);
        }

        [TestMethod]
        public void Create_NullFeedbackDTO_ReturnFalse()
        {
            FeedbackDto feedbackDto = null;

            var result = _repoitory.Create(_customerId, feedbackDto);

            result.Should().Be(false);
        }

    }
}

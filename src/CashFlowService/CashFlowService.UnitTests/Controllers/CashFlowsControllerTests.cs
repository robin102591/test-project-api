using CashFlowService.Controllers;
using CashFlowService.Model;
using CashFlowService.ProcessorManager;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace CashFlowService.UnitTests.Controllers
{
    [TestClass]
    public class CashFlowsControllerTests
    {
        private Mock<ICashFlowManager> _mockcashFlowManager;
        private CashFlowsController _cashFlowsController;

        [TestInitialize]
        public void Init()
        {
            _mockcashFlowManager = new Mock<ICashFlowManager>();
            _cashFlowsController = new CashFlowsController(_mockcashFlowManager.Object);
        }

        #region Get Tests

        [TestMethod]
        public void Get_HealthCheck_ShouldReturn()
        {
            //Arrange
            //Act
            var response = _cashFlowsController.Get();

            //Assert
            response.Should().BeOfType<OkResult>();
        }

        #endregion

        #region Post Tests

        [TestMethod]
        public void Post_CalculateWithValidRequest_ShouldReturnOkResult()
        {
            //Arrange
            var cashFlowRequest = GetMockValidCashFlowRequest();
            _mockcashFlowManager.Setup(cf => cf.CalculateNetPresentValueAsync(cashFlowRequest))
                                .ReturnsAsync(GetMockValidCashFlowResponse());
            //Act
            var response = _cashFlowsController.Post(cashFlowRequest).Result;

            //Assert
            response.Should().BeOfType<OkObjectResult>();
            response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<CashFlowResponse>();
        }

        [TestMethod]
        public void Post_CalculateWithNullRequest_ShouldReturnBadRequest()
        {
            //Arrange
            _cashFlowsController.ModelState.AddModelError("CashFlowRequest", "The CashFlowRequest field is required.");
            //Act
            var response = _cashFlowsController.Post(null).Result;

            //Assert
            response.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
            response.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should()
                             .BeOfType<ErrorResponse>().Which.Message.Should().Contain("Invalid request");
            response.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should()
                             .BeOfType<ErrorResponse>().Which.Errors[0].Should().Contain("The CashFlowRequest field is required.");
        }

        [TestMethod]
        public void Post_CashFlowManagerThrowsException_ShouldServerError()
        {
            //Arrange
            _mockcashFlowManager.Setup(cf => cf.CalculateNetPresentValueAsync(It.IsAny<CashFlowRequest>()))
                                .ThrowsAsync(new Exception("An error occured."));

            //Act
            var response = _cashFlowsController.Post(It.IsAny<CashFlowRequest>()).Result;

            //Assert
            response.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(500);
            response.Should().BeOfType<ObjectResult>().Which.Value.Should()
                             .BeOfType<ErrorResponse>().Which.Message.Should().Contain("An error occured.");
        }

        #endregion

        #region Private Method

        private CashFlowRequest GetMockValidCashFlowRequest()
        {
            return new CashFlowRequest
            {
                InitialInvestment = 1000,
                DiscountRate = 10,
                CashFlows = new List<CashFlow>()
                {
                    new CashFlow
                    {
                        Period = 1,
                        Amount = 2000
                    },
                    new CashFlow
                    {
                        Period = 2,
                        Amount = 3000
                    }
                }
            };
        }

        private CashFlowResponse GetMockValidCashFlowResponse()
        {
            return new CashFlowResponse
            {
                TotalNetPresentValue = 100,
                NetPresentValues = new List<NetPresentValue>()
                {
                    new NetPresentValue
                    {
                        Period = 1,
                        CashFlowAmount = 100,
                        PresentValue = 100
                    }
                }
            };
        }
        #endregion
    }
}

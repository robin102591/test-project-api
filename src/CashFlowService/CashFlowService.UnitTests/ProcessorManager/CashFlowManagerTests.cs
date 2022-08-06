using CashFlowService.Model;
using CashFlowService.ProcessorManager;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashFlowService.UnitTests.ProcessorManager
{
    [TestClass]
    public class CashFlowManagerTests
    {
        [TestMethod]
        public void CalculateNetPresentValueAsync_WithNullRequest_ShouldThrow()
        {
            //Arrange
            var cashFlowManager = new CashFlowManager();

            CashFlowRequest cashFlowRequest = null;

            //Act
            Action action = () => cashFlowManager.CalculateNetPresentValueAsync(cashFlowRequest).Wait();

            //Assert
            action.Should().Throw<Exception>().WithMessage("CashFlowRequest is null.");
        }

        [TestMethod]
        public void CalculateNetPresentValueAsync_WithValidRequest_ShouldReturn()
        {
            //Arrange
            var cashFlowManager = new CashFlowManager();

            var cashFlowRequest = GetCashFlowRequest();

            //Act
            var result = cashFlowManager.CalculateNetPresentValueAsync(cashFlowRequest).Result;

            //Assert
            result.Should().NotBeNull();
            result.TotalNetPresentValue.Should().BeGreaterThan(0);
            result.NetPresentValues.Any().Should().BeTrue();
        }

        #region Private Methods

        private CashFlowRequest GetCashFlowRequest()
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

        #endregion
    }
}

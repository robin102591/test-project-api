using CashFlowService.Common;
using CashFlowService.Model;
using CashFlowService.ProcessorManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CashFlowService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashFlowsController : ControllerBase
    {
        private readonly ICashFlowManager _cashFlowManager;

        public CashFlowsController(ICashFlowManager cashFlowManager)
        {
            _cashFlowManager = cashFlowManager;
        }

        [HttpGet]
        [Route("health")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        [Route("calculate")]
        public async Task<IActionResult> Post([FromBody]CashFlowRequest cashFlowRequest)
        {
            dynamic response = null;
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.GetRequestModelErrors();

                    return BadRequest(errors);
                }

                response = await _cashFlowManager.CalculateNetPresentValueAsync(cashFlowRequest);
            }
            catch (Exception ex)
            {
                var error = new ErrorResponse
                {
                    Message = ex.Message,
                };

                return StatusCode(500, error);
            }

            return Ok(response);
        }
    }
}

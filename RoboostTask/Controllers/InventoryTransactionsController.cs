using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboostTask.DTOs.Stocks;
using RoboostTask.Enums;
using RoboostTask.Features.Transaction.Commands;
using RoboostTask.Features.Transaction.Queries;
using RoboostTask.Features.Products.Commands;
using RoboostTask.GeneralResponse;
using System.Security.Claims;

namespace RoboostTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryTransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost("add-stock")]
        public async Task<Response<string>> AddStock([FromBody] AddStockRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Response<string>.Fail("Invalid input");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Response<string>.Fail("User is not authenticated");
            }

            var result = await _mediator.Send(new AddStockCommand(request, userId));

            if (!result.IsSucceeded)
            {
                return Response<string>.Fail(result.Message, null, result.StatusCode);
            }

            return Response<string>.Success("Stock added successfully");
        }
        [HttpDelete("delete-stock")]
        public async Task<ActionResult<Response<string>>> DeleteStock([FromBody] DeleteStockRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Response<string>.Fail("Invalid input"));
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(Response<string>.Fail("User is not authenticated"));
                }

                var result = await _mediator.Send(new DeleteStockCommand(request,userId));

                if (!result.IsSucceeded)
                {
                    return StatusCode(result.StatusCode, Response<string>.Fail(result.Message));
                }
                return Ok(Response<string>.Success(result.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Response<string>.Fail("An unexpected error occurred"));
            }
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentInventory()
        {
            var result = await _mediator.Send(new GetInventoryQuery());
            return Ok(result);
        }

        [Authorize]
        [HttpGet("check-role")]
        public IActionResult CheckRole()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roles = User.Claims
                           .Where(c => c.Type == ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList();

            return Ok(new
            {
                userId = userId,
                roles = roles,
                allClaims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }

    }
}

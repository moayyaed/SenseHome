using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenseHome.Common.Exceptions;
using SenseHome.Common.Values;
using SenseHome.DataTransferObjects.Subscription;
using SenseHome.Services.Subscription;

namespace SenseHome.API.Controllers
{
    [Route("api")]
    [Authorize]
    public class SubscriptionController : SenseHomeBaseController
    {
        private readonly ISubscriptionService subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            this.subscriptionService = subscriptionService;
        }

        [HttpPost("subscriptions")]
        [Authorize(Policy = PolicyName.Admin)]
        public async Task<ActionResult<SubscriptionDto>> CreateAsync([FromBody] SubscriptionInsertDto subscription)
        {
            var createdSubscription = await subscriptionService.AddSubscriptionAsync(subscription);
            return Created($"/api/subscriptions/{createdSubscription.Id}", createdSubscription);
        }

        [HttpGet("users/{id}/subscriptions")]
        public async Task<ActionResult<SubscriptionDto>> GetUserSubscriptionsAsync(string id)
        {
            var currentUserId = GetCurrentUserId();
            var currentUserRole = GetCurrentUserRole();
            if(currentUserId != id && currentUserRole != Common.Enums.UserType.Admin)
            {
                throw new ForbiddenException("Access Denied");
            }
            var subscription = await subscriptionService.GetUserSubscriptionsAsync(id);
            return Ok(subscription);
        }

        [HttpPut("subscriptions/{id}")]
        [Authorize(Policy = PolicyName.Admin)]
        public async Task<ActionResult<SubscriptionDto>> UpdateAsync([FromRoute]string id, [FromBody]SubscriptionUpdateDto subscription)
        {
            var updatedSubscription = await subscriptionService.UpdateSubscriptionAsync(subscription);
            return Ok(updatedSubscription);
        }
    }
}

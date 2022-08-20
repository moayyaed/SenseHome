using System;
using System.Threading.Tasks;
using AutoMapper;
using SenseHome.Common.Exceptions;
using SenseHome.DataTransferObjects.Subscription;
using SenseHome.Repositories.Subscription;

namespace SenseHome.Services.Subscription
{
    public class SubscriptionService : BaseService, ISubscriptionService
    {
        private readonly ISubscriptionRepository subscriptionRepository;

        public SubscriptionService(IMapper mapper, ISubscriptionRepository subscriptionRepository) : base(mapper)
        {
            this.subscriptionRepository = subscriptionRepository;
        }

        public async Task<SubscriptionDto> AddSubscriptionAsync(SubscriptionInsertDto subscription)
        {
            var userExistingSubscriptions = await subscriptionRepository.GetByUserIdOrDefaultAsync(subscription.UserId);
            if(userExistingSubscriptions != null)
            {
                throw new BadRequestException("User already has subscription");
            }
            var subscriptionToCreate = mapper.Map<DomainModels.Subscription>(subscription);
            var createdSubscription = await subscriptionRepository.CreateAsync(subscriptionToCreate);
            return mapper.Map<SubscriptionDto>(createdSubscription);
        }

        public async Task<bool> DeleteSubscriptionByIdAsync(string id)
        {
            var deleted = await subscriptionRepository.DeleteAsync(id);
            if(!deleted)
            {
                throw new BadRequestException("Failed to delete this entity");
            }
            return true;
        }

        public async Task<SubscriptionDto> GetUserSubscriptionsAsync(string userId)
        {
            var subscriptions = await subscriptionRepository.GetByUserIdAsync(userId);
            var subscriptionDto = mapper.Map<SubscriptionDto>(subscriptions);
            return subscriptionDto;
        }

        public async Task<SubscriptionDto> UpdateSubscriptionAsync(SubscriptionUpdateDto subscription)
        {
            var subscriptionToUpdate = await subscriptionRepository.GetOrDefaultAsync(subscription.Id);
            if(subscriptionToUpdate == null)
            {
                throw new NotFoundException("No subscription found with this id");
            }
            subscriptionToUpdate.Path = subscription.Path;
            subscriptionToUpdate.LastModifedDate = DateTime.Now;
            var updatedSubscription= await subscriptionRepository.UpdateAsync(subscriptionToUpdate);
            var subscriptionDto = mapper.Map<SubscriptionDto>(updatedSubscription);
            return subscriptionDto;
        }
    }
}

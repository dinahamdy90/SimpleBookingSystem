using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.Infrastructure.Contracts
{
    public interface IResourceService
    {
        Task<List<ResourceModel>> GetResourcesAsync();
    }
}

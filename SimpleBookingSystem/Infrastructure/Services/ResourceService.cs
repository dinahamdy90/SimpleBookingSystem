using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Infrastructure.Contracts;
using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.Infrastructure.Services
{
    public class ResourceService: IResourceService
    {
        private readonly SimpleBookingContext _context;
        private readonly IMapper _mapper;

        public ResourceService(SimpleBookingContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ResourceModel>> GetResourcesAsync()
        {
            var resources = await _context.Resource.ToListAsync();

            if (!resources.Any())
            {
                return new List<ResourceModel>();
            }
            else
            {
                return _mapper.Map<List<ResourceModel>>(resources);
            }
        }
    }
}

using AutoMapper;
using SimpleBookingSystem.Infrastructure.Domain;
using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.Infrastructure.Mappings
{
    public class ResourceMappingProfile: Profile
    {
        public ResourceMappingProfile()
        {
            CreateMap<Resource, ResourceModel>();
        }
    }
}

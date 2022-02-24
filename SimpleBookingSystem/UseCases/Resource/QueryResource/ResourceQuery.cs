using MediatR;
using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.UseCases.Resource
{
    public class ResourceQuery : IRequest<List<ResourceModel>>
    {
    }
}

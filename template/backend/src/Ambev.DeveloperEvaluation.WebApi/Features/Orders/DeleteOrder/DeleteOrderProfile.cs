using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.DeleteOrder;

/// <summary>
/// Profile for mapping DeleteOrder feature requests to commands
/// </summary>
public class DeleteOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteOrder feature
    /// </summary>
    public DeleteOrderProfile()
    {
        CreateMap<Guid, Application.Orders.DeleteOrder.DeleteOrderCommand>()
            .ConstructUsing(id => new Application.Orders.DeleteOrder.DeleteOrderCommand(id));
    }
}

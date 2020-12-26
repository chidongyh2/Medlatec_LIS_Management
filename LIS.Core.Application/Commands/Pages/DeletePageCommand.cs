using LIS.Infrastructure.Models;
using MediatR;

namespace LIS.Core.Application.Commands.Pages
{
    public class DeletePageCommand : IRequest<ActionResultResponse>
    {
        public int Id { get; private set; }
        public DeletePageCommand(int id)
        {
            Id = id;
        }
    }
}

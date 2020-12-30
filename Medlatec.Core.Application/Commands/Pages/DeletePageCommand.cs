using Medlatec.Infrastructure.Models;
using MediatR;

namespace Medlatec.Core.Application.Commands.Pages
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

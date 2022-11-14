using Demo.Domain.Model;
using MediatR;

namespace Demo.Application.Hash.Queries
{
    public class ShutdownQuery : IRequest<ShutdownResult>
    {
        public bool IsShutdown { get; set; }
    }

    public class ShutdownQueryHandler : IRequestHandler<ShutdownQuery, ShutdownResult>
    {
        private readonly IHashLogic hashLogic;

        public ShutdownQueryHandler(
            IHashLogic hashLogic)
        {
            this.hashLogic = hashLogic;
        }
        public Task<ShutdownResult> Handle(ShutdownQuery request, CancellationToken cancellationToken)
        {

            return this.hashLogic.StartOrShutdown(request.IsShutdown);
        }
    }
}

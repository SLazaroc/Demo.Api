using Demo.Domain.Model;
using MediatR;

namespace Demo.Application.Hash.Queries
{
    public class GetStatsQuery : IRequest<HashStats>
    {

    }

    public class GetStatsQueryHandler : IRequestHandler<GetStatsQuery, HashStats>
    {
        private readonly IHashLogic hashLogic;

        public GetStatsQueryHandler(
            IHashLogic hashLogic)
        {
            this.hashLogic = hashLogic;
        }
        public Task<HashStats> Handle(GetStatsQuery request, CancellationToken cancellationToken)
        {
            return hashLogic.GetStats();
        }
    }
}

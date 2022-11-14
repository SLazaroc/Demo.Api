using Demo.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Hash.Queries
{
    public class GetHashQuery : IRequest<GetHashResult>
    {
        public int Id { get; set; }
    }

    public class GetHashQueryHandler : IRequestHandler<GetHashQuery, GetHashResult>
    {
        private readonly IHashLogic hashLogic;

        public GetHashQueryHandler(
            IHashLogic hashLogic)
        {
            this.hashLogic = hashLogic;
        }
        public Task<GetHashResult> Handle(GetHashQuery request, CancellationToken cancellationToken)
        {
            return hashLogic.Get(request.Id);
        }
    }
}

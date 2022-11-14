using Demo.Domain.Hash;
using MediatR;

namespace Demo.Application.Hash.Queries
{
    public class CreateHashQuery : IRequest<HashResult>
    {
        public string Password { get; set; }
    }

    public class CreateHashQueryHandler : IRequestHandler<CreateHashQuery, HashResult>
    {
        private readonly IHashLogic hashLogic;

        public CreateHashQueryHandler(
            IHashLogic hashLogic)
        {
            this.hashLogic = hashLogic;
        }
        public Task<HashResult> Handle(CreateHashQuery request, CancellationToken cancellationToken)
        {
            return hashLogic.GenerateHash(request.Password);
        }
    }
}

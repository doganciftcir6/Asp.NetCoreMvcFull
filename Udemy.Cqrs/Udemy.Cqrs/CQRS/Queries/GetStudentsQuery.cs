using MediatR;
using System.Collections.Generic;
using Udemy.Cqrs.CQRS.Results;

namespace Udemy.Cqrs.CQRS.Queries
{
    public class GetStudentsQuery : IRequest<List<GetStudentsQueryResult>>
    {
    }
}

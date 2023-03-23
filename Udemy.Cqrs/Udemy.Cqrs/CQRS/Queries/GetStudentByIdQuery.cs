using MediatR;
using Udemy.Cqrs.CQRS.Results;

namespace Udemy.Cqrs.CQRS.Queries
{
    //Id ile student getirecek bir query oluşturduk.
    //IRequest ten implemente ol ve geriye döneceğin şey GetStudentByIdQueryResult olsun.
    public class GetStudentByIdQuery : IRequest<GetStudentByIdQueryResult>
    {
        public int Id { get; set; }
        public GetStudentByIdQuery(int ıd)
        {
            Id = ıd;
        }
    }
}

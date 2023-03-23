using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Udemy.Cqrs.CQRS.Queries;
using Udemy.Cqrs.CQRS.Results;
using Udemy.Cqrs.data;

namespace Udemy.Cqrs.CQRS.Handlers
{
    //ben artık gerekli işimi bu Handler'ın içerisinde yapacağım.
    //IRequestHandler ile sen bir handlersın GetStudentByIdQuery bu query'i alırsın GetStudentByIdQueryResult sonucunda bu olur dedik.
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, GetStudentByIdQueryResult>
    {
        private readonly StudentContext _studentContext;

        public GetStudentByIdQueryHandler(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        public async Task<GetStudentByIdQueryResult> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentContext.Set<Student>().FindAsync(request.Id);
            return new GetStudentByIdQueryResult
            {
                Age = student.Age,
                Name = student.Name,
                Surname = student.Surname
            };
            throw new System.NotImplementedException();
        }

        //public GetStudentByIdQueryResult Handle(GetStudentByIdQuery query)
        //{
        //    var student = _studentContext.Set<Student>().Find(query.Id);
        //    return new GetStudentByIdQueryResult
        //    {
        //        Age = student.Age,
        //        Name = student.Name,
        //        Surname = student.Surname
        //    };
        //}
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Udemy.Cqrs.CQRS.Queries;
using Udemy.Cqrs.CQRS.Results;
using Udemy.Cqrs.data;

namespace Udemy.Cqrs.CQRS.Handlers
{
    public class GetStudentsQueryHandler  : IRequestHandler<GetStudentsQuery,List<GetStudentsQueryResult>>
    {
        private readonly StudentContext _context;

        public GetStudentsQueryHandler(StudentContext context)
        {
            _context = context;
        }

        //IEnumarable List'E göre daha az maliyetli çektiğim data üstünden sorgulama yapmıcam sadece listelemek ondan kullandım.
        //public List<GetStudentsQueryResult> Handle(GetStudentsQuery query)
        //{
        //    return _context.Students.Select(x => new GetStudentsQueryResult { Name = x.Name, Surname = x.Surname }).AsNoTracking().ToList();
        //}

        public async Task<List<GetStudentsQueryResult>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Students.Select(x => new GetStudentsQueryResult { Name = x.Name, Surname = x.Surname }).AsNoTracking().ToListAsync();
        }
    }
}

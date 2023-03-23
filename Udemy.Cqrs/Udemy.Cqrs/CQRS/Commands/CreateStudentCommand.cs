using MediatR;

namespace Udemy.Cqrs.CQRS.Commands
{
    //student'ı create ederken nelere ihtiyaç duyarım ?
    public class CreateStudentCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }
}

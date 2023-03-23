using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Enums;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Handlers
{
    //<> burada ilk önce requestim ne daha sonra geriye ne dönücem.
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest>
    {
        private readonly IRepository<AppUser> _repository;

        public RegisterUserCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        //ilgili user create işlemimi bu implement metot içinde yapıyorum.
        public async Task<Unit> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new AppUser
            {
                //kullanıcıyı member olarak kayıt edicem.
                //Burda dbdeki aproleıd değerini veriyorum member rolü 4. id ye denk geliyor ama burda böyle bir kod kullanıyorsak yani 4 yazıyorsak aklımıza hemen enumlar gelsin bunu diğer develler anlamaz enum kullanmalıyız.
                //bir lookup table ile karşı karşıyaysak ve datamızın orada sabit kalacağından veya çok seyrek değişeceğinden eminsek veya bir state'i tutuyorsak mutlaka orayı enumlaştırın.
                //AppRoleId = 4,
                AppRoleId = (int)RoleType.Member,
                Password = request.Password,
                Username = request.UserName,
            });
            return Unit.Value;
        }
    }
}

using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Queries;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Handlers
{
    //<> burada ilk önce requestim ne daha sonra geriye ne dönücem.
    public class CheckUserQueryRequestHandler : IRequestHandler<CheckUserQueryRequest, CheckUserResponseDto>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppRole> _roleRepository;

        public CheckUserQueryRequestHandler(IRepository<AppUser> userRepository, IRepository<AppRole> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        //ilgili check işlemimi bu implement metot içinde yapıyorum.
        public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            //önce bir dto'yu ele alalım
            var dto = new CheckUserResponseDto();

            //önce user'a gidip bakalım dışarıdan kullanıcı adı ve şifresiyle giriş yapmaya çalışan kullanıcının kullanıcı adı ve şifresi db de var mı diye.
            var user = await _userRepository.GetByFilterAsync(x => x.Username == request.Username && x.Password == request.Password);
            if (user == null)
            {
                //eğer kayıt yok ise 
                dto.IsExist = false;
            }
            else
            {
                //kayıt var ise user'ın entity bilgilerini dtonun bilgilerine atalım ve geriye dto'yu gönderelim.
                dto.Username = user.Username;
                dto.Id = user.Id;
                dto.IsExist = true;
                var role = await _roleRepository.GetByFilterAsync(x => x.Id == user.AppRoleId);
                dto.Role = role?.Defination;
            }

            return dto;

        }
    }
}

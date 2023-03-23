using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.AdvertisementApp.Business.Extensions;
using Udemy.AdvertisementApp.Business.Interfaces;
using Udemy.AdvertisementApp.Business.ValidationRules;
using Udemy.AdvertisementApp.Common;
using Udemy.AdvertisementApp.Common.Enums;
using Udemy.AdvertisementApp.DataAccess.UnitOfWork;
using Udemy.AdvertisementApp.Dtos;
using Udemy.AdvertisementApp.Entities;

namespace Udemy.AdvertisementApp.Business.Services
{
    public class AppUserService : Service<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>, IAppUserService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<AppUserCreateDto> _createDtoValidator;
        private readonly IValidator<AppUserLoginDto> _loginDtoValidator;
        public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidtor, IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow, IValidator<AppUserLoginDto> loginDtoValidator) : base(mapper, createDtoValidtor, updateDtoValidator, uow)
        {
            _uow = uow;
            _mapper = mapper;
            _createDtoValidator = createDtoValidtor;
            _loginDtoValidator = loginDtoValidator;
        }
        //GenericService'te bulunan Create Metotu User ekleme işlemi yaparken işimi karşılamıyor. Benim isteğim kullanıcıyı db'ye kayıt ederken aynı zamanda Role ataması yapsın bunun için yeni bir metot.
        public async Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto dto, int roleId)
        {
           var validationResult = _createDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                var user = _mapper.Map<AppUser>(dto);
                //1.yol;
                //user.AppUserRoles = new List<AppUserRole>();
                //user.AppUserRoles.Add(new AppUserRole
                //{
                //    AppUser = user,
                //    AppRoleId = roleId
                //});
                await _uow.GetRepository<AppUser>().CreateAsync(user);

                //2.yol;
                await _uow.GetRepository<AppUserRole>().CreateAsync(new AppUserRole
                {
                    AppUser = user,
                    AppRoleId = roleId
                });
                await _uow.SaveChangesAsync();
                return new Response<AppUserCreateDto>(ResponseType.Success, dto);
                //await _uow.GetRepository<AppUserRole>().CreateAsync(new AppUserRole
                //{
                //    AppRoleId = roleId,
                //    AppUserId =
                //});
            }
            return new Response<AppUserCreateDto>(dto, validationResult.ConvertToCustomValidationError());
        }
        //giriş işlemi yaparken kullanıcı db de var mı diye kontrol eden bir metota ihtiyacım var.
        public async Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto dto)
        {
            var validationResult = _loginDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
               var user = await _uow.GetRepository<AppUser>().GetByFilterAsync(x => x.Username == dto.Username && x.Password == dto.Password);
                if(user != null)
                {
                    var appUserDto = _mapper.Map<AppUserListDto>(user);
                    return new Response<AppUserListDto>(ResponseType.Success, appUserDto);
                }
                return new Response<AppUserListDto>(ResponseType.NotFound, "Kullanıcı adı veya şifre hatalı.");
            }
            return new Response<AppUserListDto>(ResponseType.ValidationError, "Kullanıcı adı veya şifre boş olamaz.");
        }
        //AppRole bilgilerini UserId ye dayanarak getiren bir metota ihtiyacım var.
        public async Task<IResponse<List<AppRoleListDto>>> GetRolesByUserIdAsync(int userId)
        {
          var roles =  await _uow.GetRepository<AppRole>().GetAllAsync(x => x.AppUserRoles.Any(x => x.AppUserId == userId));
          if(roles == null)
            {
                return new Response<List<AppRoleListDto>>(ResponseType.NotFound, "İlgili rol bulunamadı.");
            }
            var dto = _mapper.Map<List<AppRoleListDto>>(roles);
            return new Response<List<AppRoleListDto>>(ResponseType.Success, dto);
        }
    }
}

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
using Udemy.AdvertisementApp.Common;
using Udemy.AdvertisementApp.Common.Enums;
using Udemy.AdvertisementApp.DataAccess.UnitOfWork;
using Udemy.AdvertisementApp.Dtos;
using Udemy.AdvertisementApp.Entities;

namespace Udemy.AdvertisementApp.Business.Services
{
    public class AdvertisementAppuserService : IAdvertisementAppUserService
    {
        private readonly IUow _uow;
        private readonly IValidator<AdvertisementAppUserCreateDto> _createDtoValidator;
        private readonly IMapper _mapper;

        public AdvertisementAppuserService(IUow uow, IValidator<AdvertisementAppUserCreateDto> createDtoValidator, IMapper mapper)
        {
            _uow = uow;
            _createDtoValidator = createDtoValidator;
            _mapper = mapper;
        }
        //bir ilana başvuran kişiyi db'ye kayıt yapmak.
        public async Task<IResponse<AdvertisementAppUserCreateDto>> CreateAsync(AdvertisementAppUserCreateDto dto)
        {
            var result = _createDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                //create işlemi söz konusu istediğim dto gelmiş.
                //daha önce bu arkadaş başvurmuş mu başvurmamış mı bunun kontrolünü yapmalıyız.
                var control = await _uow.GetRepository<AdvertisementAppUser>().GetByFilterAsync(x => x.AppUserId == dto.AppUserId && x.AdvertisementId == dto.AdvertisementId); //bu null olmazsa sıkıntı var başvurmuş.
                if(control == null)
                {
                    //CreateAsync benden entity istiyor ancak elimde dto var çevirme işlemini yapmam lazım.
                    var createdAdvertisementAppUser = _mapper.Map<AdvertisementAppUser>(dto);
                    await _uow.GetRepository<AdvertisementAppUser>().CreateAsync(createdAdvertisementAppUser);
                    await _uow.SaveChangesAsync();
                    return new Response<AdvertisementAppUserCreateDto>(ResponseType.Success, dto);
                }
                List<CustomValidationError> errors = new List<CustomValidationError> { new CustomValidationError { ErrorMessage = "Daha önce başvurulan ilana başvurulamaz", PropertyName = "" } };
                return new Response<AdvertisementAppUserCreateDto>(dto,errors);
                
            }
            return new Response<AdvertisementAppUserCreateDto>(dto, result.ConvertToCustomValidationError());
        }
        //status type'a göre liste getiren bir metot.
        //aynı zamadna bir cshtml sayfasında birden fazla tablo kullanmanın örneği.
        //.ThenInclude(x=>x.Gender) ile AppUser tablosunun içine girip gender'ı da dahil et diyorum bu durumda GenderListDto da gerekli olur.
        public async Task<List<AdvertisementAppUserListDto>> GetList(AdvertisementAppUserStatusType type)
        {
            var query = _uow.GetRepository<AdvertisementAppUser>().GetQuery();
            //artık elimde IQeuyable bir şey var.
            var list = await query.Include(x => x.Advertisement).Include(x => x.AdvertisementAppUserStatus).Include(x => x.MilitaryStatus).Include(x => x.AppUser).ThenInclude(x=>x.Gender).Where(x => x.AdvertisementAppUserStatusId == (int)type).ToListAsync();
            return _mapper.Map<List<AdvertisementAppUserListDto>>(list);
        }
        public async Task SetStatusAsync(int advetisementAppUserId, AdvertisementAppUserStatusType type)
        {
            //güncelleme işlemi
            //var unchanged = await _uow.GetRepository<AdvertisementAppUser>().FindAsync(advetisementAppUserId);
            //var changed = await _uow.GetRepository<AdvertisementAppUser>().GetByFilterAsync(x=> x.Id == advetisementAppUserId);
            //changed.AdvertisementAppUserStatusId = (int)type;
            //_uow.GetRepository<AdvertisementAppUser>().Update(changed, unchanged);

            var query = _uow.GetRepository<AdvertisementAppUser>().GetQuery();
            var entity = await query.SingleOrDefaultAsync(x => x.Id == advetisementAppUserId);
            entity.AdvertisementAppUserStatusId = (int)type;
            await _uow.SaveChangesAsync();
        }
    }
}

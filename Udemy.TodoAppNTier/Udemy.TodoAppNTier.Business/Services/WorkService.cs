using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Business.Extensions;
using Udemy.TodoAppNTier.Business.Interfaces;
using Udemy.TodoAppNTier.Business.ValidationRules;
using Udemy.TodoAppNTier.Common.ResponseObjects;
using Udemy.TodoAppNTier.DataAccess.UnitOfWork;
using Udemy.TodoAppNTier.Dtos.Interfaces;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.Entities.Concrete;

namespace Udemy.TodoAppNTier.Business.Services
{
    //SERVİCELER ENTİTYLERİ DTO BİLGİSİNE DÖNÜŞTÜRMEK DTO BİLGİLERİNİ ENTİTY BİLGİSİNE DÖNÜŞTÜRMEK İÇİN VARDIR
    public class WorkService : IWorkService
    {
        //dependency injection IUOW için ve AutoMapper için ve FluentValidation için.
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _createDtoValidator;
        private readonly IValidator<WorkUpdateDto> _updateDtoValidator;


        public WorkService(IUow uow, IMapper mapper, IValidator<WorkCreateDto> createDtoValidator, IValidator<WorkUpdateDto> updateDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
        }

        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto dto)
        {
            var validationResult = _createDtoValidator.Validate(dto);
            
            if (validationResult.IsValid)
            {
                //Dto'yu Work-Entity'e çevirip Db'ye Kayıt etmek.
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, dto);
            }
            else
            {
                return new Response<WorkCreateDto>(ResponseType.ValidationError, dto, validationResult.ConverToCustomValidationError());
            }
          
        }

        public async Task<IResponse<List<WorkListDto>>> GetAll()
        {
            //var list = await _uow.GetRepository<Work>().GetAll();
            //var workList = new List<WorkListDto>();

            //if (list != null && list.Count > 0)
            //{
            //    foreach (var work in list)
            //    {
            //        workList.Add(new()
            //        {
            //            Definition = work.Definition,
            //            Id = work.Id,
            //            IsCompleted = work.IsCompleted
            //        });
            //    }
            //}

            //AUTOMAPPERDAN SONRA
            //Work-Entity bilgisini WorkListDto bilgsine çevirmek.
           var data = _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
            return new Response<List<WorkListDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            //var work = await _uow.GetRepository<Work>().GetByFilter(x=>x.Id==id);
            //return new WorkListDto()
            //{
            //    Definition = work.Definition,
            //    IsCompleted = work.IsCompleted
            //};

            //AUTOMAPPERDAN SONRA
            //awaitten gelen sonucu (workentity) WorkListDto bilgisine çevirmek.
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id));
            if(data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ye ait data bulunamadı.");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);

            if(removedEntity != null)
            {
                _uow.GetRepository<Work>().Remove(removedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı.");
            }
           
        }

        public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto)
        {
            //_uow.GetRepository<Work>().Update(new Work()
            //{
            //    Definition = dto.Definition,
            //    Id = dto.Id,
            //    IsCompleted = dto.IsCompleted
            //});

            //AUTOMAPPERDAN SONRA
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                //dto bilgisni Work-entity bilgisine çevirmek.
                var updatedEntity = await _uow.GetRepository<Work>().Find(dto.Id);
                if(updatedEntity != null)
                {
                    _uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto),updatedEntity);
                    await _uow.SaveChanges();
                    return new Response<WorkUpdateDto>(ResponseType.Success, dto);
                }
                return new Response<WorkUpdateDto>(ResponseType.NotFound, $"{dto.Id} ye ait data bulunamadı.");
            }
            else
            {
                return new Response<WorkUpdateDto>(ResponseType.ValidationError, dto, result.ConverToCustomValidationError());
            }
            
        }
    }
}

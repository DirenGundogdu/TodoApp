using AutoMapper;
using Business.Interfaces;
using Business.ValidationRules;
using Common.ResponseObjects;
using DataAccess.UnitofWork;
using Dtos.WorkDtos;
using Entities.Domains;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class WorkService : IWorkService
    {
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

            //await _uow.GetRepository<Work>().Create(new()
            //{
            //    IsCompleted = dto.IsCompleted,
            //    Definition = dto.Definition,
            //});

            //var validator = new WorkCreateDtoValidator();
            //var validationResult = validator.Validate(dto);

            var validationResult = _createDtoValidator.Validate(dto);           
            if (validationResult.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, dto);
            }
            else
            {
                List<CustomValidationError> errors = new();
                foreach (var error in validationResult.Errors)
                {
                    errors.Add(new()
                    {
                        ErrorMessage = error.ErrorMessage,
                        PropertName = error.PropertyName,
                    });
                }
                return new Response<WorkCreateDto>(ResponseType.ValidationError,dto,errors);
            }
            
        }

        public async Task<List<WorkListDto>> GetAll()
        {
            //var list = await _uow.GetRepository<Work>().GetAll();

            //var workList = new List<WorkListDto>();

            //if (list != null && list.Count > 0)
            //{
            //    foreach (var item in list)
            //    {
            //        workList.Add(new()
            //        {
            //            Definition = item.Definition,
            //            Id = item.Id,
            //            IsCompleted = item.IsCompleted,
            //        });
            //    }
            //}
            //return workList;

            return  _mapper.Map<List<WorkListDto>>(await _uow.GetRepository<Work>().GetAll());
        }

        public async Task<IDto> GetById<IDto>(int id)
        {
            return _mapper.Map<IDto>(await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id));
            //var work = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            //return new()
            //{
            //    Definition = work.Definition,
            //    IsCompleted = work.IsCompleted,
            //};
        }

        public async Task Remove(int id)
        {
            //var deletedWork = await _uow.GetRepository<Work>().GetById(id);
           var removedEntity = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
            _uow.GetRepository<Work>().Remove(removedEntity);
            await _uow.SaveChanges();
            }
        }

        public async Task Update(WorkUpdateDto dto)
        {
            var result = _updateDtoValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Work>().Find(dto.Id);
                if (updatedEntity != null) { }
                _uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto), updatedEntity);
                await _uow.SaveChanges();
            }
            
            
            //_uow.GetRepository<Work>().Update(new()
            //{
            //    Id = dto.Id,
            //    Definition = dto.Definition,
            //    IsCompleted = dto.IsCompleted,
            //});

            
        }
    }
}

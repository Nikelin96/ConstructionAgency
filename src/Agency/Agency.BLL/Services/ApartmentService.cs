namespace Agency.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core;
    using System.Linq;
    using System.Linq.Expressions;
    using Agency.BLL.DTOs;
    using Agency.BLL.Infrastructure;
    using Agency.DAL.Interfaces;
    using Agency.DAL.Model.Entities;
    using AutoMapper;

    public class ApartmentService : IApartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ApartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IList<ApartmentEditDto> GetAll(Expression<Func<Apartment, bool>> filter = null)
        {

            return _mapper.MapToList<Apartment, ApartmentEditDto>(_unitOfWork.Apartments.GetAll(filter));
        }

        public void Update(ApartmentEditDto dto)
        {
            Apartment entity = _unitOfWork.Apartments.Get(dto.Id);
            if (entity == null)
            {
                throw new ObjectNotFoundException($"Entity with id {dto.Id} was not found");
            }

            _unitOfWork.Apartments.Update(_mapper.Map(dto, entity));
            _unitOfWork.Commit();
        }
    }
}
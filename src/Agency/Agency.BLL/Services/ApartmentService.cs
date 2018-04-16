namespace Agency.BLL.Services
{
    using System;
    using System.Collections.Generic;
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
            _unitOfWork.Apartments.Update(_mapper.Map<Apartment>(dto));
            _unitOfWork.Commit();
        }

//        public IEnumerable<ApartmentState> AllowedStates =>
//            Enum.GetValues(typeof(ApartmentState)).OfType<ApartmentState>().Where(state => (int) state > (int) State);

        public (bool isValid, string message) Validate(ApartmentEditDto apartmentDto, ApartmentState newState)
        {
            IEnumerable<ApartmentState> allowedStates = GetAllowedApartmentStates(apartmentDto.State);
            if (allowedStates.Contains(newState))
            {
                return (isValid: true, message: string.Empty);
            }

            string message =
                "Allowed States are:" +
                Environment.NewLine +
                string.Join(
                    Environment.NewLine,
                    allowedStates.Select(state => $"{(int) state} {state:G}").ToArray()
                );

            return (isValid: false, message: message);
        }

        public IEnumerable<ApartmentState> GetAllowedApartmentStates(ApartmentState stateToVerify)
        {
            return Enum.GetValues(typeof(ApartmentState)).OfType<ApartmentState>()
                .Where(state => (int) state > (int) stateToVerify);
        }
    }
}
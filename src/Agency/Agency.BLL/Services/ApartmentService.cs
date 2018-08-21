namespace Agency.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Core;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using Agency.BLL.DTOs;
    using Agency.BLL.Infrastructure;
    using Agency.DAL.Interfaces;
    using Agency.DAL.Model.Entities;
    using AutoMapper;
    using NLog;

    public class ApartmentService : IApartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly Stopwatch _stopwatch;

        public ApartmentService(IUnitOfWork unitOfWork, IMapper mapper,
           Func<ILogger> getLogger)
        {
            _unitOfWork = unitOfWork; //?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper; //?? throw new ArgumentNullException(nameof(mapper));
            _logger = getLogger(); //?? throw new ArgumentNullException(nameof(logger));

            _stopwatch = new Stopwatch();
        }

        public IList<ApartmentEditDto> GetAll(Expression<Func<Apartment, bool>> filter = null)
        {
            _logger.Info("Begin Execution");

            _stopwatch.Start();

            List<ApartmentEditDto> result = _mapper.MapToList<Apartment, ApartmentEditDto>(_unitOfWork.Apartments.GetAll(filter));

            _stopwatch.Stop();

            _logger.Info($"End Execution in {_stopwatch.ElapsedMilliseconds}");

            return result;
        }

        public void Update(ApartmentEditDto dto)
        {
            _logger.Info("Begin Execution");

            _stopwatch.Start();

            Apartment entity = _unitOfWork.Apartments.Get(dto.Id);
            if (entity == null)
            {
                throw new ObjectNotFoundException($"Entity with id {dto.Id} was not found");
            }

            _unitOfWork.Apartments.Update(_mapper.Map(dto, entity));
            _unitOfWork.Commit();

            _stopwatch.Stop();

            _logger.Info($"End Execution in {_stopwatch.ElapsedMilliseconds}");
        }
    }
}
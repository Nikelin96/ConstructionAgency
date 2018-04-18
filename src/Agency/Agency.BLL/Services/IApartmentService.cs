namespace Agency.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Agency.BLL.DTOs;
    using Agency.DAL.Model.Entities;

    public interface IApartmentService
    {
        IList<ApartmentEditDto> GetAll(Expression<Func<Apartment, bool>> filter = null);

        void Update(ApartmentEditDto dto);
    }
}
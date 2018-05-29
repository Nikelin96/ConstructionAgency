namespace Agency.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL.Model.Entities;
    using DTOs;

    public interface IApartmentStateService
    {
        (bool isValid, string message) Validate(ApartmentEditDto apartmentDto, ApartmentState newState);

        IEnumerable<ApartmentState> GetAllowedApartmentStates(ApartmentState stateToVerify);
    }
}

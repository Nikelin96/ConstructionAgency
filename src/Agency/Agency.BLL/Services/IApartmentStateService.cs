namespace Agency.BLL.Services
{
    using System.Collections.Generic;
    using DAL.Model.Entities;
    using DTOs;

    public interface IApartmentStateService
    {
        (bool isValid, string message) Validate(ApartmentEditDto apartmentDto, ApartmentState newState);

        IEnumerable<ApartmentState> GetAllowedApartmentStates(ApartmentState stateToVerify);
    }
}

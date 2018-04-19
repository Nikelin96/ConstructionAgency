namespace Agency.ConsoleClient.Services
{
    using BLL.DTOs;

    public interface IApartmentControllerService
    {
        ApartmentEditDto PickApartmentForEdit();
        ApartmentEditDto UpdateApartment(ApartmentEditDto selectedApartment);
    }
}

namespace Agency.BLL.Services
{
    using DAL.Model.Entities;
    using DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApartmentStateService : IApartmentStateService
    {
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
                    allowedStates.Select(state => $"{(int)state} {state:G}").ToArray()
                );

            return (isValid: false, message: message);
        }

        public IList<ApartmentState> GetAllowedApartmentStates(ApartmentState stateToVerify)
        {
            return Enum.GetValues(typeof(ApartmentState)).OfType<ApartmentState>()
                .Where(state => (int)state > (int)stateToVerify).ToList();
        }
    }
}

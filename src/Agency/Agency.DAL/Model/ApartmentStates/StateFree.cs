
namespace Agency.DAL.Model.ApartmentStates
{
    using Agency.DAL.Model.Entities;
    using System;

    public class StateFree : StateContainer<ApartmentState>
    {
        public StateFree() : base(ApartmentState.Free) { }

        public override ApartmentState State
        {
            set
            {
                switch (value)
                {
                    case ApartmentState.Free:
                    case ApartmentState.Occupancy:
                    case ApartmentState.Occupied:
                    case ApartmentState.ClosingForRenovation:
                    case ApartmentState.Renovation:                    
                        throw new Exception($"Cannot transit from {value}");
                }
                //if (value != ApartmentState.Done)
                //{
                //    throw new Exception($"Cannot transit from {value}");
                //}
                //base.State = value;
            }
        }


    }
}

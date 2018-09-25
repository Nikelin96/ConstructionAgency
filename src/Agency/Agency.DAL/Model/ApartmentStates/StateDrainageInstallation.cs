namespace Agency.DAL.Model.ApartmentStates
{
    using Agency.DAL.Model.Entities;
    using System;

    public class StateDrainageInstallation : StateContainer<ApartmentState>
    {
        public StateDrainageInstallation() : base(ApartmentState.DrainageInstallation) { }

        public override ApartmentState State
        {
            set
            {
                if (value != ApartmentState.PartitionsDesigning)
                {
                    throw new Exception($"Cannot transit from {value}");
                }
                base.State = value;
            }
        }
    }
}

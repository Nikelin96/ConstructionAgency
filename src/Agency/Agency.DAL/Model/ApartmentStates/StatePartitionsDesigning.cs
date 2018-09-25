namespace Agency.DAL.Model.Entities
{
    using System;

    public class PartitionsDesigningState : StateContainer<ApartmentState>
    {
        public PartitionsDesigningState() : base(ApartmentState.PartitionsDesigning) { }

        public override ApartmentState State
        {
            set
            {
                if (value != ApartmentState.Done)
                {
                    throw new Exception($"Cannot transit from {value}");
                }
                base.State = value;
            }
        }
    }
}

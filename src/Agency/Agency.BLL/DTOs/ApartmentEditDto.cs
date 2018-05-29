namespace Agency.BLL.DTOs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Agency.DAL.Model.Entities;

    public class ApartmentEditDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RoomsCount { get; set; }

        public ApartmentState State { get; set; }
    }
}
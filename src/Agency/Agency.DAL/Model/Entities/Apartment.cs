﻿namespace Agency.DAL.Model.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Apartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int RoomsCount { get; set; }

        public ApartmentState State { get; set; }

        #region  relations

        public virtual Building Building { get; set; }

        public virtual ICollection<Person> Persons { get; set; }

        #endregion

        public Apartment() => Persons = new List<Person>();
    }
}
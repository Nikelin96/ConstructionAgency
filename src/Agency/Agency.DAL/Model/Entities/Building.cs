namespace Agency.DAL.Model.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Building
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Number => Id + 1;

        #region relations

        public virtual Street Street { get; set; }

        public virtual ICollection<Apartment> Apartments { get; set; }

        #endregion

        public Building() => Apartments = new List<Apartment>();
    }
}
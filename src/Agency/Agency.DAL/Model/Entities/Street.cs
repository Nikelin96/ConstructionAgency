namespace Agency.DAL.Model.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Street
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        #region reations

        public virtual District District { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }

        #endregion

        public Street() => Buildings = new List<Building>();

    }
}
namespace Agency.DAL.Model.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        #region relations

        public virtual ICollection<District> Districts { get; set; }

        #endregion

        public City() => Districts = new List<District>();

    }
}
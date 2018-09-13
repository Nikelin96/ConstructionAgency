namespace Agency.DAL.Model.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        #region reations

        public virtual City City { get; set; }

        public virtual List<Street> Streets { get; set; }

        #endregion

        public District() => Streets = new List<Street>();
    }
}
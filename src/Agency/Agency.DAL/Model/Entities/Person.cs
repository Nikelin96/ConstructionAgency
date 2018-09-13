namespace Agency.DAL.Model.Entities
{    
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Age { get; set; }

        #region relations

        public virtual Apartment Apartment { get; set; }

        #endregion
    }
}
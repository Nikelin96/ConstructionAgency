namespace Agency.DAL.Model.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Military")]
    public class Military : Person
    {
        [Required]
        public MilitaryRank? Rank { get; set; }
    }
}

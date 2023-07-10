using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TodoListAPI.Infra.Database.Models
{
    public class TodoData
    {
        [Required]
        [Display(Name = "Id")]
        [StringLength(36)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "UserId")]
        [StringLength(36)]
        public string? UserId { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "CreatedAt")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
    }
}


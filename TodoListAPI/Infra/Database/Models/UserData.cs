using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TodoListAPI.Infra.Database.Models
{
    public class UserData
    {
        [Required]
        [Display(Name = "Id")]
        [StringLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "CreatedAt")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Display(Name = "UpdatedAt")]
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }

        public ICollection<TodoData>? TodoList { get; set; }
    }
}


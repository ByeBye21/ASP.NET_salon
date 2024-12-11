using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WEB_Project.Models
{
    public class Category
    {
        [Key]
        [DisplayName("Category ID")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Number")]
        public int Num { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string Name { get; set; }
    }
}

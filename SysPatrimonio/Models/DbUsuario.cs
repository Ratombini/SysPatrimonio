using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SysPatrimonio.Models
{
    [Table("usuarios", Schema = "syspatrimonio")]
    public class DbUsuario
    {
        [Key]
        public int id { get; set; }
        public string? nome { get; set; }
        public string? login { get; set; }
        public string? senha { get; set; }   
        public string? status { get; set; }
    }
}

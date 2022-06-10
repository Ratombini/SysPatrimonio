using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysPatrimonio.Models
{
    [Table("locais", Schema = "syspatrimonio")]
    public class DbLocal
    {
        [Key]
        public int id { get; set; }
        public string? nomelocal { get; set; }
        public string? descricaolocal { get; set; }
    }
}

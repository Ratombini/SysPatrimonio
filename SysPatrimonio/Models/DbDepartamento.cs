using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysPatrimonio.Models
{
    [Table("departamentos", Schema = "syspatrimonio")]
    public class DbDepartamento
    {
        [Key]
        public int id { get; set; }
        public string? nomedepartamento { get; set; }
        public string? descricaodepartamento { get; set; }
        public int? idlocal { get; set; }

    }
}

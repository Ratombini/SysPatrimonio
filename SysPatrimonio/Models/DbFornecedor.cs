using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysPatrimonio.Models
{
    [Table("fornecedores", Schema = "syspatrimonio")]
    public class DbFornecedor
    {
        [Key]
        public int id { get; set; }
        public string? nomefornecedor { get; set; }
        public string? endereco { get; set; }
        public string? fone { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovilAsis.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Matricula { get; set; }
        [StringLength(100)]
        [Required]
        public string Contraseña { get; set; }
        [Required]
        public string Grupo { get; set; }
        public int AsistenciaId { get; set; }
    }


    public class UsuarioLogin
    {
        public int Id { get; set; }
        [Required]
        public string Matricula { get; set; }
        [StringLength(25)]
        [Required]
        public string Contraseña { get; set; }
    }

    public class UsuarioLoginDTO
    {
        public int Id { get; set; }
        public string Rol { get; set; }
    }

}
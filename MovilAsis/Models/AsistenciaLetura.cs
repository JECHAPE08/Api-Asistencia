using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace MovilAsis.Models
{
    public class Asistencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FechaEntrada { get; set; }
        public int FechaSalida { get; set; }
        public double LatitudEntrada { get; set; }
        public double LongitudEntrada { get; set; }
        public double LatitudSalida { get; set; }
        public double LongitudSalida { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }


    public class AsistenciaDelDiaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Matricula { get; set; }
        public string Grupo { get; set; }
        public int FechaEntrada { get; set; }
        public double LatitudEntrada { get; set; }
        public double LongitudEntrada { get; set; }
        public bool UbicacionValida => Global.VerificarUbicacionEnRango(LatitudEntrada, LongitudEntrada);
        public DateTime FechaEntradaDateTime => Global.ConvertirFecha(FechaEntrada);

    }

    public class AsistenciasMes
    {
        public int Id { get; set; }
        public int FechaEntrada { get; set; }
        public double LatitudEntrada { get; set; }
        public double LongitudEntrada { get; set; }
        public DateTime FechaEntradaDateTime => Global.ConvertirFecha(FechaEntrada);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaGestionEstudiantes
{
    public class EstudiantePresencial : Estudiante
    {
        // Propiedades específicas
        public string Aula { get; set; }
        public string Horario { get; set; }

        // Constructor
        public EstudiantePresencial(string matricula, string nombre, string apellido,
                                   string aula, string horario)
            : base(matricula, nombre, apellido)
        {
            Aula = aula;
            Horario = horario;
        }

        // Implementación del método abstracto (Polimorfismo)
        public override string ObtenerTipoEstudiante()
        {
            return "Presencial";
        }

        // Sobreescritura del método virtual (Polimorfismo)
        public override string MostrarInformacion()
        {
            return base.MostrarInformacion() +
                   $"\nAula: {Aula}" +
                   $"\nHorario: {Horario}";
        }
    }
}

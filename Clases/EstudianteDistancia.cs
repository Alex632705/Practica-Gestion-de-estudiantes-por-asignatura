using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaGestionEstudiantes
{
    public class EstudianteDistancia : Estudiante
    {
        // Propiedades específicas
        public string PlataformaVirtual { get; set; }
        public string Email { get; set; }

        // Constructor
        public EstudianteDistancia(string matricula, string nombre, string apellido,
                                  string plataformaVirtual, string email)
            : base(matricula, nombre, apellido)
        {
            PlataformaVirtual = plataformaVirtual;
            Email = email;
        }

        // Implementación del método abstracto (Polimorfismo)
        public override string ObtenerTipoEstudiante()
        {
            return "A Distancia";
        }

        // Sobreescritura del método virtual (Polimorfismo)
        public override string MostrarInformacion()
        {
            return base.MostrarInformacion() +
                   $"\nPlataforma: {PlataformaVirtual}" +
                   $"\nEmail: {Email}";
        }
    }
}

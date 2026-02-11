using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaGestionEstudiantes
{
    public class Docente
    {
        // Propiedades
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        // Encapsulamiento
        private Asignatura[] asignaturas;
        private int cantidadAsignaturas;
        private const int MAXIMO_ASIGNATURAS = 5;

        // Constructor
        public Docente(string cedula, string nombre, string apellido, string email)
        {
            Cedula = cedula;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            asignaturas = new Asignatura[MAXIMO_ASIGNATURAS];
            cantidadAsignaturas = 0;
        }

        // Agregar asignatura
        public OperationResult AgregarAsignatura(Asignatura asignatura)
        {
            if (asignatura == null)
                return new OperationResult(false, "La asignatura no puede ser nula");

            if (cantidadAsignaturas >= MAXIMO_ASIGNATURAS)
                return new OperationResult(false, "Máximo de asignaturas alcanzado");

            // Verificar código duplicado
            for (int i = 0; i < cantidadAsignaturas; i++)
            {
                if (asignaturas[i].Codigo == asignatura.Codigo)
                    return new OperationResult(false, "Código de asignatura ya existe");
            }

            asignaturas[cantidadAsignaturas] = asignatura;
            cantidadAsignaturas++;
            return new OperationResult(true, "Asignatura agregada");
        }

        // Buscar asignatura por código
        public OperationResult BuscarAsignatura(string codigo)
        {
            for (int i = 0; i < cantidadAsignaturas; i++)
            {
                if (asignaturas[i].Codigo == codigo)
                    return new OperationResult(true, "Encontrada", asignaturas[i]);
            }
            return new OperationResult(false, "No encontrada");
        }

        // Obtener todas las asignaturas
        public Asignatura[] ObtenerAsignaturas()
        {
            Asignatura[] resultado = new Asignatura[cantidadAsignaturas];
            for (int i = 0; i < cantidadAsignaturas; i++)
                resultado[i] = asignaturas[i];
            return resultado;
        }

        // Generar reporte completo
        public string GenerarReporteCompleto()
        {
            string reporte = $"\n╔══════════════════════════════════════════════╗\n";
            reporte += $"  DOCENTE: {Nombre} {Apellido}\n";
            reporte += $"  Cédula: {Cedula}\n";
            reporte += $"  Email: {Email}\n";
            reporte += $"╚══════════════════════════════════════════════╝\n";
            reporte += $"Total de asignaturas: {cantidadAsignaturas}\n\n";

            if (cantidadAsignaturas == 0)
            {
                reporte += "No tiene asignaturas asignadas.\n";
                return reporte;
            }

            for (int i = 0; i < cantidadAsignaturas; i++)
            {
                reporte += asignaturas[i].GenerarReporte();
            }

            return reporte;
        }
    }
}

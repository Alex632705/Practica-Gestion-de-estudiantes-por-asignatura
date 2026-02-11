using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaGestionEstudiantes
{
    public class Grupo
    {
        // Propiedades
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        // Encapsulamiento - datos privados
        private Estudiante[] estudiantes;
        private int cantidadEstudiantes;
        private const int CAPACIDAD_MAXIMA = 50;

        // Constructor
        public Grupo(string codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
            estudiantes = new Estudiante[CAPACIDAD_MAXIMA];
            cantidadEstudiantes = 0;
        }

        // Agregar estudiante
        public OperationResult AgregarEstudiante(Estudiante estudiante)
        {
            if (estudiante == null)
                return new OperationResult(false, "El estudiante no puede ser nulo");

            if (cantidadEstudiantes >= CAPACIDAD_MAXIMA)
                return new OperationResult(false, "Capacidad máxima alcanzada");

            // Verificar matrícula duplicada
            for (int i = 0; i < cantidadEstudiantes; i++)
            {
                if (estudiantes[i].Matricula == estudiante.Matricula)
                    return new OperationResult(false, "Matrícula ya existe");
            }

            estudiantes[cantidadEstudiantes] = estudiante;
            cantidadEstudiantes++;
            return new OperationResult(true, "Estudiante agregado");
        }

        // Buscar estudiante por matrícula
        public OperationResult BuscarEstudiante(string matricula)
        {
            for (int i = 0; i < cantidadEstudiantes; i++)
            {
                if (estudiantes[i].Matricula == matricula)
                    return new OperationResult(true, "Encontrado", estudiantes[i]);
            }
            return new OperationResult(false, "No encontrado");
        }

        // Calcular porcentaje de aprobados
        public double CalcularPorcentajeAprobados()
        {
            if (cantidadEstudiantes == 0) return 0;

            int aprobados = 0;
            for (int i = 0; i < cantidadEstudiantes; i++)
            {
                if (estudiantes[i].EstaAprobado())
                    aprobados++;
            }

            return (aprobados * 100.0) / cantidadEstudiantes;
        }

        // Obtener todos los estudiantes
        public Estudiante[] ObtenerEstudiantes()
        {
            Estudiante[] resultado = new Estudiante[cantidadEstudiantes];
            for (int i = 0; i < cantidadEstudiantes; i++)
                resultado[i] = estudiantes[i];
            return resultado;
        }

        // Generar reporte del grupo
        public string GenerarReporte()
        {
            string reporte = $"\n╔══════════════════════════════════════════════╗\n";
            reporte += $"  GRUPO: {Codigo} - {Nombre}\n";
            reporte += $"╚══════════════════════════════════════════════╝\n";
            reporte += $"Estudiantes: {cantidadEstudiantes}\n";
            reporte += $"Aprobados: {CalcularPorcentajeAprobados():F2}%\n\n";

            if (cantidadEstudiantes == 0)
            {
                reporte += "No hay estudiantes registrados.\n";
                return reporte;
            }

            for (int i = 0; i < cantidadEstudiantes; i++)
            {
                reporte += $"\n{i + 1}. {estudiantes[i].Matricula} - {estudiantes[i].Nombre} {estudiantes[i].Apellido}\n";
                reporte += $"   {estudiantes[i].ObtenerTipoEstudiante()}\n";
                reporte += $"   Promedio: {estudiantes[i].CalcularPromedio():F2} ";
                reporte += $"({(estudiantes[i].EstaAprobado() ? "APROBADO" : "REPROBADO")})\n";
            }

            return reporte;
        }
    }
}

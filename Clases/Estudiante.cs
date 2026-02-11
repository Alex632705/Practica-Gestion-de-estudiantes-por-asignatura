using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaGestionEstudiantes
{
    public abstract class Estudiante
    {
        // Propiedades públicas
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        // Datos privados (encapsulamiento)
        private double[] calificaciones;
        private int cantidadCalificaciones;

        // Constructor protegido para clases derivadas
        protected Estudiante(string matricula, string nombre, string apellido)
        {
            Matricula = matricula;
            Nombre = nombre;
            Apellido = apellido;
            calificaciones = new double[10];
            cantidadCalificaciones = 0;
        }

        // Método para agregar calificación
        public OperationResult AgregarCalificacion(double calificacion)
        {
            if (calificacion < 0 || calificacion > 100)
                return new OperationResult(false, "La calificación debe estar entre 0 y 100");

            if (cantidadCalificaciones >= 10)
                return new OperationResult(false, "Máximo 10 calificaciones alcanzado");

            calificaciones[cantidadCalificaciones] = calificacion;
            cantidadCalificaciones++;
            return new OperationResult(true, "Calificación agregada");
        }

        // Calcular promedio
        public double CalcularPromedio()
        {
            if (cantidadCalificaciones == 0) return 0;

            double suma = 0;
            for (int i = 0; i < cantidadCalificaciones; i++)
                suma += calificaciones[i];

            return suma / cantidadCalificaciones;
        }

        // Verificar si está aprobado (≥70)
        public bool EstaAprobado()
        {
            return CalcularPromedio() >= 70;
        }

        // Obtener lista de calificaciones
        public double[] ObtenerCalificaciones()
        {
            double[] resultado = new double[cantidadCalificaciones];
            for (int i = 0; i < cantidadCalificaciones; i++)
                resultado[i] = calificaciones[i];
            return resultado;
        }

        // Método abstracto - Polimorfismo
        public abstract string ObtenerTipoEstudiante();

        // Método virtual - Polimorfismo (puede ser sobreescrito)
        public virtual string MostrarInformacion()
        {
            return $"Matrícula: {Matricula}\n" +
                   $"Nombre: {Nombre} {Apellido}\n" +
                   $"Tipo: {ObtenerTipoEstudiante()}\n" +
                   $"Promedio: {CalcularPromedio():F2}\n" +
                   $"Estado: {(EstaAprobado() ? "APROBADO" : "REPROBADO")}";
        }
    }
}

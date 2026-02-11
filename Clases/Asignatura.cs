using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaGestionEstudiantes
{
    public class Asignatura
    {
        // Propiedades
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Creditos { get; set; }

        // Encapsulamiento
        private Grupo[] grupos;
        private int cantidadGrupos;
        private const int MAXIMO_GRUPOS = 10;

        // Constructor
        public Asignatura(string codigo, string nombre, int creditos)
        {
            Codigo = codigo;
            Nombre = nombre;
            Creditos = creditos;
            grupos = new Grupo[MAXIMO_GRUPOS];
            cantidadGrupos = 0;
        }

        // Agregar grupo
        public OperationResult AgregarGrupo(Grupo grupo)
        {
            if (grupo == null)
                return new OperationResult(false, "El grupo no puede ser nulo");

            if (cantidadGrupos >= MAXIMO_GRUPOS)
                return new OperationResult(false, "Máximo de grupos alcanzado");

            // Verificar código duplicado
            for (int i = 0; i < cantidadGrupos; i++)
            {
                if (grupos[i].Codigo == grupo.Codigo)
                    return new OperationResult(false, "Código de grupo ya existe");
            }

            grupos[cantidadGrupos] = grupo;
            cantidadGrupos++;
            return new OperationResult(true, "Grupo agregado");
        }

        // Buscar grupo por código
        public OperationResult BuscarGrupo(string codigo)
        {
            for (int i = 0; i < cantidadGrupos; i++)
            {
                if (grupos[i].Codigo == codigo)
                    return new OperationResult(true, "Encontrado", grupos[i]);
            }
            return new OperationResult(false, "No encontrado");
        }

        // Obtener todos los grupos
        public Grupo[] ObtenerGrupos()
        {
            Grupo[] resultado = new Grupo[cantidadGrupos];
            for (int i = 0; i < cantidadGrupos; i++)
                resultado[i] = grupos[i];
            return resultado;
        }

        // Generar reporte de la asignatura
        public string GenerarReporte()
        {
            string reporte = $"\n╔══════════════════════════════════════════════╗\n";
            reporte += $"  ASIGNATURA: {Codigo} - {Nombre}\n";
            reporte += $"  Créditos: {Creditos}\n";
            reporte += $"╚══════════════════════════════════════════════╝\n";
            reporte += $"Total de grupos: {cantidadGrupos}\n\n";

            if (cantidadGrupos == 0)
            {
                reporte += "No hay grupos registrados.\n";
                return reporte;
            }

            for (int i = 0; i < cantidadGrupos; i++)
            {
                reporte += grupos[i].GenerarReporte();
            }

            return reporte;
        }
    }
}

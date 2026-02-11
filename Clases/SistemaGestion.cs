using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaGestionEstudiantes
{
    public class SistemaGestion
    {
        static Docente docente = null!;

        public void Ejecutar()
        {
            Console.Clear();
            InicializarDocente();
            MostrarMenuPrincipal();
        }
        static void InicializarDocente()
        {
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║  ◉_◉ SISTEMA DE GESTIÓN DE ESTUDIANTES ◉_◉ ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");
            Console.Write("Cédula: ");
            string cedula = Console.ReadLine()!;
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()!;
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine()!;
            Console.Write("Email: ");
            string email = Console.ReadLine()!;

            docente = new Docente(cedula, nombre, apellido, email);
            Console.WriteLine($"\n¡Bienvenido/a, Prof. {nombre} {apellido}!");
            Pausa();
        }

        static void MostrarMenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("═══ MENÚ PRINCIPAL ═══\n");
                Console.WriteLine("1. Gestionar Asignaturas");
                Console.WriteLine("2. Gestionar Grupos");
                Console.WriteLine("3. Gestionar Estudiantes");
                Console.WriteLine("4. Registrar Calificaciones");
                Console.WriteLine("5. Generar Reportes");
                Console.WriteLine("6. Salir\n");
                Console.Write("Opción: ");

                switch (Console.ReadLine())
                {
                    case "1": MenuAsignaturas(); break;
                    case "2": MenuGrupos(); break;
                    case "3": MenuEstudiantes(); break;
                    case "4": MenuCalificaciones(); break;
                    case "5": MenuReportes(); break;
                    case "6": return;
                    default: Mensaje("Opción inválida", false); break;
                }
            }
        }

        static void MenuAsignaturas()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("═══ ASIGNATURAS ═══\n");
                Console.WriteLine("1. Crear asignatura");
                Console.WriteLine("2. Listar asignaturas");
                Console.WriteLine("3. Volver\n");
                Console.Write("Opción: ");

                switch (Console.ReadLine())
                {
                    case "1": CrearAsignatura(); break;
                    case "2": ListarAsignaturas(); break;
                    case "3": return;
                }
            }
        }

        static void CrearAsignatura()
        {
            Console.Clear();
            Console.WriteLine("═══ CREAR ASIGNATURA ═══\n");
            Console.Write("Código: ");
            string codigo = Console.ReadLine()!;
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()!;
            Console.Write("Créditos: ");
            int creditos = int.Parse(Console.ReadLine()!);

            var asignatura = new Asignatura(codigo, nombre, creditos);
            var resultado = docente.AgregarAsignatura(asignatura);
            Mensaje(resultado.Message, resultado.Success);
        }

        static void ListarAsignaturas()
        {
            Console.Clear();
            Console.WriteLine("═══ ASIGNATURAS ═══\n");
            var asignaturas = docente.ObtenerAsignaturas();

            if (asignaturas.Length == 0)
                Console.WriteLine("No hay asignaturas.\n");
            else
                for (int i = 0; i < asignaturas.Length; i++)
                    Console.WriteLine($"{i + 1}. {asignaturas[i].Codigo} - {asignaturas[i].Nombre}");

            Pausa();
        }

        static void MenuGrupos()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("═══ GRUPOS ═══\n");
                Console.WriteLine("1. Crear grupo");
                Console.WriteLine("2. Listar grupos");
                Console.WriteLine("3. Volver\n");
                Console.Write("Opción: ");

                switch (Console.ReadLine())
                {
                    case "1": CrearGrupo(); break;
                    case "2": ListarGrupos(); break;
                    case "3": return;
                }
            }
        }

        static void CrearGrupo()
        {
            var asignatura = SeleccionarAsignatura();
            if (asignatura == null) return;

            Console.Write("\nCódigo del grupo: ");
            string codigo = Console.ReadLine()!;
            Console.Write("Nombre del grupo: ");
            string nombre = Console.ReadLine()!;

            var grupo = new Grupo(codigo, nombre);
            var resultado = asignatura.AgregarGrupo(grupo);
            Mensaje(resultado.Message, resultado.Success);
        }

        static void ListarGrupos()
        {
            var asignatura = SeleccionarAsignatura();
            if (asignatura == null) return;

            Console.WriteLine($"\n═══ GRUPOS DE {asignatura.Nombre} ═══\n");
            var grupos = asignatura.ObtenerGrupos();

            if (grupos.Length == 0)
                Console.WriteLine("No hay grupos.\n");
            else
                for (int i = 0; i < grupos.Length; i++)
                    Console.WriteLine($"{i + 1}. {grupos[i].Codigo} - {grupos[i].Nombre}");

            Pausa();
        }

        static void MenuEstudiantes()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("═══ ESTUDIANTES ═══\n");
                Console.WriteLine("1. Agregar estudiante presencial");
                Console.WriteLine("2. Agregar estudiante a distancia");
                Console.WriteLine("3. Listar estudiantes");
                Console.WriteLine("4. Volver\n");
                Console.Write("Opción: ");

                switch (Console.ReadLine())
                {
                    case "1": AgregarEstudiantePresencial(); break;
                    case "2": AgregarEstudianteDistancia(); break;
                    case "3": ListarEstudiantes(); break;
                    case "4": return;
                }
            }
        }

        static void AgregarEstudiantePresencial()
        {
            var grupo = SeleccionarGrupo();
            if (grupo == null) return;

            Console.Write("\nMatrícula: ");
            string matricula = Console.ReadLine()!;
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()!;
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine()!;
            Console.Write("Aula: ");
            string aula = Console.ReadLine()!;
            Console.Write("Horario: ");
            string horario = Console.ReadLine()!;

            var estudiante = new EstudiantePresencial(matricula, nombre, apellido, aula, horario);
            var resultado = grupo.AgregarEstudiante(estudiante);
            Mensaje(resultado.Message, resultado.Success);
        }

        static void AgregarEstudianteDistancia()
        {
            var grupo = SeleccionarGrupo();
            if (grupo == null) return;

            Console.Write("\nMatrícula: ");
            string matricula = Console.ReadLine()!;
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine()!;
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine()!;
            Console.Write("Plataforma: ");
            string plataforma = Console.ReadLine()!;
            Console.Write("Email: ");
            string email = Console.ReadLine()!;

            var estudiante = new EstudianteDistancia(matricula, nombre, apellido, plataforma, email);
            var resultado = grupo.AgregarEstudiante(estudiante);
            Mensaje(resultado.Message, resultado.Success);
        }

        static void ListarEstudiantes()
        {
            var grupo = SeleccionarGrupo();
            if (grupo == null) return;

            Console.WriteLine($"\n═══ ESTUDIANTES DEL GRUPO {grupo.Codigo} ═══\n");
            var estudiantes = grupo.ObtenerEstudiantes();

            if (estudiantes.Length == 0)
                Console.WriteLine("No hay estudiantes.\n");
            else
                for (int i = 0; i < estudiantes.Length; i++)
                    Console.WriteLine($"\n{i + 1}. {estudiantes[i].MostrarInformacion()}\n");

            Pausa();
        }

        static void MenuCalificaciones()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("═══ CALIFICACIONES ═══\n");
                Console.WriteLine("1. Registrar calificación");
                Console.WriteLine("2. Volver\n");
                Console.Write("Opción: ");

                switch (Console.ReadLine())
                {
                    case "1": RegistrarCalificacion(); break;
                    case "2": return;
                }
            }
        }

        static void RegistrarCalificacion()
        {
            var grupo = SeleccionarGrupo();
            if (grupo == null) return;

            Console.Write("\nMatrícula: ");
            string matricula = Console.ReadLine()!;

            var busqueda = grupo.BuscarEstudiante(matricula);
            if (!busqueda.Success)
            {
                Mensaje(busqueda.Message, false);
                return;
            }

            Estudiante estudiante = (Estudiante)busqueda.Data;
            Console.Write("Calificación (0-100): ");
            double calificacion = double.Parse(Console.ReadLine()!);

            var resultado = estudiante.AgregarCalificacion(calificacion);
            Mensaje(resultado.Message, resultado.Success);
        }

        static void MenuReportes()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("═══ REPORTES ═══\n");
                Console.WriteLine("1. Reporte de grupo");
                Console.WriteLine("2. Reporte de asignatura");
                Console.WriteLine("3. Reporte completo");
                Console.WriteLine("4. Porcentaje de aprobados");
                Console.WriteLine("5. Volver\n");
                Console.Write("Opción: ");

                switch (Console.ReadLine())
                {
                    case "1": ReporteGrupo(); break;
                    case "2": ReporteAsignatura(); break;
                    case "3": ReporteCompleto(); break;
                    case "4": PorcentajeAprobados(); break;
                    case "5": return;
                }
            }
        }

        static void ReporteGrupo()
        {
            var grupo = SeleccionarGrupo();
            if (grupo == null) return;
            Console.Clear();
            Console.WriteLine(grupo.GenerarReporte());
            Pausa();
        }

        static void ReporteAsignatura()
        {
            var asignatura = SeleccionarAsignatura();
            if (asignatura == null) return;
            Console.Clear();
            Console.WriteLine(asignatura.GenerarReporte());
            Pausa();
        }

        static void ReporteCompleto()
        {
            Console.Clear();
            Console.WriteLine(docente.GenerarReporteCompleto());
            Pausa();
        }

        static void PorcentajeAprobados()
        {
            var grupo = SeleccionarGrupo();
            if (grupo == null) return;

            Console.Clear();
            Console.WriteLine($"\nGrupo: {grupo.Codigo} - {grupo.Nombre}");
            Console.WriteLine($"Aprobados: {grupo.CalcularPorcentajeAprobados():F2}%\n");
            Pausa();
        }

        // Métodos auxiliares
        static Asignatura SeleccionarAsignatura()
        {
            Console.Clear();
            var asignaturas = docente.ObtenerAsignaturas();

            if (asignaturas.Length == 0)
            {
                Mensaje("No hay asignaturas", false);
                return null!;
            }

            Console.WriteLine("═══ SELECCIONE ASIGNATURA ═══\n");
            for (int i = 0; i < asignaturas.Length; i++)
                Console.WriteLine($"{i + 1}. {asignaturas[i].Codigo} - {asignaturas[i].Nombre}");

            Console.Write("\nOpción: ");
            int opcion = int.Parse(Console.ReadLine()!) - 1;

            if (opcion < 0 || opcion >= asignaturas.Length)
            {
                Mensaje("Opción inválida", false);
                return null!;
            }

            return asignaturas[opcion];
        }

        static Grupo SeleccionarGrupo()
        {
            var asignatura = SeleccionarAsignatura();
            if (asignatura == null) return null!;

            var grupos = asignatura.ObtenerGrupos();

            if (grupos.Length == 0)
            {
                Mensaje("No hay grupos", false);
                return null!;
            }

            Console.WriteLine($"\n═══ GRUPOS DE {asignatura.Nombre} ═══\n");
            for (int i = 0; i < grupos.Length; i++)
                Console.WriteLine($"{i + 1}. {grupos[i].Codigo} - {grupos[i].Nombre}");

            Console.Write("\nOpción: ");
            int opcion = int.Parse(Console.ReadLine()!) - 1;

            if (opcion < 0 || opcion >= grupos.Length)
            {
                Mensaje("Opción inválida", false);
                return null!;
            }

            return grupos[opcion];
        }

        static void Mensaje(string texto, bool exito)
        {
            Console.WriteLine();
            Console.ForegroundColor = exito ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(exito ? $"✓ {texto}" : $"✗ {texto}");
            Console.ResetColor();
            Pausa();
        }

        static void Pausa()
        {
            Console.WriteLine("\nPresione una tecla...");
            Console.ReadKey();
        }
    }
}

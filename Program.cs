using System;
using System.IO;
using System.Threading.Tasks;

namespace LaboratorioAvengers
{
    class Program
    {
        private const string ARCHIVO_INVENTOS = "inventos.txt";
        private const string CARPETA_BACKUP = "Backup";
        private const string CARPETA_CLASIFICADOS = "ArchivosClasificados";
        private const string CARPETA_PROYECTOS = "ProyectosSecretos";
        private const string CARPETA_BASE = "LaboratorioAvengers";

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Gestion de Archivos de Tony Stark ===");

            // Asegurar que existe la carpeta base
            Directory.CreateDirectory(CARPETA_BASE);

            bool continuar = true;
            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();
                await ProcesarOpcion(opcion);

                Console.WriteLine("\n¿Deseas realizar otra operacion? (s/n)");
                continuar = Console.ReadLine().ToLower() == "s";
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\nSelecciona una opcion:");
            Console.WriteLine("1. Crear archivo de inventos");
            Console.WriteLine("2. Agregar nuevo invento");
            Console.WriteLine("3. Leer archivo linea por linea");
            Console.WriteLine("4. Leer todo el archivo");
            Console.WriteLine("5. Crear copia de seguridad");
            Console.WriteLine("6. Mover archivo a clasificados");
            Console.WriteLine("7. Crear carpeta de proyectos");
            Console.WriteLine("8. Listar archivos");
            Console.WriteLine("9. Eliminar archivo");
            Console.WriteLine("0. Salir");
        }

        static async Task ProcesarOpcion(string opcion)
        {
            try
            {
                switch (opcion)
                {
                    case "1":
                        await CrearArchivo();
                        break;
                    case "2":
                        Console.WriteLine("Ingresa el nombre del nuevo invento:");
                        string invento = Console.ReadLine();
                        await AgregarInvento(invento);
                        break;
                    case "3":
                        await LeerLineaPorLinea();
                        break;
                    case "4":
                        await LeerTodoElTexto();
                        break;
                    case "5":
                        await CopiarArchivo();
                        break;
                    case "6":
                        await MoverArchivo();
                        break;
                    case "7":
                        CrearCarpeta();
                        break;
                    case "8":
                        ListarArchivos();
                        break;
                    case "9":
                        await EliminarArchivo();
                        break;
                    default:
                        Console.WriteLine("Opcion no valida");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"¡Error! {ex.Message}");
            }
        }

        static async Task CrearArchivo()
        {
            string rutaArchivo = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);

            if (File.Exists(rutaArchivo))
            {
                Console.WriteLine("El archivo ya existe. ¿Deseas sobrescribirlo? (s/n)");
                if (Console.ReadLine().ToLower() != "s")
                    return;
            }

            await File.WriteAllTextAsync(rutaArchivo,
                "1. Traje Mark I\n" +
                "2. Reactor Arc\n" +
                "3. Inteligencia Artificial JARVIS\n");

            Console.WriteLine("Archivo creado exitosamente");
        }

        static async Task AgregarInvento(string invento)
        {
            string rutaArchivo = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);

            if (!File.Exists(rutaArchivo))
            {
                throw new FileNotFoundException("El archivo 'inventos.txt' no existe. ¡Ultron debe haberlo borrado!");
            }

            // Contar lineas existentes para el nuevo numero
            int numLineas = File.ReadAllLines(rutaArchivo).Length;
            string nuevaLinea = $"{numLineas + 1}. {invento}\n";

            await File.AppendAllTextAsync(rutaArchivo, nuevaLinea);
            Console.WriteLine("Invento agregado exitosamente");
        }

        static async Task LeerLineaPorLinea()
        {
            string rutaArchivo = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);

            if (!File.Exists(rutaArchivo))
            {
                throw new FileNotFoundException("El archivo 'inventos.txt' no existe. ¡Ultron debe haberlo borrado!");
            }

            Console.WriteLine("\nInventos registrados:");
            string[] lineas = await File.ReadAllLinesAsync(rutaArchivo);
            foreach (string linea in lineas)
            {
                Console.WriteLine(linea);
                await Task.Delay(500); // Pequena pausa para efecto dramatico
            }
        }

        static async Task LeerTodoElTexto()
        {
            string rutaArchivo = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);

            if (!File.Exists(rutaArchivo))
            {
                throw new FileNotFoundException("El archivo 'inventos.txt' no existe. ¡Ultron debe haberlo borrado!");
            }

            string contenido = await File.ReadAllTextAsync(rutaArchivo);
            Console.WriteLine("\nContenido completo del archivo:");
            Console.WriteLine(contenido);
        }

        static async Task CopiarArchivo()
        {
            string rutaOrigen = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);
            Directory.CreateDirectory(Path.Combine(CARPETA_BASE, CARPETA_BACKUP));
            string rutaDestino = Path.Combine(CARPETA_BASE, CARPETA_BACKUP, ARCHIVO_INVENTOS);

            if (!File.Exists(rutaOrigen))
            {
                throw new FileNotFoundException("El archivo 'inventos.txt' no existe. ¡Ultron debe haberlo borrado!");
            }

            File.Copy(rutaOrigen, rutaDestino, true);
            Console.WriteLine("Copia de seguridad creada exitosamente");
        }

        static async Task MoverArchivo()
        {
            string rutaOrigen = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);
            Directory.CreateDirectory(Path.Combine(CARPETA_BASE, CARPETA_CLASIFICADOS));
            string rutaDestino = Path.Combine(CARPETA_BASE, CARPETA_CLASIFICADOS, ARCHIVO_INVENTOS);

            if (!File.Exists(rutaOrigen))
            {
                throw new FileNotFoundException("El archivo 'inventos.txt' no existe. ¡Ultron debe haberlo borrado!");
            }

            File.Move(rutaOrigen, rutaDestino, true);
            Console.WriteLine("Archivo movido exitosamente a clasificados");
        }

        static void CrearCarpeta()
        {
            string rutaCarpeta = Path.Combine(CARPETA_BASE, CARPETA_PROYECTOS);
            Directory.CreateDirectory(rutaCarpeta);
            Console.WriteLine($"Carpeta '{CARPETA_PROYECTOS}' creada exitosamente");
        }

        static void ListarArchivos()
        {
            if (!Directory.Exists(CARPETA_BASE))
            {
                Console.WriteLine("El laboratorio aun no ha sido creado");
                return;
            }

            Console.WriteLine("\nArchivos en el laboratorio:");
            string[] archivos = Directory.GetFiles(CARPETA_BASE, ".", SearchOption.AllDirectories);
            foreach (string archivo in archivos)
            {
                Console.WriteLine(archivo.Replace(CARPETA_BASE + Path.DirectorySeparatorChar, ""));
            }
        }

        static async Task EliminarArchivo()
        {
            string rutaArchivo = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);

            if (!File.Exists(rutaArchivo))
            {
                throw new FileNotFoundException("El archivo 'inventos.txt' no existe. ¡Ultron debe haberlo borrado!");
            }

            // Primero hacer backup
            await CopiarArchivo();

            File.Delete(rutaArchivo);
            Console.WriteLine("Archivo eliminado exitosamente (se ha creado una copia de seguridad)");
        }
    }
}
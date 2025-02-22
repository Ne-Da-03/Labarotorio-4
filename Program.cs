using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    private const string ARCHIVO_INVENTOS = "inventos.txt";
    private const string CARPETA_BACKUP = "Backup";
    private const string CARPETA_CLASIFICADOS = "ArchivosClasificados";
    private const string CARPETA_PROYECTOS = "ProyectosSecretos";
    private const string CARPETA_BASE = "LaboratorioAvengers";

    static async Task Main()
    {
        // Configuración de colores de fondo y texto en la consola
        Console.BackgroundColor = ConsoleColor.White;  // Fondo negro
        Console.ForegroundColor = ConsoleColor.Cyan;   // Texto en color cian
        Console.Clear();  // Aplica los cambios de color de fondo

        Directory.CreateDirectory(CARPETA_BASE);
        Directory.CreateDirectory(CARPETA_BACKUP);
        Directory.CreateDirectory(CARPETA_CLASIFICADOS);
        Directory.CreateDirectory(CARPETA_PROYECTOS);

        while (true)
        {
            MostrarMenu();
            string opcion = Console.ReadLine();
            await ProcesarOpcion(opcion);
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("1. Crear archivo");
        Console.WriteLine("2. Leer todo el contenido del archivo");
        Console.WriteLine("3. Leer archivo línea por línea");
        Console.WriteLine("4. Agregar invento");
        Console.WriteLine("5. Copiar archivo");
        Console.WriteLine("6. Mover archivo");
        Console.WriteLine("7. Eliminar archivo");
        Console.WriteLine("8. Listar archivos");
        Console.WriteLine("0. Salir");
    }

    static async Task ProcesarOpcion(string opcion)
    {
        switch (opcion)
        {
            case "1":
                CrearArchivo();
                break;
            case "2":
                LeerTodoElTexto();
                break;
            case "3":
                LeerLineaPorLinea();
                break;
            case "4":
                Console.Write("Ingrese un nuevo invento: ");
                string invento = Console.ReadLine();
                await AgregarInvento(invento);
                break;
            case "5":
                CopiarArchivo();
                break;
            case "6":
                MoverArchivo();
                break;
            case "7":
                EliminarArchivo();
                break;
            case "8":
                ListarArchivos();
                break;
            case "0":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }

    static void CrearArchivo()
    {
        try
        {
            string ruta = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);
            File.WriteAllText(ruta, "Inventos de Tony Stark:\n");
            Console.WriteLine("Archivo creado con éxito.");
        }
        catch (IOException e)
        {
            Console.WriteLine("Error de E/S: " + e.Message);
        }
    }

    static async Task AgregarInvento(string invento)
    {
        try
        {
            string ruta = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);
            if (!File.Exists(ruta)) throw new FileNotFoundException("El archivo no existe.");

            await File.AppendAllTextAsync(ruta, invento + "\n");
            Console.WriteLine("Invento agregado.");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (IOException e)
        {
            Console.WriteLine("Error de E/S: " + e.Message);
        }
    }

    static void LeerTodoElTexto()
    {
        try
        {
            string ruta = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);
            Console.WriteLine(File.ReadAllText(ruta));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("El archivo no existe.");
        }
        catch (IOException e)
        {
            Console.WriteLine("Error de E/S: " + e.Message);
        }
    }

    static void LeerLineaPorLinea()
    {
        try
        {
            string ruta = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);
            using (StreamReader reader = new StreamReader(ruta))
            {
                string linea;
                while ((linea = reader.ReadLine()) != null)
                {
                    Console.WriteLine(linea);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("El archivo no existe.");
        }
        catch (IOException e)
        {
            Console.WriteLine("Error de E/S: " + e.Message);
        }
    }

    static void CopiarArchivo()
    {
        try
        {
            string origen = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);
            string destino = Path.Combine(CARPETA_BACKUP, ARCHIVO_INVENTOS);
            File.Copy(origen, destino, true);
            Console.WriteLine("Archivo copiado correctamente.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("El archivo no existe.");
        }
        catch (IOException e)
        {
            Console.WriteLine("Error de E/S: " + e.Message);
        }
    }

    static void MoverArchivo()
    {
        try
        {
            string origen = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);
            string destino = Path.Combine(CARPETA_CLASIFICADOS, ARCHIVO_INVENTOS);
            File.Move(origen, destino);
            Console.WriteLine("Archivo movido correctamente.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("El archivo no existe.");
        }
        catch (IOException e)
        {
            Console.WriteLine("Error de E/S: " + e.Message);
        }
    }

    static void EliminarArchivo()
    {
        try
        {
            string ruta = Path.Combine(CARPETA_BASE, ARCHIVO_INVENTOS);
            File.Delete(ruta);
            Console.WriteLine("Archivo eliminado correctamente.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("El archivo no existe.");
        }
        catch (IOException e)
        {
            Console.WriteLine("Error de E/S: " + e.Message);
        }
    }

    static void ListarArchivos()
    {
        try
        {
            string[] archivos = Directory.GetFiles(CARPETA_BASE);
            Console.WriteLine("Archivos en " + CARPETA_BASE + ":");
            foreach (string archivo in archivos)
            {
                Console.WriteLine(Path.GetFileName(archivo));
            }
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("El directorio no existe.");
        }
        catch (IOException e)
        {
            Console.WriteLine("Error de E/S: " + e.Message);
        }
    }
}

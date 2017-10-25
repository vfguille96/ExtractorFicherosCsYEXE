using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Añadido
using System.IO;
using System.Threading;


namespace ExtraFicheros
{
    class Directorio
    {
        string rutaDirectoriosNuevos = string.Empty;//Directorios nuevos donde copiaremos los ficheros.     
        string rutaParaSubdirectorio = string.Empty;
        string[] arryRutasNuevas;
        Ruta mir = new Ruta();
        bool hayfichero = false;
        string[] ArrayRutasOriginales;

        string[] Arraynombreejecutables;
        string[] ArrayRutasOriginalesExe;//Array de rutas hasta los ejecutables .exe

        public string[] LecturaSubDirectorios()
        {
            string subdirectorioanadidofinal = string.Empty;//Variable usada para añadir el ultimo subdirectorio mas profundo antes de llegar a los ficheros     
            string rutatmp = string.Empty; //ruta temporal , para montar la ruta hacia los nuevos directorios que se van a crear iguales que los originale, excepto el raiz.
            rutaParaSubdirectorio = mir.ExtraeRuta();
            DirectoryInfo d = new DirectoryInfo(rutaParaSubdirectorio);
            DirectoryInfo[] directorios = d.GetDirectories();
            ArrayRutasOriginales = new string[directorios.Length];
            ArrayRutasOriginalesExe = new string[ArrayRutasOriginales.Length];
            arryRutasNuevas = new string[directorios.Length];
            string[] arryRutasNuevasTmp = new string[ArrayRutasOriginales.Length];//Array de rutas donde se han creado cada uno de los nuevos subdirectorios en el nuevo directorio Raiz.  
            Arraynombreejecutables = new string[ArrayRutasOriginales.Length];
          
            for (int i = 0; i < directorios.Length; i++)
            {
                ArrayRutasOriginales[i] = rutaParaSubdirectorio + "\\" + directorios[i];//Monta y guarda todas las rutas des de la Raiz hasta cada uno de los subdirectorios.
               
            }


         

           
            
                    //---CODIGO  DE CREADO DIRECTORIOS --//             
             
            //Crea los nuevos Subdirectorios...(No se realizo en su metodo, ya que necesitamos  la longitud del array declarado en este."
            rutatmp = CreaDirectorios();
            for (int i = 0; i < directorios.Length; i++)
            {
                string apoyoruta = rutatmp;//Variable para conservar la ruta original
                apoyoruta += Path.DirectorySeparatorChar.ToString() + directorios[i].ToString();
                Directory.CreateDirectory(apoyoruta);
                arryRutasNuevasTmp[i] = apoyoruta;//Guardamos las nuevas rutas, con los nuevos subdirectorios del nuevo directorio Raiz.
                arryRutasNuevas[i] = arryRutasNuevasTmp[i];//guardo las nuevas rutas en otro Array para intentar sacarlo tambien del Metodo.           
            }



            /*-------------------------------------*/
           
            

            //------------------------Remontando rutas originales finales "relacionejercicios\ejercicio1\app_reloj--------------------
            Console.WriteLine("\n --------------------------LISTADO DIRECTORIOS -------------------------");//POSIBLE METODO LLAMADO "BUSQUEDA PROFUNDA"
            for (int i = 0; i < ArrayRutasOriginales.Length; i++)
            {
                string tmp = string.Empty;

                
              
                    DirectoryInfo directorio2 = new DirectoryInfo(ArrayRutasOriginales[i]);
                    DirectoryInfo[] dEncontrados = directorio2.GetDirectories();
                    do
                    {
                        if (dEncontrados[0].ToString() != "")
                        {
                            //Este buble comprueba que en el nivel de la ruta en el que se encuentra hay un fichero ".cs o .csproj" si no es asi busca otro subdirectorio y avanza hasta encontrarlos.

                            do
                            {
                                subdirectorioanadidofinal = dEncontrados[0].ToString();
                                tmp = ArrayRutasOriginales[i];
                                tmp += Path.DirectorySeparatorChar.ToString() + subdirectorioanadidofinal;
                                ArrayRutasOriginales[i] = tmp;
                                Fichero mifichero = new Fichero();
                                hayfichero = mifichero.CompruebaFichero(ArrayRutasOriginales[i]);
                            } while (hayfichero == false);

                            Console.WriteLine("[{0}] {1} ", i, ArrayRutasOriginales[i].ToString() + "\n");
                        }
                    } while (dEncontrados[0].ToString() == "");
                           
            }           
            Console.WriteLine("\n --------------------------FIN LISTADO -------------------------");            
            return ArrayRutasOriginales;
        }

        public string CreaDirectorios()
        {
            string datosRutaSubdirectNuevos = string.Empty;
            string nombreDirectorioNuevo = string.Empty;
            Console.WriteLine("\n-Dime la letra del volumen donde voy a crear el nuevo directorio con el contenido copiado.");
            datosRutaSubdirectNuevos = Console.ReadLine();
            rutaDirectoriosNuevos = datosRutaSubdirectNuevos + Path.VolumeSeparatorChar.ToString();

            Console.WriteLine("\n-Dime el nombre del nuevo directorio raiz a crear.\n\"Los Subdirectorios se llamaran igual que los de el directorio origen.\"");
            nombreDirectorioNuevo = Console.ReadLine();
            rutaDirectoriosNuevos += Path.DirectorySeparatorChar.ToString() + nombreDirectorioNuevo;
            Directory.CreateDirectory(rutaDirectoriosNuevos);

            Console.WriteLine("---------------Directorio \"" + nombreDirectorioNuevo + "\" creado!.-------------------");

            return rutaDirectoriosNuevos;
        }

        public string[] DevuelveRutasNuevas()
        {
            return arryRutasNuevas;
        }

        //Ficheros EXE (experimentando)
        public string[] DevuelveRutasOriginalesExe()//Devuelve las rutas originales pero hasta  los ejecutables".exe" que llaman igual que el proyecto 
        {
            string[] partesDeUnaRutaOriginal;
            string nombreejecutable = string.Empty;
            Arraynombreejecutables = new string[ArrayRutasOriginales.Length];
            for (int i = 0; i <ArrayRutasOriginales.Length ; i++)
            {
                partesDeUnaRutaOriginal = ArrayRutasOriginales[i].Split(Path.DirectorySeparatorChar);
                nombreejecutable = partesDeUnaRutaOriginal[partesDeUnaRutaOriginal.Length - 1];//Cogemos el ultimo de el array que sera el directorio con el nombre del proyecto (el mismo que del ejecutable final)
                Arraynombreejecutables[i] = partesDeUnaRutaOriginal[partesDeUnaRutaOriginal.Length - 1];
                ArrayRutasOriginalesExe[i] = ArrayRutasOriginales[i] + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug"+Path.DirectorySeparatorChar+nombreejecutable+".exe";
            }
            return ArrayRutasOriginalesExe;
        }

        public string[] DevuelveNombreProyectos()
        {
            return Arraynombreejecutables;
        }
    }
}

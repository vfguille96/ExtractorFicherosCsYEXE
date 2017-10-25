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
    class Fichero
    {
        int opcion = 0;
        Ruta miruta = new Ruta();
        Directorio miDirectorio = new Directorio();
        string[] arryRutasOriginales;
        string[] arrayRutasExe;
        string[] arryUnProyecto;//Donde guardaremos la ruta hacia cada fichero en un proyecto.
        string[] arryUnProyectoEXE;
        string[] arrayNombreProyectos;
        string[] Rutasnuevas;
        int contador = 0;
        int contadorExe = 0;

        /// <summary>
        /// Lee todos los ficheros  dentro de un directorio concreto.
        /// </summary>
        public void LecturaFicheros()
        {          
            try
            {                            
                arryRutasOriginales = miDirectorio.LecturaSubDirectorios();
                arrayRutasExe = miDirectorio.DevuelveRutasOriginalesExe();
                Rutasnuevas = miDirectorio.DevuelveRutasNuevas();//Guarda las nuevas rutas definidas por el usuario.
                
                for (int i = 0; i < arryRutasOriginales.Length; i++)
                {
                   
                    DirectoryInfo directorio = new DirectoryInfo(arryRutasOriginales[i]);
                    FileInfo[] fichero;
                  
                    fichero = directorio.GetFiles("*.cs");// Busca los "*.cs" 
                   
                    

                    Console.WriteLine("\n-Para la ruta : \n- {0}\n", arryRutasOriginales[i]);
                    for (int k = 0; k < fichero.Length; k++)
                    {

                       
                        arryUnProyecto = new string[fichero.Length];//Creamos un array con la longitud de la cantiadad de ficheros encontrados
                        Console.WriteLine("[{0}] Nombre fichero -> {1} .", k, fichero[k].Name.ToString());
                        string tmp = string.Empty;
                        tmp = fichero[k].ToString(); ;
                        tmp = fichero[k].FullName.ToString();
                        Console.WriteLine("\n\t\t---Fin Ficheros de esa Ruta ----\n");

                        if (fichero.LongLength != 0)//Si encontro algo
                        {
                            tmp = (fichero[k].FullName).ToString();
                            arryUnProyecto[k] = fichero[k].FullName.ToString();              

                            //POR AQUI *******
                            //Copia cada fichero ".cs " a su directorio correspondiente(Arreglar que aprezca con el mismo nombre que el oroginal).
                            FileInfo mifichero2 = new FileInfo(arryUnProyecto[k]);
                            mifichero2.CopyTo(Rutasnuevas[i]+Path.DirectorySeparatorChar+"program" + contador + ".cs");                           
                            contador++;                            
                            
                        }                    
                    }
                         
                }
            }
            catch
            {
                Console.WriteLine("\nOps!. Losiento pero no encontre tu ruta, o no es correcta.\n\n\t¿Quieres intentarlo de nuevo?.");
                Console.WriteLine("\n\tPulsa ENTER para continuar...");
                Console.ReadLine();
                MenuReintento();
                opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1://Si intentarlo de nuevo.
                        miruta.ExtraeRuta();
                        break;
                    case 2://No, salir del programa.
                        Console.WriteLine("\n\n\t\tBye!");
                        Thread.Sleep(1000);
                        break;
                }

            }

        }

        //POR AQUI ( ya no hay EXCEPCION extraña) pero SIGUE FALLANDO
        public void LecturaFicheroExe()
        {
            for (int i = 0; i < arryRutasOriginales.Length; i++)
            {

                arrayNombreProyectos = miDirectorio.DevuelveNombreProyectos();
                //Para ficheros EXE
                DirectoryInfo directorio2 = new DirectoryInfo(arryRutasOriginales[i] + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "Debug");
                FileInfo[] fichero2;
                fichero2 = directorio2.GetFiles(arrayNombreProyectos[i] + ".exe");// Busca los "*.exe" 

             
                #region ZONA A REVISAR o Sustitucion por Foreach
                
                for (int j = 0; j < fichero2.Length; j++)
                {


                    arryUnProyectoEXE = new string[fichero2.Length];//Creamos un array con la longitud de la cantiadad de ficheros encontrados
                    Console.WriteLine("[{0}] Nombre fichero EXE -> {1} .", j, fichero2[j].Name.ToString());
                    string tmp = string.Empty;
                    tmp = fichero2[j].ToString(); ;
                    tmp = fichero2[j].FullName.ToString();
                    Console.WriteLine("\n\t\t---Fin Ficheros EXE de esa Ruta ----\n");

                    if (fichero2.LongLength != 0)//Si encontro algo
                    {
                        tmp = (fichero2[j].FullName).ToString();
                        arryUnProyectoEXE[j] = fichero2[j].FullName.ToString();


                        FileInfo mifichero3 = new FileInfo(arrayRutasExe[i]);
                        mifichero3.CopyTo(Rutasnuevas[i] +  "ejecutable" + contadorExe);
                        contadorExe++;

                    }
                }
#endregion

            }
        
        }

        public bool CompruebaFichero(string ruta)
        {
            bool hayficheros;
            DirectoryInfo directorio = new DirectoryInfo(ruta);
            //arryRutasOriginales[i] = (directorio.GetDirectories()).ToString();//recargar el ultimo subfichero de la ruta
            if (directorio.GetFiles(".*cs").Length != 0 || directorio.GetFiles("*.csproj").Length != 0)
            {
                hayficheros = true;//Encontro esos ficheros.
            }
            else
            {
                hayficheros = false;//No encontro esos ficheros.
            }

            return hayficheros;
        }


        public void MenuReintento()
        {
            Console.Clear();
            Console.WriteLine("_____________________________________");
            Console.WriteLine("|                                   |");
            Console.WriteLine("|   ¿Intentarlo de nuevo?.          |");
            Console.WriteLine("______________________________________");
            Console.WriteLine("|                                   |");
            Console.WriteLine("|       1- Si.                      |");
            Console.WriteLine("|       2- No, Salir del programa.  |");
            Console.WriteLine("|                                   |");
            Console.WriteLine("|       Escoge una opcion:          |");
            Console.WriteLine("____________________________________");
            Console.SetCursorPosition(26, 8);

        }

        public string[] arrayRutaslesExe { get; set; }
    }
}

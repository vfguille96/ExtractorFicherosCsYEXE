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
    internal class Fichero
    {
        private int opcion = 0;
        private Ruta miruta = new Ruta();
        private Directorio miDirectorio = new Directorio();
        private string[] arryRutasOriginales;
        private string[] arrayRutasExe;
        private string[] arryUnProyecto;//Donde guardaremos la ruta hacia cada fichero en un proyecto.
        private string[] arrayNombreProyectos;
        private string[] Rutasnuevas;

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
                        Console.WriteLine("\n\t\t--- Fin Ficheros de la Ruta ----\n");

                        if (fichero.LongLength != 0)//Si encontro algo
                        {
                            tmp = (fichero[k].FullName).ToString();
                            arryUnProyecto[k] = fichero[k].FullName.ToString();
                            string nombreFichero = string.Empty;
                            nombreFichero = fichero[k].Name;

                            //POR AQUI *******
                            //Copia cada fichero ".cs " a su directorio correspondiente(Arreglar que aprezca con el mismo nombre que el oroginal).
                            FileInfo mifichero2 = new FileInfo(arryUnProyecto[k]);
                            mifichero2.CopyTo(Rutasnuevas[i] + Path.DirectorySeparatorChar + nombreFichero);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("\n¡Ops!. Lo sentimos. La ruta indicada no existe o no es correcta.\n\n\t¿Desea intentarlo de nuevo?.");
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

                #region Probando Foreach

                foreach (FileInfo fichero in fichero2)
                {
                    FileInfo mifichero3 = new FileInfo(fichero.FullName);
                    string combinacionRutasFinales = string.Empty;
                    combinacionRutasFinales = Rutasnuevas[i] + Path.DirectorySeparatorChar + arrayNombreProyectos[i] + ".exe";
                    File.Copy(mifichero3.ToString(), combinacionRutasFinales);
                }

                #endregion Probando Foreach
            }
        }

        public bool CompruebaFichero(string ruta)
        {
            bool hayficheros;
            DirectoryInfo directorio = new DirectoryInfo(ruta);
            //arryRutasOriginales[i] = (directorio.GetDirectories()).ToString();//recargar el ultimo subfichero de la ruta
            if (directorio.GetFiles("*.cs").Length != 0 || directorio.GetFiles("*.csproj").Length != 0)
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
            Console.WriteLine("|      ¿Intentarlo de nuevo?.       |");
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
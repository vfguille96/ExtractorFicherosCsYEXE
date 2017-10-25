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
    internal class Ruta
    {
        private string ruta;

        /// <summary>
        /// Monta la ruta a el volumen en el que vamos a buscar y la carpeta raiz.
        /// </summary>
        /// <returns>ruta</returns>
        public string ExtraeRuta()
        {
            string letraUnidad = string.Empty;
            string directorioRaiz = string.Empty;
            string direcRaizampliado = string.Empty;//Esta variable solo se usa en caso de añadir mas directorios a la ruta.

            Console.WriteLine(" \n-Indica la letra de la unidad a buscar la carpeta raíz.\n");
            letraUnidad = Console.ReadLine();
            Console.WriteLine("-Indica el nombre del directorio raíz a buscar.");
            directorioRaiz = Console.ReadLine();
            ruta = letraUnidad + Path.VolumeSeparatorChar.ToString() + Path.DirectorySeparatorChar.ToString() + directorioRaiz;

            do
            {
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("\tRuta: " + ruta);
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("\n\n\t-¿Desea añadir más subdirectorios a la ruta?.\n\tSi es así, escríbalo. Si no, pulsa la tecla Enter.\n");
                direcRaizampliado = Console.ReadLine();
                if (direcRaizampliado != "")
                    ruta += Path.DirectorySeparatorChar.ToString() + direcRaizampliado;
            } while (direcRaizampliado != "");

            Console.WriteLine("\n--------------- FIN MUESTRA DE RUTA -------------------\n");

            return ruta;
        }
    }
}
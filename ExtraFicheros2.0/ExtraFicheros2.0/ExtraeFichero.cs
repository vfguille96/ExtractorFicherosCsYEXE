using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraFicheros
{
    internal class ExtraeFichero
    {
        private static void Main(string[] args)
        {
            Fichero mifichero = new Fichero();
            mifichero.LecturaFicheros();
            mifichero.LecturaFicheroExe();
            Console.WriteLine("\n\t\t\t         FIN\n");
            Console.ReadLine();
        }
    }
}
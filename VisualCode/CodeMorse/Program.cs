using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using System.IO;

namespace CodeMorse
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            SoporteParaConfiguracion.CrearArchivoDeConfiguracion();
            path = SoporteParaConfiguracion.LeerArchivoDeConfiguracion();
            Console.Write(path);
            string[] archivos = Directory.GetFiles(".");
            string[] archivo;
            foreach (string s in archivos)
            {
                string aux = s.Substring(s.Length - (s.Length - 2));
                Console.WriteLine(aux);
                archivo = aux.Split(new char[] { '.' });
                if ((archivo[1] == "txt" || archivo[1] == "mp3"))
                {
                    string pathcopia = path + archivo[0] + "-copia." + archivo[1];
                    if (File.Exists(pathcopia) == true)
                    {
                        Console.Write("el archivo {0}-copia.{1} ya existe", archivo[0], archivo[1]);
                    }else
                    {
                        File.Copy(s, pathcopia);
                    }
                }
            }
            Console.Write("\ningrese linea a ser escrita en morse:");
            string text = Console.ReadLine();
            path = SoporteParaConfiguracion.LeerArchivoDeConfiguracion();
            path = path + @"Morse\";
            if(Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string pathmorse = path + "Morse_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt";
            StreamWriter writer = new StreamWriter(File.Open(pathmorse, FileMode.Create));
            writer.Write(ConversorDeMorse.TextoAMorse(text));
            writer.Close();
            ConversorDeMorse.MorseAAudio(pathmorse);
            StreamReader reader = new StreamReader(File.Open(pathmorse, FileMode.Open));
            text = reader.ReadLine();
            reader.Close();
            pathmorse = path + "Texto_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".txt";
            writer = new StreamWriter(File.Open(pathmorse, FileMode.Create));
            writer.Write(ConversorDeMorse.MorseATexto(text));
            writer.Close();
            Console.ReadKey();

        }
    }
}

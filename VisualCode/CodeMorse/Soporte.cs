using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
   public static class SoporteParaConfiguracion
    {
        static string path = @"C:\Users\Alumno\Desktop\TP09\Datos\";
        static string fileName = @"C:\Users\Alumno\Desktop\TP09\config.dat";
        public static void CrearArchivoDeConfiguracion()
        {
            BinaryWriter config = new BinaryWriter(File.Open(fileName, FileMode.Create));
            config.Write(path);
            config.Close();
        }
        public static string LeerArchivoDeConfiguracion()
        {
            BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open));
            string Linea = reader.ReadString();
            reader.Close();
            return Linea;
        }
    }
    public static class ConversorDeMorse
    {
        public static char[] letras = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static string[] morse = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "--.--", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
        public static string MorseATexto(string mor)
        {
            string text = "",mors;
            string[] morsecode = mor.Trim().Split(new char[] {' '});
            int i,k = 0;
            for (i = 0; i < morsecode.Length; i++)
            {
                mors = morsecode[i];
                while ( k < morse.Length && morse[k] != mors )
                {
                    k++;
                }

                if (k < morse.Length)
                {
                    text = text + letras[k];
                    
                }else
                {
                    text = text + " ";                    
                }
                k = 0;

            }
            return text;
        }
        public static string TextoAMorse(string text)
        {
            string mor = "";
            int i,k=0;
            char car;
            for (i = 0; i < text.Length;i++)
            {
                car = text[i];
                while(k < letras.Length && letras[k] != car)
                {
                    k++;
                }
                if (k < letras.Length)
                {
                    mor = mor +" " + morse[k];

                }
                else
                {
                    mor = mor + "  ";
                }
                k = 0;
            }
            return mor;
        }
        public static void MorseAAudio(string filepath)
        {
            byte[] punto;
            byte[] raya;
            byte[] silencio;
            string[] file = filepath.Split(new char[] {'.'});
            string filename = file[0] + ".mp3";
            FileStream Morse = new FileStream(filename, FileMode.Create);
            string puntopath = @"C:\Users\Alumno\Desktop\TP09\Datos\punto-copia.mp3";
            string rayapath = @"C:\Users\Alumno\Desktop\TP09\Datos\raya-copia.mp3";
            string silenciopath = @"C:\Users\Alumno\Desktop\TP09\Datos\silencio-copia.mp3";
            FileStream Punto = new FileStream(puntopath, FileMode.Open);
            FileStream Raya = new FileStream(rayapath, FileMode.Open);
            FileStream Silencio = new FileStream(silenciopath, FileMode.Open);
            punto = LectorCompletoDeBinario(Punto);
            raya = LectorCompletoDeBinario(Raya);
            silencio = LectorCompletoDeBinario(Silencio);
            Punto.Close();
            Raya.Close();
            Silencio.Close();
            StreamReader reader = new StreamReader(File.Open(filepath, FileMode.Open));
            string text = reader.ReadLine();
            reader.Close();
            List<byte> Listabytes = new List<byte>(); 
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '.':
                        Listabytes.AddRange(punto);
                        break;
                    case '-':
                        Listabytes.AddRange(raya);
                        break;
                    case ' ':
                        Listabytes.AddRange(silencio);
                        break;
                }
            }
            Morse.Write(Listabytes.ToArray(), 0, Listabytes.Count);
            Morse.Close();
        }
        public static byte[] LectorCompletoDeBinario(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream()) // creando un memory stream 
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length); // lee desde la última posición hasta el tamaño del buffer
                    if (read <= 0)
                        return ms.ToArray(); // convierte el stream en array 
                    ms.Write(buffer, 0, read); // graba en el stream 
                }
            }
        }
    }
}

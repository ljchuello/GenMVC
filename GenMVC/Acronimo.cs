using System;
using System.IO;
using Newtonsoft.Json;

namespace GenMVC
{
    public class Acronimo
    {
        public string ProyectoModelo { set; get; } = string.Empty;
        public string AcronimoModelo { set; get; } = string.Empty;
        public string ProyectoControlador { set; get; } = string.Empty;
        public string AcronimoControlador { set; get; } = string.Empty;
        public string ProyectoContenedor { set; get; } = string.Empty;

        private static string directorio = @"C:\GenMVC\";
        private static readonly string archivo = $"{directorio}\\acronimo.config";

        public static void Escribir(Acronimo sql)
        {
            try
            {
                // Validamos que exista u ndirectorio
                if (!Directory.Exists(directorio))
                {
                    // Creamos el directorio
                    Directory.CreateDirectory(directorio);
                }

                // Validamos que exista el archivo
                if (!File.Exists(archivo))
                {
                    // Creamos el archivos
                    File.WriteAllText(archivo, string.Empty);
                }

                // Escribimos en el archivoo
                File.WriteAllText(archivo, Fichero.ObjectToJson(sql));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static Acronimo Leer()
        {
            Acronimo acronimo = new Acronimo();

            try
            {
                // Validamos que exista un directorio
                if (!Directory.Exists(directorio))
                {
                    // Creamos el directorio
                    Directory.CreateDirectory(directorio);
                }

                // Validamos que exista el archivo
                if (!File.Exists(archivo))
                {
                    // Creamos el archivos
                    File.WriteAllText(archivo, string.Empty);
                }

                // Escribimos en el archivoo
                string temp = File.ReadAllText(archivo);

                // Convertimos
                acronimo = JsonConvert.DeserializeObject<Acronimo>(temp) ?? new Acronimo();

                // Libre de pecados
                return acronimo;
            }
            catch (Exception)
            {
                return acronimo;
            }
        }
    }
}
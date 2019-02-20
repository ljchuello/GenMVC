using System;
using System.IO;
using Newtonsoft.Json;

namespace GenMVC
{
    public class Fichero
    {
        private static string directorio = @"C:\GenMVC\";
        private static readonly string archivo = $"{directorio}\\config.config";

        public static void Escribir(Sql sql)
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
                File.WriteAllText(archivo, ObjectToJson(sql));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static Sql Leer()
        {
            Sql sql = new Sql();

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
                sql = JsonConvert.DeserializeObject<Sql>(temp);

                // Libre de pecados
                return sql;
            }
            catch (Exception)
            {
                return sql;
            }
        }

        public static string ObjectToJson(object o)
        {
            try
            {
                return JsonConvert.SerializeObject(o, Formatting.Indented);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static void Eliminar(string fullpath)
        {
            try
            {
                if (File.Exists(fullpath))
                {
                    File.Delete(fullpath);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
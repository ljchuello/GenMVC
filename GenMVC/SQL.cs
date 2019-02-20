﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace GenMVC
{
    public class Sql
    {
        public string Servidor { set; get; } = string.Empty;
        public string Usuario { set; get; } = string.Empty;
        public string Contrasenia { set; get; } = string.Empty;
        public string BaseDatos { set; get; } = string.Empty;

        public bool Exito(Sql sql)
        {
            try
            {
                // Cadena de conexión
                using (SqlConnection sqlConnection = new SqlConnection($"data source={sql.Servidor}; initial catalog={sql.BaseDatos}; persist security info=True; user id={sql.Usuario}; password={sql.Contrasenia}; MultipleActiveResultSets=True;Connection Timeout=15;"))
                {
                    // Abrimos
                    sqlConnection.Open();

                    // Comando
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT GETDATE();";
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteNonQuery();
                }

                // Libre de pecados
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable Select_Tables(Sql sql)
        {
            DataTable dataTable = new DataTable();
            try
            {
                // Cadena de conexión
                using (SqlConnection sqlConnection = new SqlConnection($"data source={sql.Servidor}; initial catalog={sql.BaseDatos}; persist security info=True; user id={sql.Usuario}; password={sql.Contrasenia}; MultipleActiveResultSets=True;Connection Timeout=15;"))
                {
                    // Abrimos
                    sqlConnection.Open();

                    // Comando
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME;";
                    sqlCommand.CommandType = CommandType.Text;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(dataTable);
                }

                // Libre de pecados
                return dataTable;
            }
            catch (Exception)
            {
                return dataTable;
            }
        }
    }
}
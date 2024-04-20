using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Crud
{
    public class DatabaseManager
    {
        readonly string connectionString = "Server=10.211.55.3;Port=3306;Database=promerica;Uid=root;Pwd=Root123;";

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public Tuple<DataSet, int> ExecuteSelectQuery(string query)
        {
            DataSet dataSet = new DataSet();
            int rowCount = 0;
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    adapter.Fill(dataSet);

                    foreach (DataTable table in dataSet.Tables)
                    {
                        rowCount += table.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error en la consulta SELECT: " + ex.Message);
            }
            return Tuple.Create(dataSet, rowCount);
        }

        public int ExecuteNonQuery(string query)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlConnection connection = GetConnection())
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error en la consulta INSERT, UPDATE o DELETE: " + ex.Message);
            }
            return rowsAffected;
        }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager dbManager = new DatabaseManager();

            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Insertar un nuevo registro");
            Console.WriteLine("2. Actualizar un registro existente");
            Console.WriteLine("3. Eliminar un registro");
            Console.WriteLine("4. Realizar una consulta SELECT");

            string selecion = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(selecion))
            {
                switch (selecion)
                {
                    case "1":
                        Console.WriteLine("Ingrese la descripción del nuevo producto:");
                        string description = Console.ReadLine();
                        string insertQuery = $"INSERT INTO producto (descripcion) VALUES ('{description}')";
                        int rowsInserted = dbManager.ExecuteNonQuery(insertQuery);
                        Console.WriteLine("Número de filas afectadas por INSERT: " + rowsInserted);
                        break;

                    case "2":
                        Console.WriteLine("Ingrese el nuevo nombre para el tipo:");
                        string newName = Console.ReadLine();
                        string updateQuery = $"UPDATE tipo SET nombre = '{newName}' WHERE cod_tipo = '3'";
                        int rowsUpdated = dbManager.ExecuteNonQuery(updateQuery);
                        Console.WriteLine("Número de filas afectadas por UPDATE: " + rowsUpdated);
                        break;

                    case "3":
                        string selectCountQuery = "SELECT COUNT(*) FROM tipo_informacionn";
                        string selectIdsQuery = "SELECT cod_tipo_informacion FROM tipo_informacionn";
                        var countResult = dbManager.ExecuteSelectQuery(selectCountQuery);
                        int rowCount = countResult.Item2;
                        Console.WriteLine($"Número total de registros disponibles para eliminación: {rowCount}");

                        var idsResult = dbManager.ExecuteSelectQuery(selectIdsQuery);
                        DataSet idsDataSet = idsResult.Item1;
                        Console.WriteLine("IDs disponibles para eliminación:");
                        foreach (DataTable table in idsDataSet.Tables)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                foreach (DataColumn col in table.Columns)
                                {
                                    Console.WriteLine(row[col]);
                                }
                            }
                        }

                        Console.WriteLine("Ingrese el ID del registro que desea eliminar:");
                        string idToDelete = Console.ReadLine();
                        string deleteQuery = $"DELETE FROM tipo_informacionn WHERE cod_tipo_informacion = '{idToDelete}'";
                        int rowsDeleted = dbManager.ExecuteNonQuery(deleteQuery);
                        Console.WriteLine("Número de filas afectadas por DELETE: " + rowsDeleted);
                        break;

                    case "4":
                        Console.WriteLine("Ingrese el nombre de la tabla para realizar la consulta SELECT:");
                        string tableName = Console.ReadLine();
                        string selectQuery = $"SELECT * FROM {tableName}";
                        var result = dbManager.ExecuteSelectQuery(selectQuery);
                        DataSet dataSet = result.Item1;
                        int rowCountSelect = result.Item2;
                        Console.WriteLine($"Número de filas retornadas por la consulta SELECT: {rowCountSelect}");
                        Console.WriteLine("Consulta SELECT realizada correctamente. Resultados:");
                        foreach (DataTable table in dataSet.Tables)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                foreach (DataColumn col in table.Columns)
                                {
                                    Console.WriteLine($"{col.ColumnName}: {row[col]}");
                                }
                                Console.WriteLine();
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ezgo_Desktop_App
{
    public class DB
    {
        private static string connectionString = "Server=DELL\\SQLEXPRESS;Database=ezgo;Trusted_Connection=True;MultipleActiveResultSets=true";

        public static DataTable Select(string[] fields, string[] tables, string[] oprtr, Dictionary<string, object> values, string[] joinConditions = null)
        {
            if (fields == null || fields.Length == 0)
            {
                throw new ArgumentException("Query failed: no fields provided.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = $"SELECT {string.Join(", ", fields)} FROM {tables[0]}";

                if (tables.Length > 1 && joinConditions != null)
                {
                    if (joinConditions.Length != tables.Length - 1)
                    {
                        throw new ArgumentException("Invalid number of join conditions provided.");
                    }

                    for (int i = 0; i < joinConditions.Length; i++)
                    {
                        selectQuery += $" LEFT JOIN {tables[i + 1]} ON {joinConditions[i]}";
                    }
                }

                int j = values.Count();

                string whereClause = values != null && values.Count > 0
                    ? $" WHERE {string.Join(" AND ", values.Select((pair) => $"{pair.Key} {oprtr[values.Keys.ToList().IndexOf(pair.Key)]} @{(tables.Length > 1 && joinConditions != null ? pair.Value + j.ToString() : pair.Key.Substring(0, 3))}"))}"
                    : string.Empty;


                string query = selectQuery + whereClause;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (values != null)
                    {
                        foreach (var pair in values)
                        {
                            if (tables.Length > 1 && joinConditions != null)
                            {
                                command.Parameters.AddWithValue($"@{pair.Value + j.ToString()}", pair.Value);
                            }
                            else {
                                command.Parameters.AddWithValue($"@{pair.Key.Substring(0, 3)}", pair.Value);
                            }
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public static int Insert(string table, Dictionary<string, object> values)
        {
            if (values.Count == 0)
            {
                throw new ArgumentException("Query failed: no values provided.");
            }

            string columns = string.Join(", ", values.Keys);
            string parameters = string.Join(", ", values.Keys.Select(key => $"@{key}"));
            string query = $"INSERT INTO {table} ({columns}) VALUES ({parameters})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var pair in values)
                    {
                        command.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        public static int Update(string table, Dictionary<string, object> where, Dictionary<string, object> values)
        {
            if (values.Count == 0)
            {
                throw new ArgumentException("Query failed: no values provided.");
            }

            string setClause = string.Join(", ", values.Select(pair => $"{pair.Key} = @{pair.Key}"));
            string whereClause = where != null && where.Count > 0
                ? $" WHERE {string.Join(" AND ", where.Select(pair => $"{pair.Key} = @{pair.Key}"))}"
                : string.Empty;

            string query = $"UPDATE {table} SET {setClause}{whereClause}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var pair in values)
                    {
                        command.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                    }

                    if (where != null)
                    {
                        foreach (var pair in where)
                        {
                            command.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                        }
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

        public static int Delete(string table, Dictionary<string, object> where)
        {
            if (where == null || where.Count == 0)
            {
                throw new ArgumentException("Delete failed: no WHERE clause provided.");
            }

            string whereClause = string.Join(" AND ", where.Select(pair => $"{pair.Key} = @{pair.Key}"));
            string query = $"DELETE FROM {table} WHERE {whereClause}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var pair in where)
                    {
                        command.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                    }

                    return command.ExecuteNonQuery();
                }
            }
        }

    }
}   

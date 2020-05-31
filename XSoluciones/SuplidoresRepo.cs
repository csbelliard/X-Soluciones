using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace XSoluciones
{
    public class SuplidoresRepo : ISuplidoresRepo
    {
        string ConnString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        public OperationResult ConsultarPorRNC(string rnc)
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = $"SELECT * FROM Suplidores WHERE Borrado = 0 AND RNC = { rnc}";
                    cmd.Connection = connection;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    connection.Open();
                    try
                    {
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            return new OperationResult() { Result = true, Data = dt };
                        }
                        return new OperationResult() { Result = false, Message = "No se encontraron suplidores." };
                    }
                    catch (Exception ex)
                    {
                        return new OperationResult(false, $"Ha ocurrido un error. {ex.Message}");
                    }
                }
            }
        }
        public OperationResult ConsultarTodos()
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM Suplidores WHERE Borrado = 0";
                    cmd.Connection = connection;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    connection.Open();
                    try
                    {
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            return new OperationResult() { Result = true, Data = dt };
                        }
                        return new OperationResult() { Result = false, Message = "No se encontraron suplidores." };
                    }
                    catch (Exception ex)
                    {
                        return new OperationResult(false, $"Ha ocurrido un error. {ex.Message}");
                    }
                }
            }
        }
        public OperationResult Crear(Suplidores suplidores)
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = $"INSERT INTO Suplidores VALUES ('{suplidores.Nombre}', '{suplidores.RNC}', '{suplidores.Representante}', '{suplidores.Direccion}', 0)";
                    cmd.Connection = connection;
                    connection.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return new OperationResult() { Result = true, Message = "Suplidor creado satisfactoriamente." };
                    }
                    catch (Exception ex)
                    {
                        return new OperationResult(false, $"Ha ocurrido un error. {ex.Message}");
                    }
                }
            }
        }
        public OperationResult Eliminar(string rnc)
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = $"UPDATE Suplidores SET Borrado = 1 WHERE RNC = { rnc}";
                    cmd.Connection = connection;
                    connection.Open();
                    try
                    {
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            return new OperationResult() { Result = false, Message = "Suplidor no encontrado." };
                        }
                        return new OperationResult() { Result = true, Message = "Suplidor eliminado satisfactoriamente." };
                    }
                    catch (Exception ex)
                    {
                        return new OperationResult(false, $"Ha ocurrido un error. {ex.Message}");
                    }
                }
            }
        }
        public OperationResult Modificar(Suplidores suplidores, string rnc)
        {
            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = $"UPDATE Suplidores SET Representante ='{suplidores.Representante}', Direccion ='{suplidores.Direccion}' WHERE RNC = { rnc} AND Borrado = 0";
                    cmd.Connection = connection;
                    connection.Open();
                    try
                    {
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            return new OperationResult() { Result = false, Message = "Suplidor no encontrado." };
                        }
                        return new OperationResult() { Result = true, Message = "Suplidor modificado satisfactoriamente." };
                    }
                    catch (Exception ex)
                    {
                        return new OperationResult(false, $"Ha ocurrido un error. {ex.Message}");
                    }
                }
            }
        }
    }
}
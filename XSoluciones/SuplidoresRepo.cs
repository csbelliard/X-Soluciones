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
                    cmd.CommandText = "ConsultarPorRNC";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("rnc", rnc);
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
                    cmd.CommandText = "ConsultarTodos";
                    cmd.CommandType = CommandType.StoredProcedure;
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
                    cmd.CommandText = "Crear";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Nombre", suplidores.Nombre);
                    cmd.Parameters.AddWithValue("RNC", suplidores.RNC);
                    cmd.Parameters.AddWithValue("Representante", suplidores.Representante);
                    cmd.Parameters.AddWithValue("Direccion", suplidores.Direccion);
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
                    cmd.CommandText = "Eliminar";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("rnc", rnc);
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
                    cmd.CommandText = "Modificar";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Representante", suplidores.Representante);
                    cmd.Parameters.AddWithValue("Direccion", suplidores.Direccion);
                    cmd.Parameters.AddWithValue("rnc", rnc);
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
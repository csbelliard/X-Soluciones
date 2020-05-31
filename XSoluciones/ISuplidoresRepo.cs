using System;

namespace XSoluciones
{
    public interface ISuplidoresRepo
    {
        OperationResult Crear(Suplidores suplidores);
        OperationResult ConsultarTodos();
        OperationResult ConsultarPorRNC(string rnc);
        OperationResult Modificar(Suplidores suplidores, string rnc);
        OperationResult Eliminar(string rnc);
    }
}

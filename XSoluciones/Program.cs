using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace XSoluciones
{
    class Program
    {
        static ISuplidoresRepo suplidoresRepo = new SuplidoresRepo();
        public static void Main(string[] args)
        {
            bool noSalir = true;
            while (noSalir)
            {
                Console.Clear();
                Console.WriteLine("Qué desea realizar?\n1  Crear Suplidor.\n2  Consultar Suplidores.\n3  Modificar Suplidor.\n4  Eliminar Suplidor.");
                Console.WriteLine("");
                Console.Write("Digite la opción: ");
                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        {
                            Console.Clear();
                            Console.Write("Nombre del suplidor: ");
                            string nombreSuplidor = Console.ReadLine();
                            Console.Write("RNC del suplidor: ");
                            string rncSuplidor = Console.ReadLine();
                            Console.Write("Representante del suplidor: ");
                            string representanteSuplidor = Console.ReadLine();
                            Console.Write("Direccion del suplidor: ");
                            string direccionSuplidor = Console.ReadLine();
                            OperationResult resultado = suplidoresRepo.Crear(new Suplidores() { Nombre = nombreSuplidor, RNC = rncSuplidor, Representante = representanteSuplidor, Direccion = direccionSuplidor });
                            Console.WriteLine(resultado.Message);
                        }
                        break;
                    case "2":
                        {
                            bool noSalir1 = true;
                            while (noSalir1)
                            {
                                Console.Clear();
                                Console.WriteLine("Qué desea realizar ?\n1  Consultar todos los suplidores.\n2  Buscar suplidor por RNC.\n3  Volver atras.");
                                Console.Write("Digite la opción: ");
                                string opcion1 = Console.ReadLine();
                                switch (opcion1)
                                {
                                    case "1":
                                        {
                                            Console.Clear();
                                            OperationResult suplidores = suplidoresRepo.ConsultarTodos();
                                            if (!suplidores.Result)
                                            {
                                                Console.WriteLine(suplidores.Message);
                                                Console.ReadKey();
                                            }
                                            DataTable dataTable = (DataTable)suplidores.Data;
                                            Console.WriteLine("------------------------------- SUPLIDORES -------------------------------");
                                            Console.WriteLine("");
                                            Console.WriteLine("");
                                            Console.WriteLine("---------------------------------------------------------------------------");
                                            Console.WriteLine("Nombre         RNC            Representante       Dirección                ");
                                            Console.WriteLine("---------------------------------------------------------------------------");
                                            foreach (DataRow suplidor in dataTable.Rows)
                                            {
                                                Console.WriteLine($"{suplidor["Nombre"]}\t {suplidor["RNC"]}\t {suplidor["Representante"]}\t {suplidor["Direccion"]}");
                                                //Console.WriteLine($"("Nombre").PadRight(15), ("RNC").ToString().PadRight(15), ("Representante").ToString().PadRight(20), ("Direccion").ToString().PadRight(25));
                                            }
                                            Console.ReadKey();
                                        }
                                        break;
                                    case "2":
                                        {
                                            Console.Clear();
                                            Console.Write("Digite el RNC del suplidor a buscar: ");
                                            string rnc = Console.ReadLine();
                                            OperationResult suplidores = suplidoresRepo.ConsultarPorRNC(rnc);
                                            if (!suplidores.Result)
                                            {
                                                Console.WriteLine(suplidores.Message);
                                                Console.ReadKey();
                                            }
                                            DataTable dataTable = (DataTable)suplidores.Data;
                                            Console.WriteLine("------------------------------- SUPLIDORES -------------------------------");
                                            Console.WriteLine("");
                                            Console.WriteLine("");
                                            Console.WriteLine("---------------------------------------------------------------------------");
                                            Console.WriteLine("Nombre         RNC            Representante       Dirección                ");
                                            Console.WriteLine("---------------------------------------------------------------------------");
                                            foreach (DataRow suplidor in dataTable.Rows)
                                            {
                                                Console.WriteLine($"{suplidor["Nombre"]}        {suplidor["RNC"]}         {suplidor["Representante"]}        {suplidor["Direccion"]}");
                                            }
                                            Console.ReadKey();
                                        }
                                        break;
                                    case "3":
                                        {
                                            noSalir1 = false;
                                        }
                                        break;
                                    default:
                                        {
                                            Console.WriteLine("\nFavor digitar una de las opciones listadas.\n");
                                            Console.Read();
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case "3":
                        {
                            Console.Clear();
                            Console.Write("Digite el RNC del suplidor a modificar: ");
                            string rnc = Console.ReadLine();
                            OperationResult suplidores = suplidoresRepo.ConsultarPorRNC(rnc);
                            if (!suplidores.Result)
                            {
                                Console.WriteLine(suplidores.Message);
                                Console.ReadKey();
                            }
                            DataTable dataTable = (DataTable)suplidores.Data;
                            Console.WriteLine("");
                            Console.WriteLine("---------------------------------------------------------------------------");
                            Console.WriteLine("Nombre         RNC            Representante       Dirección                ");
                            Console.WriteLine("---------------------------------------------------------------------------");
                            foreach (DataRow suplidor in dataTable.Rows)
                            {
                                Console.WriteLine($"{suplidor["Nombre"]}        {suplidor["RNC"]}         {suplidor["Representante"]}        {suplidor["Direccion"]}");
                            }
                            Console.WriteLine("");
                            Console.Write("Digite el nombre del nuevo representante: ");
                            string nuevoRepresentante = Console.ReadLine();
                            Console.Write("Digite la nueva dirección: ");
                            string nuevaDireccion = Console.ReadLine();
                            OperationResult modificar = suplidoresRepo.Modificar(new Suplidores() { Representante=nuevoRepresentante, Direccion = nuevaDireccion}, rnc);
                            Console.WriteLine(modificar.Message);
                            Console.ReadKey();
                        }
                        break;
                    case "4":
                        {
                            Console.Clear();
                            Console.Write("Digite el RNC del suplidor a eliminar: ");
                            string rnc = Console.ReadLine();
                            OperationResult suplidores = suplidoresRepo.ConsultarPorRNC(rnc);
                            if (!suplidores.Result)
                            {
                                Console.WriteLine(suplidores.Message);
                                Console.ReadKey();
                            }
                            DataTable dataTable = (DataTable)suplidores.Data;
                            Console.WriteLine("");
                            Console.WriteLine("---------------------------------------------------------------------------");
                            Console.WriteLine("Nombre         RNC            Representante       Dirección                ");
                            Console.WriteLine("---------------------------------------------------------------------------");
                            foreach (DataRow suplidor in dataTable.Rows)
                            {
                                Console.WriteLine($"{suplidor["Nombre"]}        {suplidor["RNC"]}         {suplidor["Representante"]}        {suplidor["Direccion"]}");
                            }
                            Console.WriteLine("");
                            Console.Write("Está seguro desea borrar este suplidor: ");
                            var respuesta = Console.ReadLine();
                            if (respuesta.ToLower() == "s")
                            {
                                OperationResult eliminar = suplidoresRepo.Eliminar(rnc);
                                Console.WriteLine(eliminar.Message);
                            }
                            Console.ReadKey();
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("\nFavor digitar una de las opciones listadas.\n");
                        }
                        break;
                }
                Console.Write("Presiona (Enter) para continuar.\nPara cerrar el programa presiona (s): ");
                noSalir = (Console.ReadLine() == "s") ? false : true;
                Console.WriteLine("");
            }
        }
    }
}
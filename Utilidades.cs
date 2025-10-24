using System;

namespace CajeroApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Cajero Automático ===");
            Console.WriteLine("1. Consultar saldo");
            Console.WriteLine("2. Retirar dinero");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            if (UtilidadesCajero.ValidarOpcion(opcion, 1, 3))
            {
                Console.WriteLine("Opción válida.");

                if (opcion == 2)
                {
                    Console.Write("Ingrese el monto a retirar: ");
                    decimal monto = decimal.Parse(Console.ReadLine());
                    if (UtilidadesCajero.ValidarMonto(monto))
                    {
                        Console.WriteLine($"Se retirarán ${monto}");
                    }
                }
            }

            UtilidadesCajero.Pausar();
        }
    }
}

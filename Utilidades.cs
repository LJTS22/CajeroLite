using System;

namespace CajeroLite.Utilidades
{
    public static class Utilidad
    {
        public static int ValidarOpcionMenu(string input, int min, int max)
        {
            if (int.TryParse(input, out int opcion))
            {
                if (opcion >= min && opcion <= max) return opcion;
            }
            return -1;
        }

        public static bool ValidarMonto(decimal monto)
        {
            return monto > 0;
        }

        public static void Pausar(string mensaje = "Presione cualquier tecla para continuar...")
        {
            Console.WriteLine($"\n{mensaje}");
            Console.ReadKey();
        }
    }
}


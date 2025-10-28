using System;

namespace CajeroLite.IO
{
    public static class Io
    {
        public static void IO_MostrarEncabezado(string titulo)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine($" {titulo}");
            Console.WriteLine("========================================\n");
        }

        public static void IO_MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: {mensaje}");
            Console.ResetColor();
        }

        public static void IO_MostrarInfo(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        public static void IO_MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        public static string IO_CapturarTexto(string mensaje)
        {
            Console.Write($"{mensaje} ");
            return Console.ReadLine();
        }

        public static string IO_LeerPin(string mensaje)
        {
            Console.Write($"{mensaje} ");
            string pin = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Backspace && pin.Length > 0)
                {
                    pin = pin.Substring(0, pin.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pin += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return pin;
        }

        public static void IO_Pausar(string mensaje = "Presione cualquier tecla para continuar...")
        {
            Console.WriteLine($"\n{mensaje}");
            Console.ReadKey();
        }
    }
}


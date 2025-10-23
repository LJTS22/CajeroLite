using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroLite
{
    internal class IO
    {
        // Muestra mensajes de info y erroeres
        public static void MostrarMensaje(string mensaje, bool esError = false)
        {
            if (esError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] {mensaje}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(mensaje);
            }
            Console.ResetColor();
        }

        // Captura texto o números con validación y reintentos
        public static string CapturarEntrada(string mensaje, bool obligatorio = true)
        {
            string entrada = "";
            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine()?.Trim() ?? "";

                if (obligatorio && string.IsNullOrEmpty(entrada))
                {
                    MostrarMensaje(" El valor no puede estar vacío. Intenta nuevamente.", true);
                }

            } while (obligatorio && string.IsNullOrEmpty(entrada));

            return entrada;
        }

        // Lee información privada sin mostrar caracteres
        public static string LeerPIN(string mensaje)
        {
            Console.Write(mensaje);
            string pin = "";
            ConsoleKeyInfo tecla;

            do
            {
                tecla = Console.ReadKey(intercept: true);

                if (tecla.Key == ConsoleKey.Enter)
                    break;

                if (tecla.Key == ConsoleKey.Backspace && pin.Length > 0)
                {
                    pin = pin.Substring(0, pin.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(tecla.KeyChar))
                {
                    pin += tecla.KeyChar;
                    Console.Write("*");
                }

            } while (true);

            Console.WriteLine();
            return pin;
        }

        // Muestra el títulos de sección
        public static void MostrarEncabezado(string titulo)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==========================================");
            Console.WriteLine($"     {titulo.ToUpper()}");
            Console.WriteLine("==========================================");
            Console.ResetColor();
        }

        // Pausa la ejecución para que el usuario lea los resultados
        public static void Pausar()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Presiona cualquier tecla para continuar..");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}

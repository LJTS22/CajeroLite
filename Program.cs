using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//estrysuiop´+sssss
namespace CajeroLite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            IO.MostrarEncabezado("Simulador CajeroLite - Prueba del módulo IO");

            
            IO.MostrarMensaje("Bienvenido al CajeroLite. Vamos a realizar algunas pruebas.");

            
            string nombre = IO.CapturarEntrada("Ingresa tu nombre: ");
            IO.MostrarMensaje($"Hola, {nombre}!");

         
            string pin = IO.LeerPIN("Por favor ingresa tu PIN: ");
            IO.MostrarMensaje("PIN recibido correctamente (oculto en pantalla).");

            
            IO.MostrarEncabezado("Mensajes de prueba");

            
            IO.MostrarMensaje("Error: Saldo insuficiente para realizar el retiro.", esError: true);

          
            IO.MostrarMensaje("Operación completada correctamente ");

            IO.Pausar();

            IO.MostrarEncabezado("Fin de la prueba del módulo IO");
            IO.MostrarMensaje("Gracias por usar CajeroLite.IO");
            IO.Pausar();
        }
    }
}

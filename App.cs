using CajeroLite.Utilidades;
using CajeroLite.Data;
using CajeroLite.IO;
using CajeroLite.Operaciones;
using System;

namespace CajeroLite.App
{
    public static class Program
    {
        private const int MAX_INTENTOS_ACCESO = 3;

        public static void Main(string[] args)
        {
            Io.IO_MostrarEncabezado("BIENVENIDO AL CAJERO LITE");

            int idUsuario = GestionarInicioSesion();

            if (idUsuario != -1)
            {
                MostrarMenuPrincipal(idUsuario);
            }
            else
            {
                Io.IO_MostrarError("Demasiados intentos fallidos. Tarjeta retenida.");
            }

            Io.IO_MostrarInfo("Gracias por usar Cajero Lite. ¡Hasta pronto!");
            Io.IO_Pausar("Presione cualquier tecla para salir...");
        }

        public static int GestionarInicioSesion()
        {
            int intentos = 0;
            while (intentos < MAX_INTENTOS_ACCESO)
            {
                Io.IO_MostrarEncabezado("INICIO DE SESIÓN");
                string usuario = Io.IO_CapturarTexto("Ingrese su ID de usuario:");
                string pin = Io.IO_LeerPin("Ingrese su PIN:");

                if (Datos.UsuarioExiste(usuario) && Datos.PinEsCorrecto(usuario, pin))
                {
                    Io.IO_MostrarExito($"Inicio de sesión correcto. Usuario: {usuario}");
                    return Datos.ObtenerIndiceUsuario(usuario);
                }
                else
                {
                    intentos++;
                    Io.IO_MostrarError($"ID o PIN incorrectos. Intentos restantes: {MAX_INTENTOS_ACCESO - intentos}");
                    if (intentos < MAX_INTENTOS_ACCESO) Io.IO_Pausar("Presione una tecla para reintentar...");
                }
            }
            return -1;
        }

        public static void MostrarMenuPrincipal(int indiceUsuario)
        {
            bool salir = false;
            while (!salir)
            {
                Io.IO_MostrarEncabezado("MENÚ PRINCIPAL");
                Console.WriteLine("1. Consultar Saldo");
                Console.WriteLine("2. Realizar Depósito");
                Console.WriteLine("3. Realizar Retiro");
                Console.WriteLine("4. Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcionStr = Console.ReadLine();
                int opcion = Utilidad.ValidarOpcionMenu(opcionStr, 1, 4);

                if (opcion == -1)
                {
                    Io.IO_MostrarError("Opción inválida. Ingrese 1-4.");
                    Io.IO_Pausar();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        decimal saldo = Operacion.ConsultarSaldo(indiceUsuario);
                        Io.IO_MostrarInfo($"Saldo disponible: {saldo:C}");
                        break;
                    case 2:
                        Operacion.RealizarDeposito(indiceUsuario);
                        break;
                    case 3:
                        Operacion.RealizarRetiro(indiceUsuario);
                        break;
                    case 4:
                        salir = true;
                        Io.IO_MostrarInfo("Cerrando sesión...");
                        break;
                }

                if (!salir) Io.IO_Pausar();
            }
        }
    }
}

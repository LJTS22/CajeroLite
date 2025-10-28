using System;
using static CajeroLite.Data.Datos;
using static CajeroLite.Utilidades.Utilidades;
using CajeroOperaciones = CajeroLite.Operaciones.Operaciones;

namespace CajeroLite
{
    public static class App
    {
        private static string usuarioAutenticado;
        private static CajeroOperaciones operaciones;

        public static void Iniciar()
        {
            Console.Title = "Cajero Automático Lite";
            MostrarBienvenida();

            bool continuarEjecucion = true;

            while (continuarEjecucion)
            {
                continuarEjecucion = EjecutarFlujoPrincipal();
            }

            MostrarDespedida();
        }

        private static bool EjecutarFlujoPrincipal()
        {
            if (!RealizarAutenticacion())
            {
                return PreguntarSiReintentar();
            }

            bool usuarioActivo = true;
            while (usuarioActivo)
            {
                usuarioActivo = MostrarYProcesarMenuPrincipal();
            }

            return PreguntarSiReintentar();
        }

        private static bool RealizarAutenticacion()
        {
            LimpiarConsola();
            Console.WriteLine("=== INICIO DE SESIÓN ===");

            string usuario = SolicitarUsuario();
            if (usuario == null) return false;

            string pin = SolicitarPin();
            if (pin == null) return false;

            if (PinEsCorrecto(usuario, pin))
            {
                usuarioAutenticado = usuario;
                operaciones = new CajeroOperaciones(usuarioAutenticado);
                Console.WriteLine("Autenticación exitosa!");
                PausarEjecucion("Presione cualquier tecla para continuar al menú principal...");
                return true;
            }
            else
            {
                Console.WriteLine("PIN incorrecto. Autenticación fallida.");
                PausarEjecucion();
                return false;
            }
        }

        private static string SolicitarUsuario()
        {
            int intentos = 0;
            const int maxIntentos = 3;

            while (intentos < maxIntentos)
            {
                Console.Write("Ingrese su número de usuario: ");
                string usuario = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(usuario))
                {
                    Console.WriteLine("El usuario no puede estar vacío.");
                    intentos++;
                    continue;
                }

                if (UsuarioExiste(usuario))
                {
                    return usuario;
                }
                else
                {
                    intentos++;
                    Console.WriteLine($"Usuario no encontrado. Intentos restantes: {maxIntentos - intentos}");
                }
            }

            Console.WriteLine("Demasiados intentos fallidos para usuario.");
            return null;
        }

        private static string SolicitarPin()
        {
            int intentos = 0;
            const int maxIntentos = 3;

            while (intentos < maxIntentos)
            {
                Console.Write("Ingrese su PIN: ");
                string pin = Console.ReadLine()?.Trim();

                if (PinEsValido(pin))
                {
                    return pin;
                }
                else
                {
                    intentos++;
                    if (intentos < maxIntentos)
                    {
                        Console.WriteLine($"Intentos restantes: {maxIntentos - intentos}");
                    }
                }
            }

            Console.WriteLine("Demasiados intentos fallidos para PIN.");
            return null;
        }

        private static bool MostrarYProcesarMenuPrincipal()
        {
            LimpiarConsola();
            Console.WriteLine("=== MENÚ PRINCIPAL ===");
            Console.WriteLine($"Usuario: {usuarioAutenticado}");
            Console.WriteLine("1. Consultar saldo");
            Console.WriteLine("2. Retirar dinero");
            Console.WriteLine("3. Depositar dinero");
            Console.WriteLine("5. Cerrar sesión");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();
            string[] opcionesValidas = { "1", "2", "3", "4", "5", "6" };

            if (!OpcionMenuEsValida(opcion, opcionesValidas))
            {
                PausarEjecucion("Opción no válida. Intente nuevamente.");
                return true;
            }

            return ProcesarOpcionMenu(opcion);
        }

        private static bool ProcesarOpcionMenu(string opcion)
        {
            switch (opcion)
            {
                case "1":
                    ConsultarSaldo();
                    return true;
                case "2":
                    RealizarRetiro();
                    return true;
                case "3":
                    RealizarDeposito();
                    return true;
                case "4":
                    CerrarSesion();
                    return false;
                case "5":
                    return false;
                default:
                    return true;
            }
        }

        private static void ConsultarSaldo()
        {
            LimpiarConsola();
            Console.WriteLine("=== CONSULTA DE SALDO ===");
            Console.WriteLine(operaciones.ObtenerResumenSaldo());
            PausarEjecucion();
        }

        private static void RealizarRetiro()
        {
            LimpiarConsola();
            Console.WriteLine("=== RETIRO ===");

            decimal monto = SolicitarMonto("retirar");
            if (monto <= 0) return;

            var resultado = operaciones.RealizarRetiro(monto);
            MostrarResultadoOperacion(resultado.exitosa, resultado.mensaje);

            if (resultado.exitosa)
            {
                Console.WriteLine($"Saldo anterior: {resultado.saldoAnterior:C}");
                Console.WriteLine($"Saldo actual: {resultado.saldoNuevo:C}");
            }

            PausarEjecucion();
        }

        private static void RealizarDeposito()
        {
            LimpiarConsola();
            Console.WriteLine("=== DEPÓSITO ===");

            decimal monto = SolicitarMonto("depositar");
            if (monto <= 0) return;

            var resultado = operaciones.RealizarDeposito(monto);
            MostrarResultadoOperacion(resultado.exitosa, resultado.mensaje);

            if (resultado.exitosa)
            {
                Console.WriteLine($"Saldo anterior: {resultado.saldoAnterior:C}");
                Console.WriteLine($"Saldo actual: {resultado.saldoNuevo:C}");
            }

            PausarEjecucion();
        }

        private static decimal SolicitarMonto(string operacion)
        {
            Console.Write($"Ingrese el monto a {operacion}: ");
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal monto))
            {
                if (MontoEsValido(monto))
                {
                    return monto;
                }
            }
            else
            {
                Console.WriteLine("ERROR: Por favor ingrese un monto válido.");
            }

            return -1;
        }

        private static void MostrarResultadoOperacion(bool exitosa, string mensaje)
        {
            if (exitosa)
            {
                Console.WriteLine(mensaje);
                Console.WriteLine("Operación completada correctamente");
            }
            else
            {
                Console.WriteLine("ERROR: " + mensaje);
            }
        }

    

        private static void CerrarSesion()
        {
            Console.WriteLine("Cerrando sesión...");
            usuarioAutenticado = null;
            operaciones = null;
            PausarEjecucion("Sesión cerrada. Presione cualquier tecla para continuar...");
        }

        private static bool PreguntarSiReintentar()
        {
            LimpiarConsola();
            Console.Write("¿Desea intentar nuevamente? (S/N): ");
            string respuesta = Console.ReadLine()?.Trim().ToUpper();

            return respuesta == "S" || respuesta == "SI";
        }

        private static void MostrarBienvenida()
        {
            LimpiarConsola();
            Console.WriteLine("===================================");
            Console.WriteLine("BIENVENIDO A CAJERO AUTOMÁTICO LITE");
            Console.WriteLine("===================================");
            PausarEjecucion("Presione cualquier tecla para comenzar...");
        }

        private static void MostrarDespedida()
        {
            LimpiarConsola();
            Console.WriteLine("GRACIAS POR USAR CAJERO AUTOMÁTICO LITE");
            Console.WriteLine("========================================");
            Console.WriteLine("            Vuelva pronto!");
            Console.WriteLine("========================================");
            PausarEjecucion("Presione cualquier tecla para salir...");
        }
    }
}
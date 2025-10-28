using System;

namespace CajeroLite.Utilidades
{
    public static class Utilidades
    {
        public static bool MontoEsValido(decimal monto)
        {
            // Validaciones:
            // 1. El monto debe ser positivo
            // 2. El monto debe ser mayor a cero
            // 3. El monto no debe ser excesivamente grande (límite práctico)
            // 4. El monto debe tener máximo 2 decimales (para centavos)

            if (monto <= 0)
            {
                Console.WriteLine(" El monto debe ser mayor a cero.");
                return false;
            }

            if (monto > 1000000000) // Límite de mil millones
            {
                Console.WriteLine(" El monto excede el límite permitido.");
                return false;
            }

            // Verificar que no tenga más de 2 decimales
            if (decimal.Round(monto, 2) != monto)
            {
                Console.WriteLine(" El monto no puede tener más de 2 decimales.");
                return false;
            }

            return true;
        }

        public static bool OpcionMenuEsValida(string opcion, string[] opcionesValidas)
        {
            if (string.IsNullOrWhiteSpace(opcion))
            {
                Console.WriteLine("❌ La opción no puede estar vacía.");
                return false;
            }

            // Verificar si la opción está en el array de opciones válidas
            foreach (string opcionValida in opcionesValidas)
            {
                if (opcionValida.Equals(opcion, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            Console.WriteLine($"❌ Opción '{opcion}' no válida. Opciones válidas: {string.Join(", ", opcionesValidas)}");
            return false;
        }

        /// Método sobrecargado para validar opciones numéricas del menú
        public static bool OpcionMenuEsValida(int opcion, int[] opcionesValidas)
        {
            foreach (int opcionValida in opcionesValidas)
            {
                if (opcionValida == opcion)
                {
                    return true;
                }
            }

            Console.WriteLine($"❌ Opción '{opcion}' no válida. Opciones válidas: {string.Join(", ", opcionesValidas)}");
            return false;
        }
        /// Pausa la ejecución de forma consistente donde sea necesario

        public static void PausarEjecucion(string mensaje = "Presione cualquier tecla para continuar...")
        {
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                Console.WriteLine($"\n{mensaje}");
            }
            else
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
            }

            Console.ReadKey();
            Console.WriteLine(); // Línea en blanco para mejor formato
        }

        /// Método adicional: Limpiar la consola de forma controlada

        public static void LimpiarConsola()
        {
            Console.Clear();
            Console.WriteLine("=== CAJERO AUTOMÁTICO LITE ===\n");
        }

        /// Método adicional: Validar formato de PIN
        public static bool PinEsValido(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
            {
                Console.WriteLine(" El PIN no puede estar vacío.");
                return false;
            }

            if (pin.Length != 4)
            {
                Console.WriteLine(" El PIN debe tener exactamente 4 dígitos.");
                return false;
            }

            if (!int.TryParse(pin, out _))
            {
                Console.WriteLine(" El PIN debe contener solo números.");
                return false;
            }

            return true;
        }
    }
}
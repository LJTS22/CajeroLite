using System;
using CajeroLite.Data;

namespace CajeroLite.Operaciones
{
    public class Operaciones
    {
        private string usuarioActual;
        private decimal saldoActual;

        // Constructor modificado - ahora acepta string usuario
        public Operaciones(string usuario)
        {
            usuarioActual = usuario;
            saldoActual = Datos.ObtenerSaldo(usuario);
        }

        public (bool exitosa, string mensaje, decimal saldoAnterior, decimal saldoNuevo) RealizarDeposito(decimal monto)
        {
            if (monto <= 0)
            {
                return (false, "El monto a depositar debe ser mayor a cero.", saldoActual, saldoActual);
            }

            decimal saldoAnterior = saldoActual;
            saldoActual += monto;

            // Actualizar en la base de datos
            Datos.ActualizarSaldo(usuarioActual, saldoActual);

            return (true, $"Depósito exitoso. Se han depositado {monto:C}", saldoAnterior, saldoActual);
        }

        public (bool exitosa, string mensaje, decimal saldoAnterior, decimal saldoNuevo) RealizarRetiro(decimal monto)
        {
            if (monto <= 0)
            {
                return (false, "El monto a retirar debe ser mayor a cero.", saldoActual, saldoActual);
            }

            if (monto > saldoActual)
            {
                return (false, $"Fondos insuficientes. Saldo disponible: {saldoActual:C}", saldoActual, saldoActual);
            }

            decimal saldoAnterior = saldoActual;
            saldoActual -= monto;

            // Actualizar en la base de datos
            Datos.ActualizarSaldo(usuarioActual, saldoActual);

            return (true, $"Retiro exitoso. Se han retirado {monto:C}", saldoAnterior, saldoActual);
        }

        public decimal ConsultarSaldo()
        {
            return saldoActual;
        }

        public string ObtenerResumenSaldo()
        {
            return $"Saldo disponible: {saldoActual:C}";
        }

        public bool ValidarFondosSuficientes(decimal monto)
        {
            return saldoActual >= monto && monto > 0;
        }
    }
}
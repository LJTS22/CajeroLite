using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CajeroLite.Operaciones
{
    public class Operaciones
    {
        private decimal saldoActual;

        public Operaciones(decimal saldoInicial = 0)
        {
            saldoActual = saldoInicial;
        }

        public (bool exitosa, string mensaje, decimal saldoAnterior, decimal saldoNuevo) RealizarDeposito(decimal monto)
        {

            if (monto <= 0)
            {
                return (false, "El monto a depositar debe ser mayor a cero.", saldoActual, saldoActual);
            }


            decimal saldoAnterior = saldoActual;


            saldoActual += monto;

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

        public void EstablecerSaldo(decimal nuevoSaldo)
        {
            if (nuevoSaldo >= 0)
            {
                saldoActual = nuevoSaldo;
            }
        }

        public bool ValidarFondosSuficientes(decimal monto)
        {
            return saldoActual >= monto && monto > 0;
        }
    }
}
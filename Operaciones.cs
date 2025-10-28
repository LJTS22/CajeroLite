using System;
using CajeroLite.Data;
using CajeroLite.IO;

namespace CajeroLite.Operaciones
{
    public static class Operacion
    {
        public static decimal ConsultarSaldo(int indiceUsuario)
        {
            return Datos.Saldos[indiceUsuario];
        }

        public static void RealizarDeposito(int indiceUsuario)
        {
            string montoStr = Io.IO_CapturarTexto("Ingrese el monto a depositar:");
            if (decimal.TryParse(montoStr, out decimal monto) && monto > 0)
            {
                Datos.Saldos[indiceUsuario] += monto;
                Io.IO_MostrarExito($"Depósito realizado. Nuevo saldo: {Datos.Saldos[indiceUsuario]:C}");
            }
            else
            {
                Io.IO_MostrarError("Monto inválido.");
            }
        }

        public static void RealizarRetiro(int indiceUsuario)
        {
            string montoStr = Io.IO_CapturarTexto("Ingrese el monto a retirar:");
            if (decimal.TryParse(montoStr, out decimal monto) && monto > 0)
            {
                if (Datos.Saldos[indiceUsuario] >= monto)
                {
                    Datos.Saldos[indiceUsuario] -= monto;
                    Io.IO_MostrarExito($"Retiro exitoso. Nuevo saldo: {Datos.Saldos[indiceUsuario]:C}");
                }
                else
                {
                    Io.IO_MostrarError("Fondos insuficientes.");
                }
            }
            else
            {
                Io.IO_MostrarError("Monto inválido.");
            }
        }
    }
}

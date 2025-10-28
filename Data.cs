using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroLite.Data
{
    public static class Datos
    {
        // Arreglos paralelos - cada índice representa al mismo usuario
        public static string[] Usuarios = { "1001", "2002" };
        public static string[] Pines = { "1234", "5678" };
        public static decimal[] Saldos = { 500000m, 1200000m };

        public static bool UsuarioExiste(string usuario)
        {
            for (int i = 0; i < Usuarios.Length; i++)
            {
                if (Usuarios[i] == usuario)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool PinEsCorrecto(string usuario, string pin)
        {
            for (int i = 0; i < Usuarios.Length; i++)
            {
                if (Usuarios[i] == usuario && Pines[i] == pin)
                {
                    return true;
                }
            }
            return false;
        }

        public static decimal ObtenerSaldo(string usuario)
        {
            for (int i = 0; i < Usuarios.Length; i++)
            {
                if (Usuarios[i] == usuario)
                {
                    return Saldos[i];
                }
            }
            return -1; // Usuario no encontrado
        }

        public static bool ActualizarSaldo(string usuario, decimal nuevoSaldo)
        {
            for (int i = 0; i < Usuarios.Length; i++)
            {
                if (Usuarios[i] == usuario)
                {
                    Saldos[i] = nuevoSaldo;
                    return true;
                }
            }
            return false; // Usuario no encontrado
        }

        public static int ObtenerIndiceUsuario(string usuario)
        {
            for (int i = 0; i < Usuarios.Length; i++)
            {
                if (Usuarios[i] == usuario)
                {
                    return i;
                }
            }
            return -1; // Usuario no encontrado
        }
    }
}
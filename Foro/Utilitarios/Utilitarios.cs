using Core.Entidades;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public static class Utilitarios
    {
        public static Error crearError(enumErrores enumErrores)
        {
            Error error = new Error();
            error.codigo = enumErrores;
            error.mensaje = enumErrores.ToString();

            return error;
        }
    }
}

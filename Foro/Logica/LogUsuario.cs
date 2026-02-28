using AccesoDatos;
using Core.Entidades;
using Core.Enums;
using Core.Request;
using Core.Response;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logica
{
    public class LogUsuario
    {
        public ResIngresarUsuario insertar(ReqIngresarUsuario req)
        {
            ResIngresarUsuario res = new ResIngresarUsuario();

            if (req == null)
            {
                res.resultado = false;
                res.error.Add(Utilitarios.Utilitarios.crearError(enumErrores.reqNull));
            }
            if (String.IsNullOrEmpty(req.usuario.nombre))
            {
                res.resultado = false;
                res.error.Add(Utilitarios.Utilitarios.crearError(enumErrores.nombreFaltante));
            }
            if (String.IsNullOrEmpty(req.usuario.apellidos))
            {
                res.resultado = false;
                res.error.Add(Utilitarios.Utilitarios.crearError(enumErrores.apellidosFaltantes));
            }
             if (String.IsNullOrEmpty(req.usuario.correoElectronico))
            {
                res.resultado = false;
                res.error.Add(Utilitarios.Utilitarios.crearError(enumErrores.correoFaltante));
            }
            else if (EsCorreoValido(req.usuario.correoElectronico))
            {
                res.resultado = false;
                res.error.Add(Utilitarios.Utilitarios.crearError(enumErrores.correoNoValido));
            }
            if (String.IsNullOrEmpty(req.usuario.password))
            {
                res.resultado = false;
                res.error.Add(Utilitarios.Utilitarios.crearError(enumErrores.passwordVacio));
            }
            else if (EsPasswordFuerte(req.usuario.password))
            {
                res.resultado = false;
                res.error.Add(Utilitarios.Utilitarios.crearError(enumErrores.passwordDebil));
            }

            //¿hay errores?
            if (res.error.Any())
            {
                //Hay errores
                return res;
            }
            else
            {
                Guid? guidUsuario = Guid.Empty;
                int? idReturn = 0;
                int? errorId = 0;
                string errorDescripcion = "";


                //No hay errores ¡enviar a la base de datos!
                ConexionLinqDataContext miLinq = new ConexionLinqDataContext();
                miLinq.SP_INGRESAR_USUARIO(req.usuario.nombre, req.usuario.apellidos, req.usuario.correoElectronico, req.usuario.password, "XXXX", ref guidUsuario, ref idReturn, ref errorId, ref errorDescripcion);

            }


        }


        public static bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            try
            {
                var mail = new MailAddress(correo);
                return mail.Address == correo;
            }
            catch
            {
                return false;
            }
        }

        public static bool EsPasswordFuerte(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Mínimo 8 caracteres, 1 mayúscula, 1 minúscula, 1 número y 1 carácter especial
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");
        }
    }
}

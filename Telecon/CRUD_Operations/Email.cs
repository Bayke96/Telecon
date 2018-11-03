using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Telecon.CRUD_Operations
{
    public class Email
    {
        public void SendPasswordEmail(string email, string userName, string password)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();

            try
            {
                msg.Subject = "Telecon Erickson C.A. - Registro de Usuario / User Registration";
                msg.Body = "<html><center><h1>TELECON ERICKSON C.A. - REGISTRO DE USUARIO / USER REGISTRATION</h1><br /><br />" +
                     "<b>Credenciales de Inicio / User Credentials</b><br /><br />" +
                    "<table border='1'>" +
                    "<tr bgcolor='cyan'>" +
                    "<td><b><center>Nombre de Usuario / Username</center></b></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td><b><center>"+ userName +"</center></b></td>" +
                    "</tr>" +
                    "<tr bgcolor='cyan'>" +
                    "<td><b><center>Contraseña / Password</center></b></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td><b style='color: red;'><center>" + password + "</center></b></td>" +
                    "</tr>" +
                    "</table>" +
                    "<br /><br /><p style='color: green;'><b>Use esta información para ingresar al sistema y cambiar su contraseña.<br />" +
                    "Use this information to log into the system and change your password.</b></p><br />" +
                    "<b>Se despide - Farewell,</b>" +
                    "<p style='color: blue;'><b>Telecon Erickson C.A.<br />RIF - J-30277176-5," +
                    "<br />+58 286-9313393.</b><p></center>";
                msg.To.Add(email);
                msg.IsBodyHtml = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
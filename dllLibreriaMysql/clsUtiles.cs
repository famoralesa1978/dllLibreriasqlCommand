using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.Net.Mime;

namespace dllLibreriaMysql
{
    public class clsUtiles
    {
        private SymmetricAlgorithm alg;
        const string sKey = "compass";

        #region "Contraseñas"

        public string GenerateHashMD5(string input)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            //  Compute the MD5 hash.
            DES.Key = hashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(sKey));
            //  Set the cipher mode.
            DES.Mode = CipherMode.ECB;
            //  Create the encryptor.
            ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            //  Get a byte array of the string.
            byte[] Buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(input);
            //  Transform and return the string.
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public string DecryptTripleDES(string sOut)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            //  Compute the MD5 hash.
            DES.Key = hashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(sKey));
            //  Set the cipher mode.
            DES.Mode = CipherMode.ECB;
            //  Create the decryptor.
            ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            byte[] Buffer = Convert.FromBase64String(sOut);
            //  Transform and return the string.
            return System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public Byte[] insertarPDF(string pdfnombre)
        {
            FileStream fs = new FileStream(pdfnombre, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            br.Close();
            fs.Close();
            return bytes;
        }

        #endregion

        #region "Enviar correo"

        public Boolean SendMailGmail(RichTextBox texto, string NombrePersona, string Asunto, string Correo, ListBox lstArchivo,string Desde,string Credencial,string Clave,string Host, int Port)
        {
            try
            {
                MailMessage correos = new MailMessage();
                SmtpClient envios = new SmtpClient();
                correos.To.Clear();
                correos.Body = "";
                //correos.Body = texto;
                correos.Subject = Asunto;
                correos.IsBodyHtml = true;
                correos.To.Add(Correo);
                string strMensaje = texto.Text.Replace("\n","<br/>") ;
               
                string htmlBody = "<html><body><div class='adn ads'>" + 
                            "<div class='gs'>" +
                            "<div class='qQVYZb'></div><div class='utdU2e'></div><div class='tx78Ic'></div><div class='aHl'></div><div tabindex='-1' id=':lt'></div><div class='ii gt adP adO' id=':li'>" +
                            "<div class='a3s aXjCH m15c9dda4a91c0951' id=':lh'><div dir='ltr'><div class='adM'><br clear='all'></div><div class='adM'><br></div>" + strMensaje + "<br><div class='m_744731337232571497gmail_signature' data-smartmail='gmail_signature'>" +
                            "<div dir='ltr'><div><div dir='ltr'><div><div dir='ltr'><div><div dir='ltr'><div><div dir='ltr'><div><div dir='ltr'><div><div dir='ltr'><div style='font-family: arial; font-size: small;'><div style='text-align: center;'>" +
                            "<div style='text-align: left;'><b><font color='#0b5394'><font face='tahoma, sans-serif'><br></font></font></b></div>" +
                            "<div style='text-align: left;'><b><font color='#0b5394'><font face='tahoma, sans-serif'><br></font></font></b></div>" +
                            "<div style='text-align: left;'><b>" +
                            "<font color='#0b5394'><font face='tahoma, sans-serif'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</font></font></b><b><font color='#0b5394'>" +
                            "<font face='tahoma, sans-serif'>" + NombrePersona + "&nbsp;</font></font></b>" +
                            "</div><div style='text-align: left;'><font face='tahoma, sans-serif'><font color='#0b5394'><b>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Administraci&oacuten</b>&nbsp;</font></font>" +
                            "<span style='font-family: tahoma,sans-serif;'>&nbsp;</span></div></div></div><div style='text-align: center; font-family: arial; font-size: small;'><div style='text-align: left;'>" +
                            "<span style='text-align: center;'>&nbsp;&nbsp;<font face='tahoma, sans-serif'>&nbsp; &nbsp; &nbsp; X-Ray Protecci&oacuten Radiol&oacutegica Ltda.</font></span></div><div style='text-align: left;'>" +
                            "<font face='tahoma, sans-serif'><span style='text-align: center; font-size: x-small;'>&nbsp; &nbsp;&nbsp;</span><span style='text-align: center;'><font size='2'>&nbsp;San Antonio 50 Of. 403, Santiago, Chile</font></span>" +
                            "</font></div><div style='text-align: left;'><font face='tahoma, sans-serif' size='2' style='text-align: center;'>&nbsp; &nbsp; &nbsp;(56)-2-26380724 / (56)-2-26323485&nbsp;</font></div></div>" +
                            "<span style='font-family: arial; font-size: small;'>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</span>" +
                            "<div style='font-family: arial; font-size: small;'>&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;" +
                            "<img width='200' height='143' tabindex='0' class='CToWUd a6T' src='https://ci4.googleusercontent.com/proxy/K72XLe_xqyjLoDUzOzjPd_eCAICzOMPPAI7e0drPiXcHO-Hw-YhFhpJCTCCf4NhaCU4GoVJ70kU=s0-d-e1-ft#http://oi60.tinypic.com/jk7mz4.jpg'/>" +
                            "<div class='a6S' style='left: 181.6px; top: 363.34px; opacity: 0.01;' dir='ltr'>" +
                            "<div tabindex='0' class='T-I J-J5-Ji aQv T-I-ax7 L3 a5q' id=':ng' role='button' aria-label='Descargar el archivo adjunto ' data-tooltip='Descargar' data-tooltip-class='a1V'><div class='aSK J-J5-Ji aYr'>" +
                            "</div></div></div></div></div></div></div></div></div></div></div></div></div></div></div></div></div></div><div class='yj6qo'></div><div class='adL'>" +
                            "</div></div><div class='adL'>" +
                            "</div></div></div><div class='ii gt' id=':mb' style='display: none;'><div class='a3s aXjCH undefined' id=':mc'></div></div><div class='hi'></div></div><div class='ajx'></div></div>" +
                            "</body></html>";
                //AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                //   (htmlBody, null, MediaTypeNames.Text.Html);
                //LinkedResource inline = new LinkedResource("firma.jpg", MediaTypeNames.Image.Jpeg);
                //inline.ContentId = "firma.jpg";
                //avHtml.LinkedResources.Add(inline);


                //htmlBody = htmlBody + String.Format(
                //        "<h3>Saluda atte:</h3>" +
                //       @"<img src=""cid:{0}"" />", inline.ContentId);

                correos.Body = htmlBody;
                //MailMessage mail = new MailMessage();
                //mail.AlternateViews.Add(avHtml);

             //   Attachment att = new Attachment(@"D:\Sistema_Desarrollo\MARCELO_24032017\FUENTES\ControlDosimetro2_sqlserver_sqlcommand\ControlDosimetro\Imagen\firma.jpg");
             //   att.ContentDisposition.Inline = true;

              //  correos.Attachments.Add(att);
               
                for (int i = 0; i < lstArchivo.Items.Count; i++)
                {
                    correos.Attachments.Add(new Attachment(@lstArchivo.Items[i].ToString()));     
                }


                correos.From = new MailAddress(Desde);
                envios.Credentials = new NetworkCredential(Credencial, DecryptTripleDES(Clave));

                //Datos importantes no modificables para tene¿¿r acceso a las cuentas

                envios.Host = Host;
                envios.Port = Port;
                envios.EnableSsl = true;

                envios.Send(correos);
                MessageBox.Show("El mensaje fue enviado correctamente");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se envio el correo correctamente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
                ////Configuración del Mensaje
                //MailMessage mail = new MailMessage();
                //mail.IsBodyHtml = true;
                //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                ////Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
            //    mail.From = new MailAddress("pipecato2@gmail.com", "TuNombre", Encoding.UTF8);
            //    //Aquí ponemos el asunto del correo
            //    mail.Subject = "Prueba de Envío de Correo";
            //    //Aquí ponemos el mensaje que incluirá el correo

            //    string htmlBody = "<html><body><h3>" + texto + "</h3><br><img src=\"cid:firma\"></body></html>";
            //    AlternateView avHtml = AlternateView.CreateAlternateViewFromString
            //       (htmlBody, null, MediaTypeNames.Text.Html);
            //    LinkedResource inline = new LinkedResource("firma.jpg", MediaTypeNames.Image.Jpeg);
            //    inline.ContentId = "firma.jpg";
            //    avHtml.LinkedResources.Add(inline);


            //    htmlBody = htmlBody + String.Format(
            //            "<h3>Saluda atte:</h3>" +
            //           @"<img src=""cid:{0}"" />", inline.ContentId);

            //    mail.Body = htmlBody;
            //   // MailMessage mail = new MailMessage();
            //    mail.AlternateViews.Add(avHtml);

            //    Attachment att = new Attachment(@"D:\Sistema_Desarrollo\MARCELO_24032017\FUENTES\ControlDosimetro2_sqlserver_sqlcommand\ControlDosimetro\Imagen\firma.jpg");
            //    att.ContentDisposition.Inline = true;
               
            //   // mail.Attachments.Add(att);

            //   // mail.Body = texto;
               
               
            //    //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
            //    mail.To.Add("pipecato2@hotmail.com");
            //    //Si queremos enviar archivos adjuntos tenemos que especificar la ruta en donde se encuentran
            //    mail.Attachments.Add(new Attachment(@"d:\\Documento\\compass.docx"));

            //    //Configuracion del SMTP
            //    SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
            //    //Especificamos las credenciales con las que enviaremos el mail
            //    SmtpServer.Credentials = new System.Net.NetworkCredential("pipecato2@gmail.com", "beto1980");
            //    SmtpServer.EnableSsl = true;
            //    SmtpServer.Send(mail);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }



        #endregion

#region "ConvertToHTML"
        //public string ConvertToHTML1(RichTextBox Box)
        //{
        //    // Takes a RichTextBox control and returns a
        //    // simple HTML-formatted version of its contents
        //    string strHTML;
        //    string strColour;
        //    bool blnBold;
        //    bool blnItalic;
        //    string strFont;
        //    strFont = "";
        //    short shtSize;
        //    int lngOriginalStart;
        //    int lngOriginalLength;
        //    int intCount;
        //    strColour = "";
        //    blnBold = false;
        //    shtSize = 1;
        //    strHTML = "";
        //    // If nothing in the box, exit
        //    if (Box.Text.Length == 0) return strHTML;
        //    // Store original selections, then select first character
        //    lngOriginalStart = 0;
        //    lngOriginalLength = Box.TextLength;
        //    Box.Select(0, 1);
        //    // Add HTML header
        //    strHTML = "<html>";
        //    // Set up initial parameters
        //    //strColour = Box.SelectionColor.ToKnownColor.ToString();
        //    //blnBold = Box.SelectionFont.Bold;
        //    //blnItalic = Box.SelectionFont.Italic;
        //    //strFont = Box.SelectionFont.FontFamily.Name;
        //    //shtSize = Box.SelectionFont.Size;
        //    // Include first 'style' parameters in the HTML
        //    strHTML += "<span style=\"font-family: " + strFont + "; font-size: " + shtSize + "pt; color: " + strColour + "\">";
        //    // Include bold tag, if required
        //    if (blnBold == true)
        //    {
        //        strHTML += "<b>";
        //    }
        //    // Include italic tag, if required
        //    if (blnItalic == true)
        //    {
        //        strHTML += "<i>";
        //    }
        //    // Finally, add our first character
        //    strHTML += Box.Text.Substring(0, 1);
        //    // Loop around all remaining characters
        //    for (intCount = 2; intCount <= Box.Text.Length; intCount++)
        //    {
        //        // Select current character
        //        Box.Select(intCount - 1, 1);
        //        // If this is a line break, add HTML tag
        //        if (Convert.ToChar(Box.Text.Substring(intCount - 1, 1)) == Convert.ToChar(10))
        //        {
        //            strHTML += "<br>";
        //        }
        //        // Check/implement any changes in style
        //        if (Box.SelectionColor.ToKnownColor.ToString() != strColour | Box.SelectionFont.FontFamily.Name != strFont | Box.SelectionFont.Size != shtSize)
        //        {
        //            strHTML += "</span><span style=\"font-family: " + Box.SelectionFont.FontFamily.Name + "; font-size: " + Box.SelectionFont.Size + "pt; color: " + Box.SelectionColor.ToKnownColor.ToString() + "\">";
        //        }
        //        // Check for bold changes
        //        if (Box.SelectionFont.Bold != blnBold)
        //        {
        //            if (Box.SelectionFont.Bold == false)
        //            {
        //                strHTML += "</b>";
        //            }
        //            else
        //            {
        //                strHTML += "<b>";
        //            }
        //        }
        //        // Check for italic changes
        //        if (Box.SelectionFont.Italic != blnItalic)
        //        {
        //            if (Box.SelectionFont.Italic == false)
        //            {
        //                strHTML += "</i>";
        //            }
        //            else
        //            {
        //                strHTML += "<i>";
        //            }
        //        }
        //        // Add the actual character
        //        strHTML += Box.Text.Substring(intCount, 1);
        //       // strHTML += Mid(Box.Text, intCount, 1);
        //        // Update variables with current style
        //       // strColour = Box.SelectionColor.tostring();
        //        blnBold = false;// Box.SelectionFont;
        //        //blnItalic = Box.SelectionFont.Italic;
        //       // strFont = Box.SelectionFont.FontFamily.Name;
        //        //shtSize = Box.SelectionFont.Size;
        //    }
        //    // Close off any open bold/italic tags
        //    if (blnBold == true) strHTML += "";
        //    if (blnItalic == true) strHTML += "";
        //    // Terminate outstanding HTML tags
        //    strHTML += "</span></html>";
        //    // Restore original RichTextBox selection
        //    Box.Select(lngOriginalStart, lngOriginalLength);
        //    // Return HTML
        //    return strHTML;
        //}
#endregion
        

    }
}

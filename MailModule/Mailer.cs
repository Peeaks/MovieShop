using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DLL.Entities;

namespace MailModule
{
    public class Mailer
    {
        public void SendMail(Order order) {
            var fromAddress = new MailAddress("NoReplyPineappleInc@gmail.com", "Pineapple Inc.");
            var toAddress = new MailAddress(order.ApplicationUser.Email, $"{order.ApplicationUser.FirstName} {order.ApplicationUser.LastName}");
            const string fromPassword = "Test123$";
            const string subject = "Confirmation mail from Movie Shop";
            const string body = "Thank you for buying your movies at the Pineapple Inc. movie shop";

            var smtp = new SmtpClient {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress) {
                Subject = subject,
                Body = body
            }) {
                smtp.Send(message);
            }
        }
    }
}

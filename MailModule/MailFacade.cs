using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DLL.Entities;

namespace MailModule {
    public class MailFacade {

        public void SendReceiptMail(Order order) {
            var thread = new Thread(Target);
            thread.Start(order);
        }

        private void Target(Object o) {
            var mailer = new Mailer();
            mailer.SendReceipt((Order) o);
        }
    }
}

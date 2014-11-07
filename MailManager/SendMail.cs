using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailManager
{
    public class SendMail
    {
        System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
        System.Net.Mail.MailMessage Message;

        public SendMail(string ID="logmein@akamatsu.info", string Pass="akmt7092")
        {
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;

            smtpClient.Credentials = new System.Net.NetworkCredential(ID, Pass);

            smtpClient.EnableSsl = true;
        }

        public void CreateMessage(string FromEMail, string ToEMail, string Subject, string Body)
        {
            Message = new System.Net.Mail.MailMessage(FromEMail, ToEMail, Subject, Body);
        }

        public void AddAttachemnt(string FileName)
        {
            if (Message == null)
                return;

            Message.Attachments.Add(new System.Net.Mail.Attachment(FileName));
        }

        public void SendMessage()
        {
            smtpClient.Send(Message);
        }
    }


}

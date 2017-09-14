using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RAT
{
    class mailsender
    {
        

        public void sendmail()
        {
            String mailfilepath = "C:/Rat/incidents.html";
           
            
            
            ArrayList list_emails = new ArrayList();
            


            MailAddress from = new MailAddress("mukeshcse41@gmail.com");
            MailAddress to = new MailAddress("kumar.muk@hcl.com");
            //MailMessage mail = new MailMessage();
            MailMessage mail = new MailMessage(from, to);


            //mail.CC.Add("Vikas.Garg@brisbane.qld.gov.au");
            //mail.Bcc.Add("Mukesh.Kumar@brisbane.qld.gov.au");
            //mail.CC.Add("mukesh.cse1@gmail.com");
            mail.Subject = "RAT: Queue Notifications";

            
            string a = ("<html><body>");
            string b = ("<p>Hi,<br>Below incidents are in Queue, Please take action</p><br>");
            string c = ("<head><style>table, th, td { border: 1px solid black;  border - collapse: collapse;} th, td { padding: 5px; text - align: left;}</style> </head>");
            string d = ("<table style='width: 100 % '>  <font size='2' face='Courier New'><tr><td><caption>Incident Summary</caption>");
            string e = ("</td></tr></font></table></body></html>");
            
            
            mail.Body = a+b+c+d+System.IO.File.ReadAllText("C:/Rat/incidents.html")+e;
            
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                "mukeshcse41@gmail.com", "NunuPiku7629");
            smtp.EnableSsl = true;
           
            smtp.Send(mail);
            

        }
    }
}

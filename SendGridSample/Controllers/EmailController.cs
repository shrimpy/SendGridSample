using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using SendGrid;

namespace SendGridSample.Controllers
{
    public class EmailController : ApiController
    {
        [HttpGet]
        [Route("api/SendMail")]
        public string SendMail(string email, string username, string password)
        {

            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();

            // Add the message properties.
            myMessage.From = new MailAddress("hello@hello.com");

            // Add multiple addresses to the To field.
            List<String> recipients = new List<String> { email };

            myMessage.AddTo(recipients);

            myMessage.Subject = "Testing the SendGrid Library";

            //Add Text bodies
            myMessage.Text = "Hello World from " + Environment.MachineName;

            // Create a Web transport, using API Key
            NetworkCredential nc = new NetworkCredential(username, password);
            var transportWeb = new Web("This string is a SendGrid API key", nc, TimeSpan.FromSeconds(15));

            // Send the email.
            transportWeb.DeliverAsync(myMessage);

            return "done";
        }
    }
}
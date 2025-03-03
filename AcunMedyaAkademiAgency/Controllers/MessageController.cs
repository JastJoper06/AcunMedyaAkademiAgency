using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiAgency.Context;
using AcunMedyaAkademiAgency.Entities;

namespace AcunMedyaAkademiAgency.Controllers
{
    public class MessageController : Controller
    {
        AgencyContext context = new AgencyContext();
        public ActionResult MessageList()
        {
            var values = context.Messages.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CreateMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMessage(Message message)
        {
            context.Messages.Add(message);
            context.SaveChanges();
            return RedirectToAction("MessageList");
        }
        [HttpGet]
        public ActionResult DeleteMessage(int id)
        {
            var value = context.Messages.Find(id);
            context.Messages.Remove(value);
            context.SaveChanges();
            return RedirectToAction("MessageList");
        }
        [HttpGet]
        public ActionResult UpdateMessage(int id)
        {
            var value = context.Messages.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateMessage(Message message)
        {
            var value = context.Messages.Find(message.MessageId);
            value.NameSurname = message.NameSurname;
            value.Email = message.Email;
            value.Title = message.Title;
            value.Description = message.Description;
            value.SendDate = message.SendDate;
            value.IsRead = message.IsRead;
            context.SaveChanges();
            return RedirectToAction("MessageList");
        }
        [HttpPost]
        public ActionResult SendMessage(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // SMTP ayarları (Örneğin Gmail için)
                    var smtpClient = new SmtpClient("smtp.example.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("your-email@example.com", "your-password"),
                        EnableSsl = true
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("your-email@example.com"),
                        Subject = "New Contact Form Message",
                        Body = $"Name: {model.NameSurname}\nEmail: {model.Email}\nPhone: {model.TelNo}\nMessage: {model.Message}",
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add("your-email@example.com");

                    smtpClient.Send(mailMessage);

                    return Json(new { success = true, message = "Your message has been sent successfully!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "An error occurred while sending the email. " + ex.Message });
                }
            }

            return Json(new { success = false, message = "Please fill out all required fields correctly." });
        }
    }

}

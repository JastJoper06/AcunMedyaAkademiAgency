using AcunMedyaAkademiAgency.Context;
using AcunMedyaAkademiAgency.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Message = AcunMedyaAkademiAgency.Entities.Message;

namespace AcunMedyaAkademiAgency.Controllers
{
    public class DefaultController : Controller
    {
        AgencyContext context = new AgencyContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProjectPartial()
        {
            var values = context.Projects.OrderByDescending(x => x.ProjectId).Take(6).ToList();
            return PartialView(values);
        }
        public PartialViewResult ModalPartial()
        {
            var values = context.ProjectDetails.ToList();
            return PartialView(values);
        }
        public PartialViewResult HeadPartial()
        {
            return PartialView();
        }
        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
        public PartialViewResult ServicePartial()
        {
            var values = context.Services.OrderByDescending(x => x.ServiceId).Take(3).ToList();
            return PartialView(values);
        }
        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult AboutPartial()
        {
            var values = context.Abouts.ToList();
            return PartialView(values);
        }
        public PartialViewResult ClientPartial()
        {
            return PartialView();
        }

        public PartialViewResult ScriptPartial()
        {
            return PartialView();
        }
        public PartialViewResult MastheadPartial()
        {
            var values = context.Features.ToList();
            return PartialView(values);
        }
        public PartialViewResult TeamPartial()
        {
            var values = context.Teams.ToList();
            return PartialView(values);
        }
        public PartialViewResult MessagePartial()
        {
            var values = context.Messages.ToList();
            return PartialView(values);
        }
        [HttpPost]
        public ActionResult SendMessage(Message message)
        {
            if (message.NameSurname != null && message.Title != null && message.Email != null && message.Description != null)
            {
                message.SendDate = DateTime.Now;
                message.IsRead = false;
                context.Messages.Add(message);
                context.SaveChanges();

                TempData["successMessage"] = "Mesajınız Başarı İle Gönderildi";

            }
            else
            {
                TempData["errorMessage"] = "Mesajınız Gönderilemedi";
            }
            return Redirect("/Default/Index#contact");
        }    
        public PartialViewResult SocialMediaPartial()
        {
            var values = context.SocialMedias.ToList();
            return PartialView(values);
        }
        public PartialViewResult GoogleMapsPartial()
        {
            return PartialView();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiAgency.Context;

namespace AcunMedyaAkademiAgency.Controllers
{
    public class DashboardController : Controller
    {
        AgencyContext context = new AgencyContext();
        public ActionResult Index()
        {
            ViewBag.bildirimsayisi = context.Notifications.ToList().Count();
            ViewBag.projectsayisi = context.Projects.ToList().Count();
            int categorysosyal = context.Categories.Where(x => x.CategoryName == "Eğitim").Select(y => y.CategoryId).FirstOrDefault();
            ViewBag.sosyalsayisi = context.Projects.Where(x => x.CategoryId == categorysosyal).Count();
            ViewBag.personelsayisi = context.Teams.ToList().Count();
            ViewBag.hizmetsayisi = context.Services.ToList().Count();
            ViewBag.mesajsayisi = context.Messages.ToList().Count();
            ViewBag.kategorisayisi = context.Categories.ToList().Count();
            ViewBag.kullanicisayisi = context.Admins.ToList().Count();

            return View();
        }
        public PartialViewResult StaffPartial()
        {
            var values = context.Teams.ToList();
            return PartialView(values);
        }
        public ActionResult BusinessSurveyPartial()
        {
            var values = context.Messages.OrderByDescending(x => x.MessageId).Take(5).ToList();
            return PartialView(values);

        }
    }
}
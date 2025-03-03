using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiAgency.Context;
using AcunMedyaAkademiAgency.Entities;

namespace AcunMedyaAkademiAgency.Controllers
{
    public class NotificationController : Controller
    {
        AgencyContext context = new AgencyContext();
        public ActionResult NotificationList()
        {
            var values = context.Notifications.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CreateNotification()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNotification(Notification notification)
        {
            context.Notifications.Add(notification);
            context.SaveChanges();
            return RedirectToAction("NotificationList");
        }
        public ActionResult DeleteNotification(int id)
        {
            var value = context.Notifications.Find(id);
            context.Notifications.Remove(value);
            context.SaveChanges();
            return RedirectToAction("NotificationList");
        }
        [HttpGet]
        public ActionResult UpdateNotification(int id)
        {
            var value = context.Notifications.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateNotification(Notification notification)
        {
            var value = context.Notifications.Find(notification.NotificationId);
            value.NameSurname = notification.NameSurname;
            value.Description = notification.Description;
            context.SaveChanges();
            return RedirectToAction("NotificationList");
        }
    }
}
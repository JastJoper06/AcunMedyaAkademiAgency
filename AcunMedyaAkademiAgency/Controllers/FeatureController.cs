using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcunMedyaAkademiAgency.Context;
using AcunMedyaAkademiAgency.Entities;

namespace AcunMedyaAkademiAgency.Controllers
{
    public class FeatureController : Controller
    {
        AgencyContext context = new AgencyContext();
        public ActionResult FeatureList()
        {
            var values = context.Features.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CreateFeature()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateFeature(Feature feature)
        {
            context.Features.Add(feature);
            context.SaveChanges();
            return RedirectToAction("FeatureList");
        }
        public ActionResult DeleteFeature(int id)
        {
            var value = context.Features.Find(id);
            context.Features.Remove(value);
            context.SaveChanges();
            return RedirectToAction("FeatureList");
        }
        [HttpGet]
        public ActionResult UpdateFeature(int id)
        {
            var value = context.Features.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateFeature(Feature feature)
        {
            var value = context.Features.Find(feature.FeatureId);
            value.Title = feature.Title;
            value.Description = feature.Description;
            context.SaveChanges();
            return RedirectToAction("FeatureList");
        }
    }
}
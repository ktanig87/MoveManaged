
using Microsoft.AspNet.Identity;
using MoveManaged.Models;
using MoveManaged.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoveManaged.WebMVC.Controllers
{
    [Authorize]
    public class DamageClaimController : Controller
    {
        // GET: DamageClaim
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DamageClaimService(userId);
            var model = service.GetClaims();

            return View(model);
        }

        //GET
        public ActionResult Create()
        { return View(); }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DamageClaimCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var service = CreateClaimService();
            if (service.CreateClaim(model))
            {
                TempData["SaveResult"] = "Your Damage Claim was Created";
                return RedirectToAction("index");
            };
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateClaimService();
            var model = svc.GetClaimById(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateClaimService();
            var detail = service.GetClaimById(id);
            var model =
                new DamageClaimEdit
                {
                    ClaimId = detail.ClaimId,
                    Description = detail.Description,
                    InventoryId = detail.InventoryId,
                    ClaimSubmitted = detail.ClaimSubmitted,
                    ClaimNotes = detail.ClaimNotes,
                    ClaimResolved = detail.ClaimResolved
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DamageClaimEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.ClaimId != id)
            {
                ModelState.AddModelError("", "Invalid ID");
                return View(model);
            }

            var service = CreateClaimService();
            if (service.UpdateClaim(model))
            {
                TempData["SaveResult"] = "Your Damage Claim has been updated";
                return RedirectToAction("index");
            }
            ModelState.AddModelError("", "Your Damage Claim could not be updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateClaimService();
            var model = svc.GetClaimById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBox(int id)
        {
            var service = CreateClaimService();
            service.DeleteClaim(id);
            TempData["SaveResult"] = "Your Claim was deleted successfully";
            return RedirectToAction("index");
        }


        private DamageClaimService CreateClaimService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DamageClaimService(userId);
            return service;
        }
    }
}
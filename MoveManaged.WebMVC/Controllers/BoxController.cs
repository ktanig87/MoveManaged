
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
    public class BoxController : Controller
    {
        // GET: Box
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BoxService(userId);
            var model = service.GetBoxes();

            return View(model);
        }

        //GET
        public ActionResult Create()
        { return View(); }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BoxCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var service = CreateBoxService();
            if (service.CreateBox(model))
            {
                TempData["SaveResult"] = "Your Box was Created";
                return RedirectToAction("index");
            };
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateBoxService();
            var model = svc.GetBoxId(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateBoxService();
            var detail = service.GetBoxId(id);
            var model =
                new BoxEdit
                {
                    BoxSize = detail.BoxSize,
                    MoveId = detail.MoveId,
                    RoomId = detail.RoomId
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BoxEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.BoxId != id)
            {
                ModelState.AddModelError("", "Invalid ID");
                return View(model);
            }

            var service = CreateBoxService();
            if (service.UpdateBox(model))
            {
                TempData["SaveResult"] = "Your Box has been updated";
                return RedirectToAction("index");
            }
            ModelState.AddModelError("", "Your Box could not be updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateBoxService();
            var model = svc.GetBoxId(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBox(int id)
        {
            var service = CreateBoxService();
            service.DeleteBox(id);
            TempData["SaveResult"] = "Your Box was deleted successfully";
            return RedirectToAction("index");
        }


        private BoxService CreateBoxService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BoxService(userId);
            return service;
        }
    }
}
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
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            var service = CreateRoomService();
            var model = service.GetRooms();
            return View(model);
        }
        
        public ActionResult Create()
        { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var service = CreateRoomService();

            if(service.CreateMove(model))
            {
                TempData["SaveResult"] = "Your room was created";
                return RedirectToAction("index");
            };
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateRoomService();
            var detail = service.GetRoombyId(id);
            var model =
                new RoomEdit
                {
                    RoomId = detail.RoomId,
                    RoomName = detail.RoomName,
                    MoveId = detail.MoveId
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RoomEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.RoomId !=id)
            {
                ModelState.AddModelError("", "Id does not match");
                return View(model);
            }
            var service = CreateRoomService();

            if(service.UpdateRoom(model))
            {
                TempData["SaveResult"] = "Your Room has been updated";
                return RedirectToAction("index");
            }
            ModelState.AddModelError("", "Your Room could not be updated");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateRoomService();
            var model = svc.GetRoombyId(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoom(int id)
        {
            var service = CreateRoomService();
            service.DeleteRoom(id);
            TempData["SaveResult"] = "You Room was successfully deleted";
            return RedirectToAction("index");
        }


        private RoomService CreateRoomService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RoomService(userId);
            return service;
        }
    }
}
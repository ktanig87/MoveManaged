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
    public class InventoryItemController : Controller
    {
        // GET: InventoryItem
        public ActionResult Index()
        {
            var service = CreateInventoryItemService();
            var model = service.GetInventoryItems();
            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryItemCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateInventoryItemService();

            if (service.CreateItem(model))
            {
                TempData["SaveResult"] = "Your Inventory Item was created.";
                return RedirectToAction("index");
            };
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateInventoryItemService();
            var model = svc.GetItemById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateInventoryItemService();
            var detail = service.GetItemById(id);
            var model =
                new InventoryItemEdit
                {
                    InventoryId = detail.InventoryId,
                    Name = detail.Name,
                    Description = detail.Description,
                    Condition = detail.Condition,
                    ItemValue = detail.ItemValue,
                    UPC = detail.UPC,
                    BoxId = detail.BoxId,
                    RoomId = detail.RoomId
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InventoryItemEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.InventoryId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateInventoryItemService();

            if (service.UpdateInventoryItem(model))
            {
                TempData["SaveResult"] = "You inventory item was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your inventory could not be updated");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateInventoryItemService();
            var model = svc.GetItemById(id);

            return View(model);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateInventoryItemService();
            service.DeleteInventoryItem(id);
            TempData["SaveResult"] = "Your Inventory Item was deleted";
            return RedirectToAction("index");
        }


        private InventoryItemService CreateInventoryItemService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new InventoryItemService(userId);
            return service;
        }
    }
}
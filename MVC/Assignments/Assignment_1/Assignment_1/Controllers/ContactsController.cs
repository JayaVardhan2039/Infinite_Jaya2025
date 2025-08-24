using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Assignment_1.Models;
using Assignment_1.Repositories;

namespace Assignment_1.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactRepository _repository;
        public ContactsController() : this(new ContactRepository(new Data.ContactContext()))
        {

        }
        public ContactsController(IContactRepository repository)
        {
            _repository = repository;
        }
        public async Task<ActionResult> Index()
        {
            var contacts = await _repository.GetAllAsync();
            return View(contacts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contact = await _repository.GetByIdAsync(id.Value);
            if (contact == null) return HttpNotFound();
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(long id, Contact contact)
        {
            if (id != contact.Id) return new HttpStatusCodeResult(400);

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var contact = await _repository.GetByIdAsync(id.Value);
            if (contact == null) return HttpNotFound();
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
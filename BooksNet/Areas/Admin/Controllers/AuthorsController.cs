﻿using BooksNet.Areas.Admin.ViewModels.Author;
using BooksNet.Models;
using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BooksNet.Areas.Admin.Controllers
{
  public class AuthorsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    public async Task<ActionResult> Index()
    {
      return View(await db.Authours.Include(a => a.Books).ToListAsync());
    }

    public async Task<ActionResult> Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Author author = await db.Authours.FindAsync(id);
      if (author == null)
      {
        return HttpNotFound();
      }
      return View(author);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(NewAuthorViewModel model)
    {
      if (ModelState.IsValid)
      {
        db.Authours.Add(new Author()
        {
          FirstName = model.FirstName,
          LastName = model.LastName,
          Email = model.Email,
          Address = model.Address,
          PhoneNumber = model.PhoneNumber,
          CreateDate = DateTime.Now,
          LastUpdate = DateTime.Now
        });
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }

      return View(model);
    }

    public async Task<ActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Author author = await db.Authours.FindAsync(id);
      if (author == null)
      {
        return HttpNotFound();
      }
      return View(new EditAuthorViewModel()
      {
        Id = author.Id,
        FirstName = author.FirstName,
        LastName = author.LastName,
        Email = author.Email,
        Address = author.Address,
        PhoneNumber = author.PhoneNumber,
        Version = author.Version
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(EditAuthorViewModel model)
    {
      if (ModelState.IsValid)
      {
        Author author = await db.Authours.FindAsync(model.Id);

        author.FirstName = model.FirstName;
        author.LastName = model.LastName;
        author.Email = model.Email;
        author.Address = model.Address;
        author.PhoneNumber = model.PhoneNumber;
        author.LastUpdate = DateTime.Now;

        db.Entry(author).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      return View(model);
    }

    public async Task<ActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Author author = await db.Authours.FindAsync(id);
      if (author == null)
      {
        return HttpNotFound();
      }
      return View(author);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
      Author author = await db.Authours.FindAsync(id);
      db.Authours.Remove(author);
      await db.SaveChangesAsync();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
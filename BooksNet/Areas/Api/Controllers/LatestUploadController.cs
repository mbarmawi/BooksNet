﻿using BooksNet.Models;
using System.Linq;
using System.Web.Http;

namespace BooksNet.Areas.Api.Controllers
{
  public class LatestUploadController : ApiController
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    public IQueryable<Book> Get()
    {
      return db.Books.OrderByDescending(b => b.Id).Take(8);
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageGallery.DAL;
using ImageGallery.Models;

namespace ImageGallery.Controllers
{
    public class HomeController : Controller
    {
        private GalleryContext db = new GalleryContext();

        public ActionResult Index(string filter = null, int page = 1, int pageSize = 20)
        {
            var records = new PagedList<Photo>();
            ViewBag.filter = filter;

            records.Content = this.db.Photos
                        .Where(x => filter == null || x.Description.Contains(filter))
                        .OrderByDescending(x => x.PhotoId)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = this.db.Photos
                            .Where(x => filter == null || x.Description.Contains(filter)).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return this.View(records);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var photo = new Photo();
            return this.View(photo);
        }

        [HttpPost]
        public ActionResult Create(Photo photo, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
            {
                return this.View(photo);
            }

            if (files.Count() == 0 || files.FirstOrDefault() == null)
            {
                ViewBag.error = "Please choose a file";
                return this.View(photo);
            }

            var model = new Photo();
            foreach (var file in files)
            {
                if (file.ContentLength == 0)
                {
                    continue;
                }

                model.Description = photo.Description;
                var fileName = Guid.NewGuid().ToString();
                var extension = System.IO.Path.GetExtension(file.FileName).ToLower();

                using (var img = Image.FromStream(file.InputStream))
                {
                    model.ThumbPath = string.Format("/GalleryImages/thumbs/{0}{1}", fileName, extension);
                    model.ImagePath = string.Format("/GalleryImages/{0}{1}", fileName, extension);

                    // Save thumbnail size image, 100 x 100
                    this.SaveToFolder(img, fileName, extension, new Size(100, 100), model.ThumbPath);

                    // Save large size image, 800 x 800
                    this.SaveToFolder(img, fileName, extension, new Size(600, 600), model.ImagePath);
                }

                // Save record to database
                this.SaveToDB(model);
            }

            return this.Redirect("/home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return this.View();
        }

        public Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                {
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                }
                else
                {
                    tempval = newSize.Width / (imageSize.Width * 1.0);
                }

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
            {
                finalSize = imageSize; // image is already small size
            }

            return finalSize;
        }

        private void SaveToFolder(Image img, string fileName, string extension, Size newSize, string pathToSave)
        {
            // Get new resolution
            Size imgSize = this.NewImageSize(img.Size, newSize);
            using (Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(Server.MapPath(pathToSave), img.RawFormat);
            }
        }

        private void SaveToDB(Photo photo)
        {
            photo.CreatedOn = DateTime.Now;
            this.db.Photos.Add(photo);
            this.db.SaveChanges();
        }
    }
}
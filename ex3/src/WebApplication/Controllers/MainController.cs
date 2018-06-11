using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class MainController : Controller
    {
        private Client tcpClient = Client.Instance;
        //create student property
        private static StudentsInfoList students = new StudentsInfoList();

        // GET: Main
        [HttpGet]
        public ActionResult ImageWeb()
        {
            //try to connect to server
            tcpClient.Connect();
            //get status of connection
            Boolean status = tcpClient.IsConnected();
            if (status) ViewBag.Status = "Connected yay";
            else ViewBag.Status = "dis-Connected boo";
            return View(students.Students);
        }
        [HttpGet]
        public ActionResult Logs()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Photos()
        {
            ViewBag.Ta = "Thum!";
            PhotosGallery gallery = PhotosGallery.Instance;
            return View(gallery.PhotoList);
        }

        [HttpGet]
        public string RemovePic(string path)
        {
            PhotosGallery gallery = PhotosGallery.Instance;
            return gallery.RemovePicFromComp(path);
            //return path;
        }
    }
}
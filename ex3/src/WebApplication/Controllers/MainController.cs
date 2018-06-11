﻿using System;
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
            Settings.Instance.GetData();
            //get status of connection
            Boolean status = tcpClient.IsConnected();
            if (status) ViewBag.Status = "Connected";
            else ViewBag.Status = "dis-Connected";
            ViewBag.picNumber = PhotosGallery.Instance.PhotoList.Count();
            return View(students.Students);
        }


        [HttpGet]
        public ActionResult Photos()
        {
            tcpClient.Connect();
            Settings.Instance.GetData();
            ViewBag.Ta = "Thum!";
            PhotosGallery gallery = PhotosGallery.Instance;
            gallery.CreatePhotosList(Settings.Instance.OutPutDur + '\\' + "Thumbnails");
            return View(gallery.PhotoList);
        }

        [HttpGet]
        public string RemovePic(string path)
        {
            PhotosGallery gallery = PhotosGallery.Instance;
            return gallery.RemovePicFromComp(path);
            //return path;
        }

        public ActionResult ViewFullPhoto(string name, string timeTaken, string path, string relPath)
        {
            //path = path.Replace(@"\Thumbnails", "");
            string cleanPath = relPath.Replace("/Thumbnails", "");

            return View(new PhotoInfo(path, timeTaken, name, cleanPath));
        }
    }
}
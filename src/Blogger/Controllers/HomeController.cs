﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Blogger.Models;

namespace Blogger.Controllers
{
    public class HomeController : Controller
    {
        [FromServices]
        public ApplicationDbContext DbContext { get; set; }
        public IActionResult Index()
        {
            var allPosts = new PostCollection(DbContext.Posts.OrderBy(p => p.TimeStamp).ToArray());
            return View(allPosts);
        }

        public IActionResult About()
        {
            ViewBag.Message = "Post stuff on this site";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Have an issue? Contact us";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        [Authorize]
        public IActionResult Profile()
        {

            var allMyPosts = new PostCollection(DbContext.Posts
                .Where(p => p.PosterEmail == Context.User.Identity.Name).ToArray());
           
            return View(allMyPosts);
        }


    }
}

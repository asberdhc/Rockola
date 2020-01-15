using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Web.Mvc;

namespace Rockola.Controllers
{
    public class HomeController : Controller
    {
        List<string> PlayList = new List<string>();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SearchVideo(string Keyword)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyAV7dZdpjsqH9K2IkJ0tlqHWklDay0qMW4",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = Keyword; // Replace with your search term.
            searchListRequest.MaxResults = 10;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();
            return PartialView("Search", searchListResponse.Items);
        }
        [HttpGet]
        public ActionResult AddPlayList(string idVideo)
        {
            //var arreglo = idVideo.Split('_');
            //var id = arreglo[0].Trim(' ');
            //var titulo = arreglo[1];
            Declare();
            List<string> auxList = (List<string>)Session["Playlist"];
            auxList.Add(idVideo);
            Session["Playlist"] = auxList;
            return PartialView("AddPlay", auxList);
        }
        [HttpGet]
        public ActionResult reproduceVideo(string idVideo)
        {
            return PartialView("reproduceVideo", idVideo);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public void Declare()
        {
            List<string> PLAYLISTVIDEOS = new List<string>();
            if (Session["Playlist"] == null)
            {
                Session["Playlist"] = PLAYLISTVIDEOS;

            }
        }
    }
}
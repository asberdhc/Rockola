using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Rockola.ViewModels;
using Newtonsoft.Json;

namespace Rockola.Controllers
{
    public class HomeController : Controller
    {
        private static string PLAYLIST = "PLAYLIST";

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult SearchVideo(string Keyword)
        {
            List<SearchResultDTO> lisvideos = new List<SearchResultDTO>();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50144/");
            var response = client.GetAsync("api/MyYTAPI/GetVideo?keyword=" + Keyword);
            response.Wait();
            var result = response.Result;
            var readresult = result.Content.ReadAsStringAsync().Result;
            var resultadoFinal = JsonConvert.DeserializeObject<List<SearchResultDTO>>(readresult);

            return PartialView("Search", resultadoFinal);
        }

        [HttpGet]
        public ActionResult AddPlayList(string idVideo)
        {
            if (Session[PLAYLIST] == null)
                Session[PLAYLIST] = new List<string>();

            var auxList = (List<string>)Session[PLAYLIST];
            auxList.Add(idVideo);
            Session[PLAYLIST] = auxList;
            return PartialView("AddPlay", auxList);
        }

        [HttpGet]
        public ActionResult RemoveFromPlayList(string videoId)
        {
            if(Session[PLAYLIST] != null)
            {
                var auxList = (List<string>)Session[PLAYLIST];
                auxList.Remove(videoId);
                Session[PLAYLIST] = auxList;
                return PartialView("AddPlay", auxList);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult reproduceVideo(string idVideo)
        {
            return PartialView("reproduceVideo", idVideo);
        }

        [HttpGet]
        public void AddToDB(string idVideo)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:53673/");
            var response = client.PostAsync("api/history?videoId=" + idVideo, new StringContent(""));
            response.Wait();
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
    }
}
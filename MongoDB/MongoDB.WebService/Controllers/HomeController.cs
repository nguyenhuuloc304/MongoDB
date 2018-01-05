using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.WebService.Models;
using MongoDB.Driver;

namespace MongoDB.WebService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            WorkWithMongo();
            return View();
        }

        private void WorkWithMongo()
        {
            // To directly connect to a single MongoDB server
            // (this will not auto-discover the primary even if it's a member of a replica set)

            // or use a connection string
            var client = new MongoClient("mongodb://alexadb:alexadb@ds237947.mlab.com:37947/alexadb");
            var database = client.GetDatabase("alexadb");
            var activityCollection = database.GetCollection<Models.Activity>(typeof(Models.Activity).Name);

            var act = new Models.Activity();
            act.Comment = "I love a...";
            act.Status = Status.Suspend;            
            act.CreatedDate = DateTime.Now;            
            act.yourDate = DateTime.Now.AddDays(3);
            act.People = new List<People>();
            act.People.Add(new People { Name = "Nguyen", Address = "OSD" });
            act.People.Add(new People { Name = "Luc", Address = "RnD" });


            activityCollection.InsertOne(act);


            var filterBuilder = Builders<Activity>.Filter;
            var filter = filterBuilder.Empty;

            var list = activityCollection.Find(filter).ToList();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

    }
}

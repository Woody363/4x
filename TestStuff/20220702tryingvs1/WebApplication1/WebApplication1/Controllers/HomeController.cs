using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public class LocationTableInDb //we will be able to use the generated class
        {
            public int xcoord {get;set;}
            public int ycoord {get;set;}
            public int phenominaFK {get;set;}


        }
        public IActionResult Index()
        {
            //we want natural phenomina
            int naturalPhenominaTypeId = 1;
            List<int> naturalPhenominaIds = new List<int>{1,2,3,4,5,10,11,12}; //we would do db2.phenomina.where(x=>x.typed_id =naturalPhenominaTypeId).Select(x=>x.id).ToList();
            List<LocationTableInDb>  locationTableInDb = new List<LocationTableInDb>();
            //populate the locations table
            for(int i = 0; i<10;i++)//this logic assumes the table is empty and does not check to ensure no duplication
            {
                    for(int j = 0; j<10;j++)
                {   
                        if(i==0 && j==0){continue;}//ive put something in zero zero already
                        if(new Random().NextDouble > 0.2)//we will populate 20% of the time
                        {
                            locationTableInDb.Add(new LocationTableInDb(){
                            xcoord = i,
                            ycoord = j,
                            phenominaFK = naturalPhenominaIds[new Random().Next(naturalPhenominaIds.Count)]}
                            );
                        };
                    }
                }
            }

            //db.LocationTableInDb.saveRange(locationTableInDb);
            //return dataToGiveToFront = db.locationTableInDb.Include(x=>x.phenomina.visualTable.id)



            //bool theyHavePermission = Dataquieries.CheckPermission();
            //if(theyHavePermission){
            return View();
            //}else
            //{throw new Exception();}
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
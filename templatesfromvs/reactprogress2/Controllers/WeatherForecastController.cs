using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using reactprogress2.Data.DataQueries;
using reactprogress2.Data.DbTables;
using reactprogress2.Data.DataModels;

namespace reactprogress2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


        protected readonly AppSettings appSettings;
        private TestQueries testQueries;
        public WeatherForecastController(AppSettings appSettings, TestQueries testQueries)
        {
            this.appSettings = appSettings;
            this.testQueries = testQueries;
        }




        public IActionResult GetPhenomTransistions( List<PhenomTransTriggerInstruction> phenomInst) //create js class
        {
            try
            {
                List<PhenomLoc> changedPhenoms = new List<PhenomLoc>() { };
                
                //just for  testing
                //List<PhenomTransTriggerInstruction> phenomInst = testQueries.GetDeletedPhenoms().Select(x => new PhenomTransTriggerInstruction(x.Id)).ToList<PhenomTransTriggerInstruction>();

                changedPhenoms = testQueries.GetPhenomsAfterTransistionTrigger(phenomInst);

                String serializedJson = JsonConvert.SerializeObject(changedPhenoms, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(serializedJson);

            } catch (Exception e) {
                FileHandler.FileHandler.WriteExceptionFile(e);
                return Ok(new { didFail = true });

            }

        }

        [HttpGet]
        public IActionResult InitialPhenomGenerations()
        {
            try
            {

                //we want natural phenomina
                int naturalPhenominaTypeId = 1;
                List<int> phenomIds = new List<int>();
                int[,] space = new int[1, 10];


                int numberPhenomsThatWereDeleted = testQueries.DeleteAllPhenomsExcept0();


                phenomIds = testQueries.GetPhenomOfTypeIds(new List<int>() { naturalPhenominaTypeId });
                List<PhenomLoc> locationTableInDb = new List<PhenomLoc>();
                //populate the locations table
                for (int i = 0; i < space.GetLength(0); i++)//this logic assumes the table is empty and does not check to ensure no duplication
                {
                    for (int j = 0; j < space.GetLength(1); j++)
                    {
                        if (i == 0 && j == 0) { continue; }//ive put something in zero zero already
                        if (new Random().NextDouble() < 0.2)//we will populate 20% of the time
                        {
                            locationTableInDb.Add(new PhenomLoc()
                            {
                                Xcoord = i,
                                Ycoord = j,
                                PhenominaId = phenomIds[new Random().Next(phenomIds.Count)]
                            }
                            );
                        };
                    }
                }
                if (locationTableInDb.Count == 0)
                {//if by chance their is nout give us something
                    locationTableInDb.Add(new PhenomLoc()
                    {
                        Xcoord = 9,
                        Ycoord = 9,
                        PhenominaId = phenomIds[new Random().Next(phenomIds.Count)]
                    }
                );
                }
                if (!testQueries.InsertPhenomLocs(locationTableInDb))
                {
                    //there was an error saving
                };

                List<PhenomLoc> locPenoms = new List<PhenomLoc>();
                locPenoms = testQueries.GetPhenomsInAllLoc(); //we would return this

                String serializedJson = JsonConvert.SerializeObject(locPenoms, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });


                return Ok(serializedJson);
          
            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                return Ok(new { didFail = true }); //FYI in the ajax start by diding if(response.didFail == true){Hande the error and return skipping the code below}
                ///qqqq would be nice to know how this is done else where BadRequest http codes etc really this is a 500 




            }
        }
    }
}




 
    

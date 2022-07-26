﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using reactprogress2.Data.DataQueries;

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




        [HttpGet]
        public IActionResult Get()
        {

            //we want natural phenomina
            int naturalPhenominaTypeId = 1;
            List<int> phenomIds = new List<int>();

            phenomIds = testQueries.GetPhenomOfTypeIds(new List<int>() { naturalPhenominaTypeId });
            List <WLocationsOfPhenomenon> locationTableInDb = new List<WLocationsOfPhenomenon>();
            //populate the locations table
            for (int i = 0; i < 10; i++)//this logic assumes the table is empty and does not check to ensure no duplication
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 && j == 0) { continue; }//ive put something in zero zero already
                    if (new Random().NextDouble() < 0.2)//we will populate 20% of the time
                    {
                        locationTableInDb.Add(new WLocationsOfPhenomenon()
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
                locationTableInDb.Add(new WLocationsOfPhenomenon()
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

                List<WLocationsOfPhenomenon> locPenoms = new List<WLocationsOfPhenomenon>();
                locPenoms = testQueries.GetPhenomsInAllLoc(); //we would return this

            String serializedJson = JsonConvert.SerializeObject(locPenoms, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            return Ok(serializedJson); 
                //this works
                //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                //{
                //    //data comes through this controller qqqq
                //    Date = DateTime.Now.AddDays(index),
                //    TemperatureC = Dataqueries.Dataqueries.GetAnyData(),//Random.Shared.Next(-20, 55),
                //    Summary = Dataqueries.Dataqueries.GetAnyName()
                //})
                //.ToArray();




            }
        }




 
    }

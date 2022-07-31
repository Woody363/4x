using Microsoft.EntityFrameworkCore;
using System.Configuration;
//using WebApi.Helpers;
using Microsoft.Extensions.Configuration;
using reactprogress2.Data;
using reactprogress2.Data.DbTables;
using reactprogress2.Data.DataModels;
using System.Linq;

namespace reactprogress2.Data.DataQueries
{
    public class TestQueries
    {

        protected readonly AppSettings appSettings;
        private PostgresContext db;

        public TestQueries(AppSettings appSettings, PostgresContext db)
        {
            this.appSettings = appSettings;
            this.db = db;
        }

        public int GetAnyData()
        {
            try
            {

                return db.Phenoms.FirstOrDefault()?.Id ?? 0;

            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                return 0; //return nothing found
            }


        }
        public string GetAnyName()
        {
            try
            {

                return db.Phenoms.FirstOrDefault()?.Name ?? "Nameless";

            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                return "Nameless"; //return nothing found
            }

        }

        public List<int> GetPhenomOfTypeIds(List<int> phenomTypeId)
        {
            try
            {
                return db.PhenomSts.Where(x => phenomTypeId.Contains(x.SpacePhenominaTypeId)).Select(x => x.Id).ToList();

            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                //we might throw the error catch it in the controller and pass an error message to the user or provide an empty list check for it
                //and if it can only be caused by error then handle it there
                throw e;
            }

        }

        public bool InsertPhenomLocs(List<PhenomLoc> phenomLocs)
        {
            bool succeeded = false;
            try
            {


                db.AddRange(phenomLocs);
                succeeded = db.SaveChanges() == phenomLocs.Count();//if we saved as many as passed it succeeded

            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);


            }
            return succeeded;

        }

        public List<PhenomLoc> GetPhenomsInAllLoc()
        {
            try
            {

                List<PhenomLoc> locPhenoms = new List<PhenomLoc>();
                locPhenoms = db.PhenomLocs
                    .Include(x => x.Phenomina.ImageFiles)
                    .Where(x=>x.Deleted == false)
                    .Select(x => x)
                    .ToList();
                return locPhenoms;

            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                //we might throw the error catch it in the controller and pass an error message to the user or provide an empty list check for it
                //and if it can only be caused by error then handle it there
                return new List<PhenomLoc>(); //return empty list
            }

        }

        public int DeleteAllPhenomsExcept0()
        {
            try
            {
                //FYI As a rule we will set deleted not actually delete rows for now

                db.PhenomLocs
                    .Where(x => x.Deleted == false && x.Id !=1)
                    .ToList()
                    .ForEach(x => x.Deleted = true);

                int numberOfDeletions = db.SaveChanges();
                return numberOfDeletions;

            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                //we might throw the error catch it in the controller and pass an error message to the user or provide an empty list check for it
                //and if it can only be caused by error then handle it there
                return 0; //return empty list
            }

        }

        //qqqqqq next put in controller add make a js class not expecting this to work straught away need refinement
        public List<PhenomLoc> GetDeletedPhenoms() 
        {
            //query just useful for testing --- be aware there will be lots in same loc
            List<PhenomLoc> deletedPhenoms = new List<PhenomLoc>(){ };
            try {


                return db.PhenomLocs.Where(x => x.Deleted == true).ToList<PhenomLoc>();

            } catch (Exception e) {
                FileHandler.FileHandler.WriteExceptionFile(e);
                return new List<PhenomLoc>(); 

            }
        
        }
        public List<PhenomLoc> GetPhenomsAfterTransistionTrigger(IList<PhenomTransTriggerInstruction> phenomInstructions)
        {
            try
            {
                //The transistion is only in one direcion (unless bindings do otherwise e.g if we bound protostar to dustcloud) however we could reverse
                //the process but as this would require and extra join it is a little slower so it would be provided in a seperate dq
                //this logic assumes an id is represented only once ... if we wanted to cause two transistions in a row we would need to loop the list instead
                //this also means if we wont have phenomena that happen instantly them move to the next stage so a supernova would take a whole turn
                
                Random rnd = new Random();
                List<int> phenomIds = phenomInstructions.Select(x => x.PhenomId).ToList(); //check if empty
                List<PhenomTrans> phenomTrans = new List<PhenomTrans>() { };
                List<PhenomLoc> phenomLocUpdate = new List<PhenomLoc>() { };

                phenomTrans = 
                    db.PhenomLocs
                    .Where(x =>
                    phenomIds.Contains(x.Id) 
                    /*&& x.Deleted == false*/
                    )
                    .Join( //we need to track which each transistions to
                    db.PhenomTransBs.Include(x=>x.NextPhenomenaStage.ImageFiles),
                    phenom => phenom.PhenominaId,
                    trans => trans.PhenomenaId,
                    (phenom, trans) => new { phenom = phenom, trans = trans })
                    .ToList()//I dont want to return the data yet but i dont know how to pass to the database my list<objectt>
                
                //All our phenom are linked to their transistion they could have multiple
             
                    .Where //filter out transistions that dont hit the threshould next i think is exclusive
                (x =>
                //we will have excluded transistionless phenomena here
                 //find the phenoma in the trigger list if found (dont know why it wouldnt be) assume each phenomina only once
                 ((Double)rnd.Next(0, 100) / 100) < (
                    phenomInstructions.First(y => y.PhenomId == x.phenom.Id) //get the phenom
                    .OverrideProbability //use its overide
                    ?? x.trans.ProbabilityOfTransistion 
                    ) //if the overide is null then
                )
                    .GroupBy(
                         x => x.phenom.Id)
                    // grp =>  //we could grab by fk here but we are reducing further first
                    //(key, grp) => new { key, grp.First() })
                    .Select(x => x.OrderBy(y => rnd.Next(x.Count())).First())//if we have more than one transistion pick it at random
                   //.Include(x => x.trans.NextPhenomenaStage.ImageFiles)//the update only changes the id but the front needs the new associated data
                    .Select(x => new PhenomTrans()
                    {
                        PhenomLoc = x.phenom,
                        NewPhenomSt = x.trans.NextPhenomenaStage
                    }).ToList<PhenomTrans>();//we cant just update in the query because we need to return it the front


                //qqqqq turn off lazy loading

                if (phenomTrans.Count > 0)
                {
                    phenomTrans.ForEach(x => x.PhenomLoc.PhenominaId = x.NewPhenomSt.Id);
                    phenomLocUpdate = phenomTrans.Select(x => x.PhenomLoc).ToList<PhenomLoc>();

                    if (phenomLocUpdate.Count() != db.SaveChanges()) { throw new Exception("saving failure"); }; //unsure if will work
                }
                return phenomLocUpdate;

            }
            catch (Exception e)
            {
               FileHandler.FileHandler.WriteExceptionFile(e);
                //we might throw the error catch it in the controller and pass an error message to the user or provide an empty list check for it
                //and if it can only be caused by error then handle it there
                return new List<PhenomLoc>(); //return empty list
            }

        }







    }
}

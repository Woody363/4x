using Microsoft.EntityFrameworkCore;
using System.Configuration;
//using WebApi.Helpers;
using Microsoft.Extensions.Configuration;
using reactprogress2.Data;

namespace reactprogress2.Dataquieries
{
    public  class TestQuieries 
    {
      
        protected readonly AppSettings appSettings;
        private PostgresContext db;

        public TestQuieries(AppSettings appSettings,PostgresContext db)
        {
            this.appSettings = appSettings;
            this.db = db;
        }

        

        public int GetAnyData()
        {
            try
            {

       
                    return db.WSpacePhenominas.FirstOrDefault()?.Id ?? 0;
                
            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                return 0; //return nothing found
            }


        }
        public  String GetAnyName()
        {
            try
            {
   
                    return db.WSpacePhenominas.FirstOrDefault()?.Name ?? "Nameless";
                
            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                return "Nameless"; //return nothing found
            }

        }

        public  List<int> GetPhenomOfTypeIds(List<int> phenomTypeId)
        {
            try
            {
       

                    return db.WSpacePhenominas.Where(x => phenomTypeId.Contains(x.SpacePhenominaTypeId)).Select(x => x.Id).ToList<int>();
                
            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                //we might throw the error catch it in the controller and pass an error message to the user or provide an empty list check for it
                //and if it can only be caused by error then handle it there
                throw e;
            }

        }

        public  bool InsertPhenomLocs(List<WLocationsOfPhenomenon> phenomLocs)
        {
            bool succeeded = false;
            try
            {

         

                    db.AddRange(phenomLocs);
                    succeeded = (db.SaveChanges() == phenomLocs.Count());//if we saved as many as passed it succeeded
                
            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);


            }
            return succeeded;

        }

        public  List<WLocationsOfPhenomenon> GetPhenomsInAllLoc()
        {
            try
            {
         
                    List<WLocationsOfPhenomenon> locPhenoms = new List<WLocationsOfPhenomenon>();
                    locPhenoms = db.WLocationsOfPhenomena
                        .Include(x => x.Phenomina.ImageFiles)
                        .Select(x => x)
                        .ToList<WLocationsOfPhenomenon>();
                    return locPhenoms;
                
            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                //we might throw the error catch it in the controller and pass an error message to the user or provide an empty list check for it
                //and if it can only be caused by error then handle it there
                return new List<WLocationsOfPhenomenon>(); //return empty list
            }

        }





    }
}

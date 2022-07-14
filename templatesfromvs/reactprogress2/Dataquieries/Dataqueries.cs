using Microsoft.EntityFrameworkCore;

namespace reactprogress2.Dataquieries
{
    public static class Dataqueries
    {
        //qqqq
        public static int GetAnyData(){
            try
            {
                using (postgresContext db = new postgresContext())
                {

                    return db.WSpacePhenominas.FirstOrDefault()?.Id ?? 0;
                }
            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                return 0; //return nothing found
            }


        }
        public static String GetAnyName()
        {
            try
            {
                using (postgresContext db = new postgresContext())
                {

                    return db.WSpacePhenominas.FirstOrDefault()?.Name ?? "Nameless";
                }
            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                return "Nameless"; //return nothing found
            }

        }

        public static List<int> GetPhenomOfTypeIds(List<int> phenomTypeId)
        {
            try
            {
                using (postgresContext db = new postgresContext())
                {

                    return db.WSpacePhenominas.Where(x => phenomTypeId.Contains(x.SpacePhenominaTypeId)).Select(x => x.Id).ToList<int>();
                }
            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                //we might throw the error catch it in the controller and pass an error message to the user or provide an empty list check for it
                //and if it can only be caused by error then handle it there
                throw e;
            }

        }

        public static bool InsertPhenomLocs(List<WLocationsOfPhenomenon> phenomLocs)
        {
            bool succeeded = false;
            try
            {
                using (postgresContext db = new postgresContext())
                {

                    db.AddRange(phenomLocs);
                    succeeded = (db.SaveChanges() == phenomLocs.Count());//if we saved as many as passed it succeeded
                }
            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);


            }
            return succeeded;

        }

        public static List<WLocationsOfPhenomenon> GetPhenomsInAllLoc()
        {
            try
            {
                using (postgresContext db = new postgresContext())
                {
                    List<WLocationsOfPhenomenon> locPhenoms = new List<WLocationsOfPhenomenon>();
                    locPhenoms = db.WLocationsOfPhenomena
                        .Include(x => x.Phenomina.ImageFiles)
                        .Select(x => x)
                        .ToList<WLocationsOfPhenomenon>();
                    return locPhenoms;
                }
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

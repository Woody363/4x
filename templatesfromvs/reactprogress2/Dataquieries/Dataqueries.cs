using Microsoft.EntityFrameworkCore;

namespace reactprogress2.Dataquieries
{
    public static class Dataqueries
    {
        //qqqq
        public static int GetAnyData(){

            using (postgresContext db = new postgresContext()) {

                return db.WSpacePhenominas.FirstOrDefault()?.Id??0;
            }
        
        }
        public static String GetAnyName()
        {

            using (postgresContext db = new postgresContext())
            {

                return db.WSpacePhenominas.FirstOrDefault()?.Name ?? "Nameless";
            }

        }

        public static List<int> GetPhenomOfTypeIds(List<int> phenomTypeId)
        {

            using (postgresContext db = new postgresContext())
            {

                return db.WSpacePhenominas.Where(x=>phenomTypeId.Contains(x.SpacePhenominaTypeId)).Select(x=>x.Id).ToList<int>();
            }

        }

        public static void InsertPhenomLocs(List<WLocationsOfPhenomenon> phenomLocs)
        {

            using (postgresContext db = new postgresContext())
            {

                db.AddRange(phenomLocs);
                db.SaveChanges();
            }

        }

        public static List<WLocationsOfPhenomenon> GetPhenomsInAllLoc()
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




    }
}

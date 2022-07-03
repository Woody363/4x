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
    }
}

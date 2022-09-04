using Microsoft.EntityFrameworkCore;
using reactprogress2.Data.DataModels;
//using WebApi.Helpers;
using reactprogress2.Data.DbTables;
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
        //qqqq put in a query for woody takes a set of phenomena locations and their ids and moves them to there and returns them
        //do some checking as to the outcome --- event
        //identify two types phenomena in same sector and generate an outcome --- for example 2 suns - blackhole, nebula + sun = quasar
        //woody to write an object for movement rules
        //uses as references again the phenominatype e.g suns can only move down, blackholes cant move, and nebulas cant go into spaces with nebulas
        public List<PhenomLoc> UpdatePhenoms(List<PhenomLoc> changedPhenom) 
        {
            try
            {
                //Intention
                //Get originals - or we could be passed them?
                //Get ones in same location as new ones want to go
                //save ones going to empty space
                //combine ones that share a coord to produce and event and potentially a phenom type (keep id of one???)
                //i want to do all this then save but cant get phenoms by locations without getting all of them!
                //i dont want to save two phenoms to an overlapping locations to be able to do it and then fix it after --- (unless they have an event chart and its done in query?)




                //may we need to say which deleted so dont have to send up all
                //really we need to return a list of space where things have changed
                //Overide+ Phenom+Phenom = event and resulting Phenom

                //first join phenomLocs with phenomLocs by coord, but dont 
                //if i stored geometry instead how will we set the world as reference if i want to know if a block of
                //4 coord overlap one if i have referenced every square it easy but how to tell the geometric its in a custom geographic system?

                //locs are just a coordinate system so really this should be the gameworld instantiated phenomina should be the way its named because the ids wont change loc will
                //qqqqq start a database design chart
                //qqqqq turn updated columns!

                //no good solution for getting all phenomlocs by a list of coord

                List<int> phenomLocIds = changedPhenom.Select(x=>x.Id).ToList();
                List<PhenomLoc> originalPhenomLocs = new List<PhenomLoc>();
                //try attach which havent used before
                db.PhenomLocs.AttachRange(changedPhenom); //its void so cant do anywork
                db.SaveChanges();
                db.PhenomLocs
                    .Where(x => phenomLocIds.Contains(x.Id))
                    /*
                      .Union( //not sure how to use select many or i comparer to make more rows
                    y=>db.PhenomLocs.Where(x=>x.Xcoord == y.XCoord && x.Ycoord == y.Ycoord && x.Id != y.Id).Se
                    )*/

                    .GroupJoin(
                    db.PhenomLocs.Where(x => x.Deleted == false),//we could exclude changedPhenom ids here, but then how do we make our join object for a variable with each class rather than 2 rows// for speed not worrying about overlaps already happened
                    x => new { x.Xcoord, x.Ycoord },//we have a model try using it --- its also our key
                    y => new { y.Xcoord, y.Ycoord },
                    (x, y) => new {
                        loc =
                        new { x.Xcoord, x.Ycoord }, //key  //if we excluded the key we would of had a union in a way
                        //phenomsAtLoc : 
                        phenomsInLoc = y //group 
                    } //y  has the original in it as well
                    )
                    .Where(x => x.phenomsInLoc.Count()! > 1)//its only joined on itself
                    //.Join
                    //(db.PhenomLocs,
                    //x=> x.phenomsInLoc.   //there are many in a group how do we loop through need a join with no requirement other than not to themselves within a group
                                            //then we will for each join to an event have a list of event joined to the parent of the group
                                            //this is now very much proc territory
                                            //the list of events should each have an outcome that can again be combined or ignored, ho


                    //)
                    //.join to a table event type
                    //event type is phenomena share location
                    //if no event they both remain
                    //have event table for sharedLocationEvent
                            //if both of type phenomena
                            //then probabilities - both destroyed
                            // blackhole
                            // some joint mass phenomena
                            //if with the creature the creature survives the planet is destroyed
                            //
                    //have newsFeedTable

                    //.Select(x=>x.
                    



                    //we will have joined withour self but thats okay because we will use origin only for coords and if we are only interact withourselves we will drop the group
                   // .GroupBy(x=> new {xCo })
                    /*
                    .SelectMany(x=>x.originalSelect(z=>z.original)).Union(x.Select(z=>z.moverPhenom)))
                    /*
                    .GroupBy
                    (x=>
                        
                    )
                    ).SelectMany(x=>x.)
                    
                    .Where(
                    x => x.original.Id != x.movedPhenom.Id // we dont want it interacting with itself
                    
                    )
                   // .SelectMany(x=>)
                    .GroupBy(x => new {x. })
                    .ToList(); //handle it in the c# until we have the tables
                    /*
                    .GroupJoin //if it has no mass equivalent it blackholes
                    (
                    db.CollisonOutComes
                    )
                    //.Join(eventBinding
                    //x=>x.Key.Id,
                    //y=>y.First().Id //first because we are for now going to assume if 3 things end up in one square or more only one out come blackhole a more clever thing to do would be to work them out with some sequence


                    //)
                    */


                    //what object do i want
                    //each thing an array events
                    //sum mass and do some probability when joining to calculate the results

                //foreach thing that happened add a note news feed type thing 



                //what if all the phenomina moved to one location!
                //What is two move to an empty location



                ///qqqqqqqqqq can i not just save changes on changed phenom rather than fetching from db

//# originalPhenomLocs
                /*
                var z = db.PhenomLocs
                    //GroupJoin --- into to left join where they have equivalent locations but we would need to join on two coords which means an object list -- another example where being able to pass a list of object would be useful as a temp table
                    //we could pass a list of x coord and y coord and where x and y is satified and they have the same index value in their seperate arrays
                    .Where(x =>
                    x.Deleted == false
                    && changedPhenom.Select(y => y.Id).Contains(x.Id) //dont expect this to work want to be surpised
                    && phenomLocIds.Contains(x.Id)//why is Id capitalised another issue with the db
                    )
                    .ToList()
                    .Join( //join so we dont have to search for each equivalent in the list
                    changedPhenom,
                    x => x.Id,
                    y => y.Id,
                    (x, y) => new { original = x, newP = y })//coul even try changing it in here allocating the new values to the old
                    .ToList();
                    z.ForEach(//should we have a better way for list updates we replacing everything but the id which is identical
                    x => {
                        x.original.Xcoord = x.newP.Xcoord;
                        x.original.Ycoord = x.newP.Ycoord;
                        x.original.PhenominaId = x.newP.PhenominaId;
                        x.original.Deleted = x.newP.Deleted;
                    }); //we are going to update first :(
                originalPhenomLocs = z.Select(x => x.original).ToList();
                    db.SaveChanges();
                    
                */




            }
            catch (Exception e)
            {
                FileHandler.FileHandler.WriteExceptionFile(e);
                return new List<PhenomLoc>() { };
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

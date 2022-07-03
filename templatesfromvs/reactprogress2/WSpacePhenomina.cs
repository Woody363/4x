using System;
using System.Collections.Generic;

namespace reactprogress2
{
    public partial class WSpacePhenomina
    {
        public WSpacePhenomina()
        {
            WLocationsOfPhenomena = new HashSet<WLocationsOfPhenomenon>();
            WSpacePhenomenaTransistionBindingNextPhenomenaStages = new HashSet<WSpacePhenomenaTransistionBinding>();
            WSpacePhenomenaTransistionBindingPhenomena = new HashSet<WSpacePhenomenaTransistionBinding>();
        }

        public int Id { get; set; }
        public string FriendlyName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int SpacePhenominaTypeId { get; set; }
        public int ImageFilesId { get; set; }

        public virtual WImageFile ImageFiles { get; set; } = null!;
        public virtual WSpacePhenomenaType SpacePhenominaType { get; set; } = null!;
        public virtual ICollection<WLocationsOfPhenomenon> WLocationsOfPhenomena { get; set; }
        public virtual ICollection<WSpacePhenomenaTransistionBinding> WSpacePhenomenaTransistionBindingNextPhenomenaStages { get; set; }
        public virtual ICollection<WSpacePhenomenaTransistionBinding> WSpacePhenomenaTransistionBindingPhenomena { get; set; }
    }
}

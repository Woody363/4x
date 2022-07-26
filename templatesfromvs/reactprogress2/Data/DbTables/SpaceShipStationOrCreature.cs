using System;
using System.Collections.Generic;

namespace reactprogress2
{
    /// <summary>
    /// this would have super, type sub, type and player controller etc
    /// </summary>
    public partial class SpaceShipStationOrCreature
    {
        public int Id { get; set; }
        public string FriendName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FriendlyPurpose { get; set; } = null!;
        public short HullStrength { get; set; }
        public bool CanColonise { get; set; }
        public short Speed { get; set; }
        public short ScanDistance { get; set; }
        public short TechLevel { get; set; }
    }
}

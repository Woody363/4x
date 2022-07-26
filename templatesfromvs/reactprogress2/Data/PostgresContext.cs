using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace reactprogress2.Data
{
    public partial class PostgresContext : DbContext
    {


        public PostgresContext(DbContextOptions<PostgresContext> options)
            : base(options)
        {

        }


        public virtual DbSet<SpacePhenominaBindingOrJustFk> SpacePhenominaBindingOrJustFks { get; set; } = null!;
        public virtual DbSet<SpacePhenominaState> SpacePhenominaStates { get; set; } = null!;
        public virtual DbSet<SpacePhenominaStateNextStateBinding> SpacePhenominaStateNextStateBindings { get; set; } = null!;
        public virtual DbSet<SpacePhenominaSubType> SpacePhenominaSubTypes { get; set; } = null!;
        public virtual DbSet<SpacePhenominaSuperType> SpacePhenominaSuperTypes { get; set; } = null!;
        public virtual DbSet<SpacePhenominaType> SpacePhenominaTypes { get; set; } = null!;
        public virtual DbSet<SpaceShipStationOrCreature> SpaceShipStationOrCreatures { get; set; } = null!;
        public virtual DbSet<Test2> Test2s { get; set; } = null!;
        public virtual DbSet<Testing> Testings { get; set; } = null!;
        public virtual DbSet<Testing2> Testing2s { get; set; } = null!;
        public virtual DbSet<Tewast> Tewasts { get; set; } = null!;
        public virtual DbSet<WImageFile> WImageFiles { get; set; } = null!;
        public virtual DbSet<WLocationsOfPhenomenon> WLocationsOfPhenomena { get; set; } = null!;
        public virtual DbSet<WSpacePhenomenaTransistionBinding> WSpacePhenomenaTransistionBindings { get; set; } = null!;
        public virtual DbSet<WSpacePhenomenaType> WSpacePhenomenaTypes { get; set; } = null!;
        public virtual DbSet<WSpacePhenomina> WSpacePhenominas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpacePhenominaBindingOrJustFk>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("space_phenomina_binding_or_just_fks", "whatWillMultipleSchemasHelpUsWith");
            });

            modelBuilder.Entity<SpacePhenominaState>(entity =>
            {
                entity.ToTable("space_phenomina_state", "whatWillMultipleSchemasHelpUsWith");

                entity.HasComment("for suns etc");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(+ 1)");

                entity.Property(e => e.AddedOn)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("added_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.FriendlyName)
                    .HasMaxLength(100)
                    .HasColumnName("friendly_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.OrderInSubType)
                    .HasColumnName("order_in_sub_type")
                    .HasDefaultValueSql("999");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.SpstId)
                    .HasColumnName("spst_id")
                    .HasComment("space phenomina super type id");

                entity.Property(e => e.SpsubTypeId)
                    .HasColumnName("spsub_type_id")
                    .HasComment("sub type id");

                entity.Property(e => e.SptId)
                    .HasColumnName("spt_id")
                    .HasComment("space phenomina type");

                entity.Property(e => e.VisualsTableId).HasColumnName("visualsTable_id");
            });

            modelBuilder.Entity<SpacePhenominaStateNextStateBinding>(entity =>
            {
                entity.ToTable("space_phenomina_state_next_state_binding", "whatWillMultipleSchemasHelpUsWith");

                entity.HasComment("if we had two outcomes for a sun e.g neutron star, black hole");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DefaultProbabilityToOccurOverOtherChangeOutcome).HasColumnName("default_probability_to_occur_over_other_change_outcome");

                entity.Property(e => e.DefaultStartProbabilityOfChange).HasColumnName("default_start_probability_of_change");

                entity.Property(e => e.DefaultStartTurnCountDown).HasColumnName("default_start_turn_count_down");

                entity.Property(e => e.SpacePhenominaEndStateId).HasColumnName("space_phenomina_end_state_id");

                entity.Property(e => e.SpacePhenominaStartStateId).HasColumnName("space_phenomina_start_state_id");
            });

            modelBuilder.Entity<SpacePhenominaSubType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("space_phenomina_sub_type", "whatWillMultipleSchemasHelpUsWith");

                entity.HasComment("how are we defining this - e.g something that belongs to a set of stages . e.g sun ... but a wormhole and sun could result in a black hole  --- and a nebula is both something that can start a sun and sensors ... though this doesnt hold up if follow logic ... ");
            });

            modelBuilder.Entity<SpacePhenominaSuperType>(entity =>
            {
                entity.ToTable("space_phenomina_super_type", "whatWillMultipleSchemasHelpUsWith");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.FriendlyName)
                    .HasMaxLength(100)
                    .HasColumnName("friendly_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SpacePhenominaType>(entity =>
            {
                entity.ToTable("space_phenomina_type", "whatWillMultipleSchemasHelpUsWith");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.FriendlyName)
                    .HasMaxLength(100)
                    .HasColumnName("friendly_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.SuperTypeId).HasColumnName("super_type_id");

                entity.HasOne(d => d.SuperType)
                    .WithMany(p => p.SpacePhenominaTypes)
                    .HasForeignKey(d => d.SuperTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parent");
            });

            modelBuilder.Entity<SpaceShipStationOrCreature>(entity =>
            {
                entity.ToTable("space_ship_station_or_creature", "whatWillMultipleSchemasHelpUsWith");

                entity.HasComment("this would have super, type sub, type and player controller etc");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CanColonise).HasColumnName("can_colonise");

                entity.Property(e => e.FriendName)
                    .HasMaxLength(300)
                    .HasColumnName("friend_name");

                entity.Property(e => e.FriendlyPurpose)
                    .HasMaxLength(300)
                    .HasColumnName("friendly_purpose")
                    .HasDefaultValueSql("'civilian'::character varying");

                entity.Property(e => e.HullStrength)
                    .HasColumnName("hull_strength")
                    .HasDefaultValueSql("10");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.ScanDistance)
                    .HasColumnName("scan_distance")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Speed)
                    .HasColumnName("speed")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.TechLevel)
                    .HasColumnName("tech_level")
                    .HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Test2>(entity =>
            {
                entity.ToTable("test2", "whatWillMultipleSchemasHelpUsWith");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Anything).HasColumnName("anything");

                entity.Property(e => e.Defaulting)
                    .HasColumnName("defaulting")
                    .HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Testing>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("testing");

                entity.HasComment("what is this?");
            });

            modelBuilder.Entity<Testing2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("testing2");

                entity.HasComment("testing");

                entity.Property(e => e.Col1)
                    .HasColumnName("col1")
                    .HasComment("test");
            });

            modelBuilder.Entity<Tewast>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tewast");

                entity.Property(e => e.Testxzvcx)
                    .HasColumnName("testxzvcx")
                    .HasComment("chxdggd");
            });

            modelBuilder.Entity<WImageFile>(entity =>
            {
                entity.ToTable("W_image_files", "whatWillMultipleSchemasHelpUsWith");

                entity.HasComment("this is the table");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.AddedOn)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("added_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.CssRef)
                    .HasMaxLength(100)
                    .HasColumnName("css_ref")
                    .HasDefaultValueSql("'bg_grey'::character varying");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.FileLocation)
                    .HasMaxLength(300)
                    .HasColumnName("file_location");

                entity.Property(e => e.FriendlyName)
                    .HasMaxLength(300)
                    .HasColumnName("friendly_name")
                    .HasComment("this is so we know what it is when looking at the db its not for querying");
            });

            modelBuilder.Entity<WLocationsOfPhenomenon>(entity =>
            {
                entity.ToTable("W_locations_of_phenomena", "whatWillMultipleSchemasHelpUsWith");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.PhenominaId).HasColumnName("phenomina_id");

                entity.Property(e => e.Xcoord).HasColumnName("xcoord");

                entity.Property(e => e.Ycoord).HasColumnName("ycoord");

                entity.HasOne(d => d.Phenomina)
                    .WithMany(p => p.WLocationsOfPhenomena)
                    .HasForeignKey(d => d.PhenominaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("phenomina loc");
            });

            modelBuilder.Entity<WSpacePhenomenaTransistionBinding>(entity =>
            {
                entity.ToTable("W_SpacePhenomenaTransistionBindings", "whatWillMultipleSchemasHelpUsWith");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NextPhenomenaStageId)
                    .HasColumnName("next_phenomena_stage_id")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.PhenomenaId)
                    .HasColumnName("phenomena_id")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.ProbabilityOfTransistion)
                    .HasColumnName("probability_of_transistion")
                    .HasDefaultValueSql("0.1");

                entity.HasOne(d => d.NextPhenomenaStage)
                    .WithMany(p => p.WSpacePhenomenaTransistionBindingNextPhenomenaStages)
                    .HasForeignKey(d => d.NextPhenomenaStageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("result");

                entity.HasOne(d => d.Phenomena)
                    .WithMany(p => p.WSpacePhenomenaTransistionBindingPhenomena)
                    .HasForeignKey(d => d.PhenomenaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("start");
            });

            modelBuilder.Entity<WSpacePhenomenaType>(entity =>
            {
                entity.ToTable("W_SpacePhenomena_Types", "whatWillMultipleSchemasHelpUsWith");

                entity.HasComment("Natural and artificial lets says");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.FriendlyName)
                    .HasMaxLength(100)
                    .HasColumnName("friendly_name");

                entity.Property(e => e.InGameDescription)
                    .HasMaxLength(500)
                    .HasColumnName("in_game_description");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<WSpacePhenomina>(entity =>
            {
                entity.ToTable("W_SpacePhenomina", "whatWillMultipleSchemasHelpUsWith");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FriendlyName)
                    .HasMaxLength(100)
                    .HasColumnName("friendly_name")
                    .HasDefaultValueSql("'see name im lazy'::character varying");

                entity.Property(e => e.ImageFilesId).HasColumnName("image_files_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.SpacePhenominaTypeId).HasColumnName("space_phenomina_type_id");

                entity.HasOne(d => d.ImageFiles)
                    .WithMany(p => p.WSpacePhenominas)
                    .HasForeignKey(d => d.ImageFilesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("myImage");

                entity.HasOne(d => d.SpacePhenominaType)
                    .WithMany(p => p.WSpacePhenominas)
                    .HasForeignKey(d => d.SpacePhenominaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("myType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


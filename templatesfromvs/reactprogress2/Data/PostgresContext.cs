using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using reactprogress2.Data.DbTables;

namespace reactprogress2.Data
{
    public partial class PostgresContext : DbContext
    {


        public PostgresContext(DbContextOptions<PostgresContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Phenom> Phenoms { get; set; } = null!;
        public virtual DbSet<PhenomSt> PhenomSts { get; set; } = null!;
        public virtual DbSet<PhenomLoc> PhenomLocs { get; set; } = null!;
        public virtual DbSet<PhenomSbt> PhenomSbts { get; set; } = null!;
        public virtual DbSet<PhenomSpt> PhenomSpts { get; set; } = null!;
        public virtual DbSet<PhenomT> PhenomTs { get; set; } = null!;
        public virtual DbSet<PhenomTransB> PhenomTransBs { get; set; } = null!;
        public virtual DbSet<SpaceShipStationOrCreature> SpaceShipStationOrCreatures { get; set; } = null!;
        public virtual DbSet<TableAcroynmsRef> TableAcroynmsRefs { get; set; } = null!;
        public virtual DbSet<Test2> Test2s { get; set; } = null!;
        public virtual DbSet<Testing> Testings { get; set; } = null!;
        public virtual DbSet<Testing2> Testing2s { get; set; } = null!;
        public virtual DbSet<Tewast> Tewasts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image", "myschema");

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

            modelBuilder.Entity<Phenom>(entity =>
            {
                entity.ToTable("phenom_s", "myschema");

                entity.HasComment("for suns etc\nphenomina state");

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

            modelBuilder.Entity<PhenomSt>(entity =>
            {
                entity.ToTable("phenom", "myschema");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('myschema.\"W_SpacePhenomina_id_seq\"'::regclass)");

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
                    .WithMany(p => p.PhenomSts)
                    .HasForeignKey(d => d.ImageFilesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("myImage");

                entity.HasOne(d => d.SpacePhenominaType)
                    .WithMany(p => p.PhenomSts)
                    .HasForeignKey(d => d.SpacePhenominaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("myType");
            });

            modelBuilder.Entity<PhenomLoc>(entity =>
            {
                entity.ToTable("phenom_loc", "myschema");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('myschema.\"W_locations_of_phenomena_id_seq\"'::regclass)");

                entity.Property(e => e.Deleted).HasColumnName("deleted");

                entity.Property(e => e.PhenominaId).HasColumnName("phenomina_id");

                entity.Property(e => e.Xcoord).HasColumnName("xcoord");

                entity.Property(e => e.Ycoord).HasColumnName("ycoord");

                entity.HasOne(d => d.Phenomina)
                    .WithMany(p => p.PhenomLocs)
                    .HasForeignKey(d => d.PhenominaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("phenomina loc");
            });

            modelBuilder.Entity<PhenomSbt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("phenom_sbt", "myschema");

                entity.HasComment("how are we defining this - e.g something that belongs to a set of stages . e.g sun ... but a wormhole and sun could result in a black hole  --- and a nebula is both something that can start a sun and sensors ... though this doesnt hold up if follow logic ... \nsbt = sub type");
            });

            modelBuilder.Entity<PhenomSpt>(entity =>
            {
                entity.ToTable("phenom_spt", "myschema");

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

            modelBuilder.Entity<PhenomT>(entity =>
            {
                entity.ToTable("phenom_t", "myschema");

                entity.HasComment("Natural and artificial lets says for phenomena types");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('myschema.\"W_SpacePhenomena_Types_id_seq\"'::regclass)");

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

            modelBuilder.Entity<PhenomTransB>(entity =>
            {
                entity.ToTable("phenom_trans_b", "myschema");

                entity.HasComment("space phenomina transistion binding is so we know probablity and what phenomina a phenomina binds to");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('myschema.\"W_SpacePhenomenaTransistionBindings_id_seq\"'::regclass)");

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
                    .WithMany(p => p.PhenomTransBNextPhenomenaStages)
                    .HasForeignKey(d => d.NextPhenomenaStageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("result");

                entity.HasOne(d => d.Phenomena)
                    .WithMany(p => p.PhenomTransBPhenomena)
                    .HasForeignKey(d => d.PhenomenaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("start");
            });

            modelBuilder.Entity<SpaceShipStationOrCreature>(entity =>
            {
                entity.ToTable("space_ship_station_or_creature", "myschema");

                entity.HasComment("this would have super, type sub, type and player controller etc --probably needs renaming");

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

            modelBuilder.Entity<TableAcroynmsRef>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("table_acroynms_ref", "myschema");

                entity.Property(e => e.Acronym)
                    .HasMaxLength(20)
                    .HasColumnName("acronym");

                entity.Property(e => e.Comment)
                    .HasMaxLength(400)
                    .HasColumnName("comment");

                entity.Property(e => e.Meaning)
                    .HasMaxLength(400)
                    .HasColumnName("meaning");
            });

            modelBuilder.Entity<Test2>(entity =>
            {
                entity.ToTable("test2", "myschema");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}





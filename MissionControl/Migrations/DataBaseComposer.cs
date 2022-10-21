using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Composing;
using Umbraco.Core.Migrations;
using Umbraco.Core.Migrations.Upgrade;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;
using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using System;

namespace MissionControl.Migrations
{
    /*
     * You could make more composer if that is needed
     * and then use: [ComposeBefore(typeof(xxxx))] ..or after
     * */
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class DataBaseComposer : ComponentComposer<DatabaseComponent>
    {
    }

    public class DatabaseComponent : IComponent
    {
        private IScopeProvider _scopeProvider;
        private IMigrationBuilder _migrationBuilder;
        private IKeyValueService _keyValueService;
        private ILogger _logger;

        public DatabaseComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
        {
            _scopeProvider = scopeProvider;
            _migrationBuilder = migrationBuilder;
            _keyValueService = keyValueService;
            _logger = logger;
        }

        public void Initialize()
        {
            // Create a migration plan for a specific project/feature
            // We can then track that latest migration state/step for this project/feature
            var migrationPlan = new MigrationPlan("Init");

            // This is the steps we need to take
            // Each step in the migration adds a unique value
            migrationPlan.From(string.Empty)
                .To<AddUsersTable>("add-users-table");

            migrationPlan.From("add-users-table")
                .To<AddMissionReportsTable>("add-missionreports-table");

            migrationPlan.From("add-missionreports-table")
                .To<AddMissionImagesTable>("add-missionimages-table");

            migrationPlan.From("add-missionimages-table")
                .To<UpdateFKMissionReports>("update-fk-missionreports");

            migrationPlan.From("update-fk-missionreports")
                .To<UpdateFKMissionImages>("update-fk-missionimages");


            migrationPlan.From("update-fk-missionimages")
                .To<UpdateFKMissionReportsNullable>("update-fk-missionreports-nullable");

            migrationPlan.From("update-fk-missionreports-nullable")
                .To<UpdateFKMissionImagesNullable>("update-fk-missionimages-nullable");


            migrationPlan.From("update-fk-missionimages-nullable")
                .To<UpdateFKMissionReportsNotNullable>("update-fk-missionreports-notnullable");

            migrationPlan.From("update-fk-missionreports-notnullable")
                .To<UpdateFKMissionImagesNotNullable>("update-fk-missionimages-notnullable");


            migrationPlan.From("update-fk-missionimages-notnullable")
                .To<AddFacilitysTable>("add-facilitys-table");


            migrationPlan.From("add-facilitys-table")
                .To<AddColumn>("add-column");


            migrationPlan.From("add-column")
                .To<UpdateFKMissionReportsOnDelete>("update-fk-missionreports-on-delete");

            migrationPlan.From("update-fk-missionreports-on-delete")
                .To<UpdateFKMissionImagesOnDelete>("update-fk-missionimages-on-delete");


            migrationPlan.From("update-fk-missionimages-on-delete")
                .To<AddColumnISSLatitude>("add-column-isslatitude");

            migrationPlan.From("add-column-isslatitude")
                .To<AddColumnISSLongitude>("add-column-isslongitude");







            // Go and upgrade our site (Will check if it needs to do the work or not)
            // Based on the current/latest step
            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
        }

        public void Terminate()
        {
        }
    }

    public class AddUsersTable : MigrationBase
    {
        public AddUsersTable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddUsersTable>("Running migration {MigrationStep}", "AddUsersTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!TableExists("Users"))
            {
                Create.Table<UsersSchema>().Do();
            }
            else
            {
                Logger.Debug<AddUsersTable>("The database table {DbTable} already exists, skipping", "Users");
            }
        }

        [TableName("Users")]
        [PrimaryKey("Id", AutoIncrement = true)]
        [ExplicitColumns]
        public class UsersSchema
        {
            [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
            [Column("Id")]
            public long Id { get; set; }

            [Column("FirstName")]
            public string FirstName { get; set; }

            [Column("LastName")]
            public string LastName { get; set; }

            [Column("CodeName")]
            public string CodeName { get; set; }

            [Column("UserName")]
            public string UserName { get; set; }

            [Column("Email")]
            public string Email { get; set; }

            [Column("Password")]
            public string Password { get; set; }

            [Column("Avatar")]
            public string Avatar { get; set; }
        }
    }

    public class AddMissionReportsTable : MigrationBase
    {
        public AddMissionReportsTable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddUsersTable>("Running migration {MigrationStep}", "AddMissionReportsTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!TableExists("MissionReports"))
            {
                Create.Table<MissionReportsSchema>().Do();
            }
            else
            {
                Logger.Debug<AddMissionReportsTable>("The database table {DbTable} already exists, skipping", "MissionReports");
            }
        }

        [TableName("MissionReports")]
        [PrimaryKey("Id", AutoIncrement = true)]
        [ExplicitColumns]
        public class MissionReportsSchema
        {
            /*
             * Id could be int
             * */
            [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
            [Column("Id")]
            public long Id { get; set; }

            [Column("Name")]
            public string Name { get; set; }

            [Column("Description")]
            public string Description { get; set; }

            [Column("Lat")]
            public double Lat { get; set; }

            [Column("Lng")]
            public double Lng { get; set; }

            [Column("MissionDate")]
            public DateTime MissionDate { get; set; }

            [Column("FinalisationDate")]
            public DateTime FinalizationDate { get; set; }

            [Column("CreatedAt")]
            public DateTime CreatedAt { get; set; }

            [Column("UpdatedAt")]
            public DateTime UpdatedAt { get; set; }

            [Column("DeletedAt")]
            public DateTime DeletedAt { get; set; }

            [Column("UserId")]
            public long UserId { get; set; }
        }
    }

    public class AddMissionImagesTable : MigrationBase
    {
        public AddMissionImagesTable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddMissionImagesTable>("Running migration {MigrationStep}", "AddMissionImagesTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!TableExists("MissionImages"))
            {
                Create.Table<MissionImagesSchema>().Do();
            }
            else
            {
                Logger.Debug<AddUsersTable>("The database table {DbTable} already exists, skipping", "MissionImages");
            }
        }

        [TableName("MissionImages")]
        [PrimaryKey("Id", AutoIncrement = true)]
        [ExplicitColumns]
        public class MissionImagesSchema
        {
            [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
            [Column("Id")]
            public long Id { get; set; }

            [Column("CameraName")]
            public string CameraName { get; set; }

            [Column("RoverName")]
            public string RoverName { get; set; }

            [Column("RoverStatus")]
            public string RoverStatus { get; set; }

            [Column("Img")]
            public string Img { get; set; }

            [Column("CreatedAt")]
            public DateTime CreatedAt { get; set; }

            [Column("UpdatedAt")]
            public DateTime UpdatedAt { get; set; }

            [Column("DeletedAt")]
            public DateTime DeletedAt { get; set; }

            [Column("MissionReportId")]
            public long MissionReportId { get; set; }

        }
    }

    public class UpdateFKMissionReports : MigrationBase
    {
        public UpdateFKMissionReports(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateFKMissionReports>("Running migration {MigrationStep}", "UpdateFKMissionReports");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MissionReports"))
            {
                Execute.Sql(
                "ALTER TABLE MissionReports ADD " +
                    "CONSTRAINT FK_MissionReports_Users FOREIGN KEY (UserId) " +
                    "REFERENCES Users(Id)"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateFKMissionReports>("The database table {DbTable} already exists, skipping", "UpdateFKMissionReports");
            }
        }
    }

    public class UpdateFKMissionImages : MigrationBase
    {
        public UpdateFKMissionImages(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateFKMissionImages>("Running migration {MigrationStep}", "UpdateFKMissionImages");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MissionImages"))
            {
                Execute.Sql(
                "ALTER TABLE MissionImages ADD " +
                    "CONSTRAINT FK_MissionImages_MissionReports FOREIGN KEY (MissionReportId) " +
                    "REFERENCES MissionReports(Id)"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateFKMissionImages>("The database table {DbTable} already exists, skipping", "UpdateFKMissionImages");
            }
        }
    }

    public class UpdateFKMissionReportsNullable : MigrationBase
    {
        public UpdateFKMissionReportsNullable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateFKMissionReportsNullable>("Running migration {MigrationStep}", "UpdateFKMissionReportsNullable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MissionReports"))
            {
                this.
                Execute.Sql(
                "ALTER TABLE MissionReports ALTER COLUMN UserId bigint NULL"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateFKMissionReportsNullable>("The database table {DbTable} already exists, skipping", "UpdateFKMissionReportsNullable");
            }
        }
    }

    public class UpdateFKMissionImagesNullable : MigrationBase
    {
        public UpdateFKMissionImagesNullable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateFKMissionImagesNullable>("Running migration {MigrationStep}", "UpdateFKMissionImagesNullable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MissionImages"))
            {
                this.
                Execute.Sql(
                "ALTER TABLE MissionImages ALTER COLUMN MissionReportId bigint NULL"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateFKMissionImagesNullable>("The database table {DbTable} already exists, skipping", "UpdateFKMissionImagesNullable");
            }
        }
    }

    public class UpdateFKMissionReportsNotNullable : MigrationBase
    {
        public UpdateFKMissionReportsNotNullable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateFKMissionReportsNotNullable>("Running migration {MigrationStep}", "UpdateFKMissionReportsNotNullable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MissionReports"))
            {
                this.
                Execute.Sql(
                "ALTER TABLE MissionReports ALTER COLUMN UserId bigint NOT NULL"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateFKMissionReportsNotNullable>("The database table {DbTable} already exists, skipping", "UpdateFKMissionReportsNotNullable");
            }
        }
    }

    public class UpdateFKMissionImagesNotNullable : MigrationBase
    {
        public UpdateFKMissionImagesNotNullable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateFKMissionImagesNotNullable>("Running migration {MigrationStep}", "UpdateFKMissionImagesNotNullable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MissionImages"))
            {
                this.
                Execute.Sql(
                "ALTER TABLE MissionImages ALTER COLUMN MissionReportId bigint NOT NULL"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateFKMissionImagesNotNullable>("The database table {DbTable} already exists, skipping", "UpdateFKMissionImagesNotNullable");
            }
        }
    }

    public class AddFacilitysTable : MigrationBase
    {
        public AddFacilitysTable(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddFacilitysTable>("Running migration {MigrationStep}", "AddFacilitysTable");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!TableExists("Facilitys"))
            {
                Create.Table<FacilitysSchema>().Do();
            }
            else
            {
                Logger.Debug<AddFacilitysTable>("The database table {DbTable} already exists, skipping", "Facilitys");
            }
        }

        [TableName("Facilitys")]
        [PrimaryKey("Id", AutoIncrement = true)]
        [ExplicitColumns]
        public class FacilitysSchema
        {
            /*
             * Id could be int
             * */
            [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
            [Column("Id")]
            public long Id { get; set; }

            [Column("Location")]
            public string Location { get; set; }

            [Column("Latitude")]
            public double Latitude { get; set; }

            [Column("Longitude")]
            public double Longitude { get; set; }

            [Column("Timestamp")]
            public long Timestamp { get; set; }
        }
    }

    public class AddColumn : MigrationBase
    {
        public AddColumn(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddColumn>("Running migration {MigrationStep}", "AddColumn");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!ColumnExists("Facilitys", "Distance"))
            {
                //Create.Column("xxxx").OnTable("Users").AsInt32().Do();
                Create.Column("Distance").OnTable("Facilitys").AsInt32().Nullable().Do();
            }
            else
            {
                Logger.Debug<AddColumn>("The column in {DbTable} already exists, skipping", "AddColumn");
            }
        }
    }

    public class UpdateFKMissionReportsOnDelete : MigrationBase
    {
        public UpdateFKMissionReportsOnDelete(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateFKMissionReports>("Running migration {MigrationStep}", "UpdateFKMissionReports");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MissionReports"))
            {
                Execute.Sql(
                "ALTER TABLE MissionReports DROP CONSTRAINT FK_MissionReports_Users"
                ).Do();
                Execute.Sql(
                "ALTER TABLE MissionReports ADD " +
                    "CONSTRAINT FK_MissionReports_Users FOREIGN KEY (UserId) " +
                    "REFERENCES Users(Id) " +
                    "ON DELETE CASCADE"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateFKMissionReportsOnDelete>("The database table {DbTable} already exists, skipping", "UpdateFKMissionReportsOnDelete");
            }
        }
    }

    public class UpdateFKMissionImagesOnDelete : MigrationBase
    {
        public UpdateFKMissionImagesOnDelete(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<UpdateFKMissionImages>("Running migration {MigrationStep}", "UpdateFKMissionImages");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("MissionImages"))
            {
                Execute.Sql(
                "ALTER TABLE MissionImages DROP CONSTRAINT FK_MissionImages_MissionReports"
                ).Do();
                Execute.Sql(
                "ALTER TABLE MissionImages ADD " +
                    "CONSTRAINT FK_MissionImages_MissionReports FOREIGN KEY (MissionReportId) " +
                    "REFERENCES MissionReports(Id) ON DELETE CASCADE"
                ).Do();
            }
            else
            {
                Logger.Debug<UpdateFKMissionImagesOnDelete>("The database table {DbTable} already exists, skipping", "UpdateFKMissionImagesOnDelete");
            }
        }
    }

    public class AddColumnISSLatitude : MigrationBase
    {
        public AddColumnISSLatitude(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddColumn>("Running migration {MigrationStep}", "AddColumnISSLatitude");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!ColumnExists("Facilitys", "ISSLatitude"))
            {
                //Create.Column("xxxx").OnTable("Users").AsInt32().Do();
                Create.Column("ISSLatitude").OnTable("Facilitys").AsDouble().NotNullable().WithDefaultValue(0.0d).Do();
            }
            else
            {
                Logger.Debug<AddColumnISSLatitude>("The column in {DbTable} already exists, skipping", "AddColumnISSLatitude");
            }
        }
    }

    public class AddColumnISSLongitude : MigrationBase
    {
        public AddColumnISSLongitude(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug<AddColumn>("Running migration {MigrationStep}", "AddColumnISSLongitude");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (!ColumnExists("Facilitys", "ISSLongitude"))
            {
                //Create.Column("xxxx").OnTable("Users").AsInt32().Do();
                Create.Column("ISSLongitude").OnTable("Facilitys").AsDouble().NotNullable().WithDefaultValue(0.0d).Do();
            }
            else
            {
                Logger.Debug<AddColumnISSLongitude>("The column in {DbTable} already exists, skipping", "AddColumnISSLongitude");
            }
        }
    }

}
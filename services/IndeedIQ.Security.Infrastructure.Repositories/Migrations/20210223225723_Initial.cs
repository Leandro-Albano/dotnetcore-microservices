//using Microsoft.EntityFrameworkCore.Migrations;

//namespace IndeedIQ.Security.Infrastructure.Repositories.Migrations
//{
//    public partial class Initial : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            MigrationsSqlGenerator x;
//            var res = x.Generate(this.UpOperations);
//            res[0].
//            migrationBuilder.CreateTable(
//                name: "ApplicationResource",
//                columns: table => new
//                {
//                    Id = table.Column<long>(type: "INTEGER", nullable: false)
//                        .Annotation("Sqlite:Autoincrement", true),
//                    Name = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
//                    ApplicationLevel = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_ApplicationResource", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Role",
//                columns: table => new
//                {
//                    Id = table.Column<long>(type: "INTEGER", nullable: false)
//                        .Annotation("Sqlite:Autoincrement", true),
//                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Role", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "User",
//                columns: table => new
//                {
//                    Id = table.Column<long>(type: "INTEGER", nullable: false)
//                        .Annotation("Sqlite:Autoincrement", true),
//                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
//                    Currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
//                    Country = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
//                    Email = table.Column<string>(type: "TEXT", nullable: true),
//                    Login = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
//                    IdentityServerId = table.Column<string>(type: "TEXT", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_User", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "ResourceAction",
//                columns: table => new
//                {
//                    Id = table.Column<long>(type: "INTEGER", nullable: false)
//                        .Annotation("Sqlite:Autoincrement", true),
//                    ResourceId = table.Column<long>(type: "INTEGER", nullable: false),
//                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
//                    FullName = table.Column<string>(type: "TEXT", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_ResourceAction", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_ResourceAction_ApplicationResource_ResourceId",
//                        column: x => x.ResourceId,
//                        principalTable: "ApplicationResource",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "UserRole",
//                columns: table => new
//                {
//                    Id = table.Column<long>(type: "INTEGER", nullable: false)
//                        .Annotation("Sqlite:Autoincrement", true),
//                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
//                    RoleId = table.Column<long>(type: "INTEGER", nullable: false),
//                    Accounts = table.Column<string>(type: "TEXT", nullable: true),
//                    Organisations = table.Column<string>(type: "TEXT", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_UserRole", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_UserRole_Role_RoleId",
//                        column: x => x.RoleId,
//                        principalTable: "Role",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_UserRole_User_UserId",
//                        column: x => x.UserId,
//                        principalTable: "User",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "RoleActionPermission",
//                columns: table => new
//                {
//                    Id = table.Column<long>(type: "INTEGER", nullable: false)
//                        .Annotation("Sqlite:Autoincrement", true),
//                    ActionId = table.Column<long>(type: "INTEGER", nullable: false),
//                    Permission = table.Column<int>(type: "INTEGER", nullable: false),
//                    RoleId = table.Column<long>(type: "INTEGER", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_RoleActionPermission", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_RoleActionPermission_ResourceAction_ActionId",
//                        column: x => x.ActionId,
//                        principalTable: "ResourceAction",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_RoleActionPermission_Role_RoleId",
//                        column: x => x.RoleId,
//                        principalTable: "Role",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_ResourceAction_ResourceId",
//                table: "ResourceAction",
//                column: "ResourceId");

//            migrationBuilder.CreateIndex(
//                name: "IX_RoleActionPermission_ActionId",
//                table: "RoleActionPermission",
//                column: "ActionId");

//            migrationBuilder.CreateIndex(
//                name: "IX_RoleActionPermission_RoleId",
//                table: "RoleActionPermission",
//                column: "RoleId");

//            migrationBuilder.CreateIndex(
//                name: "IX_UserRole_RoleId",
//                table: "UserRole",
//                column: "RoleId");

//            migrationBuilder.CreateIndex(
//                name: "IX_UserRole_UserId",
//                table: "UserRole",
//                column: "UserId");
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "RoleActionPermission");

//            migrationBuilder.DropTable(
//                name: "UserRole");

//            migrationBuilder.DropTable(
//                name: "ResourceAction");

//            migrationBuilder.DropTable(
//                name: "Role");

//            migrationBuilder.DropTable(
//                name: "User");

//            migrationBuilder.DropTable(
//                name: "ApplicationResource");
//        }
//    }
//}

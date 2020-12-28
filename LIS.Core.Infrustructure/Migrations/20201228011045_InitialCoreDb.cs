using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace LIS.Core.Infrustructure.Migrations
{
    public partial class InitialCoreDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 250, nullable: false),
                    LastName = table.Column<string>(maxLength: 250, nullable: false),
                    ProfileImageUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Address = table.Column<string>(maxLength: 500, nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UnsignName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    LastUpdatedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    UnsignName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    IdPath = table.Column<string>(maxLength: 250, nullable: true),
                    Icon = table.Column<string>(maxLength: 250, nullable: true),
                    BgColor = table.Column<string>(maxLength: 500, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    OrderPath = table.Column<string>(maxLength: 250, nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ChildCount = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 500, nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    LastUpdatedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    UnsignName = table.Column<string>(maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: false),
                    Email = table.Column<string>(maxLength: 250, nullable: false),
                    Address = table.Column<string>(maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(maxLength: 500, nullable: true),
                    Logo = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserConnections",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ConnectionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConnections", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserConnections_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    LastUpdatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    LastUpdatedBy = table.Column<string>(nullable: true),
                    LastUpdatedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    GroupKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ethnics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    UnsignName = table.Column<string>(nullable: true),
                    NationalId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ethnics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ethnics_Nationals_NationalId",
                        column: x => x.NationalId,
                        principalTable: "Nationals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    UnsignName = table.Column<string>(maxLength: 250, nullable: false),
                    NationalId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Nationals_NationalId",
                        column: x => x.NationalId,
                        principalTable: "Nationals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UnsignName = table.Column<string>(nullable: true),
                    NationalId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Religions_Nationals_NationalId",
                        column: x => x.NationalId,
                        principalTable: "Nationals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccountRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRoles_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    Permissions = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePages_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantPages_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TenantPages_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    ProvinceId = table.Column<Guid>(nullable: false),
                    ProvinceName = table.Column<string>(maxLength: 150, nullable: false),
                    Culture = table.Column<string>(maxLength: 100, nullable: false),
                    UnsignName = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_RoleId",
                table: "AccountRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Accounts",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Accounts",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ethnics_NationalId",
                table: "Ethnics",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_NationalId",
                table: "Provinces",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_Religions_NationalId",
                table: "Religions",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePages_RoleId",
                table: "RolePages",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantPages_PageId",
                table: "TenantPages",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPages_TenantId",
                table: "TenantPages",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConnections_UserId",
                table: "UserConnections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Ethnics");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.DropTable(
                name: "RolePages");

            migrationBuilder.DropTable(
                name: "TenantPages");

            migrationBuilder.DropTable(
                name: "UserConnections");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Nationals");
        }
    }
}

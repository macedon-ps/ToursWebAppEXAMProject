using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursWebAppEXAMProject.Migrations
{
    public partial class AddUsersAndRoles_Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "d765b682-1943-4359-9708-44f6ddef3bc0", "2e781304-62ac-4ec4-b638-a00727862a58", "superadmin", "SUPERADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "cc5337e3-48b9-49c8-b1d6-e90b46c46916", "a9d9d38d-ab88-4af1-a6d5-251a545e04c5", "admin", "ADMIN" });

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { "92da6aab-3b50-461a-8df2-63607d21d475", "41cebcf2-295d-4f0d-8e17-f710235cc1c9", "editor", "EDITOR" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "BirthYear", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
				values: new object[] { "b53b8be9-9e7e-4fda-9475-9ede69dea019", 0, 1971, "656d85fe-9abc-4261-a0e0-f22acacbf2af", "andrey", true, false, null, "ANDREY", "ANDREY", "AQAAAAEAACcQAAAAEMFZ+y6WJojubSr2d/dZC3bOnPa0iE1E8aR1PLYj0vuwQK0wF+tX6gfeR9MmEK4pgg==", null, false, "ME5PPRHLLKNTTQV2U4DGCOOTGP7ZCR5F", false, "andrey" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "BirthYear", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
				values: new object[] { "afffa5ab-c5e9-45ab-818c-93da6da94dbe", 0, 2000, "0b7f2028-5835-43f3-beb3-16977abed6fc", "boris", true, false, null, "BORIS", "BORIS", "AQAAAAEAACcQAAAAEN9dEQiSmQZfWLcY77+iDbzYo0d7Oh0IxdZu+rH4IU7TUphjRBZFJIhSF5LQ7ewsOg==", null, false, "UWFPOXH37LKZDS6IJ6KP5IPUZ3VRUXRP", false, "boris" });

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "BirthYear", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
				values: new object[] { "49613e15-c95a-464c-b494-e889e8875b95", 0, 2010, "eda541da-6b37-4bf5-8639-407b8606fa0f", "sergey", true, false, null, "SERGEY", "SERGEY", "AQAAAAEAACcQAAAAEAs5i2aFSC35hvsGwdMiUt252r8XTsDQt16m6Fjznlzrcg9LezwI8GTcQv+p3fU9aQ==", null, false, "XA5OY6RRFIQJSBKKJ5GUJ4T4TEIHVRB7", false, "sergey" });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "d765b682-1943-4359-9708-44f6ddef3bc0", "b53b8be9-9e7e-4fda-9475-9ede69dea019" });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "cc5337e3-48b9-49c8-b1d6-e90b46c46916", "afffa5ab-c5e9-45ab-818c-93da6da94dbe" });

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "92da6aab-3b50-461a-8df2-63607d21d475", "49613e15-c95a-464c-b494-e889e8875b95" });

		}

        protected override void Down(MigrationBuilder migrationBuilder) {}
    }
}

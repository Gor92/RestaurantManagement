using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagement.DAL.Migrations
{
    public partial class DataSeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Name", "RowVersion" },
                values: new object[] { 1, "Restaurant 1", 1L });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "RowVersion","Id", "BirthDate", "CreateDate", "Email", "FirstName", "IsDeleted", "IsLocked", "LastName", "Login", "MiddleName", "MobilePhoneNumber", "Password", "PhoneNumber", "RestaurantId", "UpdateDate" },
                values: new object[] { 1L,1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "test@gmail.com", "Test", false, false, "Test", "test", "Test", "43214324213", "1234567", "4324213", 1, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "RowVersion", "Id", "CreateDate", "CreatedByUserId", "Description", "Name", "Price", "RestaurantId", "UpdateDate", "UpdatedByUserId" },
                values: new object[,]
                {
                    { 1L,1, new DateTimeOffset(new DateTime(1997, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, "drink", "Product 1", 10m, 1, new DateTimeOffset(new DateTime(1997, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 1L,2, new DateTimeOffset(new DateTime(1997, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, "drink", "Product 2", 15m, 1, new DateTimeOffset(new DateTime(1997, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 1L,3, new DateTimeOffset(new DateTime(1997, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, "drink", "Product 3", 20m, 1, new DateTimeOffset(new DateTime(1997, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 }
                });

            migrationBuilder.InsertData(
                table: "Table",
                columns: new[] { "RowVersion","Id", "IsReserved", "RestaurantId", "RestaurantRelatedTableId" },
                values: new object[] { 1L,1, false, 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Table",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

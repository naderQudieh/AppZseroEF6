using Microsoft.EntityFrameworkCore.Migrations;

namespace Bridge.Data.Migrations
{
    public partial class addData27082020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeliveryStatuses",
                columns: new[] { "Id", "Name", "Priority" },
                values: new object[,]
                {
                    { 1L, "Bridge Shop has received the order", 0 },
                    { 9L, "This products are out of stock", -10 },
                    { 8L, "The shipper is busy now", -5 },
                    { 7L, "This order has been cancel by Bridge Shop! Sorry about this case.", -10 },
                    { 10L, "This order has been cancel by customer", -10 },
                    { 4L, "The shipper is on his way to you", 5 },
                    { 3L, "The shipper has taken from the repositories", 5 },
                    { 2L, "This products is ready to ship", 5 },
                    { 5L, "Delivery successful", 10 }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Male" },
                    { 2L, "FeMale" }
                });

            migrationBuilder.InsertData(
                table: "Smell",
                columns: new[] { "Id", "B", "G", "Name", "O", "R" },
                values: new object[,]
                {
                    { 6L, 70, 63, "Cafe Mocha", 1, 80 },
                    { 1L, 70, 63, "Chocolate Fudge", 1, 80 },
                    { 2L, 70, 63, "Vanilla", 1, 80 },
                    { 3L, 70, 63, "Chocolate Peanut Butter", 1, 80 },
                    { 4L, 70, 63, "Cookies & Crème", 1, 80 },
                    { 5L, 70, 63, "Strawberry Milkshake", 1, 80 },
                    { 7L, 70, 63, "Frozen Banana", 1, 80 }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "GenderId", "Name", "Standard" },
                values: new object[,]
                {
                    { 1L, 1L, "Lbs5", null },
                    { 2L, 1L, "Lbs10", null },
                    { 3L, 2L, "Lbs3", null },
                    { 4L, 2L, "Lbs6.6", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Smell",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Smell",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Smell",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Smell",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Smell",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Smell",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Smell",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Bridge.Data.Migrations
{
    public partial class DeleteAllColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Products_ProductId",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_Colors_SmellId",
                table: "ProductColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_SmellId",
                table: "ProductColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "ProductColors");

            migrationBuilder.RenameTable(
                name: "ProductColors",
                newName: "ProductSmells");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "Smell");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "OrderDetails",
                newName: "Smell");

            migrationBuilder.RenameIndex(
                name: "IX_ProductColors_ProductId",
                table: "ProductSmells",
                newName: "IX_ProductSmells_ProductId");

            migrationBuilder.AlterColumn<long>(
                name: "SmellId",
                table: "ProductSmells",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSmells",
                table: "ProductSmells",
                columns: new[] { "SmellId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Smell",
                table: "Smell",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSmells_Products_ProductId",
                table: "ProductSmells",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSmells_Smell_SmellId",
                table: "ProductSmells",
                column: "SmellId",
                principalTable: "Smell",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSmells_Products_ProductId",
                table: "ProductSmells");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSmells_Smell_SmellId",
                table: "ProductSmells");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Smell",
                table: "Smell");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSmells",
                table: "ProductSmells");

            migrationBuilder.RenameTable(
                name: "Smell",
                newName: "Colors");

            migrationBuilder.RenameTable(
                name: "ProductSmells",
                newName: "ProductColors");

            migrationBuilder.RenameColumn(
                name: "Smell",
                table: "OrderDetails",
                newName: "Color");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSmells_ProductId",
                table: "ProductColors",
                newName: "IX_ProductColors_ProductId");

            migrationBuilder.AlterColumn<long>(
                name: "SmellId",
                table: "ProductColors",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "ColorId",
                table: "ProductColors",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                columns: new[] { "ColorId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_SmellId",
                table: "ProductColors",
                column: "SmellId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Products_ProductId",
                table: "ProductColors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_Colors_SmellId",
                table: "ProductColors",
                column: "SmellId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

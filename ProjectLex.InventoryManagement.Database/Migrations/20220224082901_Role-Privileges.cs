using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectLex.InventoryManagement.Database.Migrations
{
    public partial class RolePrivileges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CategoriesAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CategoriesDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CategoriesEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CategoriesView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CustomersAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CustomersDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CustomersEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CustomersView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DefectivesAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DefectivesDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DefectivesEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DefectivesView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LocationsAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LocationsDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LocationsEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LocationsView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LogsAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LogsDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LogsEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LogsView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OrdersAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OrdersDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OrdersEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OrdersView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProductsAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProductsDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProductsEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProductsView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RolesAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RolesDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RolesEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RolesView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StaffsAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StaffsDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StaffsEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StaffsView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StoragesAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StoragesDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StoragesEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StoragesView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SuppliersAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SuppliersDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SuppliersEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SuppliersView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriesAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CategoriesDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CategoriesEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CategoriesView",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CustomersAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CustomersDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CustomersEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CustomersView",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DefectivesAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DefectivesDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DefectivesEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DefectivesView",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LocationsAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LocationsDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LocationsEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LocationsView",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LogsAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LogsDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LogsEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "LogsView",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "OrdersAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "OrdersDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "OrdersEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "OrdersView",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ProductsAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ProductsDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ProductsEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ProductsView",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RolesAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RolesDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RolesEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RolesView",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "StaffsAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "StaffsDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "StaffsEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "StaffsView",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "StoragesAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "StoragesDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "StoragesEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "StoragesView",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "SuppliersAdd",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "SuppliersDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "SuppliersEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "SuppliersView",
                table: "Roles");
        }
    }
}

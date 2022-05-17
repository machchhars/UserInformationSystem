using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.DAL.Migrations
{
    public partial class viewGetUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE VIEW GetUser
                        AS  
                            SELECT
                                UserDataId,
                                Name,
                                Age,
                                Gender,
                                Email,
                                Address,
                                MobileNumber,
                                ProfilePictureBase64Data,
                                FileName
                            FROM UserData;   
                        GO";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW GetUser");
        }
    }
}

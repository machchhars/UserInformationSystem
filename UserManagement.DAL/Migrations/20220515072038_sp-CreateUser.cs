using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.DAL.Migrations
{
    public partial class spCreateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[CreateUser]
                    @Name VARCHAR(200),
                    @Age INT NULL,
                    @Gender VARCHAR(7),
                    @Email VARCHAR(100),
                    @Address VARCHAR(250) NULL = NULL,
                    @MobileNumber BIGINT NULL = NULL,
                    @ProfilePictureBase64Data VARCHAR(MAX) NULL = NULL,
                    @FileName VARCHAR(150) NULL = NULL
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF EXISTS(SELECT 1 FROM UserData WHERE Email = @Email)
                    BEGIN
                        RAISERROR (N'User already present with %s.', 16, 1, @Email);
                        RETURN; 
                    END

                    Insert into UserData
                    (
                        Name,
                        Age,
                        Gender,
                        Email,
                        Address,
                        MobileNumber,
                        ProfilePictureBase64Data,
                        FileName
                    )
                    Values 
                    (
                        @Name, 
                        @Age,
                        @Gender,
                        @Email,
                        @Address,
                        @MobileNumber,
                        @ProfilePictureBase64Data,
                        @FileName
                    )
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.CreateUser");
        }
    }
}

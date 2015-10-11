namespace Applicant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ApplicantId = c.Int(nullable: false, identity: true),
                        PhotoId = c.Int(),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Residence = c.String(nullable: false),
                        TypeSalary = c.Int(nullable: false),
                        Salary = c.Int(nullable: false),
                        TypeMoney = c.Int(nullable: false),
                        TypeWork = c.Int(nullable: false),
                        TypeBussy = c.Int(nullable: false),
                        Comments = c.String(maxLength: 500),
                        Email = c.String(nullable: false),
                        Skype = c.String(),
                        Phone = c.String(nullable: false),
                        WebSite = c.String(),
                        GitHub = c.String(),
                        Linkedin = c.String(),
                        Facebook = c.String(),
                        VKontakte = c.String(),
                        Photo_AttachmentId = c.Int(),
                    })
                .PrimaryKey(t => t.ApplicantId)
                .ForeignKey("dbo.Attachments", t => t.Photo_AttachmentId)
                .Index(t => t.Photo_AttachmentId);
            
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        AttachmentId = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(),
                        HistoryId = c.Int(),
                        Type = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Attach = c.Binary(),
                        Applicant_ApplicantId = c.Int(),
                    })
                .PrimaryKey(t => t.AttachmentId)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .ForeignKey("dbo.Histories", t => t.HistoryId)
                .ForeignKey("dbo.Applicants", t => t.Applicant_ApplicantId)
                .Index(t => t.ApplicantId)
                .Index(t => t.HistoryId)
                .Index(t => t.Applicant_ApplicantId);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        HistoryId = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(),
                        CommunicationDate = c.DateTime(nullable: false),
                        TypeCommunication = c.Int(nullable: false),
                        HistoryComments = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.HistoryId)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId)
                .Index(t => t.ApplicantId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TagApplicants",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Applicant_ApplicantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Applicant_ApplicantId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Applicants", t => t.Applicant_ApplicantId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Applicant_ApplicantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TagApplicants", "Applicant_ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.TagApplicants", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.Applicants", "Photo_AttachmentId", "dbo.Attachments");
            DropForeignKey("dbo.Attachments", "Applicant_ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Attachments", "HistoryId", "dbo.Histories");
            DropForeignKey("dbo.Histories", "ApplicantId", "dbo.Applicants");
            DropForeignKey("dbo.Attachments", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.TagApplicants", new[] { "Applicant_ApplicantId" });
            DropIndex("dbo.TagApplicants", new[] { "Tag_TagId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Histories", new[] { "ApplicantId" });
            DropIndex("dbo.Attachments", new[] { "Applicant_ApplicantId" });
            DropIndex("dbo.Attachments", new[] { "HistoryId" });
            DropIndex("dbo.Attachments", new[] { "ApplicantId" });
            DropIndex("dbo.Applicants", new[] { "Photo_AttachmentId" });
            DropTable("dbo.TagApplicants");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tags");
            DropTable("dbo.Histories");
            DropTable("dbo.Attachments");
            DropTable("dbo.Applicants");
        }
    }
}

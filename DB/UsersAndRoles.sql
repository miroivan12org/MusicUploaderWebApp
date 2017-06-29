begin tran
	CREATE TABLE [Common].[LookupRole](  
    [LookupRoleID] [int] IDENTITY(1,1) NOT NULL,  
    [RoleName] [varchar](100) DEFAULT '',  
    [RoleDescription] [varchar](500) DEFAULT '',  
    [RowCreatedUserID] [int] NOT NULL,  
    [RowCreatedDateTime] [datetime]  DEFAULT GETDATE(),  
    [RowModifiedUserID] [int] NOT NULL,  
    [RowModifiedDateTime] [datetime] DEFAULT GETDATE(),  
PRIMARY KEY (LookupRoleID)  
    )  
GO  

INSERT INTO Common.LookupRole (RoleName,RoleDescription,RowCreatedUserID,RowModifiedUserID)  
    VALUES ('Admin','Can Edit, Update, Delete',1,1)  
INSERT INTO Common.LookupRole (RoleName,RoleDescription,RowCreatedUserID,RowModifiedUserID)  
    VALUES ('Member','Read only',1,1) 
INSERT INTO Common.LookupRole (RoleName,RoleDescription,RowCreatedUserID,RowModifiedUserID)
	VALUES ('Guest','Read only',1,1)
	   
CREATE TABLE [Common].[User](  
    [UserID] [int] IDENTITY(1,1) NOT NULL,  
    [LoginName] [varchar](50) NOT NULL,  
    [PasswordEncryptedText] [varchar](200) NOT NULL,  
    [RowCreatedUserID] [int] NOT NULL,  
    [RowCreatedDateTime] [datetime] DEFAULT GETDATE(),  
    [RowModifiedUserID] [int] NOT NULL,  
    [RowMOdifiedDateTime] [datetime] DEFAULT GETDATE(),  
    PRIMARY KEY (UserID)  
)  
  
GO  

CREATE TABLE [Common].[UserProfile](  
    [UserProfileID] [int] IDENTITY(1,1) NOT NULL,  
    [UserID] [int] NOT NULL,  
    [FirstName] [varchar](50) NOT NULL,  
    [LastName] [varchar](50) NOT NULL,  
    [Gender] [char](1) NOT NULL,  
    [RowCreatedUserID] [int] NOT NULL,  
    [RowCreatedDateTime] [datetime] DEFAULT GETDATE(),  
    [RowModifiedUserID] [int] NOT NULL,  
    [RowModifiedDateTime] [datetime] DEFAULT GETDATE(),  
    PRIMARY KEY (UserProfileID)  
    )  
GO  
  
ALTER TABLE [Common].[UserProfile]  WITH CHECK ADD FOREIGN KEY([UserID])  
REFERENCES [Common].[User] ([UserID])  
GO  

CREATE TABLE [Common].[UserRole](  
    [UserRoleID] [int] IDENTITY(1,1) NOT NULL,  
    [UserID] [int] NOT NULL,  
    [LookupRoleID] [int] NOT NULL,  
    [IsActive] [bit] DEFAULT (1),  
    [RowCreatedUserID] [int] NOT NULL,  
    [RowCreatedDateTime] [datetime] DEFAULT GETDATE(),  
    [RowModifiedUserID] [int] NOT NULL,  
    [RowModifiedDateTime] [datetime] DEFAULT GETDATE(),  
    PRIMARY KEY (UserRoleID)  
)  
GO  
  
ALTER TABLE [Common].[UserRole]  WITH CHECK ADD FOREIGN KEY([LookupRoleID])  
REFERENCES [Common].[LookupRole] ([LookupRoleID])  
GO  
  
ALTER TABLE [Common].[UserRole]  WITH CHECK ADD FOREIGN KEY([UserID])  
REFERENCES [Common].[User] ([UserID])  
GO  

INSERT INTO [Common].[User] (LoginName,PasswordEncryptedText, RowCreatedUserID, RowModifiedUserID)  
VALUES ('Lija','pass',1,1)  
GO  

INSERT INTO [Common].[User] (LoginName,PasswordEncryptedText, RowCreatedUserID, RowModifiedUserID)  
VALUES ('bmatic','bmatic',1,1)  
GO

INSERT INTO Common.UserProfile (UserID,FirstName,LastName,Gender,RowCreatedUserID, RowModifiedUserID)  
VALUES (1,'Ivan','Liović','M',1,1)  
GO  

INSERT INTO Common.UserProfile (UserID,FirstName,LastName,Gender,RowCreatedUserID, RowModifiedUserID)  
VALUES (2,'bruno','matic','M',1,1)  
GO  
  
INSERT INTO Common.UserRole (UserID,LookupRoleID,IsActive,RowCreatedUserID, RowModifiedUserID)  
VALUES (1,1,1,1,1)  

INSERT INTO Common.UserRole (UserID,LookupRoleID,IsActive,RowCreatedUserID, RowModifiedUserID)  
VALUES (2,2,1,1,1) 

rollback
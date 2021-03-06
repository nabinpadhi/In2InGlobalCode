USE [ittpm]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateProject]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--exec UpdateTPMProject 6,'TeamSize',9
CREATE PROCEDURE [dbo].[sp_UpdateProject]
(
	@ID INT,
	@ColumnName VARCHAR(100),
	@NewValue VARCHAR(MAX)
)
AS
BEGIN

DECLARE @SQL nvarchar(4000)
	SET @SQL = 'UPDATE tpmProjects
				SET '+@ColumnName +'='''+ @NewValue +''''+ 
				' WHERE ID ='+ cast(@ID as varchar)
				EXEC sp_executesql @SQL
				--print @SQL
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ExcludeTeamMeber]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec sp_ExcludeTeamMeber '2,4,5'
-- =============================================
CREATE PROCEDURE [dbo].[sp_ExcludeTeamMeber]
(
	@SelectedTeamMembers varchar(max)
)
AS
BEGIN
	DECLARE @SQL nvarchar(4000)
	SET @SQL = 'DELETE FROM tpmMemberProject
	WHERE MemberID in ('+ @SelectedTeamMembers+');DELETE FROM tpmTeamMember
	WHERE ID in ('+ @SelectedTeamMembers+');'
				EXEC sp_executesql @SQL
				--PRINT @SQL
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteProject]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- exec sp_DeleteProject '2,4,5'
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeleteProject]
(
@SelectedProjects varchar(max)
)
AS
BEGIN
	DECLARE @SQL nvarchar(4000)
	SET @SQL = 'DELETE FROM tpmProjects
	WHERE ID in ('+ @SelectedProjects+')'
				EXEC sp_executesql @SQL
				--PRINT @SQL
	
END
GO
/****** Object:  Table [dbo].[tpmTeamMember]    Script Date: 02/04/2014 15:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tpmTeamMember](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[YOE] [int] NOT NULL,
	[EmailID] [varchar](100) NOT NULL,
 CONSTRAINT [PK_tpmTeamMember] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tpmProjects]    Script Date: 02/04/2014 15:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tpmProjects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[TeamSize] [int] NOT NULL,
	[ClientName] [varchar](100) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[TargetEndDate] [date] NOT NULL,
	[Status] [int] NOT NULL,
	[TPMID] [int] NULL,
 CONSTRAINT [PK_project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02/04/2014 15:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](150) NOT NULL,
	[CompanyEmail] [varchar](100) NOT NULL,
	[CompanyName] [varchar](100) NOT NULL,
	[EncryptedPassword] [varchar](max) NOT NULL,
	[IsInitialPassword] [bit] NOT NULL,
	[UserName] [varchar](250) NOT NULL,
	[LastLogonTime] [datetime] NULL,
	[LastModifiedOn] [datetime] NOT NULL,
	[LastModifiedBy] [int] NOT NULL,
	[FeedbackStatus] [bit] NULL,
	[Technology] [varchar](20) NULL,
	[DOB] [date] NULL,
	[IsTPM] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTeamMember]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--exec sp_UpdateTeamMember 6,'TeamSize',9
Create PROCEDURE [dbo].[sp_UpdateTeamMember]
(
	@ID INT,
	@ColumnName VARCHAR(100),
	@NewValue VARCHAR(MAX)
)
AS
BEGIN

DECLARE @SQL nvarchar(4000)
	SET @SQL = 'UPDATE tpmTeamMember
				SET '+@ColumnName +'='''+ @NewValue +''''+ 
				' WHERE ID ='+ cast(@ID as varchar)
				EXEC sp_executesql @SQL
				--print @SQL
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePWD]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec sp_IsValidUser 'nabin@ittpm.com','testpass'
CREATE PROCEDURE [dbo].[sp_UpdatePWD]  
(  
 @ID INT,
 @EncryptedPassword varchar(max)
)  
AS 
UPDATE Users
SET EncryptedPassword = @EncryptedPassword,
	IsInitialPassword = 'false' 
where ID = @ID
SELECT COUNT(*) FROM Users
where ID = @ID
GO
/****** Object:  Table [dbo].[tpmUserTasks]    Script Date: 02/04/2014 15:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tpmUserTasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ProjectID] [int] NULL,
	[AssignedBy] [int] NULL,
	[TaskHeader] [varchar](200) NOT NULL,
	[TaskDetails] [varchar](max) NOT NULL,
	[LastModifiedOn] [datetime] NOT NULL,
	[LastModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_tpmUserTasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tpmMemberProject]    Script Date: 02/04/2014 15:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tpmMemberProject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
 CONSTRAINT [PK_tpmMemberProject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddTPM]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,
-- Description:	<Description,,
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddTPM] 
(
		@FullName varchar(150),
        @CompanyEmail varchar(100),
        @CompanyName varchar(100),
        @EncryptedPassword varchar(max),               
        @UserName varchar(250),
        @Technology varchar(20), 
        @IsInitialPassword bit,                       
        @DOB date
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.	
	INSERT INTO [Users]
           ([FullName]
           ,[CompanyEmail]
           ,[CompanyName]
           ,[EncryptedPassword]
           ,[IsInitialPassword]
           ,[UserName]
           ,[LastLogonTime]
           ,[LastModifiedOn]
           ,[LastModifiedBy]
           ,[FeedbackStatus]
           ,[Technology]
           ,[DOB]
           ,IsTPM)
     VALUES
           (
           @FullName ,
           @CompanyEmail,
           @CompanyName,
           @EncryptedPassword,
           @IsInitialPassword,
           @UserName,
           GETDATE(),
           GETDATE(),
           0,
           'false',
           @Technology,
           @DOB,
           'true'
           )
           
SELECT ID FROM Users WHERE UserName = @CompanyEmail;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTPM]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetTPM] 
	(
			@UserName varchar(max)
	)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT [ID]
      ,[FullName]
      ,[CompanyEmail]
      ,[CompanyName]
      ,[UserName]
      ,[Technology]
      ,[DOB]
  FROM [Users]
  WHERE UserName = @UserName
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AddProject]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,
-- Description:	<Description,,
-- =============================================
CREATE PROCEDURE [dbo].[sp_AddProject] 
(
			@Name varchar(max)
           ,@TeamSize int
           ,@ClientName varchar(100)
           ,@StartDate date
           ,@EndDate date
           ,@TargetEndDate date
           ,@UserID int
)
AS
BEGIN
	INSERT INTO [ittpm].[dbo].[tpmProjects]
           (
            [Name]
           ,[TeamSize]
           ,[ClientName]
           ,[StartDate]
           ,[EndDate]
           ,[TargetEndDate]
           ,[Status]
           ,[TPMID]
           )
     VALUES
           (
            @Name
           ,@TeamSize
           ,@ClientName
           ,@StartDate
           ,@EndDate
           ,@TargetEndDate
           ,0
           ,@UserID
           )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_IsValidUser]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec sp_IsValidUser 'nabin@ittpm.com','testpass'
CREATE PROCEDURE [dbo].[sp_IsValidUser]  
(  
 @UserName varchar(max),
 @EncryptedPass varchar(max)
)  
AS 
SELECT [ID] as UserID
      ,[FullName]
      ,[CompanyEmail]
      ,[CompanyName]      
      ,[IsInitialPassword]
      ,[UserName]
      ,[FeedbackStatus]
      ,[Technology]
      ,[DOB]
      ,[IsTPM]
  FROM [Users]  where UserName = @UserName and EncryptedPassword = @EncryptedPass
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTPMProjects]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--exec [sp_GetTPMProjects] 1
CREATE PROCEDURE [dbo].[sp_GetTPMProjects] 
(
		@UserID INT
)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT p.ID
      ,p.Name
      ,p.Teamsize
      ,p.ClientName
      ,p.StartDate
      ,p.EndDate
      ,p.TargetEndDate
      ,p.Status
  FROM tpmProjects p
  WHERE p.TPMID = @UserID
SELECT 
	   cp.ID
	   ,tm.ID as UserID
	  ,tm.Name
      ,tm.Role
      ,tm.EmailID
      ,tm.YOE
      ,mp.ProjectID
  FROM tpmTeamMember tm 
  left join tpmMemberProject mp
  on tm.ID = mp.MemberID
  LEFT join tpmProjects cp
  on mp.ProjectID = cp.ID
  WHERE cp.TPMID = @UserID
 SELECT ut.UserID      
      ,ut.ProjectID
      ,ut.TaskHeader
      ,ut.TaskDetails      
  FROM tpmUserTasks ut 
  
  WHERE ut.ProjectID IN (SELECT ID FROM tpmProjects WHERE TPMID = @UserID)
  --WHERE tm.ID in (SELECT MemberID FROM tpmMemberProject WHERE ProjectID IN
	--				(SELECT ID FROM tpmProjects WHERE TPMID = @UserID) )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AddTeamMember]    Script Date: 02/04/2014 15:27:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,
-- Description:	<Description,,
-- =============================================
-- Delete tpmTeamMember WHERE EmailID = 'n@b.in'
-- exec sp_AddTeamMember 'Nabin-1','n@b.in','PL',11,7,'gE+++Guwy/oZXAaFOLDdNg=='
CREATE PROCEDURE [dbo].[sp_AddTeamMember]
(
		@Name varchar(150),
        @Email varchar(100),
        @Role varchar(50),                      
        @YOE int,
        @ProjectID int,
        @EncryptedPassword varchar(max)
)
AS
BEGIN
DECLARE @MemberID INT
 DECLARE @TPMID INT
 DECLARE @TPMTECHNOLOGY VARCHAR(20)
 DECLARE @TPMCOMPANYNAME VARCHAR(100)
 DECLARE @IsTPM BIT
 
 
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.	
IF (SELECT COUNT(*) FROM tpmTeamMember WHERE EmailID = @Email) = 0
 BEGIN
	INSERT INTO tpmTeamMember
           (
				 [Name]
				,[Role]
				,[YOE]
				,[EmailID])
     VALUES
           (
			   @Name,
			   @Role,
			   @YOE,
			   @Email
           )
           SET @MemberID = @@IDENTITY;
 END
 ELSE
 BEGIN
 SET @MemberID = (SELECT ID FROM tpmTeamMember WHERE EmailID = @Email);
 END
 SET @TPMID = (SELECT TPMID FROM tpmProjects WHERE ID = @ProjectID);
 SET @TPMCOMPANYNAME = (SELECT CompanyName FROM Users WHERE ID = @TPMID);
 SET @TPMTECHNOLOGY = (SELECT Technology FROM Users WHERE ID = @TPMID);
 
 IF @Role = 'TPM' 
 BEGIN 
  SET @IsTPM = 'true'
  
 END
 ELSE
 BEGIN
 SET @IsTPM = 'false'
  
 END 
 
 
 IF (SELECT COUNT(*) FROM tpmMemberProject WHERE MemberID = @MemberID AND ProjectID = @ProjectID) = 0
  BEGIN
			INSERT INTO tpmMemberProject(MemberID,ProjectID)
			VALUES(@MemberID,@ProjectID)
   END        
  IF (SELECT COUNT(*) FROM Users WHERE CompanyEmail = @Email) = 0
   BEGIN
         INSERT INTO Users(FullName,CompanyEmail,CompanyName,EncryptedPassword,IsInitialPassword           ,[UserName]           
           ,LastModifiedOn,LastModifiedBy,FeedbackStatus,Technology,IsTPM)
     VALUES(@Name,@Email,@TPMCOMPANYNAME,@EncryptedPassword,'true',@Email,GETDATE(),0,'false',
           @TPMTECHNOLOGY,@IsTPM)
END
Select @MemberID as 'MemberID'
END
GO
/****** Object:  Default [DF_tpmUserTasks_LastModifiedOn]    Script Date: 02/04/2014 15:27:08 ******/
ALTER TABLE [dbo].[tpmUserTasks] ADD  CONSTRAINT [DF_tpmUserTasks_LastModifiedOn]  DEFAULT (getdate()) FOR [LastModifiedOn]
GO
/****** Object:  ForeignKey [FK_Member]    Script Date: 02/04/2014 15:27:08 ******/
ALTER TABLE [dbo].[tpmMemberProject]  WITH CHECK ADD  CONSTRAINT [FK_Member] FOREIGN KEY([MemberID])
REFERENCES [dbo].[tpmTeamMember] ([ID])
GO
ALTER TABLE [dbo].[tpmMemberProject] CHECK CONSTRAINT [FK_Member]
GO
/****** Object:  ForeignKey [FK_tpmTeamMember_tpmTeamMember]    Script Date: 02/04/2014 15:27:08 ******/
ALTER TABLE [dbo].[tpmTeamMember]  WITH CHECK ADD  CONSTRAINT [FK_tpmTeamMember_tpmTeamMember] FOREIGN KEY([ID])
REFERENCES [dbo].[tpmTeamMember] ([ID])
GO
ALTER TABLE [dbo].[tpmTeamMember] CHECK CONSTRAINT [FK_tpmTeamMember_tpmTeamMember]
GO
/****** Object:  ForeignKey [FK_AssignedBy]    Script Date: 02/04/2014 15:27:08 ******/
ALTER TABLE [dbo].[tpmUserTasks]  WITH CHECK ADD  CONSTRAINT [FK_AssignedBy] FOREIGN KEY([AssignedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[tpmUserTasks] CHECK CONSTRAINT [FK_AssignedBy]
GO

CREATE TABLE [dbo].[ApiLogs](
	[Id] [uniqueidentifier] NOT NULL,
	[TraceId] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL,
	[RequestPath] [nvarchar](max) NULL,
	[RequestMethod] [nvarchar](max) NULL,
	[RequestBody] [nvarchar](max) NULL,
	[StatusCode] [int] NOT NULL,
	[ResponseBody] [nvarchar](max) NULL,
	[ExceptionMessage] [nvarchar](max) NULL,
	[ExceptionStackTrace] [nvarchar](max) NULL,
	[ExecutionTimeMs] [bigint] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ApiLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[ErrorLog](
	[Id] [uniqueidentifier] NOT NULL,
	[ErrorNumber] [int] NULL,
	[ErrorSeverity] [int] NULL,
	[ErrorState] [int] NULL,
	[ErrorProcedure] [nvarchar](200) NULL,
	[ErrorLine] [int] NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
CREATE TABLE [dbo].[Students](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[MiddleName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [int] NOT NULL,
	[FatherName] [varchar](50) NOT NULL,
	[MotherName] [varchar](50) NOT NULL,
	[AdharNumber] [varchar](50) NOT NULL,
	[Adddress] [varchar](200) NOT NULL,
	[IsArchived] [bit] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[UpdatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ErrorLog] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ErrorLog] ADD  DEFAULT (sysdatetime()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Students] ADD  DEFAULT ((0)) FOR [IsArchived]
GO
ALTER TABLE [dbo].[Students] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Students] ADD  DEFAULT (NULL) FOR [UpdatedOn]
GO
CREATE PROCEDURE [dbo].[sp_AddNewStudent]
(
	@FirstName VARCHAR(50),
	@MiddleName VARCHAR(50),
	@LastName VARCHAR(50),
	@DateOfBirth DATE,
	@Gender INT,
	@FatherName VARCHAR(50),
	@MotherName VARCHAR(50), 
	@AdharNumber VARCHAR(50),
	@Address VARCHAR(500),
    @CreatedBy VARCHAR(50),
    @StudentId UNIQUEIDENTIFIER OUTPUT,
    @Message VARCHAR(500) OUTPUT
)
AS BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
		SET @StudentId=NEWID()
        INSERT INTO [dbo].[Students]
                    ([Id]
                    ,[FirstName]
                    ,[MiddleName]
                    ,[LastName]
                    ,[DateOfBirth]
                    ,[Gender]
                    ,[FatherName]
                    ,[MotherName]
                    ,[AdharNumber]
                    ,[Adddress]
                    ,[IsArchived]
                    ,[CreatedBy]
                    ,[CreatedOn]
                    ,[UpdatedBy]
                    ,[UpdatedOn])
                VALUES
                    (@StudentId
                    ,@FirstName
                    ,@MiddleName
                    ,@LastName
                    ,@DateOfBirth
                    ,@Gender
                    ,@FatherName
                    ,@MotherName
                    ,@AdharNumber
                    ,@Address
                    ,0
                    ,@CreatedBy
                    ,GETDATE()
                    ,NULL
                    ,NULL)
        COMMIT;
        SET @Message='Saved Successfully';
        RETURN 0;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
                ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        DECLARE @ErrorProcedure NVARCHAR(200) = ERROR_PROCEDURE();
        DECLARE @ErrorLine INT = ERROR_LINE();
        
        INSERT INTO ErrorLog (ErrorMessage, ErrorProcedure, ErrorLine, ErrorSeverity, ErrorState,CreatedOn)
        VALUES (@ErrorMessage, @ErrorProcedure, @ErrorLine, @ErrorSeverity, @ErrorState, GETDATE());

        SET @StudentId = '00000000-0000-0000-0000-000000000000';
        SET @Message = 'Error occurred: ' + @ErrorMessage;

        RETURN 1
    END CATCH
END
GO
CREATE PROCEDURE [dbo].[sp_ApiLogs]
(
	@TraceId VARCHAR(MAX),
	@RequestPath VARCHAR(MAX),
	@RequestMethod VARCHAR(MAX),
	@RequestBody VARCHAR(MAX),
	@StatusCode VARCHAR(MAX),
	@ResponseBody VARCHAR(MAX),
	@ExceptionMessage VARCHAR(MAX),
	@ExceptionStackTrace BIGINT,
	@ExecutionTimeMs VARCHAR(MAX)
)
AS BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        DECLARE @Id UNIQUEIDENTIFIER = NEWID();
        INSERT INTO [dbo].[ApiLogs]
                   ([Id]
                   ,[TraceId]
                   ,[UserId]
                   ,[RequestPath]
                   ,[RequestMethod]
                   ,[RequestBody]
                   ,[StatusCode]
                   ,[ResponseBody]
                   ,[ExceptionMessage]
                   ,[ExceptionStackTrace]
                   ,[ExecutionTimeMs]
                   ,[CreatedAt])
             VALUES
                   (@Id
                   ,@TraceId
                   ,NULL
                   ,@RequestPath
                   ,@RequestMethod
                   ,@RequestBody
                   ,@StatusCode
                   ,@ResponseBody
                   ,@ExceptionMessage
                   ,@ExceptionStackTrace
                   ,@ExecutionTimeMs
                   ,GETDATE())
        COMMIT;
        RETURN 0;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
                ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        DECLARE @ErrorProcedure NVARCHAR(200) = ERROR_PROCEDURE();
        DECLARE @ErrorLine INT = ERROR_LINE();
        
        INSERT INTO ErrorLog (ErrorMessage, ErrorProcedure, ErrorLine, ErrorSeverity, ErrorState,CreatedOn)
        VALUES (@ErrorMessage, @ErrorProcedure, @ErrorLine, @ErrorSeverity, @ErrorState, GETDATE());
        RETURN 1
    END CATCH
END
GO
CREATE PROCEDURE [dbo].[sp_UpdateStudent]
(
    @Id UNIQUEIDENTIFIER,
	@FirstName VARCHAR(50),
	@MiddleName VARCHAR(50),
	@LastName VARCHAR(50),
	@DateOfBirth DATE,
	@Gender INT,
	@FatherName VARCHAR(50),
	@MotherName VARCHAR(50), 
	@AdharNumber VARCHAR(50),
	@Address VARCHAR(500),
    @UpdatedBy VARCHAR(50),
    @Message VARCHAR(500) OUTPUT
)
AS BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        UPDATE [dbo].[Students]
           SET [FirstName] = @FirstName
              ,[MiddleName] =@MiddleName
              ,[LastName] = @LastName
              ,[DateOfBirth] = @DateOfBirth
              ,[Gender] = @Gender
              ,[FatherName] = @FatherName
              ,[MotherName] = @MotherName
              ,[AdharNumber] = @AdharNumber
              ,[Adddress] = @Address
              ,[UpdatedBy] = @UpdatedBy
              ,[UpdatedOn] = GETDATE()
         WHERE Id=@Id
        COMMIT;
        RETURN 0;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
                ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        DECLARE @ErrorProcedure NVARCHAR(200) = ERROR_PROCEDURE();
        DECLARE @ErrorLine INT = ERROR_LINE();
        
        INSERT INTO ErrorLog (ErrorMessage, ErrorProcedure, ErrorLine, ErrorSeverity, ErrorState,CreatedOn)
        VALUES (@ErrorMessage, @ErrorProcedure, @ErrorLine, @ErrorSeverity, @ErrorState, GETDATE());
        SET @Message = 'Error occurred: ' + @ErrorMessage;

        RETURN 1
    END CATCH
END
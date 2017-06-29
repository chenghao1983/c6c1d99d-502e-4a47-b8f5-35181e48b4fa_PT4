--------------------------Structure-----------------  
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map] DROP CONSTRAINT [FK_Student_Course_Map_Student]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map] DROP CONSTRAINT [FK_Student_Course_Map_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student] DROP CONSTRAINT [FK_Student_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizQuestion_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizQuestion]'))
ALTER TABLE [dbo].[QuizQuestion] DROP CONSTRAINT [FK_QuizQuestion_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizOption_QuizQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizOption]'))
ALTER TABLE [dbo].[QuizOption] DROP CONSTRAINT [FK_QuizOption_QuizQuestion]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] DROP CONSTRAINT [FK_QuizAnswer_Student]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_QuizOption]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] DROP CONSTRAINT [FK_QuizAnswer_QuizOption]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map] DROP CONSTRAINT [FK_Instructor_Course_Map_Instructor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map] DROP CONSTRAINT [FK_Instructor_Course_Map_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor]'))
ALTER TABLE [dbo].[Instructor] DROP CONSTRAINT [FK_Instructor_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content] DROP CONSTRAINT [FK_Content_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] DROP CONSTRAINT [FK_Chat_Student]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] DROP CONSTRAINT [FK_Chat_Instructor]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] DROP CONSTRAINT [FK_Chat_Course]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Admin_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Admin]'))
ALTER TABLE [dbo].[Admin] DROP CONSTRAINT [FK_Admin_User]
GO
/****** Object:  Table [dbo].[User]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Student_Course_Map]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]') AND type in (N'U'))
DROP TABLE [dbo].[Student_Course_Map]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
DROP TABLE [dbo].[Student]
GO
/****** Object:  Table [dbo].[QuizQuestion]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizQuestion]') AND type in (N'U'))
DROP TABLE [dbo].[QuizQuestion]
GO
/****** Object:  Table [dbo].[QuizOption]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizOption]') AND type in (N'U'))
DROP TABLE [dbo].[QuizOption]
GO
/****** Object:  Table [dbo].[QuizAnswer]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizAnswer]') AND type in (N'U'))
DROP TABLE [dbo].[QuizAnswer]
GO
/****** Object:  Table [dbo].[Instructor_Course_Map]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]') AND type in (N'U'))
DROP TABLE [dbo].[Instructor_Course_Map]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor]') AND type in (N'U'))
DROP TABLE [dbo].[Instructor]
GO
/****** Object:  Table [dbo].[DBVersion]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DBVersion]') AND type in (N'U'))
DROP TABLE [dbo].[DBVersion]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Course]') AND type in (N'U'))
DROP TABLE [dbo].[Course]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
DROP TABLE [dbo].[Content]
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Chat]') AND type in (N'U'))
DROP TABLE [dbo].[Chat]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 26/04/2016 23:04:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin]') AND type in (N'U'))
DROP TABLE [dbo].[Admin]
GO

drop procedure if exists GetCourseRegistrations
go

drop view if exists GetCourseRegistrationsView
go

/****** Object:  Table [dbo].[Admin]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Admin](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[UserSid] [int] NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Chat]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Chat](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[CourseSid] [int] NOT NULL,
	[StudentSid] [int] NULL,
	[InstructorSid] [int] NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Content]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Content]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Content](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[CourseSid] [int] NOT NULL,
	[Type] [char](1) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[OriginalFileName] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Course]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Course]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Course](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[DBVersion]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DBVersion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DBVersion](
	[DBVersion] [nvarchar](255) NULL,
	[CreateDT] [datetime] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Instructor](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[UserSid] [int] NOT NULL,
	[Qualification] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Instructor] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Instructor_Course_Map]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Instructor_Course_Map](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[InstructorSid] [int] NOT NULL,
	[CourseSid] [int] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
 CONSTRAINT [PK_Instructor_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[QuizAnswer]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizAnswer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuizAnswer](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizQuestionSid] [int] NOT NULL,
	[QuizOptionSid] [int] NOT NULL,
	[StudentSid] [int] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_QuizAnswer] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[QuizOption]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizOption]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuizOption](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[QuizQuestionSid] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[IsCorrect] [bit] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_QuizOption] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC,
	[QuizQuestionSid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[QuizQuestion]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QuizQuestion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[QuizQuestion](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
	[CourseSid] [int] NOT NULL,
 CONSTRAINT [PK_QuizQuestion] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Student]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Student](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[BatchNo] [nvarchar](50) NOT NULL,
	[UserSid] [int] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Student_Course_Map]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Student_Course_Map](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[StudentSid] [int] NOT NULL,
	[CourseSid] [int] NOT NULL,
	[CreateDT] [datetime] NOT NULL,
 CONSTRAINT [PK_Student_Course_Map] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[User]    Script Date: 26/04/2016 23:04:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[Sid] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,	
	[IsActive] [bit] NOT NULL,
	[Role] [char](1) NOT NULL,
	[PasswordSalt] [nvarchar](max) NOT NULL,
	[CreateDT] [datetime] NOT NULL,
	[UpdateDT] [datetime] NULL,
	[DeleteDT] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Admin_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Admin]'))
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_User] FOREIGN KEY([UserSid])
REFERENCES [dbo].[User] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Admin_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Admin]'))
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_Admin_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Instructor] FOREIGN KEY([InstructorSid])
REFERENCES [dbo].[Instructor] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Instructor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Student] FOREIGN KEY([StudentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Chat_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Chat]'))
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Student]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content]  WITH CHECK ADD  CONSTRAINT [FK_Content_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Content_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Content]'))
ALTER TABLE [dbo].[Content] CHECK CONSTRAINT [FK_Content_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor]'))
ALTER TABLE [dbo].[Instructor]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_User] FOREIGN KEY([UserSid])
REFERENCES [dbo].[User] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor]'))
ALTER TABLE [dbo].[Instructor] CHECK CONSTRAINT [FK_Instructor_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Course_Map_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map] CHECK CONSTRAINT [FK_Instructor_Course_Map_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Course_Map_Instructor] FOREIGN KEY([InstructorSid])
REFERENCES [dbo].[Instructor] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructor_Course_Map_Instructor]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instructor_Course_Map]'))
ALTER TABLE [dbo].[Instructor_Course_Map] CHECK CONSTRAINT [FK_Instructor_Course_Map_Instructor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_QuizOption]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuizAnswer_QuizOption] FOREIGN KEY([QuizOptionSid], [QuizQuestionSid])
REFERENCES [dbo].[QuizOption] ([Sid], [QuizQuestionSid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_QuizOption]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] CHECK CONSTRAINT [FK_QuizAnswer_QuizOption]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer]  WITH CHECK ADD  CONSTRAINT [FK_QuizAnswer_Student] FOREIGN KEY([StudentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizAnswer_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizAnswer]'))
ALTER TABLE [dbo].[QuizAnswer] CHECK CONSTRAINT [FK_QuizAnswer_Student]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizOption_QuizQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizOption]'))
ALTER TABLE [dbo].[QuizOption]  WITH CHECK ADD  CONSTRAINT [FK_QuizOption_QuizQuestion] FOREIGN KEY([QuizQuestionSid])
REFERENCES [dbo].[QuizQuestion] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizOption_QuizQuestion]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizOption]'))
ALTER TABLE [dbo].[QuizOption] CHECK CONSTRAINT [FK_QuizOption_QuizQuestion]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizQuestion_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizQuestion]'))
ALTER TABLE [dbo].[QuizQuestion]  WITH CHECK ADD  CONSTRAINT [FK_QuizQuestion_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_QuizQuestion_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[QuizQuestion]'))
ALTER TABLE [dbo].[QuizQuestion] CHECK CONSTRAINT [FK_QuizQuestion_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_User] FOREIGN KEY([UserSid])
REFERENCES [dbo].[User] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Student_Course_Map_Course] FOREIGN KEY([CourseSid])
REFERENCES [dbo].[Course] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map] CHECK CONSTRAINT [FK_Student_Course_Map_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map]  WITH CHECK ADD  CONSTRAINT [FK_Student_Course_Map_Student] FOREIGN KEY([StudentSid])
REFERENCES [dbo].[Student] ([Sid])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Course_Map_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Course_Map]'))
ALTER TABLE [dbo].[Student_Course_Map] CHECK CONSTRAINT [FK_Student_Course_Map_Student]
GO
--------------------------Index--------------------- 
--------------------------Function------------------ 
--------------------------View---------------------- 
--------------------------SP------------------------ 
--------------------------Data---------------------- 


---  [dbo].[User]  --
DELETE from [dbo].[Student_Course_Map]
GO
DELETE from [dbo].[Instructor_Course_Map]
GO
DELETE from [dbo].[Student]
GO
DELETE from [dbo].[Instructor]
Go
DELETE from [dbo].[Admin]
GO
DELETE FROM  [dbo].[User] 
GO

SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (1, N'Admin1', N'/OdzAoQWZRpJ1JZa+gpRYPannNShlOb8J', 'Super Admin', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:15:17.527' AS DateTime), NULL, NULL, N'A',N'ZB/sYHfiuxnSFd/ZoMZMfNqAW+MpQu5x ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (2, N'Instructor1', N't5bahHgyfjMDi7jcwE7zz+CUTW6E8VZw', 'Kent White', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:17:34.010' AS DateTime), NULL, NULL, N'I',N'C0xyFiz5b3ZacMTRPwKleI0eFaSy0x8C ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (3, N'Instructor2', N'xk/xDrqvUTmp//xjd2VaxtsT0uJf9nDj', 'Eric Johnson', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:17:41.910' AS DateTime), NULL, NULL, N'I',N'0OTW6ch2NjxC1q7sIYl586hQMzftY+Wd ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (4, N'Instructor3', N'oa2koZYg8TLrW4qUfykT7ilKoTgL4lEy', 'Tom Cruise', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:18:59.883' AS DateTime), NULL, NULL, N'I',N'hTJOxnbQmKLChQqOwKBv6s6BCRrE2riL ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (5, N'Instructor4', N'TFQv51wShe7p5Mxinu6wEm66j7eK8AmT', 'Walker Smith', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'I',N'i2k7XSN+Gi8kDpq9feHX2H7zMjVx4dGR ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (6, N'Instructor5', N'NSr+4maxT00PDMoKNGuJ4xWo+4OoCQpe', 'Celestino Douglas', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'I',N'Luni0t7I0MCTOMbt8n0bpuQUpAAIgfdh ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (7, N'Instructor6', N'6yG3xPGAdpnlCy1UlodH4GGhNw5yEX08', 'Alexandros Hafiz', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'I',N'cxxx/Zd2AKXaUaNAze+t20MF+gCnbMFZ ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (8, N'Instructor7', N'TmHNU5R0Fpw5INQrazI/PQA+7zy5iVta', 'Dimas Channing', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'I',N'xnnwmyxMXSfQtcUpnSHceSMTlJZ+VdDO ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (9, N'Instructor8', N'm9Z8my2Kx0tAaEpWOJ3CDki2oua8bDgO', 'Codie Lucinda', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'I',N'VklxCbwR/oHKBRV6npO0VmbAvbR46Sd/ ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (10, N'Instructor9', N'KUlleZSbFADpi8n+M7pIOlCmhEZYJnR6', 'Torger Kayleen','joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'I',N'VJkbnSovc1BtPy04RybAo8B8sAAwTn2J ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (11, N'Instructor10', N'MWM6zwwL1eeZxfZ3XjUeWYHWkbLM0A4C', 'Filippa Cynbel', 'joechenghao@gmail.com', 0, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'I',N'cZZ7m8Sj5pv4Ag7hP8ot9ZqDCHbrOooI ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (12, N'Student1', N'QV7JswCtrAikzyagdRArK41ZR7CHuqf7', 'Adil Hayati', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'S',N'KrTW84isQkhtiMQxzbwLKfIsLmOhobSY ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (13, N'Student2', N'+iFZH18HLlJnGWWOWsanO5fxhVgHKIp3', 'Mette Iris', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'S',N'HKjH9qgCG1tyVMVcEIJXdxtxJ5bXYufr ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (14, N'Student3', N'bV2Wt4MR/iKklHcxNfEl5YJd6gtwqZiH', 'Amelia Funanya', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'S',N'8YQJqFAV9xUWwHiXfODFuhuDTS5ukQwt ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (15, N'Student4', N'B2Cw0JIvHX5auHKvd+qJawpAh0xEZFJR', 'Jurgen Sofia', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'S',N'yKV616WHc2i8rUKGMXMhskBWkWXm4AQC ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (16, N'Student5', N'cP/4dmCYJMfCqT8zEEWCecJLZQzYVqc4', 'Athena Herbert', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'S',N'7bDVnI3WlC1q2ApB+ASvPHsZwKjkvBzZ ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (17, N'Student6', N'eO060WBvNSFuNE2ZE3HU0yGjqQOK3+yf', 'Chrissy Ambre', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'S',N'4c08nVFZyiI73vZd9P7bEuBvpvHJhYZO ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (18, N'Student7', N'p4ZhdGoLJKQW8sscjoP+Vkcg/F679pAH', 'Faddei Kir', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'S',N'FHBd7pjdeFwdNJaRJPzxdfpg/zXWChDx ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (19, N'Student8', N'GucDVKmV2/8cVDywlKRr4g++4/OpJksc', 'Sima Folke', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'S',N'qDzmSff2D9K2BlhQYrZ6YfGB1BETav+j ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (20, N'Student9', N'8KJLSQXv5bURV8U2ZU+rAI54+w7A9xYI', 'Wendelin Jaci', 'joechenghao@gmail.com', 1, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'S',N'zK1HYfERIWKCtu3djAlZD8SheRXI7w6B ')
GO
INSERT [dbo].[User] ([Sid], [Username], [Password], [FullName], [Email], [IsActive], [CreateDT], [UpdateDT], [DeleteDT], [Role], [PasswordSalt]) VALUES (21, N'Student10', N'czIthJ583ZfkcszEIEfEK+q2pWDq3ofB', 'Valencia Demeter', 'joechenghao@gmail.com', 0, CAST(N'2016-04-13 23:21:00.933' AS DateTime), NULL, NULL, N'S',N'JolFZ40Wq1Hz3Jfuo4NhIt+BQL06TmFZ ')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO

---  [dbo].[Admin]  -- 

SET IDENTITY_INSERT [dbo].[Admin] ON 
GO

INSERT [dbo].[Admin]([Sid],[UserSid]) VALUES (1,1)
GO

SET IDENTITY_INSERT [dbo].[Admin] OFF
GO

---  [dbo].[Instructor]  -- 

SET IDENTITY_INSERT [dbo].[Instructor] ON 
GO

INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (1,2,N'Doctorate')
GO
INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (2,3,N'Doctorate')
GO
INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (3,4,N'Doctorate')
GO
INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (4,5,N'Doctorate')
GO
INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (5,6,N'Mater degree')
GO
INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (6,7,N'Mater degree')
GO
INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (7,8,N'Honor Bachelor')
GO
INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (8,9,N'Mater degree')
GO
INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (9,10,N'Mater degree')
GO
INSERT [dbo].[Instructor]([Sid],[UserSid], [Qualification]) VALUES (10,11,N'Doctorate')
GO

SET IDENTITY_INSERT [dbo].[Instructor] OFF
GO

---  [dbo].[Student]  -- 

SET IDENTITY_INSERT [dbo].[Student] ON 
GO

INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (1,12,'SE20')
GO
INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (2,13,'SE20')
GO
INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (3,14,'SE20')
GO
INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (4,15,'SE21')
GO
INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (5,16,'SE21')
GO
INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (6,17,'SE22')
GO
INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (7,18,'SE22')
GO
INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (8,19,'SE23')
GO
INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (9,20,'SE23')
GO
INSERT [dbo].[Student]([Sid],[UserSid],[BatchNo]) VALUES (10,21,'SE24')
GO

SET IDENTITY_INSERT [dbo].[Student] OFF
GO


-- [dbo].[Course] --
DELETE from [dbo].[Chat]
GO
DELETE from [dbo].[QuizOption]
GO
DELETE from [dbo].[QuizQuestion]
GO
DELETE FROM [dbo].[Content]
GO
DELETE FROM [dbo].[Course]
GO

SET IDENTITY_INSERT [dbo].[Course] ON
GO

INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (1, 'Enterprise .NET - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (2, 'Object Oriented Software Development - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (3, 'Project Initiation and Scope Management - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (4, 'Scope Management and Risk Management - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (5, 'Risk Management and Work Breakdown Structure - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (6, 'Object Oriented Software Design - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (7, 'Scheduling and Producing Project Plans - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (8, 'Advanced Project Estimation - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (9, 'Project Tracking and Control - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (10, 'People Management - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (11, 'Computational Intelligence - SE24', GETDATE())
GO
INSERT INTO [dbo].[Course] (Sid, CourseName, CreateDT) VALUES (12, 'Agile Software Project Management - SE24', GETDATE())
GO

SET IDENTITY_INSERT [dbo].[Course] OFF
GO


-- Student Course Map

SET IDENTITY_INSERT [dbo].[Student_Course_Map] ON
GO

INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (1, 1, 1, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (2, 1, 2, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (3, 1, 6, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (4, 1, 12, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (5, 2, 1, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (6, 2, 3, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (7, 2, 4, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (8, 2, 6, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (9, 2, 7, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (10, 2, 8, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (11, 2, 10, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (12, 2, 11, GETDATE())
GO
INSERT INTO [dbo].[Student_Course_Map] (Sid, StudentSid, CourseSid, CreateDT) VALUES (13, 3, 9, GETDATE())
GO

SET IDENTITY_INSERT [dbo].[Student_Course_Map] OFF
GO

-- Instructor Course Map

SET IDENTITY_INSERT [dbo].[Instructor_Course_Map] ON
GO

INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (1, 1, 1, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (2, 1, 2, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (3, 2, 3, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (4, 2, 6, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (5, 3, 3, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (6, 4, 1, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (7, 5, 1, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (8, 6, 12, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (9, 7, 10, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (10, 8, 4, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (11, 8, 5, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (12, 8, 11, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (13, 9, 7, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (14, 10, 9, GETDATE())
GO
INSERT INTO [dbo].[Instructor_Course_Map] (Sid, InstructorSid, CourseSid, CreateDT) VALUES (15, 10, 8, GETDATE())
GO

SET IDENTITY_INSERT [dbo].[Instructor_Course_Map] OFF
GO

-- Quiz Question --

SET IDENTITY_INSERT [dbo].[QuizQuestion] ON 
GO

INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (1, N'When was .NET first released?', CAST(N'2016-04-20 12:22:15.503' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (2, N'What fictional company did Nancy Davolio work for?', CAST(N'2016-04-20 12:22:15.503' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (3, N'The first and still the oldest domain name on the internet is:', CAST(N'2016-04-20 12:22:15.503' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (4, N'Which is not actually a Thing.js?', CAST(N'2016-04-20 12:22:15.503' AS DateTime), NULL, NULL, 1)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (5, N'In what year was the first Voice Over IP (VOIP, GETDATE(), 4) call made?', CAST(N'2016-04-20 12:22:15.503' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (6, N'"Chica" was codename for what Microsoft product?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (7, N'How many loop constructs are there in C#?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 2)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (8, N'What was the first CodePlex.com project?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 3)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (9, N'Last name of the employee who wears Microsoft badge 00001', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 3)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (10, N'When did Scott Hanselman join Microsoft?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (11, N'How big is a nibble?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (12, N'How many function calls did Windows 1.0 approximately have?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (13, N'Which Star Wars movie was filmed entirely in the studio?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (14, N'What is Superman''s Kryptonian name?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (15, N'What is the image name for the Windows Task Manager application?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (16, N'When was the internet opened to commercial use?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (17, N'When was the Xbox unveiled?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (18, N'What is the value of an Object + Array in JavaScript?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (19, N'Why was the IBM PCjr despised by users?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 5)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (20, N'What was the max memory supported by MS-DOS?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 5)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (21, N'When was the first laser mouse released?', CAST(N'2016-04-20 12:22:15.507' AS DateTime), NULL, NULL, 8)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (22, N'What was Microsoft''s first product?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 8)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (23, N'What building does not exist on the Microsoft campus?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (24, N'Who wrote the first computer program?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 7)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (25, N'Visual Basic was first released in what year?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 7)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (26, N'Which of the following is NOT a prime number?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (27, N'Yukihiro Matsumoto conceived what programming language on February 24, 1993?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (28, N'Which release of the .NET Framework introduced support for dynamic languages?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (29, N'What is the package manager for Node.js?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (30, N'In the acronym PaaS, what do the P stand for', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 6)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (31, N'What is the speed of light in metres per second?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 6)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (32, N'What was the original name of the C# programming language?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 6)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (33, N'Which of the following is an example of Boxing in C#?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 6)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (34, N'Which of the following was not an alternative name considered for XML?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (35, N'How many HTML tags are defined in the original description of the markup language?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (36, N'Which of the following ECMA standards represents the standardization of JavaScript?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (37, N'What was the first Web Browser called?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (38, N'In version control systems, the process of bringing together two sets of changes is called what?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (39, N'In 1980, Microsoft released there first operating system. What was it called?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 4)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (40, N'Which ASCII code (in decimal, GETDATE(), 4) represents the character B?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 5)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (41, N'Which are the first 6 decimal digits of Pi?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 5)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (42, N'Internet Protocol v4 provides approximately how many addresses?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 5)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (43, N'What is Layer 4 of the OSI Model?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 5)
GO
INSERT [dbo].[QuizQuestion] ([Sid], [Title], [CreateDT], [UpdateDT], [DeleteDT], [CourseSid]) VALUES (44, N'Which of the following is NOT a value type in the .NET Framework Common Type System?', CAST(N'2016-04-20 12:22:15.510' AS DateTime), NULL, NULL, 5)
GO
SET IDENTITY_INSERT [dbo].[QuizQuestion] OFF
GO

-- QuizOption --


SET IDENTITY_INSERT [dbo].[QuizOption] ON
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (1, 1, N'2000', 0, CAST(N'2016-04-20 12:49:50.567' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (2, 1, N'2001', 0, CAST(N'2016-04-20 12:49:50.570' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (3, 1, N'2002', 1, CAST(N'2016-04-20 12:49:50.570' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (4, 1, N'2003', 0, CAST(N'2016-04-20 12:49:50.570' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (5, 2, N'Contoso Ltd.', 0, CAST(N'2016-04-20 12:49:50.570' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (6, 2, N'Initech', 0, CAST(N'2016-04-20 12:49:50.570' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (7, 2, N'Fabrikam, Inc.', 0, CAST(N'2016-04-20 12:49:50.573' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (8, 2, N'Northwind Traders', 1, CAST(N'2016-04-20 12:49:50.573' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (9, 3, N'Network.com', 0, CAST(N'2016-04-20 12:49:50.573' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (10, 3, N'Alpha4.com', 0, CAST(N'2016-04-20 12:49:50.573' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (11, 3, N'Symbolics.com', 1, CAST(N'2016-04-20 12:49:50.573' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (12, 3, N'InterConnect.com', 0, CAST(N'2016-04-20 12:49:50.573' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (13, 4, N'Mustache.js', 0, CAST(N'2016-04-20 12:49:50.573' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (14, 4, N'Hammer.js', 0, CAST(N'2016-04-20 12:49:50.577' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (15, 4, N'Horseradish.js', 1, CAST(N'2016-04-20 12:49:50.577' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (16, 4, N'Uglify.js', 0, CAST(N'2016-04-20 12:49:50.577' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (17, 5, N'1973', 1, CAST(N'2016-04-20 12:49:50.577' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (18, 5, N'1982', 0, CAST(N'2016-04-20 12:49:50.577' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (19, 5, N'1991', 0, CAST(N'2016-04-20 12:49:50.577' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (20, 5, N'1994', 0, CAST(N'2016-04-20 12:49:50.577' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (21, 6, N'Visual Basic', 0, CAST(N'2016-04-20 12:49:50.577' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (22, 6, N'Microsoft Surface', 0, CAST(N'2016-04-20 12:49:50.577' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (23, 6, N'Windows 95', 1, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (24, 6, N'Xbox', 0, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (25, 7, N'2', 0, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (26, 7, N'3', 0, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (27, 7, N'4', 1, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (28, 7, N'5', 0, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (29, 8, N'EntLib', 0, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (30, 8, N'IronPython', 1, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (31, 8, N'Ajax Toolkit', 0, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (32, 8, N'JSON.Net', 0, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (33, 9, N'McDonald', 1, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (34, 9, N'Gates', 0, CAST(N'2016-04-20 12:49:50.580' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (35, 9, N'Ballmer', 0, CAST(N'2016-04-20 12:49:50.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (36, 9, N'Allen', 0, CAST(N'2016-04-20 12:49:50.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (37, 10, N'2007', 1, CAST(N'2016-04-20 12:49:50.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (38, 10, N'2000', 0, CAST(N'2016-04-20 12:49:50.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (39, 10, N'2005', 0, CAST(N'2016-04-20 12:49:50.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (40, 10, N'2009', 0, CAST(N'2016-04-20 12:49:50.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (41, 11, N'4 bits', 1, CAST(N'2016-04-20 12:49:50.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (42, 11, N'8 bits', 0, CAST(N'2016-04-20 12:49:50.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (43, 11, N'16 bits', 0, CAST(N'2016-04-20 12:49:50.583' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (44, 11, N'2 bits', 0, CAST(N'2016-04-20 12:49:50.587' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (45, 12, N'100', 0, CAST(N'2016-04-20 12:49:50.587' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (46, 12, N'200', 0, CAST(N'2016-04-20 12:49:50.587' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (47, 12, N'600', 0, CAST(N'2016-04-20 12:49:50.587' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (48, 12, N'400', 1, CAST(N'2016-04-20 12:49:50.587' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (49, 13, N'1', 0, CAST(N'2016-04-20 12:49:50.587' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (50, 13, N'2', 0, CAST(N'2016-04-20 12:49:50.587' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (51, 13, N'3', 1, CAST(N'2016-04-20 12:49:50.587' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (52, 13, N'4', 0, CAST(N'2016-04-20 12:49:50.587' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (53, 14, N'Jor-El', 0, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (54, 14, N'Zod', 0, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (55, 14, N'Kal-El', 1, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (56, 14, N'Jax-Ur', 0, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (57, 15, N'taskmgr', 1, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (58, 15, N'tmanager', 0, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (59, 15, N'wtaskmgr', 0, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (60, 15, N'wintaskm', 0, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (61, 16, N'1989', 0, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (62, 16, N'1992', 0, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (63, 16, N'1990', 0, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (64, 16, N'1991', 1, CAST(N'2016-04-20 12:49:50.590' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (65, 17, N'2000', 0, CAST(N'2016-04-20 12:49:50.593' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (66, 17, N'2001', 1, CAST(N'2016-04-20 12:49:50.593' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (67, 17, N'2002', 0, CAST(N'2016-04-20 12:49:50.593' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (68, 17, N'2003', 0, CAST(N'2016-04-20 12:49:50.593' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (69, 18, N'0', 1, CAST(N'2016-04-20 12:49:50.593' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (70, 18, N'Array', 0, CAST(N'2016-04-20 12:49:50.593' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (71, 18, N'Object', 0, CAST(N'2016-04-20 12:49:50.593' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (72, 18, N'Type Error', 0, CAST(N'2016-04-20 12:49:50.593' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (73, 19, N'Chicklet keyboard', 0, CAST(N'2016-04-20 12:49:50.593' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (74, 19, N'No Hard Disk', 0, CAST(N'2016-04-20 12:49:50.597' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (75, 19, N'Not PC compatible', 0, CAST(N'2016-04-20 12:49:50.597' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (76, 19, N'All the above', 1, CAST(N'2016-04-20 12:49:50.597' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (77, 20, N'256K', 0, CAST(N'2016-04-20 12:49:50.597' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (78, 20, N'512K', 0, CAST(N'2016-04-20 12:49:50.597' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (79, 20, N'640K', 0, CAST(N'2016-04-20 12:49:50.597' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (80, 20, N'1M', 1, CAST(N'2016-04-20 12:49:50.597' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (81, 21, N'2001', 0, CAST(N'2016-04-20 12:49:50.597' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (82, 21, N'2002', 0, CAST(N'2016-04-20 12:49:50.597' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (83, 21, N'2003', 0, CAST(N'2016-04-20 12:49:50.597' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (84, 21, N'2004', 1, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (85, 22, N'DOS', 0, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (86, 22, N'Altair Basic', 1, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (87, 22, N'PC Basic', 0, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (88, 22, N'Windows', 0, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (89, 23, N'1', 0, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (90, 23, N'7', 1, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (91, 23, N'99', 0, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (92, 23, N'115', 0, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (93, 24, N'Charles Babbage', 0, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (94, 24, N'Herman Hollerith', 0, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (95, 24, N'Ada Lovelace', 1, CAST(N'2016-04-20 12:49:50.600' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (96, 24, N'Jakob Bernoulli', 0, CAST(N'2016-04-20 12:49:50.603' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (97, 25, N'1990', 0, CAST(N'2016-04-20 12:49:50.603' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (98, 25, N'1991', 1, CAST(N'2016-04-20 12:49:50.603' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (99, 25, N'1992', 0, CAST(N'2016-04-20 12:49:50.603' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (100, 25, N'1993', 0, CAST(N'2016-04-20 12:49:50.603' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (101, 26, N'257', 0, CAST(N'2016-04-20 12:49:50.603' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (102, 26, N'379', 0, CAST(N'2016-04-20 12:49:50.603' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (103, 26, N'571', 0, CAST(N'2016-04-20 12:49:50.603' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (104, 26, N'697', 1, CAST(N'2016-04-20 12:49:50.603' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (105, 27, N'Python', 0, CAST(N'2016-04-20 12:49:50.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (106, 27, N'Ruby', 1, CAST(N'2016-04-20 12:49:50.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (107, 27, N'Perl', 0, CAST(N'2016-04-20 12:49:50.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (108, 27, N'Boo', 0, CAST(N'2016-04-20 12:49:50.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (109, 28, N'1.1', 0, CAST(N'2016-04-20 12:49:50.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (110, 28, N'2.0', 0, CAST(N'2016-04-20 12:49:50.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (111, 28, N'3.5', 0, CAST(N'2016-04-20 12:49:50.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (112, 28, N'4.0', 1, CAST(N'2016-04-20 12:49:50.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (113, 29, N'npm', 1, CAST(N'2016-04-20 12:49:50.607' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (114, 29, N'yum', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (115, 29, N'rpm', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (116, 29, N'PEAR', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (117, 30, N'Programming', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (118, 30, N'Power', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (119, 30, N'Platform', 1, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (120, 30, N'Pedestrian', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (121, 31, N'299,792,458', 1, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (122, 31, N'312,123,156', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (123, 31, N'100,000,000', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (124, 31, N'541,123,102', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (125, 32, N'Boo', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (126, 32, N'C+++', 0, CAST(N'2016-04-20 12:49:50.610' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (127, 32, N'Cool', 1, CAST(N'2016-04-20 12:49:50.613' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (128, 32, N'Anders', 0, CAST(N'2016-04-20 12:49:50.613' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (129, 33, N'int foo = 12;', 0, CAST(N'2016-04-20 12:49:50.613' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (130, 33, N'System.Box(56);', 0, CAST(N'2016-04-20 12:49:50.613' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (131, 33, N'int foo = (int)bar;', 0, CAST(N'2016-04-20 12:49:50.613' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (132, 33, N'object bar = 42;', 1, CAST(N'2016-04-20 12:49:50.613' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (133, 34, N'MAGMA', 0, CAST(N'2016-04-20 12:49:50.613' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (134, 34, N'SGML', 1, CAST(N'2016-04-20 12:49:50.613' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (135, 34, N'SLIM', 0, CAST(N'2016-04-20 12:49:50.613' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (136, 34, N'MGML', 0, CAST(N'2016-04-20 12:49:50.617' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (137, 35, N'1', 0, CAST(N'2016-04-20 12:49:50.617' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (138, 35, N'11', 0, CAST(N'2016-04-20 12:49:50.617' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (139, 35, N'18', 1, CAST(N'2016-04-20 12:49:50.617' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (140, 35, N'25', 0, CAST(N'2016-04-20 12:49:50.617' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (141, 36, N'ECMA-123', 0, CAST(N'2016-04-20 12:49:50.617' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (142, 36, N'ECMA-262', 1, CAST(N'2016-04-20 12:49:50.617' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (143, 36, N'ECMA-301', 0, CAST(N'2016-04-20 12:49:50.617' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (144, 36, N'ECMA-431', 0, CAST(N'2016-04-20 12:49:50.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (145, 37, N'WorldWideWeb', 1, CAST(N'2016-04-20 12:49:50.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (146, 37, N'Mosaic', 0, CAST(N'2016-04-20 12:49:50.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (147, 37, N'Lynx', 0, CAST(N'2016-04-20 12:49:50.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (148, 37, N'pher', 0, CAST(N'2016-04-20 12:49:50.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (149, 38, N'Branch', 0, CAST(N'2016-04-20 12:49:50.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (150, 38, N'Commit', 0, CAST(N'2016-04-20 12:49:50.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (151, 38, N'Merge', 1, CAST(N'2016-04-20 12:49:50.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (152, 38, N'Share', 0, CAST(N'2016-04-20 12:49:50.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (153, 39, N'MS-DOS', 0, CAST(N'2016-04-20 12:49:50.620' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (154, 39, N'Windows', 0, CAST(N'2016-04-20 12:49:50.623' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (155, 39, N'Xenix', 1, CAST(N'2016-04-20 12:49:50.623' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (156, 39, N'Altair OS', 0, CAST(N'2016-04-20 12:49:50.623' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (157, 40, N'22', 0, CAST(N'2016-04-20 12:49:50.623' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (158, 40, N'66', 1, CAST(N'2016-04-20 12:49:50.623' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (159, 40, N'97', 0, CAST(N'2016-04-20 12:49:50.623' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (160, 40, N'112', 0, CAST(N'2016-04-20 12:49:50.623' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (161, 41, N'3.14159', 1, CAST(N'2016-04-20 12:49:50.623' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (162, 41, N'3.14195', 0, CAST(N'2016-04-20 12:49:50.627' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (163, 41, N'3.14132', 0, CAST(N'2016-04-20 12:49:50.627' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (164, 41, N'3.14123', 0, CAST(N'2016-04-20 12:49:50.627' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (165, 42, N'1.5 billion', 0, CAST(N'2016-04-20 12:49:50.627' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (166, 42, N'4.3 billion', 1, CAST(N'2016-04-20 12:49:50.627' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (167, 42, N'55 billion', 0, CAST(N'2016-04-20 12:49:50.627' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (168, 42, N'3.4 trillion', 0, CAST(N'2016-04-20 12:49:50.627' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (169, 43, N'Network Layer', 0, CAST(N'2016-04-20 12:49:50.627' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (170, 43, N'Transport Layer', 1, CAST(N'2016-04-20 12:49:50.630' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (171, 43, N'Session Layer', 0, CAST(N'2016-04-20 12:49:50.630' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (172, 43, N'Presentation Layer', 0, CAST(N'2016-04-20 12:49:50.630' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (173, 44, N'System.Integer', 0, CAST(N'2016-04-20 12:49:50.630' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (174, 44, N'System.String', 1, CAST(N'2016-04-20 12:49:50.630' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (175, 44, N'System.DateTime', 0, CAST(N'2016-04-20 12:49:50.630' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[QuizOption] ([Sid], [QuizQuestionSid], [Title], [IsCorrect], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (176, 44, N'System.Float', 0, CAST(N'2016-04-20 12:49:50.630' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[QuizOption] OFF

-- Content --

SET IDENTITY_INSERT [dbo].[Content] ON
INSERT INTO [dbo].[Content] ([Sid], [CourseSid], [Type], [Path], [FileName], [OriginalFileName], [CreateDT], [UpdateDT], [DeleteDT]) VALUES(1, 1, 'F', '/Upload/', '0e682838-8ea1-4f47-b8b7-7e55fe322769.txt', 'test.txt', Getdate(), null,  null)
GO
INSERT INTO [dbo].[Content] ([Sid], [CourseSid], [Type], [Path], [FileName], [OriginalFileName], [CreateDT], [UpdateDT], [DeleteDT]) VALUES(2, 1, 'V', '/Upload/', 'B2ADDC26-CBA3-4F78-AA45-57832EB2AF12.mp4', 'Interstellar Movie - Official Trailer 3.mp4', Getdate(), null,  null)
GO
SET IDENTITY_INSERT [dbo].[Content] OFF

-- chat --

SET IDENTITY_INSERT [dbo].[Chat] ON 
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (1, 1, 1, NULL, N'Hi', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (2, 1, 1, NULL, N'Hi there', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (3, 1, 1, NULL, N'Everything good ?', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (4, 7, 2, NULL, N'Hi', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (5, 6, 1, NULL, N'Hey', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (6, 6, 2, NULL, N'let''s talk', GETDATE(),  NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (7, 6, 1, NULL, N'what you would like to talk ?', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (8, 6, 2, NULL, N'how do you find this chat ?', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (9, 6, 2, NULL, N'good or not ?', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (10, 6, 1, NULL, N'I think so so ba', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (11, 6, 1, NULL, N'what you think ?', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (12, 6, 2, NULL, N'me a ?', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (13, 6, 2, NULL, N'no idea ...', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (14, 6, 1, NULL, N'alright ... bye', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (15, 1, NULL, 1,  N'Hi students, how was morning class ?', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (16, 1, 1, NULL,  N'Hi sir, it was good ! ', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (17, 1, NULL, 1,  N'That is great ! Feel free to contact me if you have any doubts ', GETDATE() , NULL, NULL)
GO
INSERT [dbo].[Chat] ([Sid], [CourseSid], [StudentSid], [InstructorSid], [Message], [CreateDT], [UpdateDT], [DeleteDT]) VALUES (18, 1, 2, NULL,  N'Thank you sir, you have a nice day !', GETDATE() , NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Chat] OFF
GO

-- =======================================================
-- Create Stored Procedure Template for Azure SQL Database
-- =======================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE GetCourseRegistrations

AS
BEGIN

   select Sid, courseName, (select count(1) from Student_Course_Map M where M.CourseSid = C.Sid) as Registration from course C

END
GO

CREATE VIEW [dbo].[GetCourseRegistrationsView] as

select convert(varchar, dateadd(hour,8, getdate())) as [Time],
N'<table style="width:500px;text-align:left">' +
N'<tr><th>Course Name</th>
<th>Registration</th>
</tr>' +
CAST ( (
select td= courseName,'', td =  (select count(1)  from Student_Course_Map M where M.CourseSid = C.Sid) from course C
FOR XML PATH('tr'), TYPE
) AS NVARCHAR(MAX) ) +
N'</table>' as [Registration]
   
GO



--------------------------Others---------------------- 
-- DB version --

delete from DBVersion
go

insert into DBVersion ( DBVersion, CreateDT) values ('Schema revision 1.4.1', '2016-04-22')
go
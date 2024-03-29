USE [master]
GO
CREATE DATABASE [CourseAppDB]
go
USE [CourseAppDB]
GO

CREATE TABLE [dbo].[Courses](
	[CourseId] uniqueidentifier  NOT NULL,
	[Title] [nvarchar](100) NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK__Courses__PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
))

CREATE TABLE [dbo].[StudentCourses](
	[StudentId] uniqueidentifier  NOT NULL,
	[CourseId] uniqueidentifier  NOT NULL,
 CONSTRAINT [PK__StudentCourse__PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[CourseId] ASC
))

CREATE TABLE [dbo].[Students](
	[StudentId] uniqueidentifier  NOT NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK__Students__PrimaryKey] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
))

CREATE NONCLUSTERED INDEX [Index_Courses_CategoryId] ON [dbo].[Courses]
(
	[CategoryId] ASC
)

ALTER TABLE [dbo].[StudentCourses] 
WITH CHECK ADD  CONSTRAINT [FK__StudentCourse_Course] 
FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[StudentCourses] 
CHECK CONSTRAINT [FK__StudentCourse_Course]
go
ALTER TABLE [dbo].[StudentCourses]  
WITH CHECK ADD  CONSTRAINT [FK__StudentCourse_Student]
FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[StudentCourses] 
CHECK CONSTRAINT [FK__StudentCourse_Student]
GO


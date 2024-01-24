# project for a Rest API code challenge
I did this project in 3 days for a Backend Coding Challenge. This the details:
Create a REST API in C#/.Net 8 that provides the following functionality: 
- CRUD operations for students 
- CRUD operations for courses: 
-- a course needs to be of a certain category. 
-- valid categories can be retrieved via an already existing 3rd party category REST API 
located at: https://6523c967ea560a22a4e8d725.mockapi.io/CourseCategories 
--consider that additional categories might be added by the 3rd party at any time 
- allows to manage the assignment of students to their courses 
-- a student can be assigned to multiple courses 
-- courses can be attended by multiple students 
## Additional constraints: 
access to the API shall be secured (authorized) 
the data store of the API shall be a SQL Server database 
don’t use Entity Framework or another ORM, we also want to see your SQL skills 
## Tasks  
create the database schema 
design and implement the REST API 
These are the basic requirements, it is up to you to go into more details in areas that you deem most 
important when developing such a piece of software. 

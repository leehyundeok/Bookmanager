/****** SSMS의 SelectTopNRows 명령 스크립트 ******/
SELECT TOP 1000 [Isbn]
      ,[Name]
      ,[Publisher]
      ,[Page]
      ,[UserId]
      ,[UserName]
      ,[isBorrowed]
      ,[BorrowedAt]
  FROM [MYDB1].[dbo].[BookTable]
  
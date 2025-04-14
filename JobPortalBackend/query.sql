-- List all tables
SELECT * FROM INFORMATION_SCHEMA.TABLES;

-- View Users table data
SELECT * FROM Users;
EXEC sp_helplogins;
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "JobPortalDb"

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'TiendaExamen')
BEGIN
    DROP DATABASE TiendaExamen;
END
GO

CREATE DATABASE TiendaExamen;
GO

USE TiendaExamen;
GO

SELECT name, database_id, create_date 
FROM sys.databases 
WHERE name = 'TiendaExamen';
GO

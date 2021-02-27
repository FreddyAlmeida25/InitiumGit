CREATE TABLE Clientes
(
	id int identity(1,1),
	cedula varchar(10) NOT NULL,
	nombre nvarchar(50) NOT NULL,
	fechainicio DateTime NOT NULL,
	fechafin DateTime NOT NULL,
	idcola int DEFAULT(0) NOT NULL,
	estado bit DEFAULT(1) NOT NULL
)

GO

CREATE PROCEDURE ConsultaClientes
(
	@fecha DateTime
)
AS
SELECT * FROM Clientes
WHERE fechafin > @fecha
ORDER BY fechafin asc

GO

CREATE PROCEDURE IngresaClientes
(
	@cedula varchar(10),
	@nombre nvarchar(50),
	@fechainicio DateTime,
	@fechafin DateTime,
	@idcola int
)
AS
BEGIN
	INSERT INTO Clientes (cedula,nombre,fechainicio,fechafin,idcola)
	VALUES(@cedula,@nombre,@fechainicio,@fechafin,@idcola)
END

GO
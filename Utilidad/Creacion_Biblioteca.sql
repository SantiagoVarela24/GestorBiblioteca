-- =========================================================
-- SCRIPT: GESTOR DE BIBLIOTECA
-- Ejecutar sobre la base de datos DBCRUDCORE que ya tienes.
-- (No borra la tabla CONTACTO anterior, solo agrega lo nuevo)
-- =========================================================

USE DBCRUDCORE
GO

-- =========================================================
-- TABLA DE USUARIOS (para el login)
-- =========================================================
CREATE TABLE USUARIO(
    IdUsuario int identity primary key,
    NombreUsuario varchar(50) NOT NULL UNIQUE,
    ClaveHash varchar(200) NOT NULL,
    NombreCompleto varchar(100)
)
GO

-- Usuario de prueba -> usuario: admin / clave: admin123
-- (la clave se guarda con hash SHA256, nunca en texto plano)
INSERT INTO USUARIO(NombreUsuario, ClaveHash, NombreCompleto)
VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Administrador')
GO

CREATE PROCEDURE sp_ValidarLogin(
    @NombreUsuario varchar(50),
    @ClaveHash varchar(200)
)
AS
BEGIN
    SELECT * FROM USUARIO
    WHERE NombreUsuario = @NombreUsuario AND ClaveHash = @ClaveHash
END
GO

-- =========================================================
-- TABLA DE PRESTAMOS (persona + libro + fecha)
-- =========================================================
CREATE TABLE PRESTAMO(
    IdPrestamo int identity primary key,
    NombrePersona varchar(100) NOT NULL,
    Telefono varchar(50),
    TituloLibro varchar(150) NOT NULL,
    Autor varchar(100),
    FechaPrestamo date NOT NULL,
    FechaDevolucion date NULL,
    Devuelto bit NOT NULL DEFAULT(0)
)
GO

CREATE PROCEDURE sp_ListarPrestamo
AS
BEGIN
    SELECT * FROM PRESTAMO ORDER BY IdPrestamo DESC
END
GO

CREATE PROCEDURE sp_ObtenerPrestamo(
    @IdPrestamo int
)
AS
BEGIN
    SELECT * FROM PRESTAMO WHERE IdPrestamo = @IdPrestamo
END
GO

CREATE PROCEDURE sp_GuardarPrestamo(
    @NombrePersona varchar(100),
    @Telefono varchar(50),
    @TituloLibro varchar(150),
    @Autor varchar(100),
    @FechaPrestamo date,
    @FechaDevolucion date
)
AS
BEGIN
    INSERT INTO PRESTAMO(NombrePersona, Telefono, TituloLibro, Autor, FechaPrestamo, FechaDevolucion, Devuelto)
    VALUES (@NombrePersona, @Telefono, @TituloLibro, @Autor, @FechaPrestamo, @FechaDevolucion, 0)
END
GO

CREATE PROCEDURE sp_EditarPrestamo(
    @IdPrestamo int,
    @NombrePersona varchar(100),
    @Telefono varchar(50),
    @TituloLibro varchar(150),
    @Autor varchar(100),
    @FechaPrestamo date,
    @FechaDevolucion date
)
AS
BEGIN
    UPDATE PRESTAMO SET
        NombrePersona = @NombrePersona,
        Telefono = @Telefono,
        TituloLibro = @TituloLibro,
        Autor = @Autor,
        FechaPrestamo = @FechaPrestamo,
        FechaDevolucion = @FechaDevolucion
    WHERE IdPrestamo = @IdPrestamo
END
GO

CREATE PROCEDURE sp_EliminarPrestamo(
    @IdPrestamo int
)
AS
BEGIN
    DELETE FROM PRESTAMO WHERE IdPrestamo = @IdPrestamo
END
GO

CREATE PROCEDURE sp_MarcarDevueltoPrestamo(
    @IdPrestamo int
)
AS
BEGIN
    UPDATE PRESTAMO SET Devuelto = 1 WHERE IdPrestamo = @IdPrestamo
END
GO

-- select * from USUARIO
-- select * from PRESTAMO

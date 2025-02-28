-----BD MICROSERVICIO PERSONAS------------------------------------------
------------------------------------------------------------------------

-- Crear la tabla TipoPersona
CREATE TABLE dbo.TipoPersona (
    Id INT PRIMARY KEY NOT NULL,
    Descriptor NVARCHAR(20) NOT NULL
);

-- Crear la tabla Persona
CREATE TABLE dbo.Persona (
    Id INT PRIMARY KEY NOT NULL,
    Nombre NVARCHAR(MAX) NULL,
    Apellido NVARCHAR(MAX) NULL,
    IdTipoPersona INT NOT NULL,
    Identificacion NVARCHAR(20) NOT NULL,
    Fecha_Nacimiento DATE NOT NULL,
    Telefono NVARCHAR(MAX) NULL,
    Email NVARCHAR(MAX) NULL,
    Especialidad NVARCHAR(MAX) NULL,
    Genero NVARCHAR(1) NULL,
    Activo BIT NOT NULL,
    CONSTRAINT FK_Persona_TipoPersona FOREIGN KEY (IdTipoPersona) REFERENCES dbo.TipoPersona(Id)
);


-----BD MICROSERVICIO CITAS---------------------------------------------
------------------------------------------------------------------------
-- Crear la tabla Cita
CREATE TABLE dbo.Cita (
    Id INT PRIMARY KEY NOT NULL,
    IdPaciente INT NOT NULL,
    Paciente NVARCHAR(MAX) NULL,
    IdMedico INT NOT NULL,
    Medico NVARCHAR(MAX) NULL,
    Fecha_Hora DATETIME NOT NULL,
    Motivo NVARCHAR(MAX) NULL,
    Lugar NVARCHAR(MAX) NULL,
    Estado NVARCHAR(MAX) NULL
);

-----BD MICROSERVICIO RECETAS-------------------------------------------
------------------------------------------------------------------------
-- Crear la tabla Receta
CREATE TABLE dbo.Receta (
    Id INT PRIMARY KEY NOT NULL,
    IdCita INT NOT NULL,
    IdPaciente INT NOT NULL,
    Paciente NVARCHAR(MAX) NULL,
    IdMedico INT NOT NULL,
    Medico NVARCHAR(MAX) NULL,
    Fecha_Emision DATETIME NOT NULL,
    Descriptor NVARCHAR(MAX) NULL
);

-- Crear la tabla Medicamento
CREATE TABLE dbo.Medicamento (
    Id INT PRIMARY KEY NOT NULL,
    IdReceta INT NOT NULL,
    NombreMedicamento NVARCHAR(MAX) NULL,
    Dosis NVARCHAR(MAX) NULL,
    Frecuencia NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Medicamento_Receta FOREIGN KEY (IdReceta) REFERENCES dbo.Receta(Id)
);

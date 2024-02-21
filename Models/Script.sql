create database PruebaTec02GSMCDB
go


use PruebaTec02GSMCDB
go

create table Directores(
Id int primary key identity(1,1),
Nombre nvarchar(100) not null
);

create table Peliculas(
PeliculaId int primary key identity(1,1),
Nombre nvarchar(100) not null,

Descripcion nvarchar(max),
Imagen image,
Id int foreign key references Directores(Id) not null
);
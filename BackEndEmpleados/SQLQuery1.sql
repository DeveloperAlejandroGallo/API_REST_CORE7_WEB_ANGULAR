create table Departamento 
(
	IdDepartamento int primary key identity,
	Nombre varchar(50),
	FechaCreacion datetime default getdate()
)


create table Empleado
(
	IdEmpleado int primary key identity,
	NombreCompleto varchar(50),
	IdDepartamento int references Departamento(IdDepartamento),
	Sueldo int,
	FechaContrato datetime,
	FechaCreacion datetime default getdate()
)

insert into Departamento(Nombre) values 
('Administracion'),
('Ventas'),
('Marketing'),
('Comercio')

Insert into Empleado (NombreCompleto, IdDepartamento, Sueldo,FechaContrato) values
('Ale Gallo', 1, 15000, getdate())

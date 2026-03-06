-- 1 nos conectamos a la bd de master para crear la nueva base de datos y el login a nivel servidor
USE master;
GO

-- 2 creamos la base de datos
CREATE DATABASE PruebaVoultech;
GO

-- 3 creamos el login a nivel servidor
CREATE LOGIN UsuarioPrueba 
WITH PASSWORD = 'Password123!';
GO

-- 4 nos conectamos a la nueva base de datos para crear el usuario y asignar permisos
USE PruebaVoultech;
GO

-- 5 creamos el usuario a nivel de base de datos
CREATE USER UsuarioPrueba 
FOR LOGIN UsuarioPrueba;
GO

-- 5 asignamos el rol de db_owner al usuario para que tenga permisos completos sobre la base de datos
ALTER ROLE db_owner 
ADD MEMBER UsuarioPrueba;
GO
-- para mayor seguridad, es recomendable quitar al usuario del rol de db_owner y asignarle solo los permisos necesarios para sus tareas específicas.
-- asi como tambien que no pueda ver el resto de bd que tengamos en el servidor y darle permisos especificos, como esto es una prueba no lo realizaremos, pero es importante tenerlo en cuenta para entornos de producción.
USE PruebaVoultech;
GO


create table OrdenCompra(
 Id INT IDENTITY(1,1) PRIMARY KEY,
 Cliente varchar(50) NOT NULL,
 FechaCreacion DATETIME DEFAULT GETDATE(),
 Total DECIMAL(19, 2) NOT NULL
)
Create index idx_cliente ON OrdenCompra(Cliente)

Create table Producto(
 Id INT IDENTITY(1,1) PRIMARY KEY,
 Nombre varchar(50) NOT NULL,
 Precio DECIMAL(19, 2) NOT NULL
)

Create Unique Index idx_producto_nombre on Producto(Nombre)

Create index idx_nombre ON Producto(Nombre)

Create table OrdenProducto(
 id INT IDENTITY(1,1) PRIMARY KEY,
 OrdenCompraId INT NOT NULL,
 ProductoId INT NOT NULL,
 FOREIGN KEY (OrdenCompraId) REFERENCES OrdenCompra(id) on Delete Cascade,
 FOREIGN KEY (ProductoId) REFERENCES Producto(id)
)

Create index idx_orden_id ON OrdenProducto(OrdenCompraId)
create index idx_producto_id ON OrdenProducto(ProductoId)




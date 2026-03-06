Esta es una prueba técnica.

Este proyecto está creado con .NET 10, Visual Studio 2026 y SDK 10.0.3; es necesario tener esa versión de SDK instalada, además de SQL Server 2025.

Tiene instaladas distintas dependencias.

<img width="360" height="119" alt="image" src="https://github.com/user-attachments/assets/00c4fa43-1608-4f86-b472-1d488c221d4d" />

para poder correr el servicio es necesario antes usar el comando:
- dotnet restore

Además de esto, se debe tener habilitada la autenticación del servidor con Windows y SQL Server.

<img width="735" height="692" alt="image" src="https://github.com/user-attachments/assets/72b7ea85-085e-4d1d-9a11-53950d028f7c" />


Luego, ocupar los archivos de la carpeta BaseDatos, primero el archivo 1_Creacion_DB_y_usuario.sql y luego 2_TablasSQL.sql, estos para crear BD, usuario y tablas.

Se debe configurar el archivo appsettings.json y cambiar el servidor.

<img width="1213" height="209" alt="image" src="https://github.com/user-attachments/assets/0e0269b0-086a-40d6-8c3b-050d3c80be38" />

Para ejecutar la API, se debe usar el comando dotnet run o abrir el proyecto desde Visual Studio.

Esta aplicación cuenta con la implementación de Swagger para el versionamiento de la aplicación.

<img width="1576" height="720" alt="image" src="https://github.com/user-attachments/assets/aeb2e9eb-a57d-4040-a3eb-6a1c3bda0802" />

Las pruebas se realizaron por Postman; dejo imágenes de las pruebas realizadas.

Orden Compra:

GET:

<img width="1053" height="813" alt="image" src="https://github.com/user-attachments/assets/dc2b0d21-afcf-4674-9bc0-37e0b9108dbb" />

GET ID:

<img width="1053" height="823" alt="image" src="https://github.com/user-attachments/assets/eafb0ad7-6ecf-4ee0-8ec3-6d3871c19c38" />


POST:

<img width="1050" height="819" alt="image" src="https://github.com/user-attachments/assets/0521cfca-16ec-4089-929a-1f7bab4e663a" />

PUT:

<img width="1055" height="826" alt="image" src="https://github.com/user-attachments/assets/47ec84a4-e9b4-43bc-adf8-387655673cc4" />

DELETE:

<img width="1047" height="830" alt="image" src="https://github.com/user-attachments/assets/1f72a033-d231-4349-988a-8b09724eb28d" />

<img width="1058" height="706" alt="image" src="https://github.com/user-attachments/assets/dc6de138-69ae-4b8a-a211-52d965ae5fc7" />


Producto:

GET:

<img width="1053" height="754" alt="image" src="https://github.com/user-attachments/assets/a6a73766-61ce-48f8-b3e8-dad3c48d04f3" />

GET ID:

<img width="1072" height="697" alt="image" src="https://github.com/user-attachments/assets/7fd8fa76-7ea5-4c67-9cdb-f9a3abf2b961" />

POST:

<img width="1053" height="681" alt="image" src="https://github.com/user-attachments/assets/121bfcb2-f515-4150-b373-0c05b762a805" />

SELECT de BD:

<img width="1202" height="859" alt="image" src="https://github.com/user-attachments/assets/72900a4d-975b-4b57-91fc-8d72fa4b7c3f" />






















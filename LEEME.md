# Gestor de Biblioteca (basado en tu proyecto CRUDCORE)

Le agregué a tu proyecto:

1. **Login** con usuario y clave (la clave se guarda con hash, nunca en texto plano).
2. Se cambió el CRUD de "Contacto" por un **gestor de préstamos de biblioteca**: cada
   registro tiene una persona, un libro (título + autor), fecha de préstamo, fecha de
   devolución y un estado (Pendiente / Devuelto).
3. Una pantalla de inicio con un pequeño resumen (total de préstamos, pendientes, devueltos).
4. Todas las pantallas están protegidas: si no inicias sesión, te manda al login.

## Pasos para dejarlo funcionando

### 1. Base de datos
Abre SQL Server Management Studio, conéctate a tu base `DBCRUDCORE` (la misma que ya
tenías, la del CRUD de contactos) y ejecuta el script:

```
Utilidad/Creacion_Biblioteca.sql
```

Esto crea 2 tablas nuevas (`USUARIO` y `PRESTAMO`) con sus procedimientos almacenados.
No borra tu tabla `CONTACTO` anterior, así que no pierdes nada.

El script ya deja creado un usuario de prueba:
- **Usuario:** admin
- **Clave:** admin123

### 2. Cadena de conexión
Revisa `CRUDCORE/appsettings.json`. Si tu proyecto anterior ya conectaba bien a
`DBCRUDCORE`, no necesitas tocar nada — dejé la misma cadena que ya tenías.

### 3. Abrir y ejecutar
Abre `CRUDCORE.sln` con Visual Studio, espera que restaure los paquetes NuGet y
presiona **F5** (o el botón de Ejecutar). Se abrirá el navegador en la pantalla de login.

Entra con admin / admin123 y ya puedes registrar préstamos, editarlos, marcarlos como
devueltos o eliminarlos.

## ¿Cómo agregar más usuarios?

Por simplicidad no hice una pantalla de "Registrar usuario" (para no complicar el
proyecto). Si quieres agregar otro usuario, corre esto en SSMS, cambiando los datos:

```sql
INSERT INTO USUARIO(NombreUsuario, ClaveHash, NombreCompleto)
VALUES ('nuevo_usuario', HASHBYTES('SHA2_256', 'la_clave_que_quieras'), 'Nombre Completo')
```

⚠️ Ojo: ese `HASHBYTES` de SQL Server devuelve bytes binarios, no el mismo formato de
texto que genera el programa en C#. Si quieres hacerlo bien, dime y te agrego una
pantalla sencilla de "Crear usuario" — pero por ahora, más fácil: dime el usuario/clave
que quieras y te devuelvo el `INSERT` ya calculado con el hash correcto en texto.

## Estructura de lo que se agregó/cambió

- `Utilidad/Creacion_Biblioteca.sql` → script nuevo (tablas + procedimientos).
- `Models/PrestamoModel.cs`, `Models/LoginModel.cs` → nuevos.
- `Datos/PrestamoDatos.cs`, `Datos/UsuarioDatos.cs`, `Datos/Seguridad.cs` → nuevos.
- `Controllers/PrestamoController.cs`, `Controllers/AccountController.cs` → nuevos.
- `Controllers/HomeController.cs` → editado (dashboard + requiere login).
- `Views/Prestamo/*` → nuevas vistas (antes eran `Views/Mantenedor/*`).
- `Views/Account/Login.cshtml` → nueva.
- `Views/Shared/_Layout.cshtml` → editado (menú, usuario logueado, botón salir).
- `Program.cs` → editado (se agregó autenticación por cookies).
- Se eliminó todo lo de `Contacto` (ya no se usa).

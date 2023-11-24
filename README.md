# CodeChallenge Backend

## Requisitos

Se requiere utilizar al menos estas tecnologías, pero el desarrollador es libre de utilizar todas las que considere necesarias.

- dotnet core 6.X o superior
- EF core 6.X o superior
- JWT autentication (Bearer Authentication)

## Descrición del problema

Se quiere disponer de una api que nos permita:

- logarse en la applicación (obtención del JWT para Bearer Authentication)
- consultar los dispositivos (con las conexiones que tengan)
- consultar los switches (con los devices conectados que tengan)
- connectar dispositivo a un switch (solo usuarios autenticados)
- desconectar dispositivo de un switch (sólo usuarios autenticados)

Los datos se han de persistir en una base de datos local, a elección del desarrollador, pero que al arrancar la aplicación, los datos anteriores se han de mantener.
Se han de respetar la posibilidad de migraciones de datos.
Todas las comunicaciones se enviaran y recibirán usando json.
Sería deseable tener una buena documentación de los entrypoints, sus usos, ejemplos...

### Modelos

Estos son los modelos mínimos que hay en la BBDD, pero se podrán crear todos aquellos que se consideren necesarios.

Device:

- Id
- Hostname
- PortList

Switch:

- Id
- Hostname
- PortList

### Entrypoints

Se implementarán, al menos, los siguientes entrypoints:

- POST login
- GET devices
- GET switches
- POST switches/<switch_id>/connect-device/<device_id>, con un body indicando los puertos que les conectarán.

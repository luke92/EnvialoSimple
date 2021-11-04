# EnvialoSimple
API hecha en .NET CORE para comunicarnos con la API de EnvialoSimple

# Documentación de EnvialoSimple
https://envialosimple.com/es-ar/api

# Crear cuenta gratis (Podes usar hasta 3 campos parametrizables)
https://envialosimple.com/

# Configurar API KEY
- Entrar al dahsboard de la V4 https://v4.envialosimple.com/#!/es/dashboard
- Luego cambiar v4 por v3 https://v3.envialosimple.com/#!/es/dashboard
- Ir a cuenta - Configuracion - Clave API - Nueva Clave API
- https://v3.envialosimple.com/config/#/api-key/
- Guardamos la clave API y luego click en Editar en la clave guardada
- Copiamos la APIKEY y la usamos en appsettings.json

# Uso de datos parametrizables en una campaña
- En un HTML se deben poner los datos de la siguiente manera %Member:CustomField1% %Member:CustomField2% segun el orden que se agreguen los valores en "CustomFields": [] de cada Miembro (Member) de "Members": [] en "MailListWithMembers": { }

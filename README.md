# EnvialoSimple API
API hecha en .NET CORE para comunicarnos con la API de EnvialoSimple

# Documentación de EnvialoSimple
- https://envialosimple.com/es-ar/api
- Crear cuenta gratis (Podes usar hasta 3 campos parametrizables) - https://envialosimple.com/

# Documentación de EnvialoSimple API
- {baseUrl}/swagger

# Requisitos
- NET CORE 2.1

# Configurar appsettings.json

## Configurar API KEY
- Entrar al dahsboard de la V4 https://v4.envialosimple.com/#!/es/dashboard
- Luego cambiar v4 por v3 https://v3.envialosimple.com/#!/es/dashboard
- Ir a cuenta - Configuracion - Clave API - Nueva Clave API
- https://v3.envialosimple.com/config/#/api-key/
- Guardamos la clave API y luego click en Editar en la clave guardada
- Copiamos la APIKEY y la usamos en appsettings.json

## Configurar Email de administrador
- Para pruebas configurar `"FromEmailDefault"` con el correo que se creó la cuenta
- Para ambiente productivo se debe verificar las casillas que se coloquen ahi mismo en "Dominios Verificados" de Envialo Simple https://v3.envialosimple.com/config/#/domain-verification/

# TEST API

## Uso de datos parametrizables en una campaña
- En el `SenderRequestModel.Content.HTML` se deben poner los datos de la siguiente manera `%Member:CustomField1%` `%Member:CustomField2%` segun el orden que se agreguen los valores en `"CustomFields": []` de cada Miembro (Member) de `"Members": []` en `"MailListWithMembers": { }`
- Tener en cuenta que en el `SenderRequestModel.Content.HTML` siempre debe estar dentro el link de Desubscripción (Aunque este oculto)
  - `‹span›Para desuscribirse de nuestra lista haga‹/span› ‹a href="%UnSubscribe%" target="_blank"›Click Aquí‹/a›`

## Programar envio o mandar en el momento
- Si estas en UTC -3 (Argentina Time) si o si es necesario enviar en `SenderRequestModel.Campaign.SendDate` una fecha y hora que sea 3 horas mas de la actual
- `SenderRequestModel.Campaign.ScheduleCampaign` enviarlo en True
- Si lo vas a enviar en el momento colocar en `SenderRequestModel.Campaign..DontSendNow` como `False`, sino en `True` (para que se envie a la hora programada)

## Request de ejemplo
- Podes usar el siguiente request de ejemplo cambiando los correos y campos parametrizables de los Members
- https://github.com/luke92/EnvialoSimple/blob/master/EnvialoSimpleAPIRequest.json

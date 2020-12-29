# Clip Money


## Desarrollo

### General

Haciendo un get a `./api/Account/` se obtienen las cuentas del usuario logeado.
Se devuelve un JSON con un array de objetos de cuentas.

Ejemplo:

```json
{
    "Exito": 1,
    "Mensaje": "Exito - cuentas del usuario obtenidas exitosamente",
    "Data": [
        {
            "IdCuenta": 1,
            "TipoCuenta": {
                "IdTipoCuenta": 1,
                "TipoCuenta": "CORRIENTE"
            },
            "Divisa": {
                "IdDivisa": 1,
                "Divisa": "PESO ARGENTINO",
                "Fee": 1.0,
                "SalePrice": 70.0000,
                "PurchasePrice": 60.0000
            },
            "Usuario": null,
            "CVU": "4879413218432184163518",
            "Saldo": 10000.0000,
            "Alias": "PRUEBA1.PRUEBA2",
            "OpeningDate": "2020-12-19T00:00:00"
        },
        {
            "IdCuenta": 2,
            "TipoCuenta": {
                "IdTipoCuenta": 1,
                "TipoCuenta": "CORRIENTE"
            },
            "Divisa": {
                "IdDivisa": 1,
                "Divisa": "PESO ARGENTINO",
                "Fee": 1.0,
                "SalePrice": 70.0000,
                "PurchasePrice": 60.0000
            },
            "Usuario": null,
            "CVU": "627934562342331       ",
            "Saldo": 200000.0000,
            "Alias": "PRUEBA3.PRUEBA4",
            "OpeningDate": "2020-12-19T00:00:00"
        }
    ]
}
```

Haciendo un get a `./api/Transactions/` se obtienen las 10 ultimas transacciones del usuario, actualmente solo muestra depositos y transferencias

Ejemplo:
```json
{
    "Exito": 1,
    "Mensaje": "Exito - transactiones obtenidas correctamente",
    "Data": [
        {
            "TransactionType": "DEPOSITO",
            "DateTime": "2020-12-27T20:31:00",
            "Account": "8831892375712317645923",
            "Amount": 35000.0000,
            "Concept": "",
            "VoucherNumber": 2
        },
        {
            "TransactionType": "TRANSFERENCIA",
            "DateTime": "2020-12-27T20:22:00",
            "Account": "4879413218432184163518",
            "Amount": 14657.9500,
            "Concept": "PAGO SERVICIO",
            "VoucherNumber": 4
        },
        {
            "TransactionType": "TRANSFERENCIA",
            "DateTime": "2020-12-27T20:21:00",
            "Account": "8831892375712317645923",
            "Amount": 10000.0000,
            "Concept": "PRESTAMO",
            "VoucherNumber": 3
        },
        {
            "TransactionType": "TRANSFERENCIA",
            "DateTime": "2020-12-27T20:19:00",
            "Account": "8831892375712317645923",
            "Amount": 1500.0000,
            "Concept": "PAGO CUOTA",
            "VoucherNumber": 2
        },
        {
            "TransactionType": "DEPOSITO",
            "DateTime": "2020-12-27T16:52:00",
            "Account": "8831892375712317645923",
            "Amount": 15000.0000,
            "Concept": "",
            "VoucherNumber": 1
        },
        {
            "TransactionType": "TRANSFERENCIA",
            "DateTime": "2020-12-20T23:20:00",
            "Account": "4879413218432184163518",
            "Amount": 100.0000,
            "Concept": "Prueba",
            "VoucherNumber": 1
        }
    ]
}
```

#### Usuario


Haciendo un get a `./api/User` se obtienen los datos del usuario logeado

Ejemplo de respuesta:
```json
{
    "Exito": 1,
    "Mensaje": "Exito - se obtuvo los datos del usuario",
    "Data": {
        "Cuil": "81711677771",
        "Nombre": "david",
        "Apellido": "Alvarez",
        "Email": "pablito@gmail.com",
        "Telefono": "351321421  "
    }
}
```

Haciendo un put a `./api/User` se actualizan los datos del usuario enviando un json con el siguiente formato, ademas como respuesta se obtienen los nuevos datos del usuario.
```json 
{
    "PhoneNumber": "35132142123",
    "Email": "contacto@gmail.com"
}
```


### Deposito

#### Tarjeta

Haciendo un get a `./api/Deposit/CreditCard?number=NumeroDeTarjeta` se obtiene un json diciendo si la tarjeta de credito es valida y de que entidad financiera es.

No es necesario pasar el numero completo de la tarjeta para obtener la entidad financiera

Ejemplo llamando a
`http://localhost:49220/api/Deposit/CreditCard?number=4111111111111111`
```json
{
    "Exito": 1,
    "Mensaje": "Exito - validacion de la tarjeta realizada",
    "Data": {
        "IsValid": true,
        "Brand": "Visa"
    }
}
```

Para hacer un deposito con tarjeta hacer un post a `./api/Deposit/CreditCard` con un JSON con el formato:

```json
{
    "FullName": "Cristian Almada",
    "ExpirationDate": "06/22",
    "CreditCardNumber": "4111111111111111",
    "SecurityNumber": 422,
    "DocumentNumber": 11222333,
    "Amount": 15000,
    "DebitAccountId": 2
}
```

#### Transferencias

Para obtener informacion basica de cuenta de destino, hacer get a `./api/Transfer?cvu=numeroDeCvu`
Ejemplo: ./api/Transfer?cvu=4879413218432184163518
```json
{
    "Exito": 1,
    "Mensaje": "Exito - cuenta obtenida",
    "Data": {
        "CVU": "4879413218432184163518",
        "IdCuenta": 1,
        "Propietario": {
            "Nombre": "david",
            "Apellido": "Alvarez",
            "CUIL": "81711677771"
        }
    }
}
```

Para realizar una transferencia hacer un post a `./api/Transfer` con un json con el formato:
```json
{
    "DebitAccountId": 1,
    "Amount": 100,
    "CreditAccountId": 2,
    "Concept": "Prueba",
    "DestinationReference": "Prueba",
    "EmailNotificacion": "contacto@gmail.com"
}
```
# Clip Money


## Desarrollo

#### Transferencias

Haciendo un get a `./api/Transfer` se obtienen las cuentas del usuario logeado.
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
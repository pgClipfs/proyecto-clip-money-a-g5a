# Clip Money

- Support Standard Markdown / CommonMark and GFM(GitHub Flavored Markdown);
- Full-featured: Real-time Preview, Image (cross-domain) upload, Preformatted text/Code blocks/Tables insert, Code fold, Search replace, Read only, Themes, Multi-languages, L18n, HTML entities, Code syntax highlighting...;
- Markdown Extras : Support ToC (Table of Contents), Emoji, Task lists, @Links...;
- Compatible with all major browsers (IE8+), compatible Zepto.js and iPad;
- Support identification, interpretation, fliter of the HTML tags;
- Support TeX (LaTeX expressions, Based on KaTeX), Flowchart and Sequence Diagram of Markdown extended syntax;
- Support AMD/CMD (Require.js & Sea.js) Module Loader, and Custom/define editor plugins;

### Desarrollo

Haciendo un get a ./api/Transfer se obtienen las cuentas del usuario logeado.
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
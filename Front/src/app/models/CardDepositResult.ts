// tipo con los datos necesarios para el Dialog de deposito con Tarjetas
export interface CardDepositResult {
    Error: string;
    CVU: string; 
    Currency: string;
    Amount: string;
    CardNumber: number;
    CardBrand: string;
}

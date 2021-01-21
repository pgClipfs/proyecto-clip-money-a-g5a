import { TypeAccount } from "./typeAccount";
import { Currency } from "./currency";

export interface UserAccount {
    AccountId: number;
    TypeAccount: TypeAccount;
    Currency: Currency;
    User: null;
    CVU: string;
    Balance: number;
    Alias: string;
    OpeningDate: string;
}
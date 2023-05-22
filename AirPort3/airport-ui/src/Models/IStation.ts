import { IFlight } from "./IFlight";

export interface IStation {
    stationId: number;
    flight: IFlight;
}

import { flightHistory } from "./FlightHistory";
import { IFlight } from "./IFlight";
import { IStation } from "./IStation";

export interface IFlightContext {
    // simulator service
    startSimulator: () => Promise<any>;
    stopSimulator: () => Promise<any>;
    resetSimulator: () => Promise<any>;
    connected: boolean;

    // main service
    getFlightHistory: (count: number) => Promise<any>;
    flightHistory: flightHistory[];

    // useSignalR
    stationDataQ: IStation[];
    flightList: IFlight[];

    // messages
    messages: string[];
    setMessages: React.Dispatch<React.SetStateAction<string[]>>;
}

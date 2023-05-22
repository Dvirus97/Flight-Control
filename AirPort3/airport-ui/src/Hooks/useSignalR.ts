//! connection.on("js function that c# call", () => {});
//! connection.onclose(()=>{};)
//! await connection.stop();
//! await connection.invoke("c# function", {parameter});

import {
    HubConnection,
    HubConnectionBuilder,
    LogLevel,
} from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";
import { IStation } from "../Models/IStation";
import { IFlight } from "../Models/IFlight";

export function useSignalR() {
    const connection = useRef<HubConnection>(0 as any);

    const [stationDataQ, setStationDataQ] = useState<IStation[]>([]);
    const [flightList, setFlightList] = useState<IFlight[]>([]);

    try {
        connection.current = new HubConnectionBuilder()
            // .withUrl("http://localhost:5218/flight")
            // .withUrl("http://localhost:5001/flight")
            .withUrl("http://localhost:5000/flight")
            .configureLogging(LogLevel.Information)
            .build();

        connection.current.on("GetStation", (text: string) => {
            const data: {
                stationId: number;
                flightId: number;
                flightState: string;
            } = JSON.parse(text);

            const res: IStation = {
                stationId: data.stationId,
                flight: {
                    flightId: data.flightId,
                    state: data.flightState,
                },
            };
            setStationDataQ((prev) => [...prev, res]);
        });
        // connection.current.on("GetStations", (text: string) => {
        //     const stations = JSON.parse(text);
        //     console.log(stations);
        //     setStationList(stations);
        // });
        connection.current.on("GetFlights", (text: string) => {
            const flights: IFlight[] = JSON.parse(text);
            setFlightList(flights);
        });

        useEffect(() => {
            connection.current.stop().then(() => {
                connection.current
                    .start()
                    .then(() => console.log("signalR : connection started"))
                    .catch((e) => console.log(e));
            });

            return () => {
                connection.current
                    .stop()
                    .then(() => console.log("signalR : connection stopped"))
                    .catch((e) => console.log(e));
            };
        }, []);
    } catch (e) {
        console.log(e);
    }

    return {
        stationDataQ,
        flightList,
    };
}

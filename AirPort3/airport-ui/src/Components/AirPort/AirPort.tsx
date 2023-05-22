import { useContext, useEffect, useState } from "react";
import { IStation } from "../../Models/IStation";
import "./AirPort.css";
import { Station } from "../Station/Station";
import { flightContext } from "../../App";

export function AirPort() {
    const [disableStart, setDisableStart] = useState(false);
    const [disableStop, setDisableStop] = useState(false);

    const {
        stationDataQ,
        startSimulator,
        stopSimulator,
        resetSimulator,
        setMessages,
    } = useContext(flightContext);

    const [stationList, setStationList] = useState<IStation[]>([
        { stationId: 1, flight: { flightId: 0 } },
        { stationId: 2, flight: { flightId: 0 } },
        { stationId: 3, flight: { flightId: 0 } },
        { stationId: 4, flight: { flightId: 0 } },
        { stationId: 5, flight: { flightId: 0 } },
        { stationId: 6, flight: { flightId: 0 } },
        { stationId: 7, flight: { flightId: 0 } },
        { stationId: 8, flight: { flightId: 0 } },
        { stationId: 9, flight: { flightId: 0 } },
    ]);

    // const [s1, setS1] = useState<IStation>({
    //     flight: { flightId: 0 },
    //     stationId: 1,
    // });
    // const [s2, setS2] = useState<IStation>({
    //     flight: { flightId: 0 },
    //     stationId: 2,
    // });
    // const [s3, setS3] = useState<IStation>({
    //     flight: { flightId: 0 },
    //     stationId: 3,
    // });
    // const [s4, setS4] = useState<IStation>({
    //     flight: { flightId: 0 },
    //     stationId: 4,
    // });
    // const [s5, setS5] = useState<IStation>({
    //     flight: { flightId: 0 },
    //     stationId: 5,
    // });
    // const [s6, setS6] = useState<IStation>({
    //     flight: { flightId: 0 },
    //     stationId: 6,
    // });
    // const [s7, setS7] = useState<IStation>({
    //     flight: { flightId: 0 },
    //     stationId: 7,
    // });
    // const [s8, setS8] = useState<IStation>({
    //     flight: { flightId: 0 },
    //     stationId: 8,
    // });
    // const [s9, setS9] = useState<IStation>({
    //     flight: { flightId: 0 },
    //     stationId: 9,
    // });

    // const funcList = [
    //     (_a: any) => {},
    //     setS1,
    //     setS2,
    //     setS3,
    //     setS4,
    //     setS5,
    //     setS6,
    //     setS7,
    //     setS8,
    //     setS9,
    // ];
    // const dataList = [s1, s1, s2, s3, s4, s5, s6, s7, s8, s9];

    const setStation = (sId: number, fId: number, fState: string) => {
        if (sId < 1 || sId > 9) return;
        // dataList[sId].flight = { flightId: fId, state: fState };
        // funcList[sId]((p) => ({ ...p, ...dataList[sId] }));
        stationList[sId - 1].flight = { flightId: fId, state: fState };
        setStationList((p) => [...p]);
    };

    const dataHandle = () => {
        setTimeout(() => {
            const first = stationDataQ.shift();
            if (first !== undefined) {
                const {
                    stationId,
                    flight: { flightId, state },
                } = first;
                setStation(stationId, flightId, state!);
                if (stationDataQ.length > 0) {
                    dataHandle();
                }
            }
        }, 20);
    };

    useEffect(() => {
        dataHandle();
    }, [stationDataQ]);

    return (
        <div className="AirPortBoard">
            <div className="board">
                <img
                    className="background"
                    src="../public/back.png"
                    alt="back"
                />
                <div className="AirPort">
                    {stationList.map((x) => (
                        <Station
                            key={x.stationId}
                            flight={x.flight}
                            stationId={x.stationId}
                        />
                    ))}
                </div>
            </div>

            <button
                disabled={disableStart}
                onClick={() => {
                    startSimulator().then((x) => {
                        console.log(x);
                        setMessages((prev) => [...prev, "Starting"]);
                    });
                    setDisableStart(true);
                    setDisableStop(false);
                }}
            >
                Start
            </button>
            <button
                disabled={disableStop}
                onClick={() => {
                    stopSimulator().then(() => {
                        setMessages((prev) => [...prev, "Stopping"]);
                    });
                    setDisableStart(false);
                    setDisableStop(true);
                }}
            >
                Stop
            </button>
            <button
                onClick={() => {
                    resetSimulator().then(() => {
                        setMessages((prev) => [...prev, "Reset!"]);
                    });
                }}
            >
                Reset
            </button>
        </div>
    );
}

/* 
    <Station
        stationId={s1!.stationId}
        flight={{ ...s1.flight }}
    ></Station>
    <Station
        stationId={s2!.stationId}
        flight={{ ...s2!.flight }}
    ></Station>
    <Station
        stationId={s3!.stationId}
        flight={{ ...s3!.flight }}
    ></Station>
    <Station
        stationId={s4!.stationId}
        flight={{ ...s4!.flight }}
    ></Station>
    <Station
        stationId={s5!.stationId}
        flight={{ ...s5!.flight }}
    ></Station>
    <Station
        stationId={s6!.stationId}
        flight={{ ...s6!.flight }}
    ></Station>
    <Station
        stationId={s7!.stationId}
        flight={{ ...s7!.flight }}
    ></Station>
    <Station
        stationId={s8!.stationId}
        flight={{ ...s8!.flight }}
    ></Station>
    <Station
        stationId={s9!.stationId}
        flight={{ ...s9!.flight }}
    ></Station>
 */

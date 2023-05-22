import { useContext } from "react";
import "./FlightsBoard.css";
import { flightContext } from "../../App";

export function FlightsBoard() {
    const { flightList } = useContext(flightContext);

    return (
        <>
            <div className="FlightsBoard">
                <h1>Active Flights</h1>
                {flightList.map((x, i) => (
                    <div
                        key={i}
                        className={`flightItem ${
                            x.state == "Landing" ? "landing" : "departure"
                        }`}
                    >
                        <div>Flight : {x.flightId}</div>
                        <div>{x.state}</div>
                        <hr />
                    </div>
                ))}
            </div>
        </>
    );
}

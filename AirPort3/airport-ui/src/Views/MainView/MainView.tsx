import { FlightsBoard } from "../../Components/FlightsBoard/FlightsBoard";
import { AirPort } from "../../Components/AirPort/AirPort";
import { MessageBoard } from "../../Components/MessageBoard/MessageBoard";
import FlightHistory from "../../Components/FlightHistory/FlightHistory";

import "./MainView.css";

export function MainView() {
    return (
        <>
            <header>
                <img className="title" src="title.png" alt="title" />
            </header>
            <main>
                <FlightsBoard />

                <AirPort />

                <MessageBoard />

                <FlightHistory />
            </main>
            <footer>&copy; D & S</footer>
        </>
    );
}

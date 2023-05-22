import { IFlight } from "../../Models/IFlight";
import { Icon } from "../../Util/Icon/Icon";
import "./Flight.css";

const Flight = ({flight}: IProps) => {
    const { flightId, state } = flight;

    const stateName = state == "Landing" ? "landing" : "departure";
    const icon = state == "Landing" ? "flight_land" : "flight_takeoff";

    return (
        <>
            <div className={"Flight " + stateName}>
                <Icon i={icon} />
                <div className="flightId">{flightId}</div>
            </div>
        </>
    );
};

export default Flight;

interface IProps {
    flight: IFlight;
}

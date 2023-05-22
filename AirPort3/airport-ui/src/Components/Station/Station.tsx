import { IStation } from "../../Models/IStation";
import { Icon } from "../../Util/Icon/Icon";
import Flight from "../Flight/Flight";
import "./Station.css";

export function Station({ stationId, flight }: IStation) {
    return (
        <>
            <div className="Station">
                <div className="background">
                    <Icon i={"counter_" + stationId} />
                </div>
                {flight.flightId != 0 && <Flight {...{flight}} />}
            </div>
        </>
    );
}

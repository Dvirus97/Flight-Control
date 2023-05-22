import moment from "moment";
import { useContext, useState } from "react";
import { flightContext } from "../../App";

// interface IProps {
//     flightHistory: flightHistory[];
// }
const HistoryTable = () => {
    const { flightHistory, getFlightHistory } = useContext(flightContext);

    const [rows, setRows] = useState(10);

    const dateToString = (date: Date) => {
        return moment(date).format("DD-MM-YY HH:mm:ss");
    };

    return (
        <div>
            <button
                className="GetHistory"
                onClick={() => {
                    getFlightHistory(rows);
                }}
            >
                Get Update Flights
            </button>
            <label>
                Rows Number
                <input
                    type="number"
                    value={rows}
                    min={1}
                    max={20}
                    onChange={(e) => setRows(+e.target.value)}
                />
            </label>

            <div className="table">
                <div className="table-head">
                    <div className="row">
                        <div className="col">id</div>
                        <div className="col">flightId</div>
                        <div className="col">enter time</div>
                        <div className="col">exit time</div>
                        <div className="col">state</div>
                    </div>
                </div>
                <div className="table-body">
                    {flightHistory.map((x) => (
                        <div className="row" key={x.id}>
                            <div className="col">{x.id}</div>
                            <div className="col">{x.flightId}</div>
                            <div className="col">
                                {dateToString(x.enterTime)}
                            </div>
                            <div className="col">
                                {dateToString(x.exitTime)}
                            </div>
                            <div className="col">{x.state}</div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default HistoryTable;

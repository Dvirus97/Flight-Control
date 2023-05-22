import { useState } from "react";
import "./FlightHistory.css";
import HistoryTable from "./HistoryTable";

const FlightHistory = () => {
    const [showTable, setShowTable] = useState(false);

    return (
        <div className="FlightHistory">
            <button
                className="ToggleHistory"
                onClick={() => setShowTable((p) => !p)}
            >
                {showTable ? "Hide" : "Show"} History
            </button>

            {showTable && <HistoryTable />}
        </div>
    );
};

export default FlightHistory;

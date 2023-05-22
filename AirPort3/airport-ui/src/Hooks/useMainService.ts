import axios from "axios";
import { useState } from "react";
import { flightHistory } from "../Models/FlightHistory";

const useMainService = () => {
    const url = "http://localhost:5000/api/airport/";

    const [flightHistory, setFlightHistory] = useState<flightHistory[]>([]);

    const getFlightHistory = async (count: number) => {
        const res = await axios.get(url + "GetFlightHistory/" + count);
        setFlightHistory(res.data);
        return res.data;
    };

    return { getFlightHistory, flightHistory };
};

export default useMainService;

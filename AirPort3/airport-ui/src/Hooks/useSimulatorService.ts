import axios from "axios";
import { useEffect, useState } from "react";

export function useSimulatorService() {
    const url = "http://localhost:3030/";

    const [connected, setConnected] = useState(false);

    const startSimulator = async () => {
        const res = await axios.get(url + "start");
        return res.data;
    };

    const stopSimulator = async () => {
        const res = await axios.get(url + "Stop");
        return res.data;
    };

    const resetSimulator = async () => {
        const res = await axios.get(url + "reset");
        return res.data;
    };

    const checkConnection = async () => {
        try {
            const res = await axios.get(url + "CheckConnection");
            console.log(res);
            setConnected(true);
        } catch (e: any) {
            console.log(e);
            console.log(e.response.data);
            setConnected(false);
        }
    };

    useEffect(() => {
        checkConnection();
    }, []);

    return {
        startSimulator,
        connected,
        stopSimulator,
        resetSimulator,
    };
}

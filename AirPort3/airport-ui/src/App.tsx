import "./App.css";
import { MainView } from "./Views/MainView/MainView";
import { useSimulatorService } from "./Hooks/useSimulatorService";
import { LoadingView } from "./Views/LoadingView/LoadingView";
import { createContext, useState } from "react";
import { useSignalR } from "./Hooks/useSignalR";
import { IFlightContext } from "./Models/IFlightContext";
import useMainService from "./Hooks/useMainService";

export const flightContext = createContext<IFlightContext>(0 as any);

function App() {
    const signalR = useSignalR();
    const simulatorService = useSimulatorService();
    const mainService = useMainService();

    const [messages, setMessages] = useState<string[]>([]);

    return (
        <>
            <flightContext.Provider
                value={{
                    ...signalR,
                    ...simulatorService,
                    ...mainService,
                    messages,
                    setMessages,
                }}
            >
                {simulatorService.connected ? <MainView /> : <LoadingView />}
            </flightContext.Provider>
        </>
    );
}

export default App;

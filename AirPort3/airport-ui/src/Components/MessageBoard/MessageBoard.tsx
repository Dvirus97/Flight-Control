import "./MessageBoard.css";

import { useContext, useEffect } from "react";
import { flightContext } from "../../App";

export function MessageBoard() {
    const { messages, setMessages } = useContext(flightContext);

    useEffect(() => {
        if (messages.length > 3) {
            setMessages((prev) => prev.slice(-3));
        }
    }, [messages]);

    return (
        <div className="MessagesBoard">
            <h1>Messages</h1>
            {messages.map((x, i) => (
                <div key={i}>
                    {x}
                    <hr />
                </div>
            ))}
        </div>
    );
}

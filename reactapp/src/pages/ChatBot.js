import ChatUI from "../components/ChatUI";
import CardList from "../components/Cards/CardList";

import React, { useState } from 'react'

export default function ChatBot() {
    const [chatroleState, setChatroleState] = useState(-1);

    const handleClick = (chatbotRole) => {
        setChatroleState(chatbotRole)
    }


    return (
        <div className="body-content">
            {(() => {
                switch (chatroleState) {
                    case -1:
                        return <h1> ChatBot </h1>;
                    case 0: 
                        return <h1> Fine-Tuning </h1>;
                    case 1:
                        return <h1> AI Assistant </h1>;
                    case 2:
                        return <h1> WeatherBot </h1>;
                }
            })()}


            {chatroleState == -1 ?
                <CardList handleClick={handleClick} /> :
                <ChatUI chatroleState={chatroleState} />
            }
        </div>
    )
}
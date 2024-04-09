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
            <h1> ChatBot {chatroleState} </h1>

            {chatroleState == -1 ?
                <CardList handleClick={handleClick} /> :
                <ChatUI chatroleState={chatroleState} />
            }
        </div>
    )
}
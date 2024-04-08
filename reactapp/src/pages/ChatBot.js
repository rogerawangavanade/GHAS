import ChatUI from "../components/ChatUI";
import ChatUICards from "../components/ChatUICards";

import React, { useState } from 'react'

export default function ChatBot() {
    const [chatroleState, setChatroleState] = useState(-1);

    return (
        <div className="body-content">
            <h1> ChatBot {chatroleState} </h1>

            {/*{chatroleState == -1 ? */}
            {/*    <ChatUICards /> :*/}
                <ChatUI />
{/*            }*/}

        </div>
    )
}
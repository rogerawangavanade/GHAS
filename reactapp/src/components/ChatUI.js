import React, { useState, useEffect } from 'react';
import { ChatService } from '../Services/ChatService';
import "./ChatUI.css";
function ChatApp() {
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState('');

    // Initial response from ChatGPT (runs once)
    const fetchData = async (prompt) => {
        try {
            const service = new ChatService();
            // get response from ChatGPT using prompt with system role as the second parameter.
            const text = await service.getChatGPTResponse(prompt, 0);
            const message = { text: text, sender: 'User 2' };
            setMessages((prevMessages) => [...prevMessages, message]);
        }
        catch {
            setMessages((prevMessages) => [...prevMessages, "Could Not Connect to Service"]);
        }
    };

    useEffect(() => {
        fetchData("Hi");
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        if (newMessage.trim() === '') return;

        // Add the new message to the messages array
        const newMessageObj = { text: newMessage, sender: 'User 1' };
        setMessages([...messages, newMessageObj]);
        // Clear the input field
        setNewMessage('');
        fetchData(newMessageObj.text);
    };

    return (
        <div className="chat-app">
            <div className="chat-container">
                <div className="messages">
                    {messages.map((message, index) => (
                        <div key={index} className={`message ${message.sender === 'User 1' ? 'user' : 'other'}`}>
                            {message.text}
                        </div>
                    ))}
                </div>
                <form className="message-input" onSubmit={handleSubmit}>
                    <input
                        type="text"
                        placeholder="Type your message..."
                        value={newMessage}
                        onChange={(e) => setNewMessage(e.target.value)}
                    />
                    <button type="submit">Send</button>
                </form>
            </div>
        </div>
    );
}

export default ChatApp;




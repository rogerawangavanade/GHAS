import React from 'react';
import ReactDOM from 'react-dom/client';
import './styles.css';
import { EventType } from '@azure/msal-browser';


import App from './App';
import { msalConfig } from './authConfig';

export function handleLoginSuccess(event, setName) {
    if (event.eventType === EventType.LOGIN_SUCCESS) {
        console.log(event);
        msalConfig.setActiveAccount(event.payload.account);

        // Update the state inside the event callback
        setName(event.payload.account.name);
    }
}

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <App instance={msalConfig}/>
);

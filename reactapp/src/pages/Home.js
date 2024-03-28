import {AuthenticatedTemplate, UnauthenticatedTemplate, useMsal} from "@azure/msal-react"
import {useState, useEffect} from "react"
import { msalConfig } from '../authConfig';
import { handleLoginSuccess } from "..";


export default function Home() {
    const {instance} = useMsal();
    const [name, setName] = useState('');


    useEffect(() => {
        msalConfig.addEventCallback((event) => {
            // Use the event handling function here
            handleLoginSuccess(event, setName);
        });
        
        const currentAccount = instance.getActiveAccount();

        console.log(currentAccount);

        if (currentAccount) {
            setName(currentAccount.name);
        }
    
    }, [instance]);

    return (
        <div>
            <AuthenticatedTemplate>
                <div className="body-content">
                    <h1>Welcome {name}. This is a chatbot assistant. Please choose which of the following chat service you would like to use.</h1>
                </div>
            </AuthenticatedTemplate>
            <UnauthenticatedTemplate>
                <div className="body-content">
                    <h1>Welcome to chatbot assistant. Please Sign In to use the chat service.</h1>
                </div>
            </UnauthenticatedTemplate>
        </div>
    )
}
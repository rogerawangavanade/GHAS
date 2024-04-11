import {AuthenticatedTemplate, UnauthenticatedTemplate, useMsal} from "@azure/msal-react"
import {useState, useEffect} from "react"
import { msalConfig } from '../authConfig';
import { handleLoginSuccess } from "..";
import { ChatService } from '../Services/ChatService';


export default function Home() {
    const {instance} = useMsal();
    const [name, setName] = useState('');

    // Initial response from ChatGPT (runs once)
    const getWeather = async (lat, long) => {
        const service = new ChatService();
        // get response from ChatGPT using prompt with system role as the second parameter.
        const text = await service.getOpenWeatherResponse(lat, long);
        console.log(text);
    };



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
                    <button onClick={() => getWeather(40, -40)}>Get Weather</button>
                </div>
            </UnauthenticatedTemplate>
        </div>
    )
}
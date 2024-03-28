import { useMsal } from "@azure/msal-react"
import "./SignButton.css";

export const SignOutButton = () => {
    const {instance} = useMsal();
    
    const handleSignOut = () => {
        instance.logoutRedirect();
    }

    return (
        <button className="button" onClick={handleSignOut}>Sign out</button>
    )
}
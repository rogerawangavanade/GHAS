import { useMsal } from "@azure/msal-react"
import "./SignButton.css";

export const SignInButton = () => {
    const {instance} = useMsal();
    const handleSignIn = () => {
        instance.loginRedirect({
            scopes:['user.read']
        });
    }
    return (
        <button className="button" onClick={handleSignIn}>Sign in</button>
    )
};
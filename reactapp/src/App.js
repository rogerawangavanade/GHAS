import React from 'react';
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal, MsalProvider } from "@azure/msal-react";
import { loginRequest } from './authConfig';
import Navbar from "./Navbar";

const WrappedView = () => {
    const {instance, accounts} = useMsal();
    const activeAccount = instance.getActiveAccount();

    const handleLogin = () => {
        instance.loginRedirect(loginRequest)
          .catch(error => console.error(error));
      };
      
    
      const handleLogout = () => {
        accounts.forEach(account => {
          instance.logout(account);
        });
      };
    
      const handleSignup = () => {
        instance.loginPopup({ ...loginRequest, prompt: 'login' })
          .then(() => instance.acquireTokenSilent(loginRequest))
          .catch(error => console.error(error));
      };
    

    return (
        <div>
            <AuthenticatedTemplate>
            {activeAccount ? (
                <div>
                    <p>Authentication Successful</p>
                    <button onClick={handleLogout}> 
                        Log out
                    </button>
                    <Navbar />
                </div>
            ) : null}
            </AuthenticatedTemplate>
            <UnauthenticatedTemplate>
                <button onClick={handleLogin}>
                    Log in
                </button>
                <button onClick={handleSignup}>
                    Sign up
                </button>
            </UnauthenticatedTemplate>
        </div>
    )

};

export default function App({ instance }) {
    return (
      <MsalProvider instance={instance}>
        <WrappedView />
      </MsalProvider>
    );
  }
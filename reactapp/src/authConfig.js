import { LogLevel } from "@azure/msal-browser";


// Config object to be passed to Msal on creation
export const msalConfig = {
    auth: {
        clientId: "5fa5dd49-6bae-4af1-96f6-07fb8e725563",
        authority: "https://login.microsoftonline.com/openaichatbotb2c.onmicrosoft.com",
        redirectUri: "/",
        postLogoutRedirectUri: "/",
    },
    cache: {
        cacheLocation: "sessionStorage",
        storeAuthStateInCookie: false,
    },
    system: {
        loggerOptions: {
            loggerCallback: (level, message, containsPii) => {
                if (containsPii) {
                    return;
                }
                switch (level) {
                    case LogLevel.Error:
                        console.error(message);
                        return;
                    case LogLevel.Info:
                        console.info(message);
                        return;
                    case LogLevel.Verbose:
                        console.debug(message);
                        return;
                    case LogLevel.Warning:
                        console.warn(message);
                        return;
                    default:
                        return;
                }
            },
        },
    },
};

// Add here scopes for id token to be used at MS Identity Platform endpoints.
export const loginRequest = {
    scopes: ["User.Read"]
};

export const tokenRequest = {
    scopes: ['user.read'],
  };
import { PublicClientApplication } from '@azure/msal-browser';

export const msalConfig = new PublicClientApplication({
    auth:{
        // Personal account client+ + tenant id
        clientId: 'e76227aa-29ee-4b1e-88c3-1d901b517fac',
        authority: 'https://login.microsoftonline.com/fb5b08bd-3687-4a2c-8e35-2d5907c7ed17',
        redirectUri: '/',
    }
});
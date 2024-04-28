import { Routes, Route, BrowserRouter as Router } from "react-router-dom";

import Home from "./pages/Home";
import ChatBot from "./pages/ChatBot";

import { MsalProvider } from "@azure/msal-react";
import Navbar from "./components/Navbar";

function App({ instance }) {
    return (
        <MsalProvider instance = {instance}>
            <Router>
                <Navbar/>
                <Pages />
            </Router>
        </MsalProvider>
    );
}

const Pages = () => {
    return (
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/ChatBot" element={<ChatBot />} />
        </Routes>
    );
}

export default App;

import { BrowserRouter as Router, Routes, Route, Link , Navigate} from 'react-router-dom';
import Home from "./pages/Home"
import ChatBot from "./pages/ChatBot";

export default function Navbar() {
    return (
        <Router>
            <nav className="navbar">
                <h1 className="site-title"> ChatBotProject </h1>
                <ul>
                    <li>
                        <Link to="/Home">Home</Link>
                    </li>
                    <li>
                        <Link to="/ChatBot">ChatBot</Link>
                    </li>

                </ul>
            </nav>
            <Routes>
                <Route path="/Home" element={<Home />} />
                <Route path="/ChatBot" element={<ChatBot />} />
                {/* Fallback route */}
                <Route path="*" element={<Navigate to="/Home" />} />
            </Routes>
        </Router>
    )
}
import React from "react";
import { SignInButton } from "./SignInButton";
import { SignOutButton } from "./SignOutButton";
import { Link } from "react-router-dom";
import "./Navbar.css";
import { useIsAuthenticated } from "@azure/msal-react";

const Navbar = () => {
  const isAuthenticated = useIsAuthenticated();

  return (
    <div className="navbar">
      <ul>
        <li>
          <Link to="/" color="inherit" variant="h6">
            Home
          </Link>
        </li>
        {/* {isAuthenticated && ( */}
          <li>
            <Link to="/ChatBot" color="inherit" variant="h6">
              ChatBot
            </Link>
          </li>
        {/* )} */}
        <li className="expand-button">
          {isAuthenticated ? <SignOutButton /> : <SignInButton />}
        </li>
      </ul>
    </div>
  );
};

export default Navbar;

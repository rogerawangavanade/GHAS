import React from 'react';
import "./Card.css";

const Card = ({ title, imageSrc, handleClick }) => {

    return (
        <div className="card" onClick={handleClick}>
            <img src={imageSrc} alt={title} />
            <h2>{title}</h2>
        </div>
    );
};

export default Card;

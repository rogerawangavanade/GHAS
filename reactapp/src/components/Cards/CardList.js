import React from 'react';
import Card from "./Card";
import "./CardList.css";
import image1 from "../../Assets/CardImages/image-1.jpg";
import image2 from "../../Assets/CardImages/image-2.jpg";
import image3 from "../../Assets/CardImages/image-3.jpg";

const CardList = ({handleClick}) => {

    return (
        <div className="cardlist">
            <Card
                title="FineTuning"
                imageSrc={image1}
                handleClick={() => handleClick(0)}
            />
            <Card
                title="AI Assistant"
                imageSrc={image2}
                handleClick={() => handleClick(1)}
            />
            <Card
                title="Card 3"
                imageSrc={image3}
                handleClick={() => handleClick(2)}
            />
        </div>
    );
};

export default CardList;

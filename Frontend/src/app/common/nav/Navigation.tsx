import React from 'react';

import '../../../styles/global.scss';
import './Navigation.scss';

import logo from '../../../resources/img/groenecomputershop_logo.png';
import {NavLink} from "react-router-dom";

const Navigation: React.FC = () => {
    return (
        <div className="bar">
            <NavLink to='/'>
                <img src={logo} alt="Het logo van de groene computershop" />
            </NavLink>
            <div className="title">
                <p>De Groene Computershop PC onderdelen samensteller!</p>
            </div>
            <div className="admin">
            </div>
        </div>
    );
}

export default Navigation;

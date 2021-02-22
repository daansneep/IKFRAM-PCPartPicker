import React from 'react';
import './App.scss';
import Navigation from '../common/nav/Navigation';
import {Route} from 'react-router-dom';
import PcBuilder from "../../functionalities/pcbuilder/PcBuilder";
import Quotation from "../../functionalities/quotation/Quotation";
import {observer} from "mobx-react-lite";

const App: React.FC = () => {
  return (
    <div className="app">
      <Navigation/>
      <div className="route">
          <Route exact path='/' component={PcBuilder}/>
          <Route path='/quotation' component={Quotation}/>
      </div>
    </div>
  );
}

export default observer(App);

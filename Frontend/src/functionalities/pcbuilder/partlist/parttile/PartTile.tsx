import React, {useContext, useEffect} from 'react';
import '../../../../styles/global.scss';
import './PartTile.scss';

import cdRom from '../../../../resources/img/cd-rom.png';
import {IPart} from "../../../../app/models/IPart";
import {observer} from "mobx-react-lite";
import {RootStoreContext} from "../../../../app/stores/rootStore";

const PartTile: React.FC<{part: IPart}> = ({part}) => {
    const generalStore = useContext(RootStoreContext).generalStore;

    const setCurrentComponent = (): void => {
        generalStore.setCurrentComponent(part.type);
        console.log(part.type)
    }

    useEffect(() => {}, [generalStore])

    return (
        <div onClick={setCurrentComponent}
             className={part.type === generalStore.currentComponent ? 'activated parttile' : 'parttile'}>
            <h2>{part.title}</h2>
            <div className="content">
                <img src={part.image ? part.image : cdRom} alt="Een plaatje van het component van de computer"/>
                <p>{part.description}</p>
            </div>
        </div>
    );
}

export default observer(PartTile);

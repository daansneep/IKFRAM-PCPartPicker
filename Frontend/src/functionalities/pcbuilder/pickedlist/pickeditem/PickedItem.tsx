import React from 'react';
import {observer} from "mobx-react-lite";
import './PickedItem.scss';
import cdRom from "../../../../resources/img/cd-rom.png";

const PickedItem: React.FC<{part: any}> = ({part}) => {
    return (
        <div className="pickedentry">
            <img src={part.part.image ? part.part.image : cdRom} alt="test"/>
            <h3>{part.part.brand} - {part.part.name}</h3>
            <p>{part.formFactor ? part.formFactor.formFactorName + ' - ' : null}
                {part.socket ? part.socket.socketName + ' - ' : null}
                {part.ramType ? part.ramType.ramTypeName + ' - ' : null}
                {part.rgb ? 'Heeft RGB' : 'Geen RGB'} -
                {part.oc ? 'Is overclockbaar' : 'Is niet overclockbaar'} - </p>
            <div className="price">
                <h4>€{part.part.retailPrice}</h4>
            </div>
        </div>
    );
}

export default observer(PickedItem)

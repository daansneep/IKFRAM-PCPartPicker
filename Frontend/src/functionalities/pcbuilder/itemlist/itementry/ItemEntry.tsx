import React, {useContext} from 'react';
import '../../../../styles/global.scss';
import './ItemEntry.scss';
import cdRom from '../../../../resources/img/cd-rom.png';
import {observer} from "mobx-react-lite";
import {RootStoreContext} from "../../../../app/stores/rootStore";

const ItemEntry: React.FC<{part: any}> = ({part}) => {
    const rootStore = useContext(RootStoreContext);
    const { currentComponent } = rootStore.generalStore;
    const { motherboardStore, processorStore, processorCoolerStore, ramStore, storageDeviceStore, caseStore,
            graphicsCardStore, operatingSystemStore, powerSupplyStore} = rootStore;
    const { selectMotherboard } = motherboardStore;
    const { selectProcessor } = processorStore;
    const { selectProcessorCooler } = processorCoolerStore;
    const { selectRam } = ramStore;
    const { selectOperatingSystem } = operatingSystemStore;
    const { selectStorageDevice } = storageDeviceStore;
    const { selectGraphicsCard } = graphicsCardStore;
    const { selectPowerSupply } = powerSupplyStore;
    const { selectCase } = caseStore;


    const setPart = () => {
        if (currentComponent === 'motherboard') return selectMotherboard(part.id);
        if (currentComponent === 'processor') return selectProcessor(part.id);
        if (currentComponent === 'processorcooler') return selectProcessorCooler(part.id);
        if (currentComponent === 'ram') return selectRam(part.id);
        if (currentComponent === 'operatingsystem') return selectOperatingSystem(part.id);
        if (currentComponent === 'storagedevice') return selectStorageDevice(part.id);
        if (currentComponent === 'graphicscard') return selectGraphicsCard(part.id);
        if (currentComponent === 'powersupply') return selectPowerSupply(part.id);
        if (currentComponent === 'case') return selectCase(part.id);

    }

    return (
        <div className="itementry">
            <img src={part.part.image ? part.part.image : cdRom} alt="test"/>
            <h3>{part.part.brand} - {part.part.name}</h3>
            <p>{part.formFactor ? part.formFactor.formFactorName + ' - ' : null}
            {part.socket ? part.socket.socketName + ' - ' : null}
            {part.ramType ? part.ramType.ramTypeName + ' - ' : null}
            {part.rgb ? 'Heeft RGB' : 'Geen RGB'} -
            {part.oc ? 'Is overclockbaar' : 'Is niet overclockbaar'} - </p>
            <div className="price-and-order">
                <h4>€{part.part.retailPrice}</h4>
                <button onClick={setPart}>Voeg toe!</button>
            </div>
        </div>
    );
}

export default observer(ItemEntry);

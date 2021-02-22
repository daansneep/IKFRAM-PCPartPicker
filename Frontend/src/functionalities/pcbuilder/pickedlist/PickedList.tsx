import React, {useContext} from 'react';
import {observer} from "mobx-react-lite";
import './PickedList.scss';
import PickedItem from "./pickeditem/PickedItem";
import {RootStoreContext} from "../../../app/stores/rootStore";

const PickedList: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const { motherboardStore, processorStore, processorCoolerStore, ramStore, storageDeviceStore, caseStore,
        graphicsCardStore, operatingSystemStore, powerSupplyStore} = rootStore;
    const { currentMotherboard } = motherboardStore;
    const { currentProcessor } = processorStore;
    const { currentProcessorCooler } = processorCoolerStore;
    const { currentRam } = ramStore;
    const { currentOperatingSystem } = operatingSystemStore;
    const { currentStorageDevice } = storageDeviceStore;
    const { currentGraphicsCard } = graphicsCardStore;
    const { currentPowerSupply } = powerSupplyStore;
    const { currentCase } = caseStore;

    const clear = () => {
        motherboardStore.clear();
        processorStore.clear();
        processorCoolerStore.clear();
        ramStore.clear();
        storageDeviceStore.clear();
        caseStore.clear();
        graphicsCardStore.clear();
        operatingSystemStore.clear();
        powerSupplyStore.clear();
    }

    return (
        <div className="picklist">
            {currentCase ? (
                <div className="case">
                    <h4>Case</h4>
                    <PickedItem part={currentCase}/>
                </div>
            ) : null}
            {currentMotherboard ? (
                <div className="motherboard">
                    <h4>Motherboard</h4>
                    <PickedItem part={currentMotherboard}/>
                </div>
            ) : null}
            {currentProcessor ? (
                <div className="processor">
                    <h4>Processor</h4>
                    <PickedItem part={currentProcessor}/>
                </div>
            ) : null}
            {currentProcessorCooler ? (
                <div className="processorcooler">
                    <h4>Koeler</h4>
                    <PickedItem part={currentProcessorCooler}/>
                </div>
            ) : null}
            {currentRam ? (
                <div className="ram">
                    <h4>Ram</h4>
                    <PickedItem part={currentRam}/>
                </div>
            ) : null}
            {currentOperatingSystem ? (
                <div className="operatingsystem">
                    <h4>Besturingssysteem</h4>
                    <PickedItem part={currentOperatingSystem}/>
                </div>
            ) : null}
            {currentStorageDevice ? (
                <div className="storagedevice">
                    <h4>Opslagmedium</h4>
                    <PickedItem part={currentStorageDevice}/>
                </div>
            ) : null}
            {currentGraphicsCard ? (
                <div className="graphicscard">
                    <h4>Grafische kaart</h4>
                    <PickedItem part={currentGraphicsCard}/>
                </div>
            ) : null}
            {currentPowerSupply ? (
                <div className="powersupply">
                    <h4>Voeding</h4>
                    <PickedItem part={currentPowerSupply}/>
                </div>
            ) : null}
            {currentCase || currentMotherboard || currentRam || currentProcessorCooler || currentProcessor ||
                currentOperatingSystem || currentPowerSupply || currentGraphicsCard || currentStorageDevice ? (
                <button onClick={clear}>Lijst legen</button>
                ) : null
            }
        </div>
    )
}

export default observer(PickedList)

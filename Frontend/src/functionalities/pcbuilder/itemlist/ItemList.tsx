import React, {useContext, useEffect} from 'react';
import '../../../styles/global.scss';
import './ItemList.scss';
import ItemEntry from "./itementry/ItemEntry";
import {observer} from "mobx-react-lite";
import {RootStoreContext} from "../../../app/stores/rootStore";

const ItemList: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const { generalStore, motherboardStore, processorStore, processorCoolerStore, ramStore, caseStore,
        graphicsCardStore, operatingSystemStore, powerSupplyStore, storageDeviceStore } = rootStore;
    const currentComponent = generalStore.currentComponent;
    const cases = caseStore.casesByCreationDate;
    const graphicsCards = graphicsCardStore.graphicsCardsByCreationDate;
    const motherboards = motherboardStore.motherboardsByCreationDate;
    const operatingSystems = operatingSystemStore.operatingSystemsByCreationDate;
    const powerSupplies = powerSupplyStore.powerSuppliesByCreationDate;
    const processors = processorStore.processorsByCreationDate;
    const processorCoolers = processorCoolerStore.processorCoolersByCreationDate;
    const rams = ramStore.ramsByCreationDate;
    const storageDevices = storageDeviceStore.storageDevicesByCreationDate;

    useEffect(() => {}, [motherboards, processors, currentComponent, cases, graphicsCards, operatingSystems,
                                        powerSupplies, processorCoolers, rams, storageDevices])

    if (currentComponent === 'motherboard') {
        return (
            <div className="itemlist">
                {motherboards.map((part: any) => (
                    <ItemEntry key={part.id} part={part}/>
                ))}
            </div>
        )
    }

    if (currentComponent === 'processor') {
        return (
            <div className="itemlist">
                {processors.map((part: any) => (
                    <ItemEntry key={part.id} part={part}/>
                ))}
            </div>
        )
    }

    if (currentComponent === 'processorcooler') {
        return (
            <div className="itemlist">
                {processorCoolers.map((part: any) => (
                    <ItemEntry key={part.id} part={part}/>
                ))}
            </div>
        )
    }

    if (currentComponent === 'ram') {
        return (
            <div className="itemlist">
                {rams.map((part: any) => (
                    <ItemEntry key={part.id} part={part}/>
                ))}
            </div>
        )
    }

    if (currentComponent === 'case') {
        return (
            <div className="itemlist">
                {cases.map((part: any) => (
                    <ItemEntry key={part.id} part={part}/>
                ))}
            </div>
        )
    }

    if (currentComponent === 'graphicscard') {
        return (
            <div className="itemlist">
                {graphicsCards.map((part: any) => (
                    <ItemEntry key={part.id} part={part}/>
                ))}
            </div>
        )
    }

    if (currentComponent === 'operatingsystem') {
        return (
            <div className="itemlist">
                {operatingSystems.map((part: any) => (
                    <ItemEntry key={part.id} part={part}/>
                ))}
            </div>
        )
    }

    if (currentComponent === 'powersupply') {
        return (
            <div className="itemlist">
                {powerSupplies.map((part: any) => (
                    <ItemEntry key={part.id} part={part}/>
                ))}
            </div>
        )
    }

    if (currentComponent === 'storagedevice') {
        return (
            <div className="itemlist">
                {storageDevices.map((part: any) => (
                    <ItemEntry key={part.id} part={part}/>
                ))}
            </div>
        )
    }

    return (
        <div className="itemlist">
            <p>Geen categorie geselecteerd, of geen items gevonden!</p>
        </div>
    );
}

export default observer(ItemList);

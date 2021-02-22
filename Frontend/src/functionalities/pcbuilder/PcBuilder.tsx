import React, {useContext, useEffect} from 'react';
import '../../styles/global.scss';
import './PcBuilder.scss';
import PartList from "./partlist/PartList";
import ItemList from "./itemlist/ItemList";
import {observer} from "mobx-react-lite";
import { useHistory } from 'react-router-dom';
import {RootStoreContext} from "../../app/stores/rootStore";
import PickedList from "./pickedlist/PickedList";

const PcBuilder: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const { motherboardStore, processorStore, processorCoolerStore, ramStore, caseStore,
        graphicsCardStore, operatingSystemStore, powerSupplyStore, storageDeviceStore} = rootStore;
    const { currentMotherboard } = motherboardStore;
    const { currentProcessor } = processorStore;
    const { currentProcessorCooler } = processorCoolerStore;
    const { currentRam } = ramStore;
    const { currentOperatingSystem } = operatingSystemStore;
    const { currentStorageDevice } = storageDeviceStore;
    const { currentGraphicsCard } = graphicsCardStore;
    const { currentPowerSupply } = powerSupplyStore;
    const { currentCase } = caseStore;
    const history = useHistory();

    useEffect(() => {
        motherboardStore.loadMotherboards();
        processorStore.loadProcessors();
        processorCoolerStore.loadProcessorCoolers();
        ramStore.loadRams();
        caseStore.loadCases();
        graphicsCardStore.loadGraphicsCards();
        operatingSystemStore.loadOperatingSystems();
        powerSupplyStore.loadPowerSupplies();
        storageDeviceStore.loadStorageDevices();
    }, [rootStore, caseStore, graphicsCardStore, motherboardStore, operatingSystemStore, powerSupplyStore,
        processorStore, processorCoolerStore, ramStore, storageDeviceStore]);

    const navigateToQuotation = () => {
        history.push('/quotation');
    }

    if (motherboardStore.loading) return (
        <p>Page is loading...</p>
    );

    return (
        <div className="pcbuilder">
            <PickedList/>
            <PartList />
            {currentMotherboard && currentProcessor && currentProcessorCooler && currentRam && currentOperatingSystem
            && currentStorageDevice && currentGraphicsCard && currentPowerSupply && currentCase ? (
                <button onClick={navigateToQuotation}>Offerte versturen!</button>
                ) : null
            }
            <ItemList />
        </div>
    );
}

export default observer(PcBuilder);

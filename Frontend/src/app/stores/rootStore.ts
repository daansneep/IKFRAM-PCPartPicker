import GeneralStore from "./generalStore";
import AdminStore from "./adminStore";
import CaseStore from "./caseStore";
import GraphicsCardStore from "./graphicsCardStore";
import MotherboardStore from "./motherboardStore";
import OperatingSystemStore from "./operatingSystemStore";
import PowerSupplyStore from "./powerSupplyStore";
import ProcessorCoolerStore from "./processorCoolerStore";
import RamStore from "./ramStore";
import {createContext} from "react";
import ProcessorStore from "./processorStore";
import StorageDeviceStore from "./storageDeviceStore";

export class RootStore {
    generalStore: GeneralStore;
    adminStore: AdminStore;
    caseStore: CaseStore;
    graphicsCardStore: GraphicsCardStore;
    motherboardStore: MotherboardStore;
    operatingSystemStore: OperatingSystemStore;
    powerSupplyStore: PowerSupplyStore;
    processorCoolerStore: ProcessorCoolerStore;
    processorStore: ProcessorStore;
    ramStore: RamStore;
    storageDeviceStore: StorageDeviceStore;

    constructor() {
        this.generalStore = new GeneralStore(this);
        this.adminStore = new AdminStore(this);
        this.caseStore = new CaseStore(this);
        this.graphicsCardStore = new GraphicsCardStore(this);
        this.motherboardStore = new MotherboardStore(this);
        this.operatingSystemStore = new OperatingSystemStore(this);
        this.powerSupplyStore = new PowerSupplyStore(this);
        this.processorCoolerStore = new ProcessorCoolerStore(this);
        this.processorStore = new ProcessorStore(this);
        this.ramStore = new RamStore(this);
        this.storageDeviceStore = new StorageDeviceStore(this);

    }
}

export const RootStoreContext = createContext(new RootStore())

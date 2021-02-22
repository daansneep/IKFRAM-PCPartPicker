import {action, computed, configure, observable, runInAction} from "mobx";
import {createContext} from "react";
import {PowerSupply} from "../models/PowerSupply";
import {api} from "../api/agent";
import {RootStore} from "./rootStore";

configure({enforceActions: 'always'});

export default class PowerSupplyStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable powerSupplyRegistry = new Map();
    @observable powerSupplies: PowerSupply[] = [];
    @observable currentPowerSupply: PowerSupply | null = null

    @computed get powerSuppliesByCreationDate() {
        return Array.from(this.powerSupplyRegistry.values())
    }

    @action loadPowerSupplies = async () => {
        try {
            const powerSupplies = await api.PowerSupplies.list();
            runInAction(() => {
                powerSupplies.forEach((powerSupply) => {
                    this.powerSupplyRegistry.set(powerSupply.id, powerSupply);
                })
            })
        } catch (error) {
            console.log(error);
        }
    }

    @action createPowerSupply = (powerSupply: PowerSupply) => {
        try {
            api.PowerSupplies.create(powerSupply)
                .then(() => {
                    this.powerSupplyRegistry.set(powerSupply.id, powerSupply);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action editPowerSupply = (powerSupply: PowerSupply) => {
        try {
            api.PowerSupplies.update(powerSupply)
                .then(() => {
                    this.powerSupplyRegistry.set(powerSupply.id, powerSupply);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action deletePowerSupply = (id: number) => {
        try {
            api.PowerSupplies.delete(id)
                .then(() => {
                    this.powerSupplyRegistry.delete(id);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action selectPowerSupply = (id: number) => {
        this.currentPowerSupply = this.powerSupplyRegistry.get(id);
    }

    @action clear = () => {
        this.currentPowerSupply = null;
    }
}

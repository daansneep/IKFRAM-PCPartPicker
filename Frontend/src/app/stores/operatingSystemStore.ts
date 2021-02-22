import {action, computed, configure, observable, runInAction} from "mobx";
import {createContext} from "react";
import {OperatingSystem} from "../models/OperatingSystem";
import {api} from "../api/agent";
import {RootStore} from "./rootStore";

configure({enforceActions: 'always'});

export default class OperatingSystemStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable operatingSystemRegistry = new Map();
    @observable operatingSystems: OperatingSystem[] = [];
    @observable currentOperatingSystem: OperatingSystem | null = null

    @computed get operatingSystemsByCreationDate() {
        return Array.from(this.operatingSystemRegistry.values())
    }

    @action loadOperatingSystems = async () => {
        try {
            const operatingSystems = await api.OperatingSystems.list();
            runInAction(() => {
                operatingSystems.forEach((operatingSystem) => {
                    this.operatingSystemRegistry.set(operatingSystem.id, operatingSystem);
                })
            })
        } catch (error) {
            console.log(error);
        }
    }

    @action createOperatingSystem = (operatingSystem: OperatingSystem) => {
        try {
            api.OperatingSystems.create(operatingSystem)
                .then(() => {
                    this.operatingSystemRegistry.set(operatingSystem.id, operatingSystem);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action editOperatingSystem = (operatingSystem: OperatingSystem) => {
        try {
            api.OperatingSystems.update(operatingSystem)
                .then(() => {
                    this.operatingSystemRegistry.set(operatingSystem.id, operatingSystem);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action deleteOperatingSystem = (id: number) => {
        try {
            api.OperatingSystems.delete(id)
                .then(() => {
                    this.operatingSystemRegistry.delete(id);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action selectOperatingSystem = (id: number) => {
        this.currentOperatingSystem = this.operatingSystemRegistry.get(id);
    }

    @action clear = () => {
        this.currentOperatingSystem = null;
    }
}

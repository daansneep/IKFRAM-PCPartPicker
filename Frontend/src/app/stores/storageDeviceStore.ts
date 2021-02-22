import {action, computed, configure, observable, runInAction} from "mobx";
import {createContext} from "react";
import {StorageDevice} from "../models/StorageDevice";
import {api} from "../api/agent";
import {RootStore} from "./rootStore";

configure({enforceActions: 'always'});

export default class StorageDeviceStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable storageDeviceRegistry = new Map();
    @observable storageDevices: StorageDevice[] = [];
    @observable currentStorageDevice: StorageDevice | null = null

    @computed get storageDevicesByCreationDate() {
        return Array.from(this.storageDeviceRegistry.values())
    }

    @action loadStorageDevices = async () => {
        try {
            const storageDevices = await api.StorageDevices.list();
            runInAction(() => {
                storageDevices.forEach((storageDevice) => {
                    this.storageDeviceRegistry.set(storageDevice.id, storageDevice);
                })
            })
        } catch (error) {
            console.log(error);
        }
    }

    @action createStorageDevice = (storageDevice: StorageDevice) => {
        try {
            api.StorageDevices.create(storageDevice)
                .then(() => {
                    this.storageDeviceRegistry.set(storageDevice.id, storageDevice);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action editStorageDevice = (storageDevice: StorageDevice) => {
        try {
            api.StorageDevices.update(storageDevice)
                .then(() => {
                    this.storageDeviceRegistry.set(storageDevice.id, storageDevice);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action deleteStorageDevice = (id: number) => {
        try {
            api.StorageDevices.delete(id)
                .then(() => {
                    this.storageDeviceRegistry.delete(id);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action selectStorageDevice = (id: number) => {
        this.currentStorageDevice = this.storageDeviceRegistry.get(id);
    }

    @action clear = () => {
        this.currentStorageDevice = null;
    }
}

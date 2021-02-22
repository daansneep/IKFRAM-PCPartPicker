import {action, computed, observable, runInAction} from "mobx";
import {createContext} from "react";
import {Motherboard} from "../models/Motherboard";
import {api} from "../api/agent";
import {RootStore} from "./rootStore";

export default class MotherboardStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable motherboardRegistry = new Map();
    @observable motherboards: Motherboard[] = [];
    @observable currentMotherboard: Motherboard | null = null
    @observable loading = false;

    @computed get motherboardsByCreationDate() {
        return Array.from(this.motherboardRegistry.values());
    }

    @action loadMotherboards = async () => {
        this.loading = true;
        try {
            const motherboards = await api.Motherboards.list();
            runInAction( () => {
                motherboards.forEach(motherboard => {
                    console.log(motherboard);
                    this.motherboardRegistry.set(motherboard.id, motherboard);
                });
            })
        } catch (error) {
            console.log(error)
        } finally {
            runInAction(() => {
                this.loading = false;
            })
        }
    }

    @action createMotherboard = (motherboard: Motherboard) => {
        this.loading = true;
        api.Motherboards.create(motherboard)
            .then(() => {
                this.motherboardRegistry.set(motherboard.id, motherboard);
            })
            .catch(e => {
                console.log("error: " + e)
            })
            .finally(() => {
                this.loading = false;
            })
    }

    @action editMotherboard = (motherboard: Motherboard) => {
        this.loading = true;
        api.Motherboards.update(motherboard)
            .then(() => {
                this.motherboardRegistry.set(motherboard.id, motherboard);
            })
            .catch(e => {
                console.log("error: " + e)
            })
            .finally(() => {
                this.loading = false;
            })
    }

    @action deleteMotherboard = (id: number) => {
        this.loading = true;
        api.Motherboards.delete(id)
            .then(() => {
                this.motherboardRegistry.delete(id);
            })
            .catch(e => {
                console.log("error: " + e)
            })
            .finally(() => {
                this.loading = false;
            })

    }

    @action selectMotherboard = (id: number) => {
        this.currentMotherboard = this.motherboardRegistry.get(id);
    }

    @action clear = () => {
        this.currentMotherboard = null;
    }
}

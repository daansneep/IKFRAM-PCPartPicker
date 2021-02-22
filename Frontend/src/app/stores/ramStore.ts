import {action, computed, configure, observable, runInAction} from "mobx";
import {Ram} from "../models/Ram";
import {api} from "../api/agent";
import {RootStore} from "./rootStore";

configure({enforceActions: 'always'});

export default class RamStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable ramRegistry = new Map();
    @observable rams: Ram[] = [];
    @observable currentRam: Ram | null = null

    @computed get ramsByCreationDate() {
        let filteredArray = Array.from(this.ramRegistry.values());
        if (this.rootStore.motherboardStore.currentMotherboard) {
            const id = this.rootStore.motherboardStore.currentMotherboard.ramType.ramTypeId
            filteredArray = Array.from(this.ramRegistry.values()).filter((elem: Ram) => {
                return id === elem.ramType.ramTypeId;
            });
        }
        return filteredArray
    }

    @action loadRams = async () => {
        try {
            const rams = await api.Rams.list();
            runInAction(() => {
                rams.forEach((ram) => {
                    this.ramRegistry.set(ram.id, ram);
                })
            })
        } catch (error) {
            console.log(error);
        }
    }

    @action createRam = (ram: Ram) => {
        try {
            api.Rams.create(ram)
                .then(() => {
                    this.ramRegistry.set(ram.id, ram);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action editRam = (ram: Ram) => {
        try {
            api.Rams.update(ram)
                .then(() => {
                    this.ramRegistry.set(ram.id, ram);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action deleteRam = (id: number) => {
        try {
            api.Rams.delete(id)
                .then(() => {
                    this.ramRegistry.delete(id);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action selectRam = (id: number) => {
        this.currentRam = this.ramRegistry.get(id);
    }

    @action clear = () => {
        this.currentRam = null;
    }
}

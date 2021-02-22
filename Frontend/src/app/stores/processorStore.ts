import {action, computed, configure, observable, runInAction} from "mobx";
import {createContext} from "react";
import {Processor} from "../models/Processor";
import {api} from "../api/agent";
import {RootStore} from "./rootStore";
import {Ram} from "../models/Ram";

configure({enforceActions: 'always'});

export default class ProcessorStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable processorRegistry = new Map();
    @observable processors: Processor[] = [];
    @observable currentProcessor: Processor | null = null

    @computed get processorsByCreationDate() {
        let filteredArray = Array.from(this.processorRegistry.values());
        if (this.rootStore.motherboardStore.currentMotherboard) {
            const id = this.rootStore.motherboardStore.currentMotherboard.socket.socketId
            filteredArray = Array.from(this.processorRegistry.values()).filter((elem: Processor) => {
                return id === elem.socket.socketId;
            });
        }
        return filteredArray
    }

    @action loadProcessors = async () => {
        try {
            const processors = await api.Processors.list();
            runInAction(() => {
                processors.forEach((processor) => {
                    this.processorRegistry.set(processor.id, processor);
                })
            })
        } catch (error) {
            console.log(error);
        }
    }

    @action createProcessor = (processor: Processor) => {
        try {
            api.Processors.create(processor)
                .then(() => {
                    this.processorRegistry.set(processor.id, processor);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action editProcessor = (processor: Processor) => {
        try {
            api.Processors.update(processor)
                .then(() => {
                    this.processorRegistry.set(processor.id, processor);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action deleteProcessor = (id: number) => {
        try {
            api.Processors.delete(id)
                .then(() => {
                    this.processorRegistry.delete(id);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action selectProcessor = (id: number) => {
        this.currentProcessor = this.processorRegistry.get(id);
    }

    @action clear = () => {
        this.currentProcessor = null;
    }
}

import {action, computed, configure, observable, runInAction} from "mobx";
import {createContext} from "react";
import {ProcessorCooler} from "../models/ProcessorCooler";
import {api} from "../api/agent";
import {RootStore} from "./rootStore";
import {Processor} from "../models/Processor";
import ProcessorStore from "./processorStore";

configure({enforceActions: 'always'});

export default class ProcessorCoolerStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable processorCoolerRegistry = new Map();
    @observable processorCoolers: ProcessorCooler[] = [];
    @observable currentProcessorCooler: ProcessorCooler | null = null

    @computed get processorCoolersByCreationDate() {
        let filteredArray = Array.from(this.processorCoolerRegistry.values());
        if (this.rootStore.motherboardStore.currentMotherboard) {
            const id = this.rootStore.motherboardStore.currentMotherboard.socket.socketId
            filteredArray = Array.from(this.processorCoolerRegistry.values()).filter((elem: ProcessorCooler) => {
                return id === elem.socket.socketId;
            });
        }
        return filteredArray
    }

    @action loadProcessorCoolers = async () => {
        try {
            const processorCoolers = await api.ProcessorCoolers.list();
            runInAction(() => {
                processorCoolers.forEach((processorCooler) => {
                    this.processorCoolerRegistry.set(processorCooler.id, processorCooler);
                })
            })
        } catch (error) {
            console.log(error);
        }
    }

    @action createProcessorCooler = (processorCooler: ProcessorCooler) => {
        try {
            api.ProcessorCoolers.create(processorCooler)
                .then(() => {
                    this.processorCoolerRegistry.set(processorCooler.id, processorCooler);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action editProcessorCooler = (processorCooler: ProcessorCooler) => {
        try {
            api.ProcessorCoolers.update(processorCooler)
                .then(() => {
                    this.processorCoolerRegistry.set(processorCooler.id, processorCooler);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action deleteProcessorCooler = (id: number) => {
        try {
            api.ProcessorCoolers.delete(id)
                .then(() => {
                    this.processorCoolerRegistry.delete(id);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action selectProcessorCooler = (id: number) => {
        this.currentProcessorCooler = this.processorCoolerRegistry.get(id);
    }

    @action clear = () => {
        this.currentProcessorCooler = null;
    }
}

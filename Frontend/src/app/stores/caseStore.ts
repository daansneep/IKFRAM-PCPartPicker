import {action, computed, observable, configure, runInAction} from "mobx";
import {Case} from "../models/Case";
import {api} from "../api/agent";
import {RootStore} from "./rootStore";

configure({enforceActions: 'always'});

export default class CaseStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable caseRegistry = new Map();
    @observable cases: Case[] = [];
    @observable currentCase: Case | null = null

    @computed get casesByCreationDate() {
        let filteredArray = Array.from(this.caseRegistry.values());
        if (this.rootStore.motherboardStore.currentMotherboard) {
            const id = this.rootStore.motherboardStore.currentMotherboard.formFactor.formFactorId;
            filteredArray = Array.from(this.caseRegistry.values()).filter((elem: Case) => {
                return id === elem.formFactor.formFactorId;
            });
        }
        return filteredArray;
    }

    @action loadCases = async () => {
        try {
            const cases = await api.Cases.list();
            runInAction(() => {
                cases.forEach((Pcase) => {
                    console.log(Pcase);
                    this.caseRegistry.set(Pcase.id, Pcase);
                })
            })
        } catch (error) {
            console.log(error);
        }
    }

    @action createCase = (Pcase: Case) => {
        try {
            api.Cases.create(Pcase)
                .then(() => {
                    this.caseRegistry.set(Pcase.id, Pcase);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action editCase = (Pcase: Case) => {
        try {
            api.Cases.update(Pcase)
                .then(() => {
                    this.caseRegistry.set(Pcase.id, Pcase);
                })
        } catch (error) {
            console.log(error);
        }
    }
    
    @action deleteCase = (id: number) => {
        try {
            api.Cases.delete(id)
                .then(() => {
                    this.caseRegistry.delete(id);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action selectCase = (id: number) => {
        this.currentCase = this.caseRegistry.get(id);
    }

    @action clear = () => {
        this.currentCase = null;
    }
}

import {action, observable} from "mobx";
import {RootStore} from "./rootStore";

export default class GeneralStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable currentComponent: string | null = null;

    @action setCurrentComponent = (component: string) => {
        this.currentComponent = component;
    }
}

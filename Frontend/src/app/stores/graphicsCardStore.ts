import {action, computed, configure, observable, runInAction} from "mobx";
import {createContext} from "react";
import {GraphicsCard} from "../models/GraphicsCard";
import {api} from "../api/agent";
import {RootStore} from "./rootStore";

configure({enforceActions: 'always'});

export default class GraphicsCardStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable graphicsCardRegistry = new Map();
    @observable graphicsCards: GraphicsCard[] = [];
    @observable currentGraphicsCard: GraphicsCard | null = null

    @computed get graphicsCardsByCreationDate() {
        return Array.from(this.graphicsCardRegistry.values())
    }

    @action loadGraphicsCards = async () => {
        try {
            const graphicsCards = await api.GraphicsCards.list();
            runInAction(() => {
                graphicsCards.forEach((graphicsCard) => {
                    this.graphicsCardRegistry.set(graphicsCard.id, graphicsCard);
                })
            })
        } catch (error) {
            console.log(error);
        }
    }

    @action createGraphicsCard = (graphicsCard: GraphicsCard) => {
        try {
            api.GraphicsCards.create(graphicsCard)
                .then(() => {
                    this.graphicsCardRegistry.set(graphicsCard.id, graphicsCard);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action editGraphicsCard = (graphicsCard: GraphicsCard) => {
        try {
            api.GraphicsCards.update(graphicsCard)
                .then(() => {
                    this.graphicsCardRegistry.set(graphicsCard.id, graphicsCard);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action deleteGraphicsCard = (id: number) => {
        try {
            api.GraphicsCards.delete(id)
                .then(() => {
                    this.graphicsCardRegistry.delete(id);
                })
        } catch (error) {
            console.log(error);
        }
    }

    @action selectGraphicsCard = (id: number) => {
        this.currentGraphicsCard = this.graphicsCardRegistry.get(id);
    }

    @action clear = () => {
        this.currentGraphicsCard = null;
    }
}

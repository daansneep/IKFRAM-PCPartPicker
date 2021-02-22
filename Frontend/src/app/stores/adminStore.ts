import {Admin, AdminFormValues} from "../models/Admin";
import {action, computed, observable, runInAction} from "mobx";
import {api} from "../api/agent";
import {RootStore} from "./rootStore";

export default class AdminStore {
    rootStore: RootStore;

    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable user: Admin | null = null;

    @computed get isLoggedIn() { return !!this.user }

    @action login = async (values: AdminFormValues) => {
        try {
            const user = await api.Admins.login(values);
            runInAction(() => {
                this.user = user;
                console.log(user);
            })
        } catch (error) {
            console.log(error)
        }
    }
}

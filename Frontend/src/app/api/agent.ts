import axios, {AxiosResponse} from 'axios';
import {Part} from '../models/Part';
import {Socket} from '../models/Socket';
import {RamType} from "../models/RamType";
import {FormFactor} from "../models/FormFactor";
import {Case} from "../models/Case";
import {GraphicsCard} from "../models/GraphicsCard";
import {Motherboard} from "../models/Motherboard";
import {OperatingSystem} from "../models/OperatingSystem";
import {PowerSupply} from "../models/PowerSupply";
import {Processor} from "../models/Processor";
import {ProcessorCooler} from "../models/ProcessorCooler";
import {Ram} from "../models/Ram";
import {StorageDevice} from "../models/StorageDevice";
import {Admin, AdminFormValues} from "../models/Admin";

axios.defaults.baseURL = 'http://localhost:5000/api';

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.interceptors.response.use(response => {
    return sleep(500).then(() => {
        return response;
    }).catch((error) => {
        console.log(error);
        return Promise.reject(error);
    })
})

axios.interceptors.response.use(undefined, error => {
    const statusCode = error.response.status;
    if (error.message === "Network Error" && !error.response) {
        console.log("Network error - make sure the backend server is running")
    } else if (statusCode >= 500) {
        console.log("Internal server error occured: ")
        console.log(error.response);
    } else if (statusCode >= 400) {
        console.log("Requested resource is either not available or invalid: ")
        console.log(error.response);
    } else if (statusCode >= 300) {
        console.log("Requested resource is no longer available here, redirecting: ")
        console.log(error.response);
    }
})

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    del: (url: string) => axios.delete(url).then(responseBody)
}

const Parts = {
    list: (): Promise<Part[]> => requests.get('/parts'),
    details: (id: number): Promise<Part> => requests.get(`parts/${id}`),
    create: (part: Part): Promise<void> => requests.post(`/parts`, part),
    update: (part: Part): Promise<void> => requests.put(`/parts/${part.partId}`, part),
    delete: (id: number): Promise<void> => requests.del(`parts/${id}`)
}

const Sockets = {
    list: (): Promise<Socket[]> => requests.get('/sockets'),
    details: (id: number): Promise<Socket> => requests.get(`sockets/${id}`),
    create: (socket: Socket): Promise<void> => requests.post(`/sockets`, socket),
    update: (socket: Socket): Promise<void> => requests.put(`/sockets/${socket.socketId}`, socket),
    delete: (id: number): Promise<void> => requests.del(`sockets/${id}`)
}

const RamTypes = {
    list: (): Promise<RamType[]> => requests.get('/ramtypes'),
    details: (id: number): Promise<RamType> => requests.get(`ramtypes/${id}`),
    create: (ramType: RamType): Promise<void> => requests.post(`/ramtypes`, ramType),
    update: (ramType: RamType): Promise<void> => requests.put(`/ramtypes/${ramType.ramTypeId}`, ramType),
    delete: (id: number): Promise<void> => requests.del(`ramtypes/${id}`)
}

const FormFactors = {
    list: (): Promise<FormFactor[]> => requests.get('/parts'),
    details: (id: number): Promise<FormFactor> => requests.get(`formfactors/${id}`),
    create: (formFactor: FormFactor): Promise<void> => requests.post(`/formfactors`, formFactor),
    update: (formFactor: FormFactor): Promise<void> => requests.put(`/formfactors/${formFactor.formFactorId}`, formFactor),
    delete: (id: number): Promise<void> => requests.del(`formfactors/${id}`)
}

const Cases = {
    list: (): Promise<Case[]> => requests.get('/cases'),
    details: (id: number): Promise<Case> => requests.get(`cases/${id}`),
    create: (Pcase: Case): Promise<void> => requests.post(`/cases`, Pcase),
    update: (Pcase: Case): Promise<void> => requests.put(`/cases/${Pcase.id}`, Pcase),
    delete: (id: number): Promise<void> => requests.del(`cases/${id}`)
}

const GraphicsCards = {
    list: (): Promise<GraphicsCard[]> => requests.get('/graphicscards'),
    details: (id: number): Promise<GraphicsCard> => requests.get(`graphicscards/${id}`),
    create: (graphicsCard: GraphicsCard): Promise<void> => requests.post(`/graphicscards`, graphicsCard),
    update: (graphicsCard: GraphicsCard): Promise<void> => requests.put(`/graphicscards/${graphicsCard.id}`, graphicsCard),
    delete: (id: number): Promise<void> => requests.del(`graphicscards/${id}`)
}

const Motherboards = {
    list: (): Promise<Motherboard[]> => requests.get('/motherboards'),
    details: (id: number): Promise<Motherboard> => requests.get(`motherboards/${id}`),
    create: (motherboard: Motherboard): Promise<void> => requests.post(`/motherboards`, motherboard),
    update: (motherboard: Motherboard): Promise<void> => requests.put(`/motherboards/${motherboard.id}`, motherboard),
    delete: (id: number): Promise<void> => requests.del(`motherboards/${id}`)
}

const OperatingSystems = {
    list: (): Promise<OperatingSystem[]> => requests.get('/operatingsystems'),
    details: (id: number): Promise<OperatingSystem> => requests.get(`operatingsystems/${id}`),
    create: (operatingSystem: OperatingSystem): Promise<void> => requests.post(`/operatingsystems`, operatingSystem),
    update: (operatingSystem: OperatingSystem): Promise<void> => requests.put(`/operatingsystems/${operatingSystem.id}`, operatingSystem),
    delete: (id: number): Promise<void> => requests.del(`operatingsystems/${id}`)
}

const PowerSupplies = {
    list: (): Promise<PowerSupply[]> => requests.get('/powersupplies'),
    details: (id: number): Promise<PowerSupply> => requests.get(`powersupplies/${id}`),
    create: (powerSupply: PowerSupply): Promise<void> => requests.post(`/powersupplies`, powerSupply),
    update: (powerSupply: PowerSupply): Promise<void> => requests.put(`/powersupplies/${powerSupply.id}`, powerSupply),
    delete: (id: number): Promise<void> => requests.del(`powersupplies/${id}`)
}

const Processors = {
    list: (): Promise<Processor[]> => requests.get('/processors'),
    details: (id: number): Promise<Processor> => requests.get(`processors/${id}`),
    create: (processor: Processor): Promise<void> => requests.post(`/processors`, processor),
    update: (processor: Processor): Promise<void> => requests.put(`/processors/${processor.id}`, processor),
    delete: (id: number): Promise<void> => requests.del(`processors/${id}`)
}

const ProcessorCoolers = {
    list: (): Promise<ProcessorCooler[]> => requests.get('/processorcoolers'),
    details: (id: number): Promise<ProcessorCooler> => requests.get(`processorcoolers/${id}`),
    create: (processorCooler: ProcessorCooler): Promise<void> => requests.post(`/processorcoolers`, processorCooler),
    update: (processorCooler: ProcessorCooler): Promise<void> => requests.put(`/processorcoolers/${processorCooler.id}`, processorCooler),
    delete: (id: number): Promise<void> => requests.del(`processorcoolers/${id}`)
}
const Rams = {
    list: (): Promise<Ram[]> => requests.get('/rams'),
    details: (id: number): Promise<Ram> => requests.get(`rams/${id}`),
    create: (ram: Ram): Promise<void> => requests.post(`/rams`, ram),
    update: (ram: Ram): Promise<void> => requests.put(`/rams/${ram.id}`, ram),
    delete: (id: number): Promise<void> => requests.del(`rams/${id}`)
}

const StorageDevices = {
    list: (): Promise<StorageDevice[]> => requests.get('/storagedevices'),
    details: (id: number): Promise<StorageDevice> => requests.get(`storagedevices/${id}`),
    create: (storageDevice: StorageDevice): Promise<void> => requests.post(`/storagedevices`, storageDevice),
    update: (storageDevice: StorageDevice): Promise<void> => requests.put(`/storagedevices/${storageDevice.id}`, storageDevice),
    delete: (id: number): Promise<void> => requests.del(`storagedevices/${id}`)
}

const Admins = {
    current: (): Promise<Admin> => requests.get('/user'),
    login: (user: AdminFormValues): Promise<Admin> => requests.post('/users/login', user),
    register: (user: AdminFormValues): Promise<Admin> => requests.post('users/register', user)
}

export const api = {
    Parts,
    RamTypes,
    Sockets,
    FormFactors,
    Cases,
    GraphicsCards,
    Motherboards,
    OperatingSystems,
    PowerSupplies,
    Processors,
    ProcessorCoolers,
    Rams,
    StorageDevices,
    Admins
}

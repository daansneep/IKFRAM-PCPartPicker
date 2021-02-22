import {Part} from "./Part";
import {Socket} from "./Socket";

export interface Processor {
    id: number;
    part: Part;
    socket: Socket;
    cores: number;
    threads: number;
    clockFreq: number;
    turboFreq: number;
    oc: boolean;
    graph: boolean;
}

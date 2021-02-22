import {Part} from "./Part";
import {Socket} from "./Socket";

export interface ProcessorCooler {
    id: number;
    part: Part;
    socket: Socket;
    rgb: boolean;
    water: boolean;
}

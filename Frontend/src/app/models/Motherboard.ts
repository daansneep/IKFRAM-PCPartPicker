import {Part} from "./Part";
import {FormFactor} from "./FormFactor";
import {Socket} from "./Socket";
import {RamType} from "./RamType";

export interface Motherboard {
    id: number;
    part: Part;
    socket: Socket;
    formFactor: FormFactor
    ramType: RamType;
    chipset: string;
    oc: boolean;
    rgb: boolean;
}

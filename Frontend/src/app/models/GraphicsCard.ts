import {Part} from "./Part";

export interface GraphicsCard {
    id: number;
    part: Part;
    clockFreq: number;
    gb: number;
    ramType: string;
    crossSli: boolean;
    rgb: boolean;
}

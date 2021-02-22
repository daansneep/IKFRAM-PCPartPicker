import {Part} from "./Part";
import {RamType} from "./RamType";

export interface Ram {
    id: number;
    part: Part;
    ramType: RamType;
    gb: number;
    stickCount: number;
    clockFreq: number;
    rgb: boolean;
}

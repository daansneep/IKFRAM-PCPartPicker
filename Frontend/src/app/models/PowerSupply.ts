import {Part} from "./Part";

export interface PowerSupply {
    id: number;
    part: Part;
    power: number;
    modular: boolean;
    powerRating: string;
}

import {Part} from "./Part";

export interface OperatingSystem {
    id: number;
    part: Part;
    size: number;
    openSource: boolean;
}

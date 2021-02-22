import {Part} from "./Part";

export interface StorageDevice {
    id: number;
    part: Part;
    gb: number;
    tb: number;
    ssd: boolean;
}

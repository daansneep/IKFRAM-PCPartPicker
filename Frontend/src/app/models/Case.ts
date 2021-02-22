import {Part} from "./Part";
import {FormFactor} from "./FormFactor";

export interface Case {
    id: number;
    part: Part;
    formFactor: FormFactor;
}

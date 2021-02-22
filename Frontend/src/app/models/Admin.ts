export interface Admin {
    username: string;
    token: string;
}


export interface AdminFormValues {
    email: string;
    password: string;
    username?: string;
}

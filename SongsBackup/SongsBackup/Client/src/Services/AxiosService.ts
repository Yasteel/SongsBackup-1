import axios, { AxiosInstance, AxiosResponse } from "axios";

class AxiosService{
    
    private apiClient: AxiosInstance;
    
    constructor() {
        this.apiClient = axios.create({
            baseURL: "https://localhost:44372",
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }
    
    public async getSomething(endpoint: string): Promise<AxiosResponse<any>>{
        return await this.apiClient.get(endpoint);
    }
    
    public async Post(endpoint: string, data?: object): Promise<AxiosResponse<any>> {
        return await this.apiClient.post(endpoint, data ?? {});
    }


}


export const axiosService = AxiosService;
import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';

export class ApiClient {
    private baseUrl: string;
    private token: string | null = null;
    private authObject: any;

    constructor(baseUrl: string, authObject: any) {
        if (baseUrl === '') throw new Error('Base URL is empty');
        this.baseUrl = 'https://localhost:7213/api';
        this.authObject = authObject;
        this.token = document.cookie;
    }

    private async refreshToken() {
        try {
            const response = await axios.post(`${this.baseUrl}/Auth/RefreshToken`, this.authObject);
            this.token = response.data.token;
        } catch (error: any) {
            throw new Error('Error refreshing token: ' + error.message);
        }
    }

    private async request<T>(
        method: string,
        url: string,
        data?: any,
        config?: AxiosRequestConfig
    ): Promise<T> {
        const headers: any = {
            'Content-Type': 'application/json',
        };

        if (this.token) {
            headers['Authorization'] = `Bearer ${this.token}`;
        }

        const axiosConfig: AxiosRequestConfig = {
            method,
            url: `${this.baseUrl}${url}`,
            data,
            headers,
            ...config,
        };

        try {
            const response: AxiosResponse<T> = await axios(axiosConfig);
            return response.data;
        } catch (error: any) {
            const statusCodeToRefreshToken = [401, 403]
            if (error.response && statusCodeToRefreshToken.includes(error.response.status)) {
                // Token expirado, tentar renovar o token
                await this.refreshToken();
                // Tentar a chamada novamente com o novo token
                const responseOnError = await this.request<T>(method, url, data, config);
                return responseOnError;
            } else {
                // Tratar outros erros aqui
                throw error;
            }
        }
    }

    public async post<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
        return await this.request<T>('post', url, data, config);
    }

    public async get<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
        return await this.request<T>('get', url, undefined, config);
    }
}
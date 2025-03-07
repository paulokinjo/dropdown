import axios from 'axios';

const api = axios.create({
    baseURL: process.env.NEXT_PUBLIC_BACKEND_API_URL || 'http://localhost:5179/api',
});

// Add request interceptor to attach Authorization token
api.interceptors.request.use(
    (config) => {
        const token = process.env.NEXT_PUBLIC_BACKEND_API_CLIENT_ID;
        if (token) {
            config.headers['x-client-id'] = `${token}`;
        }
        return config;
    },
    (error) => {
        if (error.response && error.response.status === 401) {
            // e.g., clear token, redirect to login page
            console.log(error.response);
        }
        return Promise.reject(error);
    }
);

export default api;
import axios from 'axios';
const BASE_URL = 'http://localhost:5001/api/';

const instance = axios.create({
    baseURL: BASE_URL,
    headers: { 'Content-Type': 'application/json' },
    withCredentials: true,
});

instance.interceptors.response.use(function (response) {
    return response;
}, function (error) {
    console.log(error);
    if (typeof error.response.data.errors !== 'undefined') {
        console.error(error.response.data.errors);
    } else {
        console.error(error.response.data.ErrorMessage)
    }

    return Promise.reject(error);
});

export default instance;
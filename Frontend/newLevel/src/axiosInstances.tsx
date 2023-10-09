import axios from 'axios'

const axiosInstance = axios.create({
    baseURL: 'https://localhost:7213/api'
})

export default axiosInstance
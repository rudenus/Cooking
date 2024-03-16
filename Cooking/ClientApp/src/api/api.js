import axios from "axios";

const api= axios.create({
    baseURL:"https://localhost:7248"
})

export default api
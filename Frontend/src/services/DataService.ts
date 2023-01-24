
import axios from "axios";
import { Bug, User } from '../interfaces/dtos';

const baseUrl = 'https://localhost:7104';

const getAllBugs = async function(): Promise<Bug[]> {
    const response = await axios.get(`${baseUrl}/bugList`);
    return response.data;
}

const getBugByPublicId = async function(publicId: string): Promise<Bug>{
    const response = await axios.get(`${baseUrl}/bug?publicId=${publicId}`);
    return response.data;
}

const createUser = async function(name: string): Promise<User>{
    const response = await axios.post(`${baseUrl}/user`, JSON.stringify(name), {headers:{"Content-Type" : "application/json"}}); 
    return response.data;
}

export default {
     getAllBugs: getAllBugs,
     getBugByPublicId: getBugByPublicId,
     createUser: createUser
}
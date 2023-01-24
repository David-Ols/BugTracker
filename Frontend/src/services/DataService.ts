
import axios from "axios";
import { Bug, User, CreateBug, BugStatusUpdate } from '../dtos/dtos';

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
    const response = await axios.post(
        `${baseUrl}/user`, 
        JSON.stringify(name), 
        {headers:{"Content-Type" : "application/json"}}
    ); 
    return response.data;
}

const getAllUsers = async function(): Promise<User[]>{
    const response = await axios.get<User[]>(`${baseUrl}/user`); 
    return response.data;
}

const updateUser = async function(user: User): Promise<boolean>{
    const response = await axios.put<boolean>(
        `${baseUrl}/user`, 
        JSON.stringify(user), 
        {headers:{"Content-Type" : "application/json"}}
    ); 
    return response.data;
}

const createBug = async function(bug: CreateBug): Promise<Bug>{
    const response = await axios.post<Bug>(
        `${baseUrl}/bug`, 
        JSON.stringify(bug), 
        {headers:{"Content-Type" : "application/json"}}
    ); 
    return response.data;
}

const updateBugStatus = async function(request: BugStatusUpdate): Promise<boolean>{
    const response = await axios.put<boolean>(
        `${baseUrl}/bugStatus`, 
        JSON.stringify(request), 
        {headers:{"Content-Type" : "application/json"}}
    ); 
    return response.data;
}

export default {
     getAllBugs: getAllBugs,
     getBugByPublicId: getBugByPublicId,
     createUser: createUser,
     getAllUsers: getAllUsers,
     updateUser: updateUser,
     createBug: createBug,
     updateBugStatus : updateBugStatus
}
import axios from "axios";
import { Bug, User, CreateBug, BugStatusUpdate, BugUpdate } from '../dtos/dtos';
import {Config} from '../config/config.interface'

const config: Config = require('../config/config.json');

const getAllBugs = async function(): Promise<Bug[]> {
    const response = await axios.get(`${config.BackendBaseUrl}/bugList`);
    return response.data;
}

const getBugByPublicId = async function(publicId: string): Promise<Bug>{
    const response = await axios.get(`${config.BackendBaseUrl}/bug?publicId=${publicId}`);
    return response.data;
}

const createUser = async function(name: string): Promise<User>{
    const response = await axios.post(
        `${config.BackendBaseUrl}/user`, 
        JSON.stringify(name), 
        {headers:{"Content-Type" : "application/json"}}
    ); 
    return response.data;
}

const getAllUsers = async function(): Promise<User[]>{
    const response = await axios.get<User[]>(`${config.BackendBaseUrl}/user`); 
    return response.data;
}

const updateUser = async function(user: User): Promise<boolean>{
    const response = await axios.put<boolean>(
        `${config.BackendBaseUrl}/user`, 
        JSON.stringify(user), 
        {headers:{"Content-Type" : "application/json"}}
    ); 
    return response.data;
}

const createBug = async function(bug: CreateBug): Promise<Bug>{
    const response = await axios.post<Bug>(
        `${config.BackendBaseUrl}/bug`, 
        JSON.stringify(bug), 
        {headers:{"Content-Type" : "application/json"}}
    ); 
    return response.data;
}

const updateBugStatus = async function(request: BugStatusUpdate): Promise<boolean>{
    const response = await axios.put<boolean>(
        `${config.BackendBaseUrl}/bugStatus`, 
        JSON.stringify(request), 
        {headers:{"Content-Type" : "application/json"}}
    ); 
    return response.data;
}

const updateBug = async function(request: BugUpdate): Promise<boolean>{
    const response = await axios.put<boolean>(
        `${config.BackendBaseUrl}/bug`, 
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
     updateBugStatus : updateBugStatus,
     updateBug : updateBug
}
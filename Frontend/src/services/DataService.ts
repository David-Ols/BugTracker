import Bug from "../components/BugList.vue";
import axios from "axios";

const baseUrl = 'https://localhost:7104';

const getAllBugs = async function(): Promise<typeof Bug[]> {
    const response = await axios.get(`${baseUrl}/bug`);
    console.log("from service", response);
    return response.data;
}

const getBugByPublicId = async function(publicId: string): Promise<typeof Bug>{
    const response = await axios.get(`${baseUrl}/bug/?id=${publicId}`);
    console.log("from service", response);
    return response.data;
}

export default {
     getAllBugs: getAllBugs,
     getBugByPublicId: getBugByPublicId
}
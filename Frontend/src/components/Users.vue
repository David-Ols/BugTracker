<template>
    <div>
        <h1>Users</h1>

        <v-data-table :headers="headers" :items="users" class="elevation-1">
            <template v-slot:item="row">
                <tr>
                    <td>{{row.item.name}}</td>
                    <td>
                        <v-btn 
                            @click="editRow(row.item)" 
                            :disabled="edit"
                        >
                            Edit
                        </v-btn>
                    </td>
                </tr>
            </template>
        </v-data-table>

        <EditUser
            v-if="edit"
            :name="userToEdit.name"
            :id="userToEdit.id"
            @cancelEdit="cancelEdit"
        />
    </div>
  </template>   
  
<script lang="ts">
import Vue from 'vue';
import { User } from '../dtos/dtos';
import dataService from "../services/DataService";
import EditUser from "./EditUser.vue"

export default Vue.extend({
name: 'Users',
components: {
    EditUser
},
created(){
    this.loadData();
},
data: function() {
    return {
    headers: [
        { text: 'Name', value: 'name' },
        { text: 'Actions', value: '' },
    ],
    users: <User[]>[],
    edit: false,
    userToEdit: <User>{}
    }
},
methods:{
    editRow(item: User){
        this.userToEdit = item;
        this.edit = true;
    },
    async loadData(){
        const response = await dataService.getAllUsers();
        this.users = response;
    },
    cancelEdit(){
        this.edit = false;
        this.loadData();
    }
}
});
</script>
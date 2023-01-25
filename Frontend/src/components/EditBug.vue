<template>
    <div>
      <v-alert
        v-model="alert.showAlert"
        :type="alert.alertType"
        dismissible
      >{{ alert.alertMessage }}</v-alert>
  
      <h1>Edit Bug</h1>
  
      <form>
        <v-text-field
          v-model="bug.title"
          label="Title"
          required
        ></v-text-field>
        <v-text-field
          v-model="bug.description"
          label="Description"
          required
        ></v-text-field>
        <v-select
          :items="users"
          label="User"
          item-text="name"
          item-value="id"
          v-model="bug.userId"
        ></v-select>
        <v-select
          :items="statuses"
          label="Status"
          v-model="bug.status"
        ></v-select>
        <v-btn class="mr-4" @click="submit">Edit</v-btn>
        <v-btn class="mr-4" @click="cancel">Cancel</v-btn>
      </form>
    </div>
  </template>   
  
  <script lang="ts">
  import Vue from 'vue';
  import { User, Alert, BugUpdate } from '../dtos/dtos';
  import dataService from "../services/DataService";
  import { BugStatus, AlertType } from '@/enums/enums';
  
  export default Vue.extend({
    name: 'EditUser',
    props: {
      status: {
        type: String,
        default: '',
      },
      userId: {
        type: String,
        default: '',
      },
      title: {
        type: String,
        default: '',
      },
      description: {
        type: String,
        default: '',
      },
      bugId: {
        type: String,
        default: '',
      },
    },
    created(){
        this.init();
        this.loadUsers();
    },
    data: function() {
        return {
            alert: <Alert>{ showAlert: false},
            bug: <BugUpdate>{},
            statuses: <string[]>[],
            users: <User[]>[]
        }
    },
    methods:{
        async submit(){
            const isUpdated = await dataService.updateBug(this.bug);
            if(isUpdated){
                this.cancel();
            }else{
                this.displayAlert(AlertType.error, `Failed to update bug.`);
            }
        },
        cancel(){
            this.$emit('cancelEdit');
        },
        displayAlert(type: AlertType, message: string){
            this.alert.alertType = type;
            this.alert.alertMessage = message;
            this.alert.showAlert = true;
        },
        async loadUsers(){
            this.users = await dataService.getAllUsers();
            this.users.push(<User>{id: null, name: "---No User---"});
        },
        init(){
            this.bug.status = this.status;
            this.bug.userId = this.userId;
            this.bug.title = this.title;
            this.bug.description = this.description;
            this.bug.bugId = this.bugId;
            this.statuses = [BugStatus.open, BugStatus.closed];
        }
    }
  });
  </script>
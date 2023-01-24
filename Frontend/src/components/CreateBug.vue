<template>
  <div>
    <v-alert
      v-model="alert.showAlert"
      :type="alert.alertType"
      dismissible
    >{{ alert.alertMessage }}</v-alert>
    <h1>Create Bug</h1>
    <form>
      <v-text-field
        v-model="newBug.title"
        label="Title"
        required
      ></v-text-field>
      <v-text-field
        v-model="newBug.description"
        label="Description"
        required
      ></v-text-field>
      <v-select
          :items="users"
          label="User"
          item-text="name"
          item-value="id"
          v-model="newBug.userId"
        ></v-select>
      <v-btn class="mr-4" @click="submit">submit</v-btn>
      <v-btn @click="clear">clear</v-btn>
    </form>
  </div>
</template>   

<script lang="ts">
import Vue from 'vue';
import { AlertType, BugStatus } from '@/enums/enums';
import { Bug, User, CreateBug } from '../dtos/dtos';
import dataService from "../services/DataService";

declare interface Alert{
    showAlert: boolean,
    alertType: AlertType,
    alertMessage: string
}

export default Vue.extend({
  name: 'CreateBug',
  created(){
    this.loadUsers();
  },
  data: function() {
    return {
      alert: <Alert>{ showAlert: false},
      newBug: <CreateBug>{},
      users: <User[]>[]
    }
  },
  methods:{
    async submit(){
      const bug = await dataService.createBug(this.newBug);
      if(bug){
        this.displayAlert(AlertType.success, `Bug ${bug.title} created.`);
        this.clear();
      }else{
        this.displayAlert(AlertType.error, "Failed to create bug.");
      }
    },
    clear(): void{
      this.newBug.title = '';
      this.newBug.description = '';
      this.newBug.userId = '';
    },
    async loadUsers(){
      this.users = await dataService.getAllUsers();
    },
    displayAlert(type: AlertType, message: string){
      this.alert.alertType = type;
      this.alert.alertMessage = message;
      this.alert.showAlert = true;
    }
  }
});
</script>
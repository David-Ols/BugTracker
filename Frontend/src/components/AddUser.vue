<template>
  <div>
    <v-alert
      v-model="showAlert"
      :type="alertType"
      dismissible
    >{{ alertMessage }}</v-alert>
    <h1>Add User</h1>
    <form>
      <v-text-field
        v-model="name"
        label="Name"
        required
      ></v-text-field>
      <v-btn class="mr-4" @click="submit">submit</v-btn>
      <v-btn @click="clear">clear</v-btn>
    </form>
  </div>
</template>   

<script lang="ts">
import Vue from 'vue';
import dataService from "../services/DataService";
import { AlertType } from '@/enums/enums';


export default Vue.extend({
  name: 'AddUser',
  data: function() {
    return {
      name: '',
      showAlert: false,
      alertType: AlertType.success,
      alertMessage: ''
    }
  },
  methods:{
    async submit(){
      const user = await dataService.createUser(this.name);
      if(user){
        this.displayAlert(AlertType.success, `User ${this.name} created.`);
        this.clear();
      }else{
        this.displayAlert(AlertType.error, `Failed to create user ${this.name}.`);
      }
    },
    clear(): void{
      this.name = '';
    },
    displayAlert(type: AlertType, message: string){
      this.alertType = type;
      this.alertMessage = message;
      this.showAlert = true;
    }
  }
});
</script>
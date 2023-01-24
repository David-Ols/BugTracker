<template>
  <div>
    <v-alert
      v-model="showAlert"
      :type="alertType"
      dismissible
    >{{ alertMessage }}</v-alert>

    <h1>Edit User</h1>

    <form>
      <v-text-field
        v-model="user.name"
        label="Name"
        required
      ></v-text-field>
      <v-btn class="mr-4" @click="submit">Edit</v-btn>
      <v-btn class="mr-4" @click="cancel">Cancel</v-btn>
    </form>
  </div>
</template>   

<script lang="ts">
import Vue from 'vue';
import { User } from '../dtos/dtos';
import dataService from "../services/DataService";
import { AlertType } from '@/enums/enums';

export default Vue.extend({
  name: 'EditUser',
  props: {
    name: {
      type: String,
      default: '',
    },
    id: {
      type: String,
      default: '',
    }
  },
  created(){
    this.user.id = this.id;
    this.user.name = this.name;
  },
  data: function() {
      return {
        showAlert: false,
        alertType: AlertType.success,
        alertMessage: '',
        user: <User>{}
      }
  },
  methods:{
    async submit(){
      const isUpdated = await dataService.updateUser(this.user);
      if(isUpdated){
        this.cancel();
      }else{
        this.displayAlert(AlertType.error, `Failed to update user ${this.name}.`);
      }
    },
    cancel(){
      this.$emit('cancelEdit');
    },
    displayAlert(type: AlertType, message: string){
      this.alertType = type;
      this.alertMessage = message;
      this.showAlert = true;
    }
  }
});
</script>
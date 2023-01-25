<template>
  <div>
    <v-alert
      v-model="alert.showAlert"
      :type="alert.alertType"
      dismissible
    >{{ alert.alertMessage }}</v-alert>
    <v-container class="grey lighten-5 mt-5">
    <v-row>
      <v-col cols="12"><h1>{{bug.title}}</h1></v-col>
    </v-row>
    <v-row>
      <v-col cols="8">        
        <v-card
          class="pa-2 maxHeight"
          tile
        >
          <h3>Description</h3>
          <p>{{ bug.description }}</p>
        </v-card>
      </v-col>
  
      <v-col cols="4">        
        <v-card
          class="pa-2"
          
          tile
        >
          <h3>Details</h3>

          <v-row>
            <v-col cols="6">Status</v-col>
            <v-col cols="6">{{ bug.status }}</v-col>
          </v-row>
          <v-row>
            <v-col cols="6">User</v-col>
            <v-col cols="6">{{ bug.assigneeName }}</v-col>
          </v-row>
          <v-row>
            <v-col cols="6">Opened On</v-col>
            <v-col cols="6">{{ bug.openedOn }}</v-col>
          </v-row>
          <v-row>
            <v-col cols="4">Action</v-col>
            <v-col cols="4"> <v-btn @click="closeBug" :disabled="!canCloseBug">Close</v-btn></v-col>
            <v-col cols="4"> <v-btn @click="startEdit" :disabled="editBug">Edit</v-btn></v-col>
          </v-row>
    
        </v-card>
      </v-col>
  
    </v-row>
    </v-container>

    <EditBug
      v-if="editBug"
      :status="bug.status"
      :userId="bug.userId"
      :title="bug.title"
      :description="bug.description"
      :bugId="bug.id"
      @cancelEdit="cancelEdit"
    />
  </div>
</template>   

<script lang="ts">
import Vue from 'vue';
import dataService from "../services/DataService";
import { Bug, Alert, BugStatusUpdate } from '../dtos/dtos';
import { BugStatus } from '@/enums/enums';
import { AlertType } from '@/enums/enums';
import EditBug from "./EditBug.vue"

export default Vue.extend({
  name: 'Bug',
  components:{
    EditBug
  },
  async created(){
    this.loadBug();
  },
  data: function() {
    return {
      bug: <Bug>{},
      alert: <Alert>{ showAlert: false},
      editBug: false,
    }
  },
  methods:{
    async closeBug(){
      const request: BugStatusUpdate = {
        bugId: this.bug.id,
        status: BugStatus.closed
      };

      const isClosed = await dataService.updateBugStatus(request);

      if(isClosed){
        this.displayAlert(AlertType.success, "Bug was closed.");
        this.loadBug();
      }else{
        this.displayAlert(AlertType.error, "Failed to close bug.");
      }
    },
    async loadBug(){
      const response = await dataService.getBugByPublicId(this.$route.params.publicId);
      this.bug = response;
    },
    displayAlert(type: AlertType, message: string){
      this.alert.alertType = type;
      this.alert.alertMessage = message;
      this.alert.showAlert = true;
    },
    startEdit(){
      this.editBug = true;
    },
    cancelEdit(){
      this.displayAlert(AlertType.success, "Bug was updated.");
      this.editBug = false;
      this.loadBug();
    }
  },
  computed: {
    canCloseBug() :boolean{
      return this.bug.status === BugStatus.open;
    }
  }
});
</script>

<style scoped>
.maxHeight {
  height: 100%;;
}
</style>
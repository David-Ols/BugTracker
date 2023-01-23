<template>
  <v-container class="grey lighten-5 mt-5">
    <v-row>
      <v-col cols="12"><h1>{{bug.title}}</h1></v-col>
    </v-row>
    <v-row no-gutters>
      <v-col cols="8">        
        <v-card
          class="pa-2"
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
    
    
        </v-card>
      </v-col>
  
    </v-row>
  </v-container>
</template>   

<script lang="ts">
import Vue from 'vue';
import dataService from "../services/DataService";
import { Bug } from '../interfaces/dtos';

export default Vue.extend({
  name: 'Bug',
  async created(){
    console.log("from bug", this.$route.params.publicId);
     const response = await dataService.getBugByPublicId(this.$route.params.publicId);
     console.log("bug response",response);
     this.bug = response as Bug;
  },
  data: function() {
    return {
      bug: {} as Bug
    }
  },
});
</script>
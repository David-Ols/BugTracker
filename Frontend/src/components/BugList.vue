<template>
    <div>
        <h1>Bug List</h1>
        <v-data-table
          :headers="headers"
          :items="bugs"
          :items-per-page="5"
          class="elevation-1"
          @click:row="handleRowClick"
        ></v-data-table>

    </div>
</template>   

<script lang="ts">
import Vue from 'vue';
import dataService from "../services/DataService";
import { Bug } from '../dtos/dtos';


export default Vue.extend({
  name: 'BugList',
  created(){
    this.loadData();
  },
  data: function() {
    return {
      headers: [
        { text: 'PublicId', value: 'publicId' },
        { text: 'Title', value: 'title' },
      ],
      bugs: <Bug[]>[],
    };
  },
  methods:{
    handleRowClick(item: Bug) {
      this.$router.push({ path: `/bug/${item.publicId}`});
    },
    async loadData(){
      const response = await dataService.getAllBugs();
      this.bugs = response;
    }
  }
});
</script>
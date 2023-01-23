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
import { Bug } from '../interfaces/dtos';


export default Vue.extend({
  name: 'BugList',
  created(){
    this.loadData();
  },
  data: function() {
    return {
      headers: [
        { text: 'PublicId', value: 'publicId' },
        { text: 'Status', value: 'status' },
        { text: 'Title', value: 'title' },
        { text: 'User', value: 'assigneeName' },
        { text: 'Opened On', value: 'openedOn' },
      ],
      bugs: [
        {
          publicId: 'Bug-1',
          status: "Open",
          title: "Title",
          assigneeName: "David",
          openedOn: "01.01.2012"
        },
        {
          publicId: 'Bug-2',
          status: "Open",
          title: "Title",
          assigneeName: "David",
          openedOn: "02.01.2012"
        },
      ],
    };
  },
  methods:{
    handleRowClick(item: Bug) {
      this.$router.push({ path: `/bug/${item.publicId}`});
    },
    async loadData(){
      const response = await dataService.getAllBugs();
      console.log(response);
      this.bugs = response as [];
    }
  }
});
</script>
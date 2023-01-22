<template>
    <div>
        <h1>Bug</h1>
    </div>
</template>   

<script lang="ts">
import Vue from 'vue';
import dataService from "../services/DataService";

export default Vue.extend({
  name: 'Bug',
  async created(){
    console.log("from bug", this.$route.params.publicId);
    const response = await dataService.getBugByPublicId(this.$route.params.publicId);
  },
});
</script>
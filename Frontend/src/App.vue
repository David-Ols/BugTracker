<template>
  <v-app>
    <v-app-bar
      app
      color="primary"
      dark
    >
      <div class="d-flex align-center">
        <h2>BugTracker</h2>


      </div>

  
    </v-app-bar>

    <v-main class="ml-15 mr-15 mt-5">
      <nav class="d-flex justify-center">
      <router-link to="/bugs" class="ml-3 mr-3">BugList</router-link> |
      <router-link to="/createbug" class="ml-3 mr-3">Create Bug</router-link> |
      <router-link to="/adduser" class="ml-3 mr-3">Add User</router-link> |
      <router-link to="/users" class="ml-3 mr-3">Users</router-link> 
    </nav>

      <router-view/>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'App',

  data: () => ({
    //
  }),
});
</script>

<template>
  <div id="app">
    <nav>
      <router-link to="/">Home</router-link> |
      <router-link to="/bugs">BugList</router-link> |
      <router-link to="/newbug">Create Bug</router-link> |
      <router-link to="/bug">Bug</router-link> |
      <router-link to="/adduser">Add User</router-link> |
      <router-link to="/edituser">Edit User</router-link> 
    </nav>
    <router-view/>
  </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
</style>

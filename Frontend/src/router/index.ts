import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import BugList from '../components/BugList.vue'
import CreateBug from '../components/CreateBug.vue'
import Bug from '../components/Bug.vue'
import AddUser from '../components/AddUser.vue'
import EditUser from '../components/EditUser.vue'
import Users from '../components/Users.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    redirect: '/bugs'
  },
  {
    path: '/bugs',
    name: 'bugs',
    component: BugList
  },
  {
    path: '/createbug',
    name: 'createbug',
    component: CreateBug
  },
  {
    path: '/bug/:publicId',
    name: 'bug',
    component: Bug
  },
  {
    path: '/adduser',
    name: 'adduser',
    component: AddUser
  },
  {
    path: '/users',
    name: 'users',
    component: Users
  }
]

const router = new VueRouter({
  routes
})

export default router

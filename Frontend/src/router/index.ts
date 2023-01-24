import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import BugList from '../components/BugList.vue'
import NewBug from '../components/NewBug.vue'
import Bug from '../components/Bug.vue'
import AddUser from '../components/AddUser.vue'
import EditUser from '../components/EditUser.vue'
import Users from '../components/Users.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/bugs',
    name: 'bugs',
    component: BugList
  },
  {
    path: '/newbug',
    name: 'newbug',
    component: NewBug
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

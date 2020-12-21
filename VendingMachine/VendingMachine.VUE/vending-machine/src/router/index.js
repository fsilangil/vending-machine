import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../components/login.vue'
import Shop from '../components/shop.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login
  },
  {
    path: '/shop',
    name: 'Shop',
      component: Shop,
      beforeEnter(to, from, next) {
          if (localStorage.getItem('id') != null) {
              next()
          } else {
              next('/')
          }
      }
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router

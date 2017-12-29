import Vue from 'vue'
import Router from 'vue-router'
import LoginView from '../views/LoginView'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/'
    },
    {
      path: '/login',
      name: 'LoginView',
      component: LoginView
    }
  ]
})

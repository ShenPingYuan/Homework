import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'


Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/login',
    name: 'Login',
    component: () => import('../components/Login.vue'),
  },
  { path: '/', redirect: '/Login' },
  {
    path: '/home',
    name: 'Home',
   
    component: () => import('../components/Home.vue'),
    redirect:{name:'Welcome'},
    children: [
      {
        path: '/welcome',
        name: 'Welcome',
        component: () => import('../components/Welcome.vue'),
      },
      {
        path: '/users',
        name: 'Users',
        component: () => import('../components/user/Users.vue'),
      },
      { path: '/rights', component: () => import('../components/power/Rights.vue'), },
      { path: '/roles', component: () => import('../components/power/Roles.vue'), },
      { path: '/categories', component: () => import('../components/goods/Cate.vue'), },
      { path: '/params', component: () => import('../components/goods/Params.vue'), },
      { path: '/goods', component: () => import('../components/goods/List.vue'), },
      { path: '/goods/add', component: () => import('../components/goods/Add.vue'), },
      { path: '/orders', component: () => import('../components/order/Order.vue'), },
      { path: '/reports', component: () => import('../components/report/Report.vue'), },
    ],
  },
  // {
  //   path: '/',
  //   name: 'Home',
  //   component: Home
  // },
  // {
  //   path: '/about',
  //   name: 'About',
  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  // }
];

const router = new VueRouter({
  mode:'history',
  routes
})
router.beforeEach((to, from, next) => {
  if (to.path === '/login') {
    return next();
  }
  const tokenStr = sessionStorage.getItem('token');
  if (!tokenStr) {
    return next('/login');
  } else {
    return next();
  }
});
export default router

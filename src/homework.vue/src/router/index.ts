import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'
import store from "../store/index";
import {OpenIdConnectService} from "../open-id-connect/OpenIdConnectService";
//const oidc: OpenIdConnectService = OpenIdConnectService.getInstance();

Vue.use(VueRouter)

const routes: Array<RouteConfig> = 
// [
//   {
//     path: '/login',
//     name: 'Login',
//     component: () => import('../components/Login.vue'),
//   },
//   { path: '/', redirect: '/Login' },
//   {
//     path: '/home',
//     name: 'Home',
   
//     component: () => import('../components/Home.vue'),
//     redirect:{name:'Welcome'},
//     children: [
//       {
//         path: '/welcome',
//         name: 'Welcome',
//         component: () => import('../components/Welcome.vue'),
//       },
//       {
//         path: '/users',
//         name: 'Users',
//         component: () => import('../components/user/Users.vue'),
//       },
//       { path: '/rights', component: () => import('../components/power/Rights.vue'), },
//       { path: '/roles', component: () => import('../components/power/Roles.vue'), },
//       { path: '/categories', component: () => import('../components/goods/Cate.vue'), },
//       { path: '/params', component: () => import('../components/goods/Params.vue'), },
//       { path: '/goods', component: () => import('../components/goods/List.vue'), },
//       { path: '/goods/add', component: () => import('../components/goods/Add.vue'), },
//       { path: '/orders', component: () => import('../components/order/Order.vue'), },
//       { path: '/reports', component: () => import('../components/report/Report.vue'), },
//     ],
//   },
// ];
[
  {
    path: '/',
    name: 'loading',
    component: () => import('./views/Loading.vue'),
  },
  {
    path: '/home',
    name: 'home',
    component: () => import('./views/Home.vue'),
  },
  {
    path: '/signin-oidc',
    name: 'signin-oidc',
    component: () => import('./views/SigninOidc.vue'),
  },
  {
    path: '/redirect-silent-renew',
    name: 'redirect-silent-renew',
    component: () => import('./views/RedirectSilentRenew.vue'),
  },
];

const router = new VueRouter({
  mode:'history',
  routes
});
var storeTemp = store;
router.beforeEach((to, from, next) => {
  if (!storeTemp.state.token) {
    storeTemp.commit("saveToken", window.localStorage.Token);
  }
  if (to.meta.requireAuth) {
    // 判断该路由是否需要登录权限
    if (storeTemp.state.token) {
      // 通过vuex state获取当前的token是否存在
      next();
    } else {
      //这里使用Id4授权认证，用Jwt，请删之，并把下边的跳转login 打开；
      OpenIdConnectService.getInstance().triggerSignIn();

      //这里使用Jwt登录，如果不用Id4授权认证，这里打开它；
      // next({
      //   path: "/login",
      //   query: { redirect: to.fullPath } // 将跳转的路由path作为参数，登录成功后跳转到该路由
      // });
    }
  } else {
    next();
  }
});
export default router

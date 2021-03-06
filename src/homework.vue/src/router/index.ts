import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '../views/Home.vue'
import store from "../store/index";
import {OpenIdConnectService} from "../open-id-connect/OpenIdConnectService";
//const oidc: OpenIdConnectService = OpenIdConnectService.getInstance();

Vue.use(VueRouter)

const routes: Array<RouteConfig> = 
[
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
      { path: '/homeworkManager', component: () => import('../components/homework/List.vue'), },
      { path: '/homeworkManager/add', component: () => import('../components/homework/Add.vue'), },
      { path: '/doHomework', component: () => import('../components/DoHomework/List.vue'), },
      { path: '/reports', component: () => import('../components/report/Report.vue'), },
    ],
  },
  {
    path: '/signin-oidc',
    name: 'signin-oidc',
    component: () => import('../views/SigninOidc.vue'),
  },
  {
    path: '/redirect-silent-renew',
    name: 'redirect-silent-renew',
    component: () => import('../views/RedirectSilentRenew.vue'),
  },
];
[
  {
    path: '/',
    name: 'loading',
    component: () => import('../views/Loading.vue'),
  },
  {
    path: '/home',
    name: 'home',
    component: () => import('../views/Home.vue'),
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
    // ???????????????????????????????????????
    if (storeTemp.state.token) {
      // ??????vuex state???????????????token????????????
      next();
    } else {
      //????????????Id4??????????????????Jwt????????????????????????????????????login ?????????
      OpenIdConnectService.getInstance().triggerSignIn();

      //????????????Jwt?????????????????????Id4?????????????????????????????????
      // next({
      //   path: "/login",
      //   query: { redirect: to.fullPath } // ??????????????????path????????????????????????????????????????????????
      // });
    }
  } else {
    next();
  }
});
export default router

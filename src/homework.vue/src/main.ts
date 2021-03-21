import Vue from 'vue';
import App from './App.vue';
import router from './router';
import './plugins/element.js';
import './assets/css/global.css';
import store from './store'
import axios from 'axios';
import VueAxios from 'vue-axios';

Vue.use(VueAxios, axios);
Vue.config.productionTip = false
axios.defaults.baseURL = 'http://timemeetyou.com:8889/api/private/v1/';
axios.interceptors.request.use((config) => {
  config.headers.Authorization = sessionStorage.getItem('token');
  return config;
});
Vue.config.productionTip = false;
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')

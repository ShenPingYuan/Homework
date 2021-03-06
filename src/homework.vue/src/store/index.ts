import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
 // 初始化的数据
  state: {
    formDatas: null,
    token: null
  },
  // 改变state里面的值得方法
  mutations: {
    getFormData(state, data) {
      state.formDatas = data;
    },
    saveToken(state, data) {
      state.token = data;
      window.localStorage.setItem("Token", data);
    }
  },
  actions: {
  },
  modules: {
  }
})

import axios, { AxiosResponse, AxiosRequestConfig } from 'axios';
import { endPoint } from '@/common/Config';
import store from "../store/index";
import { OpenIdConnectService } from '@/open-id-connect/OpenIdConnectService';

const oidc: OpenIdConnectService = OpenIdConnectService.getInstance();

export interface Todo { id: string; title: string; completed: boolean; }

export interface TodoEdit { title: string; completed: boolean; }

// 查询 Todo 列表
export const QueryTodos = (): Promise<Todo[]> => {
    return new Promise<Todo[]>(async (resolve, reject) => {
        try {
            const auth: string = `${oidc.user.token_type} ${oidc.user.access_token}`;
            const requestConfig: AxiosRequestConfig = { url: endPoint.QueryTodos, headers: { Authorization: auth } };
            const res: AxiosResponse<Todo[]> = await axios(requestConfig);
            resolve(res.data);
        } catch (e) {
            reject(e);
        }
    });
};

// 添加 Todo
export const AddTodo = (todo: TodoEdit): Promise<void> => {
    return new Promise<void>(async (resolve, reject) => {
        try {
            await axios({ url: endPoint.AddTodo, method: 'POST', data: todo });
            resolve();
        } catch (e) {
            reject(e);
        }
    });
};

// http request 拦截器
let storeTemp=store;
axios.interceptors.request.use(
  config => {
    console.log(config.params)
    if (storeTemp.state.token) {
      // 判断是否存在token，如果存在的话，则每个http header都加上token
      config.headers.Authorization ="Bearer "+ storeTemp.state.token;
    }
    return config;
  },
  err => {
    return Promise.reject(err);
  }
);
// http response 拦截器
axios.interceptors.response.use(
  response => {
    return response;
  },
  error => {
    if (error.response) {
      switch (error.response.status) {
        case 401:
          // 返回 401 清除token信息并跳转到登录页面
            store.commit("saveToken", "");
            window.localStorage.removeItem("USER_NAME");
            oidc.login();
          //   router.replace({
          //   path: "/login",
          //   query: { redirect: router.currentRoute.fullPath }
          // });
      }
    }
    return Promise.reject(error.response.data); // 返回接口返回的错误信息
  }
);
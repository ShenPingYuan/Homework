import axios, { AxiosResponse, AxiosRequestConfig } from 'axios';
import { endPoint } from '@/common/Config';
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
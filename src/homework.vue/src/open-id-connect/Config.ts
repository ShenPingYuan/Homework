import Oidc from "oidc-client";

export const identityServerBase = 'https://localhost:5000';

export const apiBase = 'https://localhost:5001';

export const vueBase = 'http://localhost:8080';

// 参考文档 https://github.com/IdentityModel/oidc-client-js/wiki
export const openIdConnectSettings = {
    //userStore: new Oidc.WebStorageStateStore() , 
    authority: `${identityServerBase}`,
    client_id: `homework.vue`,
    redirect_uri: `${vueBase}/signin-oidc`,
    post_logout_redirect_uri: `${vueBase}/`,
    silent_redirect_uri: `${vueBase}/redirect-silentrenew`,
    scope: 'openid profile api1 roles',
    response_type: `id_token token`,
    automaticSilentRenew: true,
    loadUserInfo: true,
};
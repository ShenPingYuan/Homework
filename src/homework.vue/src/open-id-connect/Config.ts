export const identityServerBase = 'http://localhost:5000';

export const apiBase = 'http://localhost:5001';

export const vueBase = 'http://localhost:8080';

// 参考文档 https://github.com/IdentityModel/oidc-client-js/wiki
export const openIdConnectSettings = {
    authority: `${identityServerBase}`,
    client_id: `homework.vue`,
    redirect_uri: `${vueBase}/signin-oidc`,
    post_logout_redirect_uri: `${vueBase}/`,
    silent_redirect_uri: `${vueBase}/redirect-silentrenew`,
    scope: 'openid profile api1',
    response_type: `id_token token`,
    automaticSilentRenew: true,
};
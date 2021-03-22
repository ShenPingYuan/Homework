// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResource("roles","角色",new List<string>{ JwtClaimTypes.Role }),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1","First API"),

                // invoice API specific scopes
                new ApiScope(name: "invoice.read",   displayName: "Reads your invoices."),
                new ApiScope(name: "invoice.pay",    displayName: "Pays your invoices."),

                // customer API specific scopes
                new ApiScope(name: "customer.read",    displayName: "Reads you customers information."),
                new ApiScope(name: "customer.contact", displayName: "Allows contacting one of your customers."),

                // shared scope
                new ApiScope(name: "manage", displayName: "Provides administrative access to invoice and customer data."),
            };
        public static IEnumerable<ApiResource> GetApiResources =>
            new List<ApiResource>
            {
                new ApiResource("invoice", "Invoice API")
                {
                    UserClaims=new List<string>
                    {
                        "location",
                    },
                    ApiSecrets={new Secret("Api Invoice".Sha256())},
                    Scopes = { "invoice.read", "invoice.pay", "manage" }
                },
                new ApiResource("customer", "Customer API")
                {
                    Scopes = { "customer.read", "customer.contact", "manage" }
                },
                // expanded version if more control is needed
                new ApiResource
                {
                    Name="Api1",
                    // secret for using introspection endpoint
                    ApiSecrets={new Secret("Api1 secret".Sha256())},
                    // include the following using claims in access token (in addition to subject id)
                    UserClaims=new List<string>
                    {
                        "location",
                        JwtClaimTypes.Address
                    },
                    DisplayName="api1 sources",
                    Scopes=new List<string>
                    {
                        "api1",
                    }
                },
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //machine to machine client (from quickstart 1)
                new Client
                {
                    ClientId="client1",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    //AllowedGrantTypes=
                    //{
                    //    GrantType.AuthorizationCode,
                    //    GrantType.ClientCredentials,
                    //    "my_custom_grant_type"
                    //},
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireConsent=true,
                    AllowedScopes =
                    {
                        "api1",
                    }
                },
                //mvc
                new Client
                {
                    ClientId="mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes=GrantTypes.Code,
                    // where to redirect to after login
                    // 登录成功回调处理地址，处理回调返回的数据 
                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                    //To get the Refresh Token？
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken=true,
                    //AccessToken 过期时间 设为60秒
                    AccessTokenLifetime=60,
                    RequireConsent=true,
                    LogoUri="https://www.google.com/imgres?imgurl=https%3A%2F%2Fwww.baidu.com%2Fimg%2FPCtm_d9c8750bed0b3c7d089fa7d55720d6cf.png&imgrefurl=https%3A%2F%2Fwww.baidu.com%2F&tbnid=ATwvyVEe6yKVyM&vet=12ahUKEwidqvOouL3tAhXRIaYKHTZMBl8QMygAegUIARCnAQ..i&docid=_pGZ43FnePNv5M&w=540&h=258&q=baidu%E5%9B%BE%E7%89%87&ved=2ahUKEwidqvOouL3tAhXRIaYKHTZMBl8QMygAegUIARCnAQ",
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },
               
                //Resourse Owner Password
                new Client
                {
                    ClientId="webform client",
                    ClientName="A Webform Client of Resourse Owner Password Credentials",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    RequireClientSecret=true,
                    ClientSecrets =
                    {
                        new Secret("webform secrets".Sha512())
                    },
                    RequireConsent=true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "api1"
                    },
                    AccessTokenType=AccessTokenType.Reference,
                },
                //Hybrid Client
                new Client
                {
                    ClientId="hybrid client",
                    ClientName="hybrid mvc client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes=GrantTypes.Hybrid,
                    ClientUri = "https://localhost:5005",
                    RequirePkce=false,
                    BackChannelLogoutUri = "https://localhost:5005/logout",
                    RedirectUris =
                    {
                        "https://localhost:5005/signin-oidc",
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:5005/signout-callback-oidc",
                    },
                    AllowOfflineAccess=true,
                    AllowedScopes =
                    {
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                    },
                    //设置客服端的claim，
                    Claims = new List<ClientClaim>
                    {
                        new ClientClaim(JwtClaimTypes.Role, "admin")//客服端为管理员客户端
                    },
                    AlwaysIncludeUserClaimsInIdToken=true,
                    RequireConsent=true,
                    AccessTokenType=AccessTokenType.Reference//默认AccessTokenType.Jwt
                },
                 // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris =           { "https://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "https://localhost:5003" },
                    RequireConsent=true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },
                // SPA client using implicit flow
                new Client
                {
                    ClientId = "homework.vue",
                    ClientName = "Homework.Vue",
                    ClientUri = "http://localhost:8080",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    // AccessToken 是否可以通过浏览器返回
                    AllowAccessTokensViaBrowser = true,
                    // 需要用户点击授权
                    RequireConsent = true,
                    // AccessToken 的有效期
                    AccessTokenLifetime = 60 * 5,//5分钟

                    RedirectUris =
                    {
                        // 指定登录成功跳转回来的 uri
                        "http://localhost:8080/signin-oidc",
                        // AccessToken 有效期比较短，刷新 AccessToken 的页面
                        "http://localhost:8080/redirect-silentrenew",
                        "http://localhost:8080/silent.html",
                        "http://localhost:8080/popup.html",
                    },

                    // 登出 以后跳转的页面
                    PostLogoutRedirectUris = { "http://localhost:8080/" },
                    // vue 和 IdentityServer 不在一个域上，需要指定跨域
                    AllowedCorsOrigins = { "http://localhost:8080", "http://192.168.3.10:8080" },
                    AllowedScopes = 
                    {
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                    },
                }
            };
    }
}
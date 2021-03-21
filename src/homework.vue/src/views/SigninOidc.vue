<template>
    <div>登录成功，返回中</div>
  </template>

  <script lang="ts">
  import { Component, Vue, Inject } from 'vue-property-decorator';
  import { OpenIdConnectService } from '@/open-id-connect/OpenIdConnectService';

  @Component
  export default class SigninOidc extends Vue {
    @Inject() private oidc!: OpenIdConnectService;

    public async created() {
      await this.oidc.handleCallback();
      this.$router.push({ path: '/home' });
    }
  }
  </script>
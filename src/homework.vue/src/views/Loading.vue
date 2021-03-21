<template>
    <div>loading</div>
  </template>

  <script lang="ts">
  import { Component, Vue, Inject } from 'vue-property-decorator';
  import { OpenIdConnectService } from '@/open-id-connect/OpenIdConnectService';

  @Component
  export default class Loading extends Vue {
    @Inject() private oidc!: OpenIdConnectService;

    public created() {
      // 这里去 oidc-client 获取是否已经登录
      console.log('oidc', this.oidc.userAvailavle, this.oidc);
      if (!this.oidc.userAvailavle) {
        this.oidc.triggerSignIn();
      } else {
        this.$router.push({ path: '/home' });
      }
    }
  }
  </script>
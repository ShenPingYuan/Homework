<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Provide } from 'vue-property-decorator';
import { OpenIdConnectService } from '@/open-id-connect/OpenIdConnectService';

@Component
export default class App extends Vue {
  @Provide() private oidc: OpenIdConnectService = OpenIdConnectService.getInstance();
}
</script>

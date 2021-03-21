 <template>
    <div>RedirectSilentRenew</div>
  </template>

  <script lang="ts">
  import { Component, Vue, Inject } from 'vue-property-decorator';
  import { OpenIdConnectService } from '@/open-id-connect/OpenIdConnectService';

  @Component
  export default class RedirectSilentRenew extends Vue {
    @Inject() private oidc!: OpenIdConnectService;

    public created() {
      this.oidc.handleSilentCallback();
    }
  }
  </script>
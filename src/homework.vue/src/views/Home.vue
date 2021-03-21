<template>
    <div class="home">
        <h1>这是首页</h1>
        <button @click="triggerSignOut">SignOut</button>
        <div>
        <table v-if="tableData && tableData.length > 0">
            <tr>
            <th>Id</th>
            <th>Title</th>
            <th>Completed</th>
            </tr>
            <tr v-for="(item,index) in tableData" :key="index">
            <td>{{ item.id }}</td>
            <td>{{ item.title }}</td>
            <td>{{ item.completed }}</td>
            </tr>
        </table>
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Inject } from 'vue-property-decorator';
import { OpenIdConnectService } from '@/open-id-connect/OpenIdConnectService';
import { Todo, QueryTodos } from '@/common/NetService';

@Component
export default class Home extends Vue {
@Inject() private oidc!: OpenIdConnectService;

private tableData: Todo[] = [];

private async created() {
    if (!this.oidc.userAvailavle) {
    await this.oidc.triggerSignIn();
    } else {
    this.tableData = await QueryTodos();
    }
}

    private async triggerSignOut() {
        await this.oidc.triggerSignOut();
    }
}
</script>
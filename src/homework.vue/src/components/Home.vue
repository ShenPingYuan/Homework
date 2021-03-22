<template>
  <!-- <div>
    <el-button type="info" @click="signOut">退出</el-button>
  </div>-->
  <el-container class="home-container">
    <el-header>
      <div>
        <img src="../assets/logo0.png" alt />
        <span>SPY后台管理系统</span>
      </div>
      <el-button type="info" @click="signOut">退出</el-button>
    </el-header>
    <el-container>
      <el-aside :width="isCollapse ? '64px' : '200px'">
        <div class="toggle-button" @click="toggleButton">|||</div>
        <el-menu
          default-active
          class="el-menu-vertical-demo"
          background-color="#333377"
          text-color="#fff"
          active-text-color="#ffd04b"
          :unique-opened="true"
          :collapse="isCollapse"
          ref="elementMenu"
          :collapse-transition="false"
          router
        >
          <el-submenu
            :index="item.id + ''"
            v-for="item in menulist"
            :key="item.id"
          >
            <template slot="title">
              <i :class="iconObj[item.id]"></i>
              <span>{{ item.authName }}</span>
            </template>
            <el-menu-item
              :index="childItem.path.toString()"
              v-for="childItem in item.children"
              :key="childItem.id"
            >
              <i class="el-icon-menu"></i>
              <span slot="title">{{ childItem.authName }}</span>
            </el-menu-item>
          </el-submenu>
        </el-menu>
      </el-aside>
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>

<script>
import { OpenIdConnectService } from "@/open-id-connect/OpenIdConnectService";
import { endPoint } from "@/common/Config";

const oidc = OpenIdConnectService.getInstance();
export default {
  data() {
    return {
      menulist: [],
      iconObj: {
        101: "el-icon-user-solid",
        102: "el-icon-s-goods",
        103: "el-icon-s-cooperation",
        125: "el-icon-folder",
        145: "el-icon-coin",
      },
      role: "",
      teacherDto: {
        Sub: "",
        CourseName: "",
      },
      isCollapse: false,
    };
  },
  created() {
    this.getMenulist();
    this.role = oidc.user.profile.role;
    this.teacherDto.Sub = oidc.user.profile.sub;
    this.CourseName = oidc.user.profile.course;
    //this.InitUserInfo();
  },
  computed: {},
  methods: {
    getMenulist() {
      //通过向后端发送请求得到 菜单数据 并把菜单列表赋给this.menulist
      this.axios({
        method: "get",
        url: endPoint.GetMenus,
      }).then((result) => {
        console.log(result.data);
        this.menulist = result.data;
      });
    },
    InitUserInfo() {
      console.log(oidc.user);
      if (this.role == "Teacher") {
        this.axios({
          method: "post",
          url: endPoint.InitTeacher,
          data: this.teacherDto,
        }).then((result) => {
          console.log(result.data);
        });
      } else if (this.role == "Student") {
        this.axios({
          method: "post",
          url: endPoint.InitStudent,
        }).then((result) => {
          console.log(result.data);
        });
      }
    },
    signOut() {
      //sessionStorage.clear();
      oidc.triggerSignOut();
      //sessionStorage.clear();
      //this.$router.push("/login");
    },
    toggleButton() {
      this.isCollapse = !this.isCollapse;
    },
  },
};
</script>

<style lang="less" scoped>
.home-container {
  height: 100%;
}
.el-header {
  background-color: #2f333e;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-left: 0%;
  color: #ffffff;
  font-size: 20px;
  > div {
    display: flex;
    align-items: center;
    > span {
      margin-left: 10px;
    }
  }
  img {
    height: 100%;
    height: 60px;
  }
}
.el-aside {
  background-color: #337;
  .el-menu {
    border-right: 0;
  }
}
.el-main {
  background-color: #eeeeee;
}
.toggle-button {
  background-color: #999;
  color: #ffffff;
  font-size: 10px;
  text-align: center;
  line-height: 24px;
  letter-spacing: 0.3em;
  cursor: pointer;
}
</style>
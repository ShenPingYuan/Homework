
<template>
  <div class="login-container">
    <div class="login-box">
      <!-- 头像 -->
      <div class="avatar-box">
        <img src="../assets/logo.png" alt />
      </div>
      <!-- 表单 -->
      <el-form
        ref="loginFormRef"
        label-width="0px"
        :model="loginForm"
        class="login-form"
        :rules="loginFormRules"
      >
        <el-form-item prop="username">
          <el-input v-model="loginForm.username">
            <i slot="prefix" class="el-input__icon el-icon-s-custom"></i>
          </el-input>
        </el-form-item>
        <el-form-item prop="password">
          <el-input v-model="loginForm.password" type="password">
            <i slot="prefix" class="el-input__icon el-icon-lock"></i>
          </el-input>
        </el-form-item>
        <el-form-item class="form-btns">
          <el-button type="primary" class="form-btn" @click="login">登陆</el-button>
          <el-button type="info" class="form-btn" @click="resetLoginForm">重置</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      loginForm: {
        username: "admin",
        password: "123456"
      },
      loginFormRules: {
        username: [
          { required: true, message: "请输入用户名" },
          {
            min: 3,
            max: 10,
            message: "长度在 3 到 10 个字符",
            trigger: "blur"
          }
        ],
        password: [
          { required: true, message: "请输入登陆密码" },
          { min: 3, max: 10, message: "长度在 3 到 10 个字符", trigger: "blur" }
        ]
      }
    };
  },
  methods: {
    resetLoginForm() {
      this.$refs.loginFormRef.resetFields();
    },
    login() {
      this.$refs.loginFormRef.validate(validateResult => {
        if (validateResult) {
          this.axios({
            url: "login",
            method: "post",
            data: this.loginForm
          })
            .then(result => {
              const { status,data } = result;
              console.log(data)
              console.log(data.data.token);
              if (status == 200) {
                this.$message.success("登陆成功");
                sessionStorage.setItem("token", data.data.token);
                this.$router.push("/home");
              }
            })
            .catch(error => {
              this.$message.error("登陆失败");
            });
        }
      });
    }
  }
};
</script>

<style lang="less" scoped>
.login-container {
  background-color: #2b4b6b;
  height: 100%;
}
.login-box {
  width: 450px;
  height: 300px;
  background-color: #ffffff;
  border-radius: 3px;
  position: absolute;
  transform: translate(-50%, -50%);
  left: 50%;
  top: 50%;

  .avatar-box {
    width: 130px;
    height: 130px;
    border-radius: 50%;
    border: 1px solid #eee;
    padding: 10px;
    box-shadow: 0px 0px 10px #dddddd;
    position: absolute;
    left: 50%;
    transform: translate(-50%, -50%);
    img {
      height: 100%;
      widows: 100%;
      border-radius: 50%;
      background-color: #eeeeee;
    }
  }
  .login-form {
    position: absolute;
    bottom: 0px;
    width: 100%;
    padding: 0 20px;
    box-sizing: border-box;
    .form-btns {
      display: flex;
      justify-content: flex-end;
    }
  }
}
</style>

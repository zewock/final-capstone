<template>
  <div id="login" class="hero has-text-centered is-fullheight">
    <form @submit.prevent="login" class="box">
      <h1 >Please Sign In</h1>
      <div role="alert" v-if="invalidCredentials">
        Invalid username and password!
      </div>
      <div role="alert" v-if="this.$route.query.registration">
        Thank you for registering, please sign in.
      </div>
      <div class="form-input-group">
        <label for="username">Username</label>
        <input type="text" id="username" v-model="user.username" required autofocus class="inputBars "/>
      </div>
      <div class="form-input-group">
        <label for="password">Password</label>
        <input type="password" id="password" v-model="user.password" required class="inputBars "/>
      </div>
      <button class="button is-primary is-focused mx-4"  type="submit">Sign in</button>
      <router-link class="button is-primary" v-bind:to="{ name: 'home' }">Back</router-link>
      <p>
      <router-link :to="{ name: 'register' }">Need an account? Sign up.</router-link></p>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        username: "",
        password: ""
      },
      invalidCredentials: false
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
            alert("Welcome back, " + this.user.username +"!")
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    }
  }
};
</script>

<style scoped>
.form-input-group {
  color: #000000;
   align-items: center;
   display: flex;
    justify-content: flex-end;
    padding: .1em;
}
label {
   padding: .5em 1em .5em 0;
  flex: 1;
  color: #000000;
}
#login{
  justify-content: center;
  color: #000000;
}
.box {
  background-color: #FAF3E3;
  max-width: 30%;
  min-width: 20%
}
.hero {
 display: flex;
 align-items: center;
}
.inputBars {
   flex: 2;
}
</style>
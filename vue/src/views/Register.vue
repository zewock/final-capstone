<template>
  <div id="register" class="hero is-fullheight has-text-centered">
    <form @submit.prevent="register" class="box">
      <h1>Create Account</h1>
      <div role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <div class="form-input-group">
        <label for="username">Username</label>
        <input
          type="text"
          id="username"
          v-model="user.username"
          required
          autofocus
          class="inputBars"
        />
      </div>
      <div class="form-input-group">
        <label for="password">Password</label>
        <input
          type="password"
          id="password"
          v-model="user.password"
          required
          class="inputBars"
        />
      </div>
      <div class="form-input-group">
        <label for="confirmPassword">Confirm Password</label>
        <input
          type="password"
          id="confirmPassword"
          v-model="user.confirmPassword"
          required
          class="inputBars"
        />
      </div>
      <button class="button is-primary is-focused mx-4" type="submit">
        Create Account
      </button>
      <router-link class="button is-primary" v-bind:to="{ name: 'home' }"
        >Back</router-link
      >
      <p>
        <router-link :to="{ name: 'login' }"
          >Already have an account? Log in.</router-link
        >
      </p>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "register",
  data() {
    return {
      user: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = "Password & Confirm Password do not match.";
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: "/login",
                query: { registration: "success" },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = "Bad Request: Validation Errors";
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = "There were problems registering this user.";
    },
  },
};
</script>

<style scoped>
.form-input-group {
  color: #000000;
  align-items: center;
  display: flex;
  justify-content: flex-end;
  padding: 0.2em;
}
label {
  padding: 0.5em 1em 0.5em 0;
  flex: 1;
  color: #000000;
}
#register {
  justify-content: center;
  color: #000000;
}
.box {
  background-color: #faf3e3;
  max-width: 30%;
  min-width: 20%;
}
.hero {
  display: flex;
  align-items: center;
}
.inputBars {
  flex: 2;
}
</style>

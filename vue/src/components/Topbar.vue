<template>
  <section class="nav-item">
     <nav class="navbar" role="navigation" aria-label="main navigation">
  <div class="navbar-brand">
    <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
      <span aria-hidden="true"></span>
      <span aria-hidden="true"></span>
      <span aria-hidden="true"></span>
    </a>
  </div>

  <div id="navbarBasicExample" class="navbar-menu">
    <div class="navbar-start pl-2">
      <a class="navbar-item">
         <router-link class="navbar-item" v-bind:to="{ name: 'home' }">Home</router-link>
      </a>

      <a class="navbar-item">
        Documentation
      </a>

      <div class="navbar-item has-dropdown is-hoverable">
        <a class="navbar-link">
          More
        </a>

        <div class="navbar-dropdown">
          <a class="navbar-item">
            About
          </a>
          <a class="navbar-item">
            Jobs
          </a>
          <a class="navbar-item">
            Contact
          </a>
          <hr class="navbar-divider">
          <a class="navbar-item">
            Report an issue
          </a>
        </div>
      </div>
    </div>
    
    <div class="navbar-item is-expanded">
      <input class="input is-rounded" type="text" placeholder="Search">
    </div>

    <div class="navbar-end pr-2">
      <div class="navbar-item">
        <div class="buttons">
          <a class="button" v-if="$store.state.token == ''">
            <router-link v-bind:to="{ name: 'register'}"> <strong>Sign up</strong> </router-link>  
          </a>
          <a class="button" v-if="$store.state.token == ''">
            <router-link v-bind:to="{ name: 'login'}"> <strong>Log in</strong> </router-link>
          </a>
          <a class="button" v-if="$store.state.token != ''" >
            <router-link v-bind:to="{ name: ''}" > <strong>Account</strong> </router-link>  
          </a>
          <a class="button" v-if="$store.state.token != ''" >
            <router-link v-bind:to="{ name: 'logout'}" > <strong>Log Out</strong> </router-link>
          </a>
        </div>
      </div>
    </div>
  </div>
</nav>
    </section> 
</template>

<script>
export default {
  name: 'topbar',
data() {
  return {
    loginButtonMessage: 'Login',
    searchBarMessage: 'Search bar'
  }
}
};
</script>

<style scoped>
.nav-item {
  grid-area: nav;
  position: sticky;
}
.navbar{
  background-color:#FF9F29;
  border-radius: 10px;
  margin-top: 10px;  
}
.buttons a{
  
  background-color:#1A4D2E;
  color: #FAF3E3;
  border-color: white;
}
.navbar-item{
  color:black;
}
.navbar-item:hover{
  background-color: #FF9F29;
  color: #FAF3E3;
}
.navbar-link{
  color:black;
}
.navbar-dropdown{
  background-color: #FF9F29;
  border-color:#FAF3E3;
}
.navbar-dropdown a:hover{
  background-color: #FF9F29;
  color: #FAF3E3;
}
.navbar-link:not(.is-arrowless)::after {
    border-color: black;
}
.navbar-divider{
  background-color: #FAF3E3;
}
</style>
<template>
  <section class="nav-item">
     <nav class="navbar" role="navigation" aria-label="main navigation">
  <div class="navbar-brand">
    <a class="navbar-item, logo pl-5">
      <img src="https://image.similarpng.com/very-thumbnail/2020/12/Lorem-ipsum-logo-isolated-clipart-PNG.png" width="112" height="28">
    </a>

    <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
      <span aria-hidden="true"></span>
      <span aria-hidden="true"></span>
      <span aria-hidden="true"></span>
    </a>
  </div>

  <div id="navbarBasicExample" class="navbar-menu">
    <div class="navbar-start pl-2">
      <a class="navbar-item">
         <router-link v-bind:to="{ name: 'home' }">Home</router-link>
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
      <input class="input is-rounded" type="text" placeholder="Search Bar">

    </div>

    <div class="navbar-end pr-2">
      <div class="navbar-item">
        <div class="buttons">
          <a class="button is-primary">
            <router-link v-bind:to="{ name: 'register'}"> <strong>Sign up</strong> </router-link>  
          </a>
          <a class="button is-light">
            <router-link v-bind:to="{ name: 'login'}"> Log in </router-link>
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

<style>
.nav-item {
  grid-area: nav;
  position: sticky;
}
</style>
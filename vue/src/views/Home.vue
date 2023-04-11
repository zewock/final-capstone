<template>
  <div class="home">
    
    <section class="container">
      <h1>Logo</h1>
      <input type="text" name="search bar" placeholder="Search bar">
      <button>Login Button</button>
    </section>    
    
  </div>
</template>

<script>
export default {
  name: "home",
  data() {
  return {
    loginButtonMessage: 'Login',
    searchBarMessage: 'Search bar'
  }
  }
};
</script>

<style>
.container {
  color: white;
  display: grid;
  grid-template-columns: 200px 4fr 200px;
  grid-template-areas: 
  "logo searchbar searchbar login"
  "sidebar main main ad"
  "sidebar main main pm"
}
</style>

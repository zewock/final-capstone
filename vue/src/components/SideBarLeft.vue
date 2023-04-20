<template>
  <aside class="menu">
    <p class="menu-label">
      <strong>Feeds</strong>
    </p>
    <ul class="menu-list">
      <li><router-link v-bind:to="{ name: 'forum' }" v-on:click="$store.commit('FLIP_FAVORITE_FALSE')"  >Forums</router-link></li>
      <li><router-link v-bind:to="{ name: 'forum' }" v-on:click="$store.commit('FLIP_FAVORITE_TRUE')"  >Favorite Forums</router-link></li>
    </ul>
 
   
    <button class="button" v-if="$store.state.token != ''">
      Private Messages
    </button>
  </aside>
</template>


<script>

export default {
  name: "sidebarLeft",
  data() {
    return {};
  },
  methods:{
    refreshForum() {
      this.$nextTick(() => {
        this.$router.go();
      });
    },
  
  }
};
</script>

<style scoped>
.menu {
  grid-area: menu;
  position: sticky;
  display: inline-block;
  vertical-align: text-bottom;
  overflow-y: visible;
  top: 150px;
  position: fixed;
  padding-left: 10px;
  min-width: 15%;
}
.menu-label {
  color: black;
  background-color: #ff9f29;
  border-radius: 10px;
  padding-left: 7px;
}
.menu-list li a {
  color: #faf3e3;
}

.button {
  margin-top: 20px;
  color: #faf3e3;
  background-color: #ff9f29;
  border-color: #faf3e3;
}
.menu-list li a:hover {
  background-color: #1a4d2e;
  color: #ff9f29;
}
.mainBody {
  position: sticky;
  overflow: auto;
  height: 87vh;
  background-color: #faf3e3;
  padding: 15px;
  border-radius: 10px;
}
</style>
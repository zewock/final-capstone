<template>
  <aside class="menu">
    <p class="menu-label">
      <strong>Feeds</strong>
    </p>
    <ul class="menu-list">
      <button class="fav" @mouseover="$store.commit('FLIP_FAVORITE_FALSE')"><li><router-link v-bind:to="{ name: 'forum' }"  >Forums</router-link></li></button>
      
      <button class="fav" @mouseover="$store.commit('FLIP_FAVORITE_TRUE')"> <li><router-link v-bind:to="{ name: 'forum' }"  >Favorite Forums</router-link></li></button>
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
.fav{
  background-color: transparent;
  border-color: transparent;
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
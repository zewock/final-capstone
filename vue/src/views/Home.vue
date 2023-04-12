<template>

  <div >
    <section class="container is-fullscreen">
    <topbar />
    <sidebarLeft/>
    <main-component />
    <sidebar-right />
    </section>
     
    
  </div>
</template>

<script>
import Topbar from '../components/Topbar.vue';
import sidebarLeft from '../components/SideBarLeft.vue';
import MainComponent from '../components/main.vue';
import sidebarRight from '../components/SidebarRight.vue'
export default {
  name: "home",
  components: {
    Topbar,
    sidebarLeft,
    MainComponent,
    sidebarRight
  }
};
</script>

<style>
.container {
  color: grey;
  display: grid;
  grid-template-columns: 1fr 4fr 1fr;
  row-gap: 10px;
  column-gap: 20px;
  grid-template-areas: 
  "nav nav nav"
  "menu main ad"
  "menu main pm";
}
</style>

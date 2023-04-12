<template>

  <div >
    <section class="container is-fullscreen">
    <topbar />
     <main-body />
    <sidebarLeft/>
    <sidebar-right />
    </section>
    
     
    
  </div>
</template>

<script>
import Topbar from '../components/Topbar.vue';
import sidebarLeft from '../components/SideBarLeft.vue';
import MainBody from '../components/mainBody.vue';
import sidebarRight from '../components/SidebarRight.vue'
export default {
  name: "home",
  components: {
    Topbar,
    sidebarLeft,
    sidebarRight,
    MainBody,
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
  "menu mainBody ad"
  "menu mainBody pm";
}
/*.containerMain {
   color: grey;
  display: grid;
  grid-template-columns: 1fr 4fr 1fr;
  row-gap: 10px;
  column-gap: 20px;
  grid-template-areas: 
  ". . ."
  ". mainBody ."
  ". mainBody .";
}*/
</style>

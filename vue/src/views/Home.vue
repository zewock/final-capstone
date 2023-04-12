<template>

  <div >
    <section class="container is-fullscreen">
    <topbar />
     <main-body />
    <sidebarLeft/>
    <sidebar-right />
    <pm-button />
    </section>
    
     
    
  </div>
</template>

<script>
import Topbar from '../components/Topbar.vue';
import sidebarLeft from '../components/SideBarLeft.vue';
import MainBody from '../components/mainBody.vue';
import sidebarRight from '../components/SidebarRight.vue'
import PmButton from '../components/PmButton.vue';
export default {
  name: "home",
  components: {
    Topbar,
    sidebarLeft,
    sidebarRight,
    MainBody,
    PmButton,
    
  }
};
</script>

<style>
.container {
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

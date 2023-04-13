<template>

  <div >
    <section class="container is-fullscreen">
    <topbar />
    <main-body />
    <sidebarLeft/>
    <sidebar-right />
    <logo />
    </section>
    
     
    
  </div>
</template>

<script>
import Topbar from '../components/Topbar.vue';
import sidebarLeft from '../components/SideBarLeft.vue';
import MainBody from '../components/mainBody.vue';
import sidebarRight from '../components/SidebarRight.vue'
import logo from '../components/logo.vue'
export default {
  name: "home",
  components: {
    Topbar,
    sidebarLeft,
    sidebarRight,
    MainBody,
    logo
  }
};
</script>

<style scope>
.container {
  display: grid;
  grid-template-columns: 1fr 4fr 1fr;
  row-gap: 10px;
  column-gap: 20px;
  grid-template-areas: 
  "logo nav nav"
  "menu mainBody ad"
  "menu mainBody ad";
  height: 100vh;
}
@media screen and (min-width: 1408px) {
.container:not(.is-max-desktop):not(.is-max-widescreen) {
    max-width: 100%;
    }
}
@media screen and (min-width: 1216px) {
.container:not(.is-max-desktop) {
    max-width: 100%;
    }
}
@media screen and (min-width: 1024px) {
.container {
    max-width: 100%;
    }
}

</style>

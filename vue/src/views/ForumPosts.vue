<template>
  <section class="container is-fullscreen">
    <Topbar class="topbar" />
    <SidebarLeft />
    <SidebarRight />
    <Logo />
  </section>
</template>

<script>
import Topbar from "../components/Topbar.vue";
import SidebarLeft from "../components/SideBarLeft.vue";
import SidebarRight from "../components/SidebarRight.vue";
import Logo from "../components/logo.vue";
export default {
  name: "forum",
  components: {
    Topbar,
    SidebarLeft,
    SidebarRight,
    Logo,
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
    "logo nav ad"
    "menu mainBody ad"
    "menu mainBody ad";
  height: 100vh;
}
.topbar {
  z-index: 1;
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
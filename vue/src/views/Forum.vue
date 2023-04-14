<template>
  <section class="container is-fullscreen">
    <topbar class="topbar" />
    <sidebarLeft />
    <forum-list />
    <sidebar-right />
    <logo />
  </section>
</template>

<script>
import Topbar from "../components/Topbar.vue";
import sidebarLeft from "../components/SideBarLeft.vue";
import sidebarRight from "../components/SidebarRight.vue";
import logo from "../components/logo.vue";
import ForumList from "../components/ForumList.vue";
export default {
  name: "forum",
  components: {
    Topbar,
    sidebarLeft,
    sidebarRight,
    logo,
    ForumList,
  },
};
</script>

<style scope>
.container {
  display: grid;
  grid-template-columns: 1fr 4fr 1fr;
  row-gap: 10px;
  column-gap: 20px;
  grid-template-areas:
    "logo nav ."
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
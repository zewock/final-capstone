<template>
        <aside class="menu">
            <p class="menu-label">
                Feeds
            </p>
        <ul class="menu-list">
            <li><a>Home</a></li>
            <li><a>Popular</a></li>
        </ul>
        <p class="menu-label">
            Topics
        </p>
        <ul class="menu-list">
            <li><a>Gaming</a></li>
            <li><a>Sports</a></li>
            <li><a>Tech</a></li>
            <li><a>Television</a></li>
            <li><a>Spongebob</a></li>
        </ul>
        </aside>
</template>


<script>

export default {
  name: 'sidebarLeft',
data() {
  return {
    
  }
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
}
.menu-label{
  color: #FF9F29;
}
.menu-list li a{
  color: #FAF3E3;
}

</style>
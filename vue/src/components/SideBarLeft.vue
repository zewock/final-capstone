<template>
        <aside class="menu">
            <p class="menu-label">
                <strong>Feeds</strong>
            </p>
        <ul class="menu-list">
            <li><router-link v-bind:to="{ name: 'forum' }">Forums</router-link></li>
            <li><a>Popular</a></li>
        </ul>
        <p class="menu-label">
            <strong>Topics</strong>
        </p>
        <ul class="menu-list">
            <li><a>Gaming</a></li>
            <li><a>Sports</a></li>
            <li><a>Tech</a></li>
            <li><a>Television</a></li>
            <li><a>Spongebob</a></li>
        </ul>
        <button class="button">Private Messages</button>
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
  position:fixed;
  padding-left: 10px;
}
.menu-label{
  color: black;
  background-color: #FF9F29;
  border-radius: 10px;
  padding-left: 7px;
}
.menu-list li a{
  color: #FAF3E3;
}

.button{
  margin-top: 20px;
  color: #FAF3E3;
  background-color: #FF9F29;
  border-color: #FAF3E3;
}
.menu-list li a:hover{
  background-color: #1A4D2E;
  color: #FF9F29;
}

</style>
<template>
  <div>
    <h1>Favorites</h1>
    <ul>
      <li v-for="forum in favoriteForums" :key="forum.forumID">
        {{ forum.title }}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'Favorites',
  computed: {
    favoriteForums() {
      // Return the forums where isFavoriteForum is true
      return this.$store.state.forums.filter(forum => forum.isFavoriteForum);
    }
  }
};
</script>

<style scoped>
/* Add your styles here */
</style>

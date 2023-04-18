<template>
    
  <div >
      <header class="card-header input" @click="
            retrievePosts(forum);
            togglePosts()
          ">
        <section class="card-header-title">
          {{ forum.title }}
          <span
            ><time>{{ forum.FormattedCreateDate }} </time></span
          >
        </section>
      </header>
    </div>
</template>

<script>
export default {
  name: "forumBody",
  props: ['forum'],
  methods: {
  addPosts() {
      this.$store.commit("ADD_POSTS_BY_FORUMID");
    },
    
  retrievePosts(forum) {
        this.$store.dispatch('selectForum', forum)
        this.addPosts();
    },

    togglePosts() {
      this.$store.commit("TOGGLE_SOME_POSTS");
    },
    
},


  created() {

  },
  computed: {
    
  },
};
</script>

<style>
.card-header {
  background-color: #ff9f29;
  margin-bottom: 10px;
  border-radius: 10px;
  border-color: transparent;
  height: auto;
}

</style>
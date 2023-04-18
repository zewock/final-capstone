<template>
  <div class="mainBody">
    <PostCard v-for="post in this.$store.state.postsList" :key="post.postId" :post="post" />
  </div>
</template>

<script>
import PostCard from "../Posts/PostCard.vue";
export default {
<<<<<<< HEAD:vue/src/components/Forum.vue
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

=======
  name: "PostList",
  components: {PostCard},
>>>>>>> cfa7fc7995eac6df447a2ac84f70b544f1c8d91a:vue/src/components/Posts/PostList.vue

  created() {
    this.getForumPosts();
  },
  methods: {
    sortPostsByDate(posts) {
      return posts
        .slice()
        .sort((a, b) => new Date(b.createDate) - new Date(a.createDate));
    }
  }
};
</script>

<<<<<<< HEAD:vue/src/components/Forum.vue
<style>
=======

<style scoped>
.mainBody {
  grid-area: mainBody;
  position: sticky;
  overflow-y: auto;
  height: 87vh;
  background-color: #faf3e3;
  padding: 15px;
  border-radius: 10px;
}
#in-forum-title #forum-title {
  margin-bottom: 10px;
}
#in-forum-title .card-header {
  background-color: #ff9f29;
  margin-bottom: 0;
  border-radius: 10px;
  position: sticky;
  height: auto;
  align-items: center;
  width: 100%;
}
.replies {
  color: #1a4d2e;
}
#in-forum-title .card-header h1 {
  display: inline-flex;
  color: #1a4d2e;
  font-size: larger;
  font-weight: bold;
}
#in-forum-title .card-header p {
  width: 75%;
  float: right;
  font-size: smaller;
  text-align: right;
}

>>>>>>> cfa7fc7995eac6df447a2ac84f70b544f1c8d91a:vue/src/components/Posts/PostList.vue
.card-header {
  background-color: #ff9f29;
  margin-bottom: 10px;
  border-radius: 10px;
  border-color: transparent;
  padding-left: 0;
  height: auto;
}

</style>
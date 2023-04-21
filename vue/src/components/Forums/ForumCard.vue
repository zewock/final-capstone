<template>
  <div class="card">
    <header
      class="card-header"
      @click="
        retrievePosts(forum);
        togglePosts();
        saveForumCreator();
      "
    >
      <section class="card-header-title input">
        <p>{{ forum.title }}</p>
        <h1 class="right">
          Topic: {{ forum.topic }}
          <span
            ><br /><time>{{ forum.FormattedCreateDate }} </time></span
          >
        </h1>
      </section>
    </header>
  </div>
</template>


<script>
export default {
  name: "ForumCard",
  props: {
    forum: Object,
    formatDate: Function,
  },
  methods: {
    addPosts() {
      this.$store.commit("ADD_POSTS_BY_FORUMID");
    },

    togglePosts() {
      this.$store.commit("TOGGLE_SOME_POSTS");
    },

    retrievePosts(forum) {
      this.$store.dispatch("selectForum", forum);
      this.addPosts();
    },
    saveForumCreator() {
      if (
        this.$store.state.user.userId == this.$store.state.selectForum.ownerID
      ) {
        this.$store.commit("SAVE_CREATOR");
      }
    },
  },
};
</script>

<style scoped>
.card {
  /* Add your card styles here */
  margin-bottom: 20px;
  border: 1px solid #ccc;
  border-radius: 10px;
}

.card-header {
  /* Add your card header styles here */
  background-color: #f5f5f5;
  padding: 5px;
  border-color: #ff9f29;
  border-radius: 10px;
}

.card-header-title {
  /* Add your card header title styles here */
  font-weight: bold;
  background-color: #faf3e3;
  border-color: transparent;
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
.right{
  text-align: right;
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

.card-header {
  background-color: #1a4d2e;
  margin-bottom: 10px;
  border-radius: 10px;
  border-color: transparent;
  height: auto;
}
.card-footer a {
  color: #1a4d2e;
}
.card-footer a:hover {
  background-color: #faf3e3;
}
.card-footer a:first-child:hover {
  border-bottom-left-radius: 10px;
}
.card-footer a:last-child:hover {
  border-bottom-right-radius: 10px;
}
.card-content {
  background-color: #faf3e3;
  color: #1a4d2e;
}
.card-header-title {
  color: #1a4d2e;
  justify-content: space-between;
  display: flex;
  align-items: center;
  padding: 10px;
  height: 70px;
}
.post-card {
  background-color: #ff9f29;
  border-radius: 10px;
  margin-bottom: 10px;
}
.card {
  background-color: #1a4d2e;
  padding: 15px;
  margin-bottom: 10px;
  color: #000000;
  border-radius: 10px;
  z-index: 0;
}
.cards-container::-webkit-scrollbar {
  width: 0px; /* width of the entire scrollbar */
}
.button {
  background-color: #1a4d2e;
  color: #faf3e3;
}
.dropdown-menu {
  display: none;
  left: 0;
  right: 1px;
  min-width: 6rem;
  padding-top: 4px;
  position: absolute;
  top: 100%;
}
.dropdown-item {
  right: 10px;
  align-items: center;
}
.dropdown-content {
  z-index: 1;
}
.box {
  height: 100%;
  background-color: #ff9f29;
  z-index: 20;
}
.date {
  color: black;
  padding-right: 10px;
}
.card-header:last-child {
  margin-bottom: 0;
}
.cards-container {
  height: 100%;
  overflow-y: auto;
  border-radius: 10px;
}
.topicStyle {
  padding-right: 20px;
}
</style>


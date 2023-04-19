<template>
  <body class="mainBody">
    <ForumForm v-show="visible" @cancelForm="toggleVisibility(false)" />
    <FormControls @createForm="toggleVisibility(true)" v-if="$store.state.posts == false" />
    <PostForm v-show="visiblePostForm" @cancelForm="togglePostVisibility(false)" />
    <section>
<PostControls @createPost="togglePostVisibility(true)" v-if="$store.state.posts == true" />
    </section>
    
    <div v-if="$store.state.posts == false">
    <ForumCard
      v-for="forum in formattedForums"
      :key="forum.ForumID"
      :forum="forum"
    />
    </div>
    <div v-if="$store.state.posts == true">
    <PostCard
      v-for="post in this.$store.state.postsList"
      :key="post.postId"
      :post="post"
    />
    </div>
  
    <!--<ForumReply v-for="reply in replyList" :key="reply.replyId" :reply="reply"></ForumReply>-->
  </body>
</template>

<script>
import ForumForm from "../NewForumForm/ForumForm.vue";
import ForumCard from "./ForumCard.vue";
import FormControls from "../NewForumForm/FormControls.vue";
import PostCard from "../Posts/PostCard.vue";
import PostForm from "../NewPostForm/PostForm.vue";
import PostControls from "../NewPostForm/PostControls.vue";

export default {
  name: "forumList",
  components: {
    ForumForm,
    ForumCard,
    FormControls,
    PostCard,
    PostForm,
    PostControls,
  },
  data() {
    return {
      forums: [],
      posts: [],
      visible: false,
      visiblePostForm: false,
    };
  },
  methods: {
    toggleVisibility(Bool) {
      this.visible = Bool;
    },
    togglePostVisibility(Bool) {
      this.visiblePostForm = Bool;
    },
    ClearForm() {},
    formatDate(dateString) {
      try {
        const date = new Date(dateString);
        if (isNaN(date)) {
          throw new Error("Invalid date");
        }
        const year = date.getFullYear();
        const month = ("0" + (date.getMonth() + 1)).slice(-2);
        const day = ("0" + date.getDate()).slice(-2);
        return `${month}-${day}-${year}`;
      } catch (error) {
        console.error(`Error formatting date: ${error.message}`);
        return "Invalid date";
      }
    },
    formatDateTime(dateString) {
      try {
        const date = new Date(dateString);
        if (isNaN(date)) {
          throw new Error("Invalid date");
        }
        const year = date.getFullYear();
        const month = ("0" + (date.getMonth() + 1)).slice(-2);
        const day = ("0" + date.getDate()).slice(-2);
        const hours = date.getHours();
        const minutes = ("0" + date.getMinutes()).slice(-2);
        const ampm = hours >= 12 ? "PM" : "AM";
        const formattedHours = hours % 12 || 12;
        return `${month}-${day}-${year} ${formattedHours}:${minutes} ${ampm}`;
      } catch (error) {
        console.error(`Error formatting date: ${error.message}`);
        return "Invalid date";
      }
    },
    addForums() {
      this.$store.commit("ADD_FORUMS");
    },
     sortPostsByDate(posts) {
      return posts
        .slice()
        .sort((a, b) => new Date(b.createDate) - new Date(a.createDate));
    },
    createPost() {
      this.$store.commit("ADD_POSTS");
    },
  },
  created() {
    this.addForums();
  },

  computed: {
    formattedForums() {
      return this.$store.state.forums.map((forum) => {
        const rawCreateDate = forum.createDate;
        const formattedCreateDate = this.formatDate(rawCreateDate);
        return {
          ...forum,
          FormattedCreateDate: formattedCreateDate,
        };
      });
    },
  },
};
</script>

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

.card-header {
  background-color: #ff9f29;
  margin-bottom: 10px;
  border-radius: 10px;
  border-color: transparent;
  padding-left: 0;
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
</style>
<template>
  <body class="mainBody">
    <ForumForm v-show="visible" @cancelForm="toggleVisibility(false)" />
    <FormControls
      @createForm="toggleVisibility(true)"
      v-if="$store.state.posts == false"
    />
    <PostForm
      v-show="visiblePostForm"
      @cancelForm="togglePostVisibility(false)"
    />
    <div v-if="$store.state.posts == false && $store.state.favoriteForums == false">
      <ForumCard
        v-for="forum in displayedFormattedForums"
        :key="forum.ForumID"
        :forum="forum"
      />
    </div>
    <div v-if="$store.state.posts == false && $store.state.favoriteForums == true" >
    
      <ForumCard
        v-for="forum in displayedFormattedForums"
        :key="forum.ForumID"
        :forum="forum"
      />
    </div>
    <div v-if="$store.state.posts == true">
      <section class="card-header-title post-card">
        {{ $store.state.selectForum.description }}
        <input type="checkbox" @change="toggleFavorite" /> Favorite Forum
        <div class="postControlStyle">
          <PostControls
            @createPost="togglePostVisibility(true)"
            v-if="$store.state.posts == true"
            class="postControlStyle"
          >
          </PostControls>
          <button class="button" @click="toggleSortOrder">
            {{
              sortByPopularity
                ? "Sorted by: Most Popular"
                : "Sorted by: Most Recent"
            }}
          </button>
           <input class="input is-rounded" type="search" placeholder="+ Mods by Username" v-model="keyword" @search="addModByModsandAdmin(keyword)"  >
        </div>
      </section>
      <PostCard
        v-for="post in displayedFormattedPosts"
        :key="post.postId"
        :post="post"
      />
    </div>
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
      keyword: "",
      sortByPopularity: false,
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
    sortPostsByPopularity(posts) {
      return posts
        .slice()
        .sort((a, b) => b.upVotes - b.downVotes - (a.upVotes - a.downVotes));
    },
    createPost() {
      this.$store.commit("ADD_POSTS");
    },
    toggleFavorite(event) {
      if (event.target.classList.contains("far")) {
        event.target.classList.remove("far");
        event.target.classList.add("fas");
      } else {
        event.target.classList.remove("fas");
        event.target.classList.add("far");
      }
      this.$store.dispatch("TOGGLE_FAVORITE", {
        forumId: this.$store.state.selectForum.ForumID,
      });
    },
    addModByModsandAdmin(username) {
      this.$store.state.selectForum.forums_ModsArrays.forEach(element => {
        if(element.userID == this.$store.state.user.userId || this.$store.state.user.role == "admin") {
           this.$store.commit("SAVE_MOD", username)
        }

      });
    },
    toggleSortOrder() {
      if (this.sortByPopularity) {
        this.sortByPopularity = false;
        this.displayedFormattedPosts = this.sortPostsByDate(
          this.displayedFormattedPosts.slice()
        );
      } else {
        this.sortByPopularity = true;
        this.displayedFormattedPosts = this.sortPostsByPopularity(
          this.displayedFormattedPosts.slice()
        );
      }
    },
  },
  created() {
    this.addForums();
  },

  computed: {
   displayedFormattedForums() {
      const forumsToDisplay =
        this.$store.state.filteredForums &&
        this.$store.state.filteredForums.length > 0
          ? this.$store.state.filteredForums
          : this.$store.state.forums;

      return forumsToDisplay
        .slice()
        .sort((a, b) => new Date(b.createDate) - new Date(a.createDate))
        .map((forum) => {
          const formattedCreateDate = this.formatDate(forum.createDate);
          return {
            ...forum,
            FormattedCreateDate: formattedCreateDate,
          };
        });
    },
/*displayedFormattedForums() {
  const currentUser = this.$store.state.user;
  const forumsToDisplay = this.$store.state.filteredForums.length > 0
    ? this.$store.state.filteredForums
    : this.$store.state.forums;

  let filteredForums = forumsToDisplay;

  if (this.$store.state.favoriteForums == true) {
    filteredForums = forumsToDisplay.filter(forum => {
      return forum.forums_FavoritesArrays.filter(favorite => favorite.userID === currentUser.userId);
    });
  }

  if (this.$store.state.favoriteForums == false) {
    return filteredForums.map(forum => {
      const rawCreateDate = forum.createDate;
      const formattedCreateDate = this.formatDate(rawCreateDate);
      return {
        ...forum,
        FormattedCreateDate: formattedCreateDate,
      };
    });
  }

  return filteredForums.map(forum => {
    const rawCreateDate = forum.createDate;
    const formattedCreateDate = this.formatDate(rawCreateDate);
    return {
      ...forum,
      FormattedCreateDate: formattedCreateDate,
    };
  });
},*/

    displayedFormattedPosts: {
      get() {
        const postsToDisplay =
          this.$store.state.filteredPosts &&
          this.$store.state.filteredPosts.length > 0
            ? this.$store.state.filteredPosts
            : this.$store.state.postsList;

        if (this.sortByPopularity) {
          return this.sortPostsByPopularity(postsToDisplay).map((post) => {
            const formattedCreateDate = this.formatDateTime(post.createDate);
            return {
              ...post,
              FormattedCreateDate: formattedCreateDate,
            };
          });
        } else {
          return this.sortPostsByDate(postsToDisplay).map((post) => {
            const formattedCreateDate = this.formatDateTime(post.createDate);
            return {
              ...post,
              FormattedCreateDate: formattedCreateDate,
            };
          });
        }
      },
      // Add an empty setter to satisfy Vue's reactivity system
      set() {},
    },
  },
};
</script>

<style scoped>
.far {
  cursor: pointer;
  color: #1a4d2e;
}
.fas {
  cursor: pointer;
  color: #1a4d2e;
}
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
.postControlStyle {
  display: flex;
  justify-content: left;
}
</style>
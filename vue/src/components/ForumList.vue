<template>
  <body class="mainBody">
    <section class="box" v-show="form">
      <h1 style="">New Forum Form</h1>
      <div class="field">
        <label class="label">Forum Name</label>
        <div class="control has-icons-left has-icons-right">
          <input
            class="input is-success"
            type="text"
            placeholder="Text input"
            value="bulma"
            v-model="newForum.title"
          />
          <span class="icon is-small is-left">
            <i class="fas fa-font"></i>
          </span>
          <span class="icon is-small is-right">
            <i class="fas fa-check"></i>
          </span>
        </div>
      </div>
      <div class="field">
        <label class="label">Topic</label>
        <div class="control">
          <div class="select">
            <select v-model="newForum.Topic">
              <option>Gaming</option>
              <option>Sports</option>
              <option>Tech</option>
              <option>Television</option>
              <option>Spongebob</option>
            </select>
          </div>
        </div>
      </div>

      <div class="field">
        <label class="label">Description</label>
        <div class="control">
          <textarea
            class="textarea"
            placeholder="Textarea"
            v-model="newForum.description"
          ></textarea>
        </div>
      </div>

      <div class="field is-grouped">
        <div class="control">
          <button
            class="button"
            @click="
              form = false;
              SaveForum();
              refreshForum();
            "
          >
            Submit
          </button>
        </div>
        <div class="control">
          <button class="button" @click="form = false">Cancel</button>
        </div>
      </div>
    </section>
    <div class="cards-container">
      <div class="card" v-if="$store.state.token != ''">
        <header class="card-header">
          <section class="card-header-title">
            <button class="button" :disabled="form" @click="form = true">
              New Forum
            </button>
            <div class="dropdown is-hoverable">
              <div class="dropdown-trigger">
                <button
                  class="button"
                  aria-haspopup="true"
                  aria-controls="dropdown-menu4"
                >
                  <span>Options</span>
                  <span class="icon is-small">
                    <i class="fas fa-angle-down" aria-hidden="true"></i>
                  </span>
                </button>
                <div class="dropdown-menu" id="dropdown-menu4" role="menu">
                  <div class="dropdown-content">
                    <div class="dropdown-item">
                      <button class="button">Mods</button>
                      <button class="button">Users</button>
                      <button class="button">Delete</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </section>
        </header>
      </div>
      <div v-if="posts == false">
        <div
          class="card"
          v-for="forum in formattedForums"
          :key="forum.ForumID"
          @click="
            posts = true;
            RetrievePosts(forum);
          "
        >
          <header class="card-header input">
            <section class="card-header-title">
              {{ forum.title }}
              <span
                ><time>{{ forum.FormattedCreateDate }} </time></span
              >
            </section>
          </header>
        </div>
      </div>

      <div class="card" v-else>
        <div>
          <header class="card-header input">
            <section class="card-header-title">
              {{ selectForum.title }}
              {{ selectForum.description }}
            </section>
          </header>
        </div>
        <header
          class="card-header input"
          v-for="post in postsList"
          :key="post.postId"
        >
          <section class="card-header-title">
            {{ post.title }}
            {{ post.content }}
          </section>
        </header>
      </div>
    </div>
  </body>
</template>

<script>
import ForumService from "../services/ForumService";
import PostService from "../services/PostService";

export default {
  name: "forumList",
  data() {
    return {
      forums: [],
      postsList: [],
      form: false,
      selectForum: null,
      newForum: {
        image: "",
        topic: "",
        title: "",
        description: "",
      },
      posts: false,
    };
  },
  methods: {
    ViewForum(id) {
      this.$router.push(`/forum/${id}`);
    },
    ViewPost(forumId) {
      this.$router.push(`/ForumPosts/${forumId}`);
    },
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

    SaveForum() {
      ForumService.create(this.newForum).then((response) => {
        if (response.status === 201) {
          alert("Forum Created");
          this.forums.push(response.data);
          this.form = false;
        }
      });
    },
    refreshForum() {
      this.$nextTick(() => {
        this.$router.go();
      });
    },
    RetrievePosts(forum) {
      this.selectForum = forum;
      PostService.getPost(forum.forumID).then((response) => {
        this.postsList = response.data.value;
    
      });
    },
  },
  created() {
    ForumService.getForums().then((response) => {
      this.forums = response.data.forumArray;
    });
  },

  computed: {
    formattedForums() {
      return this.forums.map((forum) => {
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

<style scope>
.mainBody {
  grid-area: mainBody;
  position: sticky;
  overflow: hidden;
  height: 87vh;
  background-color: #faf3e3;
  padding: 15px;
  border-radius: 10px;
}
.card-header {
  background-color: #ff9f29;
  margin-bottom: 10px;
  border-radius: 10px;
}
.card-header-title {
  color: #1a4d2e;
  justify-content: space-between;
  display: flex;
  align-items: center;
}
.card {
  background-color: #1a4d2e;
  padding: 15px;
  margin-bottom: 10px;
  color: #000000;
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
  overflow-y: auto; /* Enable vertical scrolling */
  border-radius: 5px;
}
</style>
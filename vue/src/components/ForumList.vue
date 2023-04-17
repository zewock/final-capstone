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

      <div v-if="$store.state.posts == false">
        <div
          class="card"
          v-for="forum in this.formattedForums"
          :key="forum.ForumID"
        >
          <forum-body :forum="forum" />
        </div>
      </div>

      <div class="card" v-if="$store.state.posts == true">
        <div id="in-forum-title">
          <header id="forum-title" class="card-header input">
            <section class="card-header-title">
              <h1>{{ $store.state.selectForum.title }}</h1>
              <p>
                {{ $store.state.selectForum.description }}<br /><br />
                @{{ $store.state.selectForum.ownerUsername }}
              </p>
            </section>
          </header>
          <div
            class="post-card"
            v-for="post in $store.state.postsList"
            :key="post.postId"
            
          >
            <post-content :post="post"/>
          </div>
        </div>
      </div>
    </div>
  </body>
</template>

<script>
import ForumService from "../services/ForumService";
import forumBody from "../components/Forum.vue";
import PostContent from "../components/Post.vue";

export default {
  name: "forumList",
  components: {
    forumBody,
    PostContent,
  },
  data() {
    return {
      postsList: [],
      replyList: [],
      form: false,
      menu: false,
      selectForum: null,
      newForum: {
        image: "",
        topic: "",
        title: "",
        description: "",
      },
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
    addForums() {
      this.$store.commit("ADD_FORUMS");
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
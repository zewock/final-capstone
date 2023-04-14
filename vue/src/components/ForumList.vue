<template>
  <body class="mainBody">
    <section class="box" v-bind="form" v-show="form">
      <h1 style="">New Forum Form</h1>
      <div class="field">
        <label class="label">Username</label>
        <div class="control has-icons-left has-icons-right">
          <input
            class="input is-success"
            type="text"
            placeholder="Text input"
            value="bulma"
            v-model="newForum.OwnerUsername"
          />
          <span class="icon is-small is-left">
            <i class="fas fa-user"></i>
          </span>
          <span class="icon is-small is-right">
            <i class="fas fa-check"></i>
          </span>
        </div>
      </div>
      <div class="field">
        <label class="label">Forum Name</label>
        <div class="control has-icons-left has-icons-right">
          <input
            class="input is-success"
            type="text"
            placeholder="Text input"
            value="bulma"
          />
          <span class="icon is-small is-left">
            <i class="fa-sharp fa-light fa-input-text"></i>
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
            <select>
              <option>Select dropdown</option>
              <option>With options</option>
            </select>
          </div>
        </div>
      </div>

      <div class="field">
        <label class="label">Description</label>
        <div class="control">
          <textarea class="textarea" placeholder="Textarea"></textarea>
        </div>
      </div>

      <div class="field is-grouped">
        <div class="control">
          <button
            class="button"
            v-bind="form"
            @click="form = false && saveForum()"
          >
            Submit
          </button>
        </div>
        <div class="control">
          <button class="button" v-bind="form" @click="form = false">
            Submit
          </button>
        </div>
        <div class="control">
          <button class="button" v-bind="form" @click="form = false">
            Cancel
          </button>
        </div>
      </div>
    </section>
    <div class="cards-container">
      <div class="card">
        <header class="card-header">
          <section class="card-header-title">
            <button class="button" v-bind="form" @click="form = true">
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
              </div>
            </div>
            <div class="dropdown-menu" id="dropdown-menu4" role="menu">
              <div class="dropdown-content">
                <div class="dropdown-item">
                  <button class="button">Moderators</button>
                  <button class="button">Users</button>
                  <button class="button">Delete</button>
                </div>
              </div>
            </div>
          </section>
        </header>
      </div>
      <div class="card">
        <header
          class="card-header"
          v-for="forum in formattedForums"
          :key="forum.ForumID"
        >
          <section class="card-header-title">
            {{ forum.Topic }}
            <span
              ><time>{{ forum.FormattedCreateDate }} </time></span
            >
          </section>
        </header>
      </div>
    </div>
  </body>
</template>

<script>
import ForumService from "../services/ForumService";

export default {
  name: "forumList",
  data() {
    return {
      forums: [],
      form: false,
      newForum: {
        CreateDate: " ",
        DownvotesLast24Hours: 0,
        ForumID: 0,
        Forums_FavoritesArrays: null,
        Forums_ModsArrays: null,
        IsAnAdminForum: false,
        IsFavoriteForum: false,
        IsModerator: false,
        IsOwner: false,
        OwnerID: 0,
        OwnerUsername: " ",
        Topic: " ",
        TotalNumDownvotes: 0,
        TotalNumUpvotes: 0,
        UpvotesLast24Hours: 0,
      },
    };
  },
  methods: {
    ViewForum(id) {
      this.$router.push(`/forum/${id}`);
    },
    formatDate(dateString) {
      const date = new Date(dateString);
      const year = date.getFullYear();
      const month = ("0" + (date.getMonth() + 1)).slice(-2);
      const day = ("0" + date.getDate()).slice(-2);
      return `${year}-${month}-${day}`;
    },
    SaveForum() {
      ForumService.create(this.newForum).then((response) => {
        if (response.status === 201) {
          alert("Forum Created");
        }
      });
    },
  },
  created() {
    ForumService.getForums().then((response) => {
      let parsedResponse = JSON.parse(response.data.value);
      const forumArray = parsedResponse.ForumArray;
      this.forums.push(...forumArray);
    });
  },
  computed: {
    formattedForums() {
      return this.forums.map((forum) => {
        const rawCreateDate = forum.CreateDate;
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
  min-width: 6rem;
  padding-top: 4px;
  position: absolute;
  top: 100%;
  z-index: 20;
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
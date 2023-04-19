<template>
  <section class="nav-item">
    <nav class="navbar" role="navigation" aria-label="main navigation">
      <div class="navbar-brand">
        <a
          role="button"
          class="navbar-burger"
          aria-label="menu"
          aria-expanded="false"
          data-target="navbarBasicExample"
        >
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>

      <div id="navbarBasicExample" class="navbar-menu">
        <div class="navbar-start pl-2">
          <router-link
            class="navbar-item"
            v-bind:to="{ name: 'home' }"
            @click.native="refreshHome"
          >
            Home
          </router-link>

          <a class="navbar-item"> Placeholder </a>

          <div class="navbar-item has-dropdown is-hoverable">
            <a class="navbar-link"> More </a>

            <div class="navbar-dropdown">
              <a class="navbar-item"> About </a>
              <a class="navbar-item"> The Team </a>
              <a class="navbar-item"> Contact </a>
              <hr class="navbar-divider" />
              <a class="navbar-item"> Report an issue </a>
            </div>
          </div>
        </div>

        <div class="navbar-item is-expanded">
          <input
            class="input is-rounded"
            v-if="$route.name === 'home'"
            type="search"
            v-model="keyword"
            @search="searchPosts(keyword)"
            placeholder="Search All Posts"
          />
          <input
            class="input is-rounded"
            v-if="$route.name === 'forum' && isPosts"
            v-model="keyword"
            type="text"
            placeholder="Search Posts"
          />
          <input
            class="input is-rounded"
            v-if="$route.name === 'forum' && !isPosts"
            v-model="keyword"
            type="search"
            @search="searchForums(keyword)"
            placeholder="Search Fourms"
          />
        </div>

        <div class="navbar-end pr-2">
          <div class="navbar-item">
            <div class="buttons">
              <a class="button" v-if="$store.state.token == ''">
                <router-link v-bind:to="{ name: 'register' }">
                  <strong>Sign up</strong>
                </router-link>
              </a>
              <a class="button" v-if="$store.state.token == ''">
                <router-link v-bind:to="{ name: 'login' }">
                  <strong>Log in</strong>
                </router-link>
              </a>
              <a class="button" v-if="$store.state.token != ''">
                <router-link v-bind:to="{ name: 'logout' }">
                  <strong>Log Out</strong>
                </router-link>
              </a>
            </div>
          </div>
        </div>
      </div>
    </nav>
  </section>
</template>

<script>
import PostService from "../services/PostService";
export default {
  name: "topbar",
  data() {
    return {
      loginButtonMessage: "Login",
      searchBarMessage: "Search bar",
      keyword: "",
    };
  },
  methods: {
    refreshHome() {
      if (this.$route.name === "home") {
        location.reload();
      }
    },
    searchPosts(keyword) {
      PostService.searchPosts(keyword).then((response) => {
        this.$store.state.postsList = response.data;
      });
    },
    searchForums(keyword) {
      const filteredForums = this.$store.state.forums.filter((forum) =>
        forum.title.toLowerCase().includes(keyword.toLowerCase())
      );
      this.$store.state.filteredForums = filteredForums;
    },
  },

  computed: {
    isPosts() {
      return this.$store.state.posts;
    },
  },
};
</script>

<style scoped>
.nav-item {
  grid-area: nav;
  position: sticky;
}
.navbar {
  background-color: #ff9f29;
  border-radius: 10px;
  margin-top: 10px;
}
.buttons a {
  background-color: #1a4d2e;
  color: #faf3e3;
  border-color: white;
}
.navbar-item {
  color: black;
}
.navbar-item:hover {
  background-color: #ff9f29;
  color: #faf3e3;
}
.navbar-link {
  color: black;
}
.navbar-dropdown {
  background-color: #ff9f29;
  border-color: #faf3e3;
}
.navbar-dropdown a:hover {
  background-color: #ff9f29;
  color: #faf3e3;
}
.navbar-link:not(.is-arrowless)::after {
  border-color: black;
}
.navbar-divider {
  background-color: #faf3e3;
}
</style>
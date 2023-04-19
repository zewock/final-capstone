import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import ForumService from "../services/ForumService";
import PostService from "../services/PostService"

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
/* this is for if we have already logged in */
let currentToken = localStorage.getItem('token')
let currentUser = '';
try {
  currentUser = JSON.parse(localStorage.getItem('user'));
} catch (e) {
  currentToken = '';
  localStorage.removeItem('token');
  localStorage.removeItem('user');
}


if (currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    topic: '',
    forums: [],
    filteredForums: [],
    postsList: [],
    filteredPosts: [],
    replyList: [],
    form: false,
    menu: false,
    selectForum: null,
    selectPost: {
      image: "",
      header: "",
      content: "",
      forumID: null,
      parentPostID: null,
    },
    SelectPostParent: null,
    newForum: {
      image: "",
      topic: "",
      title: "",
      description: "",
    },
    posts: false,
    keyword: "",
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      /* for when we log in through the front end. Saving the data */
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      /* for when we log in through the front end. Saving the data */
      state.user = user;
      localStorage.setItem('user', JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
    TOGGLE_SOME_POSTS(state) {
      state.posts = !state.posts
    },
    ADD_FORUMS(state) {
      ForumService.getForums().then((response) => {
        state.forums = response.data.forumArray;

      });
    },
    SELECT_FORUM(state, forum) {
      state.selectForum = forum
    },
    ADD_POSTS_BY_FORUMID(state) {
      PostService.getPost(state.selectForum.forumID).then((response) => {
        state.postsList = response.data;
      });
    },
    SAVE_FORUM(state, forum) {
      state.forums.push(forum)
    },
    SELECT_POST(state, post) {
      state.SelectPostParent = post
      state.selectPost = post.replies[0]
    },
    SAVE_POST(state, newPost) {
      newPost.forumID = state.selectForum.forumID
      newPost.parentPostID = state.selectPost.parentPostId
      console.log(newPost)
      PostService.createPost(newPost).then((response) => {
        if (response.status === 200) {
          alert("post created")

          PostService.getPost(state.selectForum.forumID).then((response) => {
            state.postsList = response.data;
          });
          state.selectPost = {
            image: "",
            header: "",
            content: "",
            forumID: null,
            parentPostID: null,
          }
        }
      })
    },
    SEARCH_ALL_POSTS(state, posts) {
      this.postsList = posts;

    },
    UPDATE_SELECT_POST(state) {
      state.selectPost = {
        image: "",
        header: "",
        content: "",
        forumID: null,
        parentPostID: null,
      }
    }

  },
  actions: {
    selectForum(context, forum) {
      context.commit('SELECT_FORUM', forum)
    },
    selectPost(context, post) {
      context.commit("SELECT_POST", post)
    },
    selectNewPost(context, newPost) {
      context.commit('SAVE_POST', newPost)
    },
    savePost(context, newPost) {
      context.commit('SAVE_POST', newPost);
    },
  },
  getters: {

  }

})

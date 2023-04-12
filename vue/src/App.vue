<template>
  <div id="app" class="background">
    <div id="nav">
     
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
    </div>
    <router-view />
  </div>
</template>

<style>
.background {
  height: 100%;
}
</style>
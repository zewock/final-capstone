<template>
  <div class="logo">
    <img class="img" src="../components/logo.png" />
  </div>
</template>

<script>
export default {
  data() {
    return {};
  },
};
</script>
<style scoped>
.img{
  height: 75%;
}
.logo {
  width: 150px;
  height: 150px;
  position: fixed;
  margin-top: 20px;
  margin-left: 50px;
  background-color: #ff9f29;
  background-size: 120%;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  padding-left: 1px;
}
</style>
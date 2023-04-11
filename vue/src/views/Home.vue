<template>

  <div >
    <topbar class="container"/>
     
    
  </div>
</template>

<script>
import Topbar from '../components/Topbar.vue';
export default {
  name: "home",
  components: {
    Topbar

  }
};
</script>

<style>
.container {
  color: grey;
  display: grid;
  grid-template-columns: 200px 4fr 200px;
  grid-template-areas: 
  "nav nav nav"
  "sidebar main ad"
  "sidebar main pm"
  
}
/* {
  border: 1px solid red;
  align-content: center;
}*/

</style>

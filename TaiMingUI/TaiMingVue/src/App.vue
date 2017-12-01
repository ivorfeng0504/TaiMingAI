<template>
  <div id="app">
    <img src="./assets/logo.png">
    <HelloWorld/>
    <Test/>
    <div v-html="h1"></div>
  </div>
</template>

<script>
  import HelloWorld from "./components/HelloWorld";
  import Test from "./components/Test";
  export default {
    name: "app",
    data() {
      return {
        h1: '<h1>中国</h1>'
      }
    },
    components: {
      HelloWorld,
      Test
    }
  }
</script>

<style>
  #app {
    font-family: "Avenir", Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
  }
</style>
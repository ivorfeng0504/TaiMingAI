<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
  <deram/>
  </div>
</template>

<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      msg: "我的第一个Vue！"
    };
  }
};
var deram = new Vue({
  components: {
    template: "<h1>中国梦，我的梦</h1>"
  }
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1,
h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>

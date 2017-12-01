<template>
  <div>
    <h1>{{message}}</h1>
    <div v-html="msg"></div>
    <label for="r1">修改颜色</label>
    <input type="checkbox" v-model="class1" id="r1">
    <br> {{num}}
    <br>
    <ul>
      <template v-for="s in sites">
        <li>          {{ s.name }}
        </li>
        <li>--------------</li>
      </template>
    </ul>
    <div v-bind:class="{'class1': class1}">
      {{userName|nameFilter|nameFilter1}}
    </div>
    <input v-model="userName" />
    <button v-on:click="reverseUsername">翻转</button>
  </div>
</template>
<script>
export default {
  name: "Test",
  data() {
    return {
      message: "你好",
      msg: "TMD",
      class1: false,
      userName: "悟空不会飞",
      num: Math.random(),
      sites: [
        {
          name: "水浒传"
        },
        {
          name: "西游记"
        }
      ]
    };
  },
  methods: {
    reverseUsername: function() {
      this.userName = this.userName
        .split("")
        .reverse()
        .join("");
    }
  },
  filters: {
    nameFilter: function(value) {
      if (!value) return "";
      var firstChar = value.substring(0, 1);
      var endChar = value.substring(1);
      return "&" + firstChar + "&" + endChar;
    },
    nameFilter1: function(value) {
      if (!value) return "";
      var firstChar = value.substring(0, 1);
      var endChar = value.substring(1);
      return "#" + firstChar + "#" + endChar;
    }
  }
};
</script>
<style>
.class1 {
  color: red;
}
</style>
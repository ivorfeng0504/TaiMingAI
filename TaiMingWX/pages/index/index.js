//index.js
//获取应用实例
const app = getApp()

Page({

  /**
   * 页面的初始数据
   */
  data: {
    homeImages: ["/images/index/1.jpg", "/images/index/2.jpg", "/images/index/3.jpg"],
    grids: [{ url: "1", img: "", name: "1" },
      { url: "2", img: "", name: "2" },
      { url: "3", img: "", name: "3" },
      { url: "4", img: "", name: "4" },
      { url: "../task/create/create", img: "", name: "任务" },
      { url: "6", img: "", name: "6" },
      { url: "7", img: "", name: "7" },
      { url: "8", img: "", name: "8" },
      { url: "9", img: "", name: "9" },
    ]
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  },
  click: function (e) {
    console.log(e);
  }
})
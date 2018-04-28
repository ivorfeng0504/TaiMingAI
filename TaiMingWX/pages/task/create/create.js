// pages/task/create.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    visitTime: ["及时", "工作日", "周末", "随时"],
    allInfo: "",
    formData: {
      name: "",//用户姓名
      phone: "",//联系电话
      address: "",//联系地址
      visitTimeIndex: 0,//服务时间
      taskImages: [],//任务图片
      taskContent: "",//任务内容
      isAgree: false,//阅读并同意
    }
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
  /*选择上门时间*/
  visitTimeChange: function (e) {
    this.setData({
      "formData.visitTimeIndex": e.detail.value
    })
  },
  /*上传图片*/
  chooseImage: function () {
    var that = this
    console.log("d");
    wx.chooseImage({
      sourceType: ['camera', 'album'],
      sizeType: ['compressed', 'original'],
      count: 3,
      success: function (res) {
        console.log(res)
        that.setData({
          "formData.taskImages": res.tempFilePaths
        })
      }
    })
  },
  previewImage: function (e) {
    var current = e.target.dataset.src
    wx.previewImage({
      current: current,
      urls: this.data.imageList
    })
  },
  /*阅读并同意*/
  bindAgreeChange: function (e) {
    this.setData({
      'formData.isAgree': !!e.detail.value.length
    });
  },
  bindSubmitTask: function (e) {
    this.data.formData.name = e.detail.value.name;
    this.data.formData.phone = e.detail.value.phone;
    this.data.formData.address = e.detail.value.address;
    this.data.formData.taskContent = e.detail.value.taskContent;
    console.log("x");
    var header = {
      "appId": 1001,
      "time": 123,
      "sign": "122"
    };
    wx.request({
      url: 'http://api.taiming.com/api/Task/Create',
      data: this.data.formData,
      header: header,
      method: "POST",
      success: function (data, statusCode) {
        console.log(JSON.stringify(data) + "," + statusCode);
      }
    })
  }
})
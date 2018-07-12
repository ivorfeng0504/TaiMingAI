using System.Web;
using System.Web.Optimization;

namespace TaiMingAI.Manager
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/Scripts/bootstrap.js",
                      "~/Content/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Css/bootstrap.css",
                      "~/Content/Css/site.css"));
            //layui框架
            bundles.Add(new StyleBundle("~/bundles/layuicss").Include(
                "~/Content/layui/css/layui.css",
                "~/Content/Css/pageCss/layout.css"));
            bundles.Add(new ScriptBundle("~/bundles/layuijs").Include(
                "~/Content/layui/layui.js",
                "~/Content/Scripts/pageJs/layout.js",
                "~/Content/Scripts/pageJs/cache.js"));
            bundles.Add(new ScriptBundle("~/bundles/ifreamlayuijs").Include(
                "~/Content/layui/layui.js",
                "~/Content/Scripts/pageJs/common.js"
                ));
        }
    }
}

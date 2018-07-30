using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;
using System.Runtime.Remoting.Messaging;

namespace ConsoleTest.model
{

    public class DataAccess
    {
        public string db = "sql";
        public IUserServer GetServer()
        {
            IUserServer server = null;
            //switch (db)
            //{
            //    case "sql": server = new SqlUserServer(); break;
            //    case "oracle": server = new OracleUserServer(); break;
            //    default: server = new SqlUserServer(); break;
            //}
            var typeName = ConfigurationManager.AppSettings["db"];
            server =(IUserServer) Assembly.Load("ConsoleTest").CreateInstance(typeName);
            return server;
        }
    }
    public interface ICreateDataBaseFactory
    {
        IUserServer GetUserServer();
        IProductServer GetProductServer();
    }
    public class CreateSqlFactory : ICreateDataBaseFactory
    {
        public IProductServer GetProductServer()
        {
            return new SqlProductServer();
        }

        public IUserServer GetUserServer()
        {
            return new SqlUserServer();
        }
    }
    public class CreateOracleFactory : ICreateDataBaseFactory
    {
        public IProductServer GetProductServer()
        {
            return new OracleProductServer();
        }


        public IUserServer GetUserServer()
        {
            return new OracleUserServer();
        }
    }

    public interface IUserServer
    {
        int InsertUser(UserInfo user);
        UserInfo GetUserInfoById(int id);
    }
    public class SqlUserServer : IUserServer
    {
        public UserInfo GetUserInfoById(int id)
        {
            Console.WriteLine("【sql】得到Id={0}的用户信息。", id);
            return null;
        }

        public int InsertUser(UserInfo user)
        {
            Console.WriteLine("【sql】添加用户（{0}）的信息成功。", user.name);
            return 1;
        }
    }
    public class OracleUserServer : IUserServer
    {
        public UserInfo GetUserInfoById(int id)
        {
            Console.WriteLine("【oracle】得到Id={0}的用户信息。", id);
            return null;
        }

        public int InsertUser(UserInfo user)
        {
            Console.WriteLine("【oracle】添加用户（{0}）的信息成功。", user.name);
            return 2;
        }
    }
    public class UserInfo
    {
        public int Id { get; set; }
        public string name { get; set; }
    }

    public interface IProductServer
    {
        ProductInfo GetProductInfo(int id);
        int InsertProduct(ProductInfo info);
    }
    public class SqlProductServer : IProductServer
    {
        public ProductInfo GetProductInfo(int id)
        {
            Console.WriteLine("【sql】GetProductInfo");
            return null;
        }

        public int InsertProduct(ProductInfo info)
        {
            Console.WriteLine("【sql】InsertProduct");
            return info.Id;
        }
    }
    public class OracleProductServer : IProductServer
    {
        public ProductInfo GetProductInfo(int id)
        {
            Console.WriteLine("【oracle】GetProductInfo");
            return null;
        }

        public int InsertProduct(ProductInfo info)
        {
            Console.WriteLine("【oracle】InsertProduct");
            return 0;
        }
    }
    public class ProductInfo
    {
        public int Id { get; set; }
        public string name { get; set; }
    }

    public interface IOperate
    {
        void GetResult(double x, double y);
    }
    public class OperateSum : IOperate
    {
        public void GetResult(double x, double y)
        {
            Console.WriteLine("{0}+{1}={2}", x, y, x + y);
        }
    }
    public class OperateSub : IOperate
    {
        public void GetResult(double x, double y)
        {
            Console.WriteLine("{0}-{1}={2}", x, y, x - y);
        }
    }

    public interface IOperateFactory
    {
        IOperate GetOperate();
    }
    public class SumOperateFactory : IOperateFactory
    {
        public IOperate GetOperate()
        {
            return new OperateSum();
        }
    }
    public class SubOperateFactory : IOperateFactory
    {
        public IOperate GetOperate()
        {
            return new OperateSub();
        }
    }
}

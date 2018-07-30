using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.model
{
    /// <summary>
    /// 在书单中描述一本书
    /// </summary>
    public struct Book
    {
        public string Title;
        public string Author;
        public decimal Price;
        public bool Paperback;
        public Book(string title, string author, decimal price, bool paperback)
        {
            Title = title;
            Author = author;
            Price = price;
            Paperback = paperback;
        }
    }

    /// <summary>
    /// 声明用于处理图书的委托类型
    /// </summary>
    /// <param name="book"></param>
    public delegate void ProcessBookDelegate(Book book);

    public class BookDB
    {
        public ArrayList arrayList = new ArrayList();
        public void AddBook(string title, string author, decimal price, bool paperback)
        {
            arrayList.Add(new Book(title, author, price, paperback));
        }
        public void ProcessPaperbackBooks(ProcessBookDelegate processBook)
        {

            foreach (Book item in arrayList)
            {
                if (item.Paperback)
                {
                    processBook(item);
                }
            }
        }
    }

    public class PriceTotaller
    {
        int countBooks = 0;
        decimal priceBooks = 0m;
        internal void AddBookTotal(Book book)
        {
            countBooks++;
            priceBooks += book.Price;
        }
        internal decimal AveragePrice()
        {
            return priceBooks / countBooks;
        }
        public void hello(string s, Func<Book, string> func)
        {
            Book book = new Book("金瓶梅", "", 0, false);
            Console.WriteLine(s + func(book));
        }
    }

    public class First
    {
        public static First ASecondRFirst(Second first)
        { return new First(); }

        // The return type is more derived.  
        public static Second ASecondRSecond(Second second)
        { return new Second(); }

        // The argument type is less derived.  
        public static First AFirstRFirst(First first)
        { return new First(); }

        // The return type is more derived   
        // and the argument type is less derived.  
        public static Second AFirstRSecond(First first)
        { return new Second(); }

    }
    public class Second : First
    {
        SampleDelegate sd = AFirstRFirst;
        SampleGenericDelegate<First, Second> sgd = AFirstRSecond;
    }

    public delegate First SampleDelegate(Second second);
    public delegate R SampleGenericDelegate<A, R>(A a);

}

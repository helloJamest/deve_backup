using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace book_tutorial
{
    class Other
    {
        static public string Name = "Mary Jones";
    }

    class Program
    {
        /// <summary>
        /// 子查询
        /// </summary>
        static void TestSubQuery()
        {
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
            // 获取所有长度最短的名字（注意：可能有多个）
            IEnumerable<string> outQuery = names
                .Where(n => n.Length == names　　　　　
                    .OrderBy(n2 => n2.Length)
                    .Select(n2 => n2.Length).First());      // Tom, Jay"

            // 与上面方法语法等价的查询表达式
            IEnumerable<string> outQuery2 =
                from n in names
                where n.Length ==　　　　　　　　　　　　
                    (from n2 in names orderby n2.Length select n2.Length).First()
                select n;

            // 我们可以使用Min查询运算符来简化
            IEnumerable<string> outQuery3 =
                from n in names
                where n.Length == names.Min(n2 => n2.Length)
                select n;
            
            //可以把子查询分离出来对让它只执行一次
            int shortest = names.Min(n => n.Length);
            IEnumerable<string> query = from n in names
                                        where n.Length == shortest
                                        select n;

            // 渐进式查询（Progressive query building）
            IEnumerable<string> query4 =
                from n in names
                select Regex.Replace(n, "[aeiou]", "");

            query = from n in query where n.Length > 2 orderby n select n;

            // into关键词
            IEnumerable<string> query5 = from n in names
                                         select n.Replace("a", "").Replace("e", "").Replace("i", "")
                                                 .Replace("o", "").Replace("u", "")
                                         into noVowel
                                         where noVowel.Length > 2
                                         orderby noVowel
                                         select noVowel;   // Result: Dck, Hrry, Mry


            // 用包装查询方式进行改写(Wrapping Queries)
            IEnumerable<string> query6 =
                from n1 in
                    (
                        from n2 in names
                        select Regex.Replace(n2, "[aeiou]", "")
                    )
                where n1.Length > 2
                orderby n1
                select n1;

            // 与上面等价的方法语法
            IEnumerable<string> query7 = names
                .Select(n => Regex.Replace(n, "[aeiou]", ""))
                .Where(n => n.Length > 2)
                .OrderBy(n => n);

            // let关键字
            var query8 = from n in names
                        let Vowelless = Regex.Replace(n, "[aeiou]", "")
                        where Vowelless.Length > 2
                        select n;   //正是因为使用了let，此时n仍然可见
        }


        static void Main()
        {
            string Major = "History";
            var student = new { Age = 19, Other.Name, Major };
            Console.WriteLine("{0}, Age {1}, Major {2}", student.Name, student.Age, student.Major);

            int[] numbers = { 2, 5, 28, 31, 17, 16, 42 };
            var numsQuery = from n in numbers
                            where n < 20
                            select n;
            var numsMethod = numbers.Where(x => x < 20);
            int numsCount = (from n in numbers
                             where n < 20
                             select n).Count();
            /*
            var query = from s in students
                        join c in studentsInCourses on s.StID equals c.StID
                        where c.CourseName == "History"]
                        select s.LastName;
            */

            var groupA = new[] { 3, 4, 5, 6 };
            var groupB = new[] { 6, 7, 8, 9 };
            var someInts = from a in groupA
                           from b in groupB
                           let sum = a + b
                           where sum == 12
                           where a >= 4
                           orderby b
                           select new { a, b, sum };
            
            var groupAandB = groupA.Concat(groupB);
            foreach (var c in groupAandB) 
            {
                Console.WriteLine(c);
            }


            var students = new[]
            {
                new { LName="Jones", FName="Mary", Age=19, Mahor="History"},
                new { LName="Smith", FName="Bob", Age=20, Mahor="CompSci"},
                new { LName="Fleming", FName="Carol", Age=21, Mahor="History"},
            };
            var query = from stu in students
                        group stu by stu.Mahor;
            foreach (var s in query)
            {
                Console.WriteLine(s.Key);
            }



            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
            //   extension methods make LINQ elegant
            IEnumerable<string> query1 = names
                .Where(n => n.Contains("a"))
                .OrderBy(n => n.Length)
                .Select(n => n.ToUpper());

            //   static methods lose query's fluency
            IEnumerable<string> query2 =
                Enumerable.Select(
                    Enumerable.OrderBy(
                        Enumerable.Where(names, n => n.Contains("a")
                        ), n => n.Length
                    ), n => n.ToUpper()
                );

            string[] newnames = { "Tom", "Dick", "Harry", "Mary", "Jay" };
            IEnumerable<string> newquery = newnames
                .Where(n => n.Contains("a"))
                .OrderBy(n => n.Length)
                .Select(n => n.ToUpper());
            foreach (string name in newquery) Console.WriteLine(name);

            int[] seq1 = { 1, 2, 2, 3 };
            int[] seq2 = { 3, 4, 5 };
            IEnumerable<int> concat = seq1.Concat(seq2);    // { 1, 2, 2, 3, 3, 4, 5 }
            IEnumerable<int> union = seq1.Union(seq2);      // { 1, 2, 3, 4, 5 }
        }
    }
}

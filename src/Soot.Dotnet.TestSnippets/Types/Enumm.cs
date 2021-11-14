using System;

namespace Soot.Dotnet.TestSnippets.Types
{
    /// <summary>
    /// Produce enum stuff, also for attributes
    /// </summary>
    public class Enumm
    {
        public enum MyEnum
        {
            Dies,
            Das,
            Ananas
        }

        public static void DoSth(MyEnum e = MyEnum.Ananas)
        {
            var c = e;
            if (c == MyEnum.Dies)
            {
                int x = 0;
            }
        }

        public static void CallDoSth()
        {
            var e = MyEnum.Das;
            DoSth(e);
        }

        #region Attributes

        public class MyCustomAttribute : Attribute {
            public int[] Values { get; set; }

            private MyEnum Enu { get; set; }

            public MyCustomAttribute(int[] values) {
                Values = values;
            }
        
            public MyCustomAttribute(MyEnum myEnum)
            {
                Enu = myEnum;
            }
        
            public MyCustomAttribute(object myAttrCls)
            {
            }

            private void DoSth()
            {
                var en = MyEnum.Ananas;
                InvokeWithEnum(en);
            }

            private void InvokeWithEnum(MyEnum myEnum)
            {
            
            }

        }

        // [MyCustomAttribute(new[] { 3, 4, 5 })]
        // [MyCustomAttribute(MyEnum.Ananas)]
        // [MyCustomAttribute(typeof(Typ))]
        public class MyArr { }
    
        public interface IMyAttrCls { }

        #endregion
    }
}
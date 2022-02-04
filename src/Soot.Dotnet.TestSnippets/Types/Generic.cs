using System.Collections.Generic;

namespace Soot.Dotnet.TestSnippets.Types
{
    public class Generic
    {
        private class ExampleClass { }
        
        public static void DoSth(GenericList<string> list)
        {
            // Declare a list of type int.
            GenericList<int> list1 = new GenericList<int>();
            list1.Add(1);

            // Declare a list of type string.
            GenericList<string> list2 = new GenericList<string>();
            list2.Add("");

            // Declare a list of type ExampleClass.
            GenericList<ExampleClass> list3 = new GenericList<ExampleClass>();
            list3.Add(new ExampleClass());
        }

        public static void DoGen(GenericList<int> genericList)
        {
            int x = 0;
        }

        public static void DoGen(GenericList<string> genericList)
        {
            int x = 1;
        }
    }
    
    // Declare the generic class.
    public class GenericList<T>
    {
        public void Add(T input)
        {
            _lst.Add(input);
        }

        public T Get()
        {
            return _lst[0];
        }

        public List<T> GetAll()
        {
            return _lst;
        }

        private readonly List<T> _lst = new List<T>();
    }

}
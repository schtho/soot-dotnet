using System;

namespace Soot.Dotnet.TestSnippets.Members
{
    /// <summary>
    /// indexer
    /// </summary>
    public class Indexe
    {
        public static void DoSth()
        {
            var stringCollection = new SampleCollection<string>();
            stringCollection[0] = "Hello, World";
            Console.WriteLine(stringCollection[0]);
            
            var stringCollection2 = new MyIndexer
            {
                [0] = "Hello, World"
            };
            Console.WriteLine(stringCollection2[0]);
        }
    }
    
    class SampleCollection<T>
    {
        // Declare an array to store the data elements.
        private T[] arr = new T[100];

        // Define the indexer to allow client code to use [] notation.
        public T this[int i]
        {
            get => arr[i];
            set => arr[i] = value;
        }
    }

    class MyIndexer
    {
        // Declare an array to store the data elements.
        private string[] arr = new string[100];

        // Define the indexer to allow client code to use [] notation.
        public string this[int i]
        {
            get => arr[i];
            set => arr[i] = value;
        }
    }
    
}
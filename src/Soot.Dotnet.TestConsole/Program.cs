using System;
using System.Runtime.InteropServices;
using Google.Protobuf;
using Soot.Dotnet.Decompiler;
using Soot.Dotnet.Decompiler.Models.Cli;
using Soot.Dotnet.Decompiler.Models.Protobuf;

namespace Soot.Dotnet.TestConsole
{
    /// <summary>
    /// A project for debugging ILSpy objects or proto messages
    /// Only debugging purpose
    /// </summary>
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // TestGetAllTypes1();
            // TestTypeDefinition();
            // TestTypeDefinition3();
            // TestGetMethodBody();
            // TestGetMethodBody2();
            
            TestGetMethodBody4();
            
            // TestProperty();

            // TestIsAssembly();
        }

        private const string PathTestSnippetDll = "/Users/thomasschmeiduch/Documents/Git/UNI/soot-dotnet/src/Soot.Dotnet.TestSnippets/bin/Debug/netcoreapp3.1/Soot.Dotnet.TestSnippets.dll";

        private const string PathCoreLib = "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Private.CoreLib.dll";

        #region Get Types

        static void TestGetAllTypes1()
        {
            var decompilerParams = new AnalyzerParamsMsg
            {
                AssemblyFileAbsolutePath = PathCoreLib,
                AnalyzerMethodCall = AnalyzerMethodCall.GetAllTypes
            };
            var paramArr = new CliByteArray();
            paramArr.SetArray(decompilerParams.ToByteArray());
            var allTypes = AssemblyProvider.GetAllTypesMsg(paramArr);
            
            // deserialize test
            var a = new AssemblyAllTypes();
            a.MergeFrom(allTypes.GetArray());
        }

        static void TestTypeDefinition()
        {
            AnalyzerParamsMsg protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetTypeDef,
                DebugMode = true,
                AssemblyFileAbsolutePath = PathCoreLib,
                TypeReflectionName = "System.Environment"
            };
            
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            
            // deserialize test
            var msg = new TypeDefinition();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }

        static void TestTypeDefinition3()
        {
            AnalyzerParamsMsg protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetTypeDef,
                DebugMode = true,
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.Types.IEinInter"
            };
            
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            
            // deserialize test
            var msg = new TypeDefinition();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }

        #endregion

        #region Members

        static void TestGetMethodBody()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                DebugMode = true,
                AssemblyFileAbsolutePath = PathCoreLib,
                TypeReflectionName = "System.String",
                MethodName = "Replace",
                MethodPeToken = 0x0
            };
            HelperTestMethodBody(protoParams);
        }
        
        static void TestGetMethodBody1()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "ToDictionary[[(``0),(``0,``1),(``0,``2)]]",
                MethodPeToken = 0x0,
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.Typ",
                DebugMode = true
            };
            HelperTestMethodBody(protoParams);
        }
        
        static void TestGetMethodBody2()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "TryFilter",
                MethodPeToken = 0x0,
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.IL.Try",
                DebugMode = true
            };
            HelperTestMethodBody(protoParams);
        }
        
        static void TestGetMethodBody3()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "Calc",
                MethodPeToken = 0x0,
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.SubPart.SubClass+NestedClass",
                DebugMode = true
            };
            HelperTestMethodBody(protoParams);
        }

        static void TestGetMethodBody4()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "MyFunction",
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.CallByRefVal",
                MethodPeToken = 0x06000005,
                DebugMode = true
            };
            HelperTestMethodBody(protoParams);
        }

        static void TestGetMethodBody5()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "DoAlloc",
                MethodPeToken = 0x0,
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.IL.LocAlloc",
                DebugMode = true
            };
            HelperTestMethodBody(protoParams);
        }

        static void TestProperty()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBodyOfProperty,
                DebugMode = true,
                AssemblyFileAbsolutePath = PathCoreLib,
                TypeReflectionName = "System.String",
                PropertyName = "Length",
                PropertyIsSetter = true
            };
            HelperTestMethodBody(protoParams);
        }

        #endregion

        static void TestIsAssembly()
        {
            CliString assemblyFile;
            assemblyFile.Message = Marshal.StringToHGlobalAuto(PathTestSnippetDll);

            var isAssembly = AssemblyProvider.IsAssembly(assemblyFile, assemblyFile);
        }
        
        private static void HelperTestMethodBody(AnalyzerParamsMsg protoParams)
        {
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            var msg = new IlFunctionMsg();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }
        
    }
}
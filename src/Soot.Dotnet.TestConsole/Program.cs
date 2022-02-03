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
            TestGetAllTypes1();
            // TestProperty();
            // TestGetTypeDefinitions();
            // TestGetMethodBody();
            // TestTypeDefinition();
            // TestGetMethodBody();
            // TestGetMethodBody2();
            
            // TestTypeDefinition3();
            // TestGetMethodBody5();
            // TestMultiAssembly();
            
            // TestGetMethodBody9();
            TestGetMethodBody11();
            // TestGetMethodBody8();
        }

        private const string PathTestSnippetDll = "/Users/thomasschmeiduch/Documents/Git/UNI/soot-dotnet/src/Soot.Dotnet.TestSnippets/bin/Debug/netcoreapp3.1/Soot.Dotnet.TestSnippets.dll";

        private const string CoreLibPath = "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Private.CoreLib.dll";

        #region Get Types

        static void TestGetAllTypes1()
        {
            var decompilerParams = new AnalyzerParamsMsg
            {
                AssemblyFileAbsolutePath = CoreLibPath,
                AnalyzerMethodCall = AnalyzerMethodCall.GetAllTypes
            };
            var paramArr = new CliByteArray();
            paramArr.SetArray(decompilerParams.ToByteArray());
            var jo = AssemblyProvider.GetAllTypesMsg(paramArr);
            
            var a = new AssemblyAllTypes();
            a.MergeFrom(jo.GetArray());
        }

        static void TestGetTypeDefinitions()
        {
            var decompilerParams = new AnalyzerParamsMsg
            {
                AssemblyFileAbsolutePath = CoreLibPath,
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
                AssemblyFileAbsolutePath = CoreLibPath,
                TypeReflectionName = "System.Environment"
            };
            
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            
            // deserialize test
            var msg = new AssemblyAllTypes();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }
        
        static void TestTypeDefinition2()
        {
            AnalyzerParamsMsg protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetTypeDef,
                DebugMode = true,
                AssemblyFileAbsolutePath =
                    "/Users/thomasschmeiduch/ma/assemblies/HtmlAgilityPack-PCL.dll",
                TypeReflectionName = "FBAccount.TinkerAccount"
            };
            
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            
            // deserialize test
            var msg = new AssemblyAllTypes();
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
            var msg = new AssemblyAllTypes();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }
        
        static void TestMultiAssembly()
        {
            var decompilerParams = new AnalyzerParamsMsg
            {
                AssemblyFileAbsolutePath =
                    "/Users/thomasschmeiduch/multi/myAssembly.exe",
                AnalyzerMethodCall = AnalyzerMethodCall.GetAllTypes
            };
            var paramArr = new CliByteArray();
            paramArr.SetArray(decompilerParams.ToByteArray());
            var jo = AssemblyProvider.GetAllTypesMsg(paramArr);
            
            var a = new AssemblyAllTypes();
            a.MergeFrom(jo.GetArray());
        }

        #endregion

        #region Members

        static void TestGetMethodBody1()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "ToDictionary[[(``0),(``0,``1),(``0,``2)]]",
                AssemblyFileAbsolutePath =
                    "/Users/thomasschmeiduch/RiderProjects/ConsoleApp1InnerClass/ConsoleApp1InnerClass/bin/Debug/netcoreapp3.1/ConsoleApp1InnerClass.dll",
                TypeReflectionName = "ConsoleApp1InnerClass.Typ",
                DebugMode = true
            };
            // protoParams.AssemblyFileAbsolutePath =
            //     "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Private.CoreLib.dll";
            // protoParams.AssemblyFileAbsolutePath =
            //     "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Linq.dll";
            // protoParams.TypeReflectionName = "System.Reflection.RuntimeAssembly";
            protoParams.MethodName = "DoRefOutString";
            protoParams.MethodParams.Add(new SootTypeMsg { Kind = SootTypeMsg.Types.Kind.Primitive, TypeName = "System.Int32"});
            
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            // var m = AssemblyAnalyzer.GetMethodBody(s1, s2, s3);
            var msg = new IlFunctionMsg();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }
        static void TestGetMethodBody6()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "TryFilter",
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.IL.Try",
                DebugMode = true
            };
            // protoParams.MethodParams.Add(new SootTypeMsg { Kind = SootTypeMsg.Types.Kind.Primitive, TypeName = "System.Int32"});
            
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            // var m = AssemblyAnalyzer.GetMethodBody(s1, s2, s3);
            var msg = new IlFunctionMsg();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }
        
        static void TestGetMethodBody9()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "Calc",
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.SubPart.SubClass+NestedClass",
                DebugMode = true
            };
            // protoParams.MethodParams.Add(new SootTypeMsg { Kind = SootTypeMsg.Types.Kind.Primitive, TypeName = "System.Int32"});
            
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            // var m = AssemblyAnalyzer.GetMethodBody(s1, s2, s3);
            var msg = new IlFunctionMsg();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }
        
        static void TestGetMethodBody10()
                {
                    var protoParams = new AnalyzerParamsMsg
                    {
                        AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                        MethodName = "ParamRefAction",
                        AssemblyFileAbsolutePath = "/Users/thomasschmeiduch/RiderProjects/HelloWorldConsole/HelloWorldConsole/bin/Debug/netcoreapp3.1/HelloWorldConsole.dll",
                        TypeReflectionName = "HelloWorldConsole.Program",
                        DebugMode = true
                    };
                    // protoParams.MethodParams.Add(new SootTypeMsg { Kind = SootTypeMsg.Types.Kind.Primitive, TypeName = "System.Int32"});
                    
                    CliByteArray protoParamArr = default;
                    protoParamArr.SetArray(protoParams.ToByteArray());
                    
                    var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
                    // var m = AssemblyAnalyzer.GetMethodBody(s1, s2, s3);
                    var msg = new IlFunctionMsg();
                    if (m.Length != 0)
                        msg.MergeFrom(m.GetArray());
        
                    int x = 0;
                }
        static void TestGetMethodBody11()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "Join",
                AssemblyFileAbsolutePath = "/Users/thomasschmeiduch/netfw/v4.8/mscorlib.dll",
                TypeReflectionName = "System.String",
                DebugMode = true
            };
            protoParams.MethodParams.Add(new SootTypeMsg { Kind = SootTypeMsg.Types.Kind.Ref, TypeName = "System.String"});
            protoParams.MethodParams.Add(new SootTypeMsg { Kind = SootTypeMsg.Types.Kind.ArrayRef, TypeName = "System.String", NumDimensions = 1});
            
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            // var m = AssemblyAnalyzer.GetMethodBody(s1, s2, s3);
            var msg = new IlFunctionMsg();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }
        
        static void TestGetMethodBody7()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "Overr",
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.Types.B",
                DebugMode = true
            };
            // protoParams.MethodParams.Add(new SootTypeMsg { Kind = SootTypeMsg.Types.Kind.Primitive, TypeName = "System.Int32"});
            
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            var msg = new IlFunctionMsg();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }

        static void TestGetMethodBody8()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                MethodName = "DoAlloc",
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.IL.LocAlloc",
                DebugMode = true
            };
            
            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            var msg = new IlFunctionMsg();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }

        
        static void TestProperty()
        {
            AnalyzerParamsMsg protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBodyOfProperty,
                DebugMode = true,
                AssemblyFileAbsolutePath =
                    "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Private.CoreLib.dll",
                TypeReflectionName = "System.String",
                PropertyName = "Length",
                PropertyIsSetter = false
            };


            CliByteArray protoParamArr = default;
            protoParamArr.SetArray(protoParams.ToByteArray());
            
            var m = AssemblyProvider.GetAssemblyContent(protoParamArr);
            var msg = new IlFunctionMsg();
            if (m.Length != 0)
                msg.MergeFrom(m.GetArray());

            int x = 0;
        }

        static void TestGetMethodBody()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                DebugMode = true,
                AssemblyFileAbsolutePath = CoreLibPath,
                TypeReflectionName = "System.String",
                MethodName = "Replace",
                MethodParams = { 
                    new SootTypeMsg { TypeName = "System.String", Kind = SootTypeMsg.Types.Kind.Ref},
                    new SootTypeMsg { TypeName = "System.String", Kind = SootTypeMsg.Types.Kind.Ref}
                }
            };
            HelperTestMethodBody(protoParams);
        }

        static void TestGetMethodBody2()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                DebugMode = true,
                AssemblyFileAbsolutePath =
                    "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Private.CoreLib.dll",
                TypeReflectionName = "System.Type",
                MethodName = "GetEnumNames",
                /*MethodParams = { 
                    new SootTypeMsg { TypeName = "System.Type", Kind = SootTypeMsg.Types.Kind.ArrayRef }
                }*/
                MethodParams = { }
            };
            HelperTestMethodBody(protoParams);
        }
        
        static void TestGetMethodBody3()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                DebugMode = true,
                AssemblyFileAbsolutePath =
                    "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Private.CoreLib.dll",
                TypeReflectionName = "System.Boolean",
                MethodName = "Parse[[(System.Char)]]",
                MethodParams = { 
                    new SootTypeMsg { TypeName = "System.ReadOnlySpan`1", Kind = SootTypeMsg.Types.Kind.Ref }
                }
                //MethodParams = { }
            };
            HelperTestMethodBody(protoParams);
        }

        static void TestGetMethodBody4()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                DebugMode = true,
                AssemblyFileAbsolutePath =
                    "/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Private.CoreLib.dll",
                TypeReflectionName = "System.Threading.CancellationTokenSource",
                MethodName = "ExecuteCallbackHandlers",
                MethodParams = { 
                    new SootTypeMsg { TypeName = "boolean", Kind = SootTypeMsg.Types.Kind.Primitive }
                }
                //MethodParams = { }
            };
            HelperTestMethodBody(protoParams);
        }
        
        static void TestGetMethodBody5()
        {
            var protoParams = new AnalyzerParamsMsg
            {
                AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody,
                DebugMode = true,
                AssemblyFileAbsolutePath = PathTestSnippetDll,
                TypeReflectionName = "Soot.Dotnet.TestSnippets.Members.Native",
                MethodName = "MessageBox",
                MethodParams = { 
                    new SootTypeMsg { TypeName = "int", Kind = SootTypeMsg.Types.Kind.Primitive},
                    new SootTypeMsg { TypeName = "System.String", Kind = SootTypeMsg.Types.Kind.Ref},
                    new SootTypeMsg { TypeName = "System.String", Kind = SootTypeMsg.Types.Kind.Ref},
                    new SootTypeMsg { TypeName = "int", Kind = SootTypeMsg.Types.Kind.Primitive}
                }
                // IntPtr hWnd, string lpText, string lpCaption, uint uType
            };
            HelperTestMethodBody(protoParams);
        }

        #endregion
        
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
        
        static void TestDllDeprecated()
        {
            CliString assemblyFile, type, method;
            
            // assemblyFile.Message = Marshal.StringToHGlobalAuto("/Users/thomasschmeiduch/RiderProjects/ConsoleApp1InnerClass/ConsoleApp1InnerClass/bin/Debug/netcoreapp3.1/ConsoleApp1InnerClass.dll");
            // assemblyFile.Message = Marshal.StringToHGlobalAuto("/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Private.CoreLib.dll");
            assemblyFile.Message = Marshal.StringToHGlobalAuto("/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Linq.dll");
            assemblyFile.Message = Marshal.StringToHGlobalAuto("/Users/thomasschmeiduch/RiderProjects/ConsoleApp1InnerClass/ConsoleApp1InnerClass/bin/Debug/netcoreapp3.1/ConsoleApp1InnerClass.dll");
            type.Message = Marshal.StringToHGlobalAuto("ConsoleApp1InnerClass.Try+<TryFault>d__0");
            type.Message = Marshal.StringToHGlobalAuto("ConsoleApp1InnerClass.Sync");
            
            method.Message = Marshal.StringToHGlobalAuto(".ctor");
            method.Message = Marshal.StringToHGlobalAuto("MoveNext");
            
            // s2.Message = Marshal.StringToHGlobalAuto("ConsoleApp1InnerClass.Lol.Lel+Lul");
            // s3.Message = Marshal.StringToHGlobalAuto("Alt");
            
            type.Message = Marshal.StringToHGlobalAuto("ConsoleApp1InnerClass.Typ");
            method.Message = Marshal.StringToHGlobalAuto("DoTuple");

            // s1.Message = Marshal.StringToHGlobalAuto("/usr/local/share/dotnet/shared/Microsoft.NETCore.App/3.1.13/System.Private.CoreLib.dll");
            // s2.Message = Marshal.StringToHGlobalAuto("System.Span`1");
            // s3.Message = Marshal.StringToHGlobalAuto(".cctor");
            // sizeof Opcode
            // s2.Message = Marshal.StringToHGlobalAuto("System.Runtime.InteropServices.Marshal");
            // s3.Message = Marshal.StringToHGlobalAuto("AddRef");
        }

    }
}
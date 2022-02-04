using System;
using Soot.Dotnet.Decompiler.Helper;
using Soot.Dotnet.Decompiler.Models.Cli;
using Soot.Dotnet.Decompiler.Models.Protobuf;
using Soot.Dotnet.Decompiler.Parser;
using Soot.Dotnet.Decompiler.ProtoConverter;

namespace Soot.Dotnet.Decompiler
{
    /// <summary>
    /// Main Class for the Bridge Soot.Dotnet.NativeHost
    /// </summary>
    public static class AssemblyProvider
    {
        static AssemblyProvider()
        {
            // Init Logger, because cannot find nlog.config while running with Soot.Dotnet.NativeHost
            LoggerUtils.InitNlog();
        }

        #region NativeHost Delegates

        public delegate bool IsAssemblyDelegate(CliString arg, CliString arg2);

        public delegate CliByteArray GetAllTypesMsgDelegate(CliByteArray arg);

        public delegate CliByteArray GetMethodBodyMsgDelegate(CliByteArray arg);

        public delegate CliByteArray GetMethodBodyOfPropertyMsgDelegate(CliByteArray arg);

        public delegate CliByteArray GetMethodBodyOfEventMsgDelegate(CliByteArray arg);

        public delegate CliByteArray GetAssemblyContentDelegate(CliByteArray arg);

        #endregion

        /// <summary>
        /// Check if given assembly is an assembly
        /// </summary>
        /// <param name="assemblyFileAbsolutePath">assembly with absolute path</param>
        /// <param name="arg2">not used</param>
        /// <returns></returns>
        public static bool IsAssembly(CliString assemblyFileAbsolutePath, CliString arg2)
        {
            return AssemblyUtils.FileIsAssembly(assemblyFileAbsolutePath.ToString());
        }
        
        /// <summary>
        /// Load an assembly and return a list of given classes/types with their member definitions
        /// </summary>
        /// <param name="disassemblerParamsMsg">parameters with details about the given assembly and procedure</param>
        /// <returns>list of types</returns>
        public static CliByteArray GetAllTypesMsg(CliByteArray disassemblerParamsMsg)
        {
            var paramsMsg = CliByteArrayConverter.ToAnalyzerParamsMsg(disassemblerParamsMsg);
            if (paramsMsg == null)
                return new CliByteArray();
            paramsMsg.AnalyzerMethodCall = AnalyzerMethodCall.GetAllTypes;
            var newParams = CliByteArrayConverter.FromAnalyzerParamsMsg(paramsMsg);
            return GetAssemblyContent(newParams);
        }
        
        /// <summary>
        /// For given Method Definition load method body
        /// </summary>
        /// <param name="disassemblerParamsMsg">parameters with details about the given assembly and procedure</param>
        /// <returns>set of instructions of method body</returns>
        public static CliByteArray GetMethodBodyMsg(CliByteArray disassemblerParamsMsg)
        {
            var paramsMsg = CliByteArrayConverter.ToAnalyzerParamsMsg(disassemblerParamsMsg);
            if (paramsMsg == null)
                return new CliByteArray();
            paramsMsg.AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBody;
            var newParams = CliByteArrayConverter.FromAnalyzerParamsMsg(paramsMsg);
            return GetAssemblyContent(newParams);
        }
        
        /// <summary>
        /// For given Property Definition load method body (set, get)
        /// </summary>
        /// <param name="disassemblerParamsMsg">parameters with details about the given assembly and procedure</param>
        /// <returns>set of instructions of method body</returns>
        public static CliByteArray GetMethodBodyOfPropertyMsg(CliByteArray disassemblerParamsMsg)
        {
            var paramsMsg = CliByteArrayConverter.ToAnalyzerParamsMsg(disassemblerParamsMsg);
            if (paramsMsg == null)
                return new CliByteArray();
            paramsMsg.AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBodyOfProperty;
            var newParams = CliByteArrayConverter.FromAnalyzerParamsMsg(paramsMsg);
            return GetAssemblyContent(newParams);
        }
        
        /// <summary>
        /// For given Property Definition load method body (add, remove, invoke)
        /// </summary>
        /// <param name="disassemblerParamsMsg">parameters with details about the given assembly and procedure</param>
        /// <returns>set of instructions of method body</returns>
        public static CliByteArray GetMethodBodyOfEventMsg(CliByteArray disassemblerParamsMsg)
        {
            var paramsMsg = CliByteArrayConverter.ToAnalyzerParamsMsg(disassemblerParamsMsg);
            if (paramsMsg == null)
                return new CliByteArray();
            paramsMsg.AnalyzerMethodCall = AnalyzerMethodCall.GetMethodBodyOfEvent;
            var newParams = CliByteArrayConverter.FromAnalyzerParamsMsg(paramsMsg);
            return GetAssemblyContent(newParams);
        }

        /// <summary>
        /// Base method for getting requested information of Soot (Java)
        /// Can be used if Bridge (C++) does not implement respective method, which can then used in Soot (Java),
        /// due to the fact that every proto message is a byte array
        /// </summary>
        /// <param name="disassemblerParamsMsg">parameters with details about the given assembly and procedure</param>
        /// <returns>proto message response of requested action</returns>
        public static CliByteArray GetAssemblyContent(CliByteArray disassemblerParamsMsg)
        {
            var paramsMsg = CliByteArrayConverter.ToAnalyzerParamsMsg(disassemblerParamsMsg);
            if (paramsMsg == null)
                return new CliByteArray();
            
            var logger = NLog.LogManager.GetCurrentClassLogger();
            var assemblyParser = new AssemblyParser(paramsMsg.AssemblyFileAbsolutePath, paramsMsg.DebugMode);

            try
            {
                return paramsMsg.AnalyzerMethodCall switch
                {
                    AnalyzerMethodCall.GetMethodBody => assemblyParser.GetMethodBody(paramsMsg.TypeReflectionName,
                        paramsMsg.MethodName, paramsMsg.MethodNameSuffix, paramsMsg.MethodPeToken),
                    AnalyzerMethodCall.GetMethodBodyOfEvent => assemblyParser.GetMethodBodyOfEvent(
                        paramsMsg.TypeReflectionName, paramsMsg.EventName, paramsMsg.EventAccessorType),
                    AnalyzerMethodCall.GetMethodBodyOfProperty => assemblyParser.GetMethodBodyOfProperty(
                        paramsMsg.TypeReflectionName, paramsMsg.PropertyName, paramsMsg.PropertyIsSetter),
                    AnalyzerMethodCall.GetTypeDef => assemblyParser.GetTypeDefinition(paramsMsg.TypeReflectionName),
                    AnalyzerMethodCall.GetAllTypes => assemblyParser.GetAllTypeDefinitions(),
                    AnalyzerMethodCall.NoCall => throw new SystemException("AssemblyContent Method was not set!"),
                    _ => throw new SystemException("AssemblyContent Method was not set!")
                };
            }
            catch (Exception e)
            {
                logger.Error(e);
                return new CliByteArray();
            }
        }
        
    }
}
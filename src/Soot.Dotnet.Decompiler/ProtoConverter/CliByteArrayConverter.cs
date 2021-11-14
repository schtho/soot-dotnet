using System;
using Google.Protobuf;
using Soot.Dotnet.Decompiler.Models.Cli;
using Soot.Dotnet.Decompiler.Models.Protobuf;

namespace Soot.Dotnet.Decompiler.ProtoConverter
{
    public static class CliByteArrayConverter
    {
        public static AnalyzerParamsMsg ToAnalyzerParamsMsg(CliByteArray protoParams)
        {
            // Deserialize Analyzer Params of proto message
            var logger = NLog.LogManager.GetCurrentClassLogger();
            var paramsMsg = new AnalyzerParamsMsg();
            try
            {
                var protoBytes = protoParams.GetArray();
                if (protoBytes == null || protoBytes.Length == 0)
                    throw new Exception("No Analyzer parameter were passed!");
                paramsMsg.MergeFrom(protoBytes);
                return paramsMsg;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
        }

        public static CliByteArray FromAnalyzerParamsMsg(AnalyzerParamsMsg paramsMsg)
        {
            var returnValue = new CliByteArray();
            returnValue.SetArray(paramsMsg.ToByteArray());
            return returnValue;
        }
    }
}
using System;
using System.Linq;
using ICSharpCode.Decompiler.TypeSystem;
using Soot.Dotnet.Decompiler.Exceptions;
using Soot.Dotnet.Decompiler.Helper;
using Soot.Dotnet.Decompiler.Models;
using Soot.Dotnet.Decompiler.Models.Cli;
using Soot.Dotnet.Decompiler.Models.Protobuf;

namespace Soot.Dotnet.Decompiler.Parser
{
    public partial class AssemblyParser
    {
        public CliByteArray GetMethodBodyOfEvent(string typeReflectionName, string eventName, EventAccessorType accessorType)
        {
            var returnValue = new CliByteArray();
            try
            {
                var declaringType = GetType(typeReflectionName);

                var eventDefinition = declaringType.Events.FirstOrDefault(x => x.Name.Equals(eventName));
                if (eventDefinition == null)
                    throw new MemberNotExistException(MemberNotExistException.Member.Event, eventName);

                IMethod methodDefinition;
                switch (accessorType)
                {
                    case EventAccessorType.AddAccessor: methodDefinition = eventDefinition.AddAccessor;
                        break;
                    case EventAccessorType.InvokeAccessor: methodDefinition = eventDefinition.InvokeAccessor;
                        break;
                    case EventAccessorType.RemoveAccessor: methodDefinition = eventDefinition.RemoveAccessor;
                        break;
                    default:
                        throw new SystemException("Event accessor request is invalid!");
                }
                if (!(methodDefinition is {HasBody: true}))
                    throw new MethodBodyNotExistException(typeReflectionName, methodDefinition.Name + " (event)");

                returnValue = HelperExtractMethodBody(methodDefinition);
            }
            catch (MethodBodyNotExistException e)
            {
                Logger.Warn(e.Message);
            }
            catch (MemberNotExistException e)
            {
                Logger.Warn(e.Message);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return returnValue;
        }
    }
}
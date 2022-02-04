using System;
using System.Linq;
using Soot.Dotnet.Decompiler.Exceptions;
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

                var methodDefinition = accessorType switch
                {
                    EventAccessorType.AddAccessor when eventDefinition.CanAdd => eventDefinition.AddAccessor,
                    EventAccessorType.InvokeAccessor when eventDefinition.CanInvoke => eventDefinition.InvokeAccessor,
                    EventAccessorType.RemoveAccessor when eventDefinition.CanRemove => eventDefinition.RemoveAccessor,
                    _ => null
                };
                
                if (!(methodDefinition is {HasBody: true}))
                    throw new MethodBodyNotExistException(typeReflectionName, eventDefinition.Name, accessorType);
                
                var methodBody = ExtractMethodBody(methodDefinition);
                returnValue = ProtoConverter.ProtoConverter.ConvertMethodBody(methodBody);
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
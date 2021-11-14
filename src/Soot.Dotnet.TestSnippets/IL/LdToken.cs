using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Soot.Dotnet.TestSnippets.IL
{
    public class LdToken
    {
        /**
         * LdTypeToken Case
         */
        public void DoLdTypeToken()
        {
            MethodInfo loadedMethod = new DynamicMethod("", null, null);
            var getMethodMethod = typeof(MethodBase).GetMethod(
                "GetMethodFromHandle", new[] { typeof(RuntimeMethodHandle) });

            var createdMethod = new DynamicMethod(
                "GetMethodInfo", typeof(MethodInfo), Type.EmptyTypes);

            var il = createdMethod.GetILGenerator();
            il.Emit(OpCodes.Ldtoken, loadedMethod);
            il.Emit(OpCodes.Call, getMethodMethod);
            il.Emit(OpCodes.Castclass, typeof(MethodInfo));
            il.Emit(OpCodes.Ret);

            var func = (Func<MethodInfo>)createdMethod.CreateDelegate(typeof(Func<MethodInfo>));
            Console.WriteLine(func());
        }
    }
}
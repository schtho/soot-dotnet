#include "inc/Settings.h"

/*
 * A settings class for storing relevant options for the dotnet host
 */
Settings::Settings() {
    theSettings[RuntimeConfigFileName] = "Soot.Dotnet.Decompiler.runtimeconfig.json";
    theSettings[AssemblyFileName] = "Soot.Dotnet.Decompiler.dll";
    theSettings[MethodFullyQualifiedClassName] = "Soot.Dotnet.Decompiler.AssemblyProvider";
    theSettings[MethodNamespace] = "Soot.Dotnet.Decompiler";
    // is replaced by Soot Framework (Java) at NativeHost.cpp
    theSettings[PathToAssembly] = "/Users/user/RiderProjects/Soot.Dotnet/Soot.Dotnet.NativeHost/bin/Debug/libNativeHost.dylib";

    theSettings[MethodIsAssemblyName] = "IsAssembly";
    theSettings[MethodIsAssemblyDelegateName] = "IsAssemblyDelegate";
    theSettings[MethodAllTypesName] = "GetAllTypesMsg";
    theSettings[MethodAllTypesDelegateName] = "GetAllTypesMsgDelegate";
    theSettings[MethodMethodBodyName] = "GetMethodBodyMsg";
    theSettings[MethodMethodBodyDelegateName] = "GetMethodBodyMsgDelegate";
    theSettings[MethodMethodBodyPropertyName] = "GetMethodBodyOfPropertyMsg";
    theSettings[MethodMethodBodyPropertyDelegateName] = "GetMethodBodyOfPropertyMsgDelegate";
    theSettings[MethodMethodBodyEventName] = "GetMethodBodyOfEventMsg";
    theSettings[MethodMethodBodyEventDelegateName] = "GetMethodBodyOfEventMsgDelegate";
}

string &Settings::operator[](const Settings::SettingName &theName) {
    return theSettings[theName];
}

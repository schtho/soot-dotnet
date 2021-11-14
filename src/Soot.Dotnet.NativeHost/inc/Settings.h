//
// Created by Thomas Schmeiduch
//
#include <map>
#include <string>
#include <iostream>
using namespace std;


class Settings
{
public:
    typedef enum
    {
        RuntimeConfigFileName,
        PathToAssembly,
        AssemblyFileName,
        MethodNamespace,
        MethodFullyQualifiedClassName,

        MethodIsAssemblyName,
        MethodIsAssemblyDelegateName,
        MethodAllTypesName,
        MethodAllTypesDelegateName,
        MethodMethodBodyName,
        MethodMethodBodyDelegateName,
        MethodMethodBodyPropertyName,
        MethodMethodBodyPropertyDelegateName,
        MethodMethodBodyEventName,
        MethodMethodBodyEventDelegateName,
        MethodAssemblyContentName,
        MethodAssemblyContentDelegateName
    } SettingName;

private:
    typedef map<SettingName, string> SettingCollection;
    SettingCollection theSettings;

public:
    Settings();
    string& operator[](const SettingName& theName);
    // void Save ();

};

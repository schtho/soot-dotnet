//
// Main class of this NativeHost Library
// It sets up a native host and starts the managed DLL
// Created by Thomas Schmeiduch
// Based on: https://github.com/dotnet/samples/tree/main/core/hosting/src
//

#include "NativeHost.h"

NativeHost::NativeHost(const std::string& path_to_nativehost)
{
    // Set path to the nativehost lib, which is passed by the Soot framework (Java)
    settings[Settings::SettingName::PathToAssembly] = path_to_nativehost;

    init();
}

void NativeHost::init() {
    // Get the current executable's directory
    // This sample assumes the managed assembly to load and its runtime configuration file are next to the host
    char_t host_path[MAX_PATH];
#if WINDOWS
    auto size = ::GetFullPathNameW(argv[0], sizeof(host_path) / sizeof(char_t), host_path, nullptr);
    assert(size != 0);
#else
    auto resolved = realpath(settings[Settings::SettingName::PathToAssembly].c_str(), host_path);
    assert(resolved != nullptr);
#endif

    root_path = host_path;
    auto pos = root_path.find_last_of(DIR_SEPARATOR);
    assert(pos != string_t::npos);
    root_path = root_path.substr(0, pos + 1);

    //
    // STEP 1: Load HostFxr and get exported hosting functions
    //
    if (!load_hostfxr())
    {
        assert(false && "Failure: Failed to load the HostFxr and their Hosting functions! (load_hostfxr())");
    }

    //
    // STEP 2: Initialize and start the .NET Core runtime
    //
    const string_t config_path = root_path + STR(settings[Settings::SettingName::RuntimeConfigFileName]);
    load_assembly_and_get_function_pointer = get_dotnet_load_assembly(config_path.c_str());
    assert(load_assembly_and_get_function_pointer != nullptr && "Failure: Could not init and start dotnet core runtime. Maybe the runtime config file is missing! (get_dotnet_load_assembly())");
}

bool NativeHost::AssemblyProvider_isAssembly(const std::string& assembly_path) {
    //
    // STEP 3: Load managed assembly and get function pointer to a managed method
    //
    const string_t dotnetlib_path = root_path + STR(settings[Settings::SettingName::AssemblyFileName]);
    std::string dotnetClassStr =
            settings[Settings::SettingName::MethodFullyQualifiedClassName] + ", " +
            settings[Settings::SettingName::MethodNamespace];
    const char_t *dotnet_type = STR(dotnetClassStr.c_str());

    // Function pointer to managed delegate with non-default signature
    typedef bool* (CORECLR_DELEGATE_CALLTYPE *lib_csharp_fn)(cli_string arg1, cli_string arg2);

    lib_csharp_fn csharp_method = nullptr;
    int rc = load_assembly_and_get_function_pointer(
            dotnetlib_path.c_str(),
            dotnet_type,
            STR(settings[Settings::SettingName::MethodIsAssemblyName].c_str()) /*method_name*/,
            STR((settings[Settings::SettingName::MethodFullyQualifiedClassName] + "+" +
                settings[Settings::SettingName::MethodIsAssemblyDelegateName] + ", " +
                settings[Settings::SettingName::MethodNamespace]).c_str()
            ) /*delegate_type_name*/,
            nullptr,
            (void**)&csharp_method);
    assert(rc == 0 && csharp_method != nullptr && "Failure: Could not run the managed method of the managed library! (load_assembly_and_get_function_pointer())");

    //
    // STEP 4: Run managed code
    //
    std::string not_used;
    cli_string path { STR(assembly_path.c_str()) };
    cli_string no_use { STR(not_used.c_str()) };

    bool ret = csharp_method(path, no_use);
    return ret;
}

cli_byte_array NativeHost::AssemblyProvider_getAllTypesMsg(cli_byte_array &disassemblerParams) {
    //
    // STEP 3: Load managed assembly and get function pointer to a managed method
    //
    const string_t dotnetlib_path = root_path + STR(settings[Settings::SettingName::AssemblyFileName]);
    std::string dotnetClassStr =
            settings[Settings::SettingName::MethodFullyQualifiedClassName] + ", " +
            settings[Settings::SettingName::MethodNamespace];
    const char_t *dotnet_type = STR(dotnetClassStr.c_str());

    // Function pointer to managed delegate with non-default signature
    typedef cli_byte_array (CORECLR_DELEGATE_CALLTYPE *lib_csharp_fn)(cli_byte_array args1);

    lib_csharp_fn csharp_method = nullptr;
    int rc = load_assembly_and_get_function_pointer(
            dotnetlib_path.c_str(),
            dotnet_type,
            STR(settings[Settings::SettingName::MethodAllTypesName].c_str()) /*method_name*/,
            STR((settings[Settings::SettingName::MethodFullyQualifiedClassName] + "+" +
                settings[Settings::SettingName::MethodAllTypesDelegateName] + ", " +
                settings[Settings::SettingName::MethodNamespace]).c_str()
            ) /*delegate_type_name*/,
            nullptr,
            (void**)&csharp_method);
    assert(rc == 0 && csharp_method != nullptr && "Failure: Could not run the managed method of the managed library! (load_assembly_and_get_function_pointer())");

    //
    // STEP 4: Run managed code
    //
    cli_byte_array ret = csharp_method(disassemblerParams);
    return ret;
}

cli_byte_array NativeHost::AssemblyProvider_GetMethodBodyMsg(cli_byte_array &disassemblerParams) {
    //
    // STEP 3: Load managed assembly and get function pointer to a managed method
    //
    const string_t dotnetlib_path = root_path + STR(settings[Settings::SettingName::AssemblyFileName]);
    std::string dotnetClassStr =
            settings[Settings::SettingName::MethodFullyQualifiedClassName] + ", " +
            settings[Settings::SettingName::MethodNamespace];
    const char_t *dotnet_type = STR(dotnetClassStr.c_str());

    // Function pointer to managed delegate with non-default signature
    typedef cli_byte_array (CORECLR_DELEGATE_CALLTYPE *lib_csharp_fn)(cli_byte_array arg);

    lib_csharp_fn csharp_method = nullptr;
    int rc = load_assembly_and_get_function_pointer(
            dotnetlib_path.c_str(),
            dotnet_type,
            STR(settings[Settings::SettingName::MethodMethodBodyName].c_str()) /*method_name*/,
            STR((settings[Settings::SettingName::MethodFullyQualifiedClassName] + "+" +
                settings[Settings::SettingName::MethodMethodBodyDelegateName] + ", " +
                settings[Settings::SettingName::MethodNamespace]).c_str()
            ) /*delegate_type_name*/,
            nullptr,
            (void**)&csharp_method);
    assert(rc == 0 && csharp_method != nullptr && "Failure: Maybe the given function/delegate does not exists in the given assembly! (load_assembly_and_get_function_pointer())");

    //
    // STEP 4: Run managed code
    //
    cli_byte_array ret = csharp_method(disassemblerParams);
    return ret;
}

cli_byte_array NativeHost::AssemblyProvider_GetMethodBodyOfPropertyMsg(cli_byte_array &disassemblerParams) {
    //
    // STEP 3: Load managed assembly and get function pointer to a managed method
    //
    const string_t dotnetlib_path = root_path + STR(settings[Settings::SettingName::AssemblyFileName]);
    std::string dotnetClassStr =
            settings[Settings::SettingName::MethodFullyQualifiedClassName] + ", " +
            settings[Settings::SettingName::MethodNamespace];
    const char_t *dotnet_type = STR(dotnetClassStr.c_str());

    // Function pointer to managed delegate with non-default signature
    typedef cli_byte_array (CORECLR_DELEGATE_CALLTYPE *lib_csharp_fn)(cli_byte_array arg);

    lib_csharp_fn csharp_method = nullptr;
    int rc = load_assembly_and_get_function_pointer(
            dotnetlib_path.c_str(),
            dotnet_type,
            STR(settings[Settings::SettingName::MethodMethodBodyPropertyName].c_str()) /*method_name*/,
            STR((settings[Settings::SettingName::MethodFullyQualifiedClassName] + "+" +
                settings[Settings::SettingName::MethodMethodBodyPropertyDelegateName] + ", " +
                settings[Settings::SettingName::MethodNamespace]).c_str()
            ) /*delegate_type_name*/,
            nullptr,
            (void**)&csharp_method);
    assert(rc == 0 && csharp_method != nullptr && "Failure: Maybe the given function/delegate does not exists in the given assembly! (load_assembly_and_get_function_pointer())");

    //
    // STEP 4: Run managed code
    //
    cli_byte_array ret = csharp_method(disassemblerParams);
    return ret;
}

cli_byte_array NativeHost::AssemblyProvider_GetMethodBodyOfEventMsg(cli_byte_array &disassemblerParams) {
    //
    // STEP 3: Load managed assembly and get function pointer to a managed method
    //
    const string_t dotnetlib_path = root_path + STR(settings[Settings::SettingName::AssemblyFileName]);
    std::string dotnetClassStr =
            settings[Settings::SettingName::MethodFullyQualifiedClassName] + ", " +
            settings[Settings::SettingName::MethodNamespace];
    const char_t *dotnet_type = STR(dotnetClassStr.c_str());

    // Function pointer to managed delegate with non-default signature
    typedef cli_byte_array (CORECLR_DELEGATE_CALLTYPE *lib_csharp_fn)(cli_byte_array arg);

    lib_csharp_fn csharp_method = nullptr;
    int rc = load_assembly_and_get_function_pointer(
            dotnetlib_path.c_str(),
            dotnet_type,
            STR(settings[Settings::SettingName::MethodMethodBodyEventName].c_str()) /*method_name*/,
            STR((settings[Settings::SettingName::MethodFullyQualifiedClassName] + "+" +
                settings[Settings::SettingName::MethodMethodBodyEventDelegateName] + ", " +
                settings[Settings::SettingName::MethodNamespace]).c_str()
            ) /*delegate_type_name*/,
            nullptr,
            (void**)&csharp_method);
    assert(rc == 0 && csharp_method != nullptr && "Failure: Maybe the given function/delegate does not exists in the given assembly! (load_assembly_and_get_function_pointer())");

    //
    // STEP 4: Run managed code
    //
    cli_byte_array ret = csharp_method(disassemblerParams);
    return ret;
}

cli_byte_array NativeHost::AssemblyProvider_GetAssemblyContent(cli_byte_array &protoParams) {
    //
    // STEP 3: Load managed assembly and get function pointer to a managed method
    //
    const string_t dotnetlib_path = root_path + STR(settings[Settings::SettingName::AssemblyFileName]);
    std::string dotnetClassStr =
            settings[Settings::SettingName::MethodFullyQualifiedClassName] + ", " +
            settings[Settings::SettingName::MethodNamespace];
    const char_t *dotnet_type = STR(dotnetClassStr.c_str());

    // Function pointer to managed delegate with non-default signature
    typedef cli_byte_array (CORECLR_DELEGATE_CALLTYPE *lib_csharp_fn)(cli_byte_array args);

    lib_csharp_fn csharp_method = nullptr;
    int rc = load_assembly_and_get_function_pointer(
            dotnetlib_path.c_str(),
            dotnet_type,
            STR("GetAssemblyContent") /*method_name*/,
            STR((settings[Settings::SettingName::MethodFullyQualifiedClassName] + "+" +
                 "GetAssemblyContentDelegate" + ", " +
                 settings[Settings::SettingName::MethodNamespace]).c_str()
            ) /*delegate_type_name*/,
            nullptr,
            (void**)&csharp_method);
    assert(rc == 0 && csharp_method != nullptr && "Failure: Maybe the given function/delegate does not exists in the given assembly! (load_assembly_and_get_function_pointer())");

    //
    // STEP 4: Run managed code
    //
    cli_byte_array ret = csharp_method(protoParams);
    return ret;
}

// ### PRIVATE ###

void *NativeHost::load_library(const char_t *path)
{
#ifdef WINDOWS
    HMODULE h = ::LoadLibraryW(path);
    assert(h != nullptr);
    return (void*)h;
#else
    void *h = dlopen(path, RTLD_LAZY | RTLD_LOCAL);
    assert(h != nullptr);
    return h;
#endif
}

void *NativeHost::get_export(void *h, const char *name)
{
#ifdef WINDOWS
    void *f = ::GetProcAddress((HMODULE)h, name);
    assert(f != nullptr);
    return f;
#else
    void *f = dlsym(h, name);
    assert(f != nullptr);
    return f;
#endif
}

/// SnippetLoadHostFxr: Using the nethost library, discover the location of hostfxr and get exports
/// \return
bool NativeHost::load_hostfxr()
{
    // Pre-allocate a large buffer for the path to hostfxr
    char_t buffer[MAX_PATH];
    size_t buffer_size = sizeof(buffer) / sizeof(char_t);
    int rc = get_hostfxr_path(buffer, &buffer_size, nullptr);
    if (rc != 0)
        return false;

    // Load hostfxr and get desired exports
    void *lib = load_library(buffer);
    // managed DLL is called and managed by the runtime.config.json file
    init_fptr = (hostfxr_initialize_for_runtime_config_fn)get_export(lib, "hostfxr_initialize_for_runtime_config");
    get_delegate_fptr = (hostfxr_get_runtime_delegate_fn)get_export(lib, "hostfxr_get_runtime_delegate");
    close_fptr = (hostfxr_close_fn)get_export(lib, "hostfxr_close");

    return (init_fptr && get_delegate_fptr && close_fptr);
}

/// SnippetInitialize: Load and initialize .NET Core and get desired function pointer for scenario
/// \param config_path
/// \return
load_assembly_and_get_function_pointer_fn NativeHost::get_dotnet_load_assembly(const char_t *config_path)
{
    // Load .NET Core
    void *load_assembly_and_get_function_pointer = nullptr;
    hostfxr_handle cxt = nullptr;
    // managed DLL is called and managed by the runtime.config.json file
    int rc = init_fptr(config_path, nullptr, &cxt);
    // if 1 already init - https://github.com/dotnet/runtime/blob/main/docs/design/features/host-error-codes.md
    if ((rc != 1 && rc != 0) || cxt == nullptr)
    {
        std::cerr << "Failure: (get_dotnet_load_assembly()) Init failed: " << std::hex << std::showbase << rc << std::endl;
        std::cerr << "Check https://github.com/dotnet/runtime/blob/main/docs/design/features/host-error-codes.md for error code!" << std::endl;
        close_fptr(cxt);
        return nullptr;
    }

/*  // TODO change C++ stack from initialize_for_runtime_config to initialize_for_dotnet_command_line,
    //    to load "self-contained" managed DLL instead of "framework-dependent"
    //    this leads to portable mode of this native host, where we may not need to install dotnet on the target system
    // Currently not working
    // Example project: https://github.com/V3rzeT/EasyCrossH2/tree/master/Bridge

    // insert in bool NativeHost::load_hostfxr():
    run_app_fptr = (hostfxr_run_app_fn)get_export(lib, "hostfxr_run_app");
    // same with hostfxr_initialize_for_dotnet_command_line_fn init_fptr_new; hostfxr_get_runtime_properties_fn hostfxr_get_runtime_properties (NativeHost.h)

    // change above to this
    // const char_t *argv[2] = { "/Users/thomasschmeiduch/RiderProjects/Soot.Dotnet/Soot.Dotnet.Decompiler/bin/Debug/netcoreapp3.1/publish/Soot.Dotnet.Decompiler.dll", "" };
    const char_t *argv[2] = {"/Users/thomasschmeiduch/RiderProjects/ConsoleApp2/ConsoleApp2/bin/Debug/netcoreapp3.1/publish/ConsoleApp2.dll" };
    hostfxr_initialize_parameters params {
        sizeof(hostfxr_initialize_parameters),
        "/Users/thomasschmeiduch/RiderProjects/Soot.Dotnet/Soot.Dotnet.NativeHost/bin/Debug/",
        "/Users/thomasschmeiduch/RiderProjects/ConsoleApp2/ConsoleApp2/bin/Debug/netcoreapp3.1/publish/"
    };
    int rc = init_fptr(1, argv, &params, &cxt);

    // Log properties
    size_t prop_count = 64;
    const char_t *prop_keys[64];
    const char_t *prop_values[64];
    rc = hostfxr_get_runtime_properties(cxt, &prop_count, prop_keys, prop_values);
    if (0 != rc) {
        std::cerr << "hostfxr did not set up properties" << std::hex
                  << std::showbase << rc << std::endl;
        close_fptr(cxt);
    }

    rc = run_app_fptr(cxt);
    std::cout << "Run the app: " << rc << std::endl;
*/

    // Get the load assembly function pointer
    rc = get_delegate_fptr(
            cxt,
            hdt_load_assembly_and_get_function_pointer,
            &load_assembly_and_get_function_pointer);
    if (rc != 0 || load_assembly_and_get_function_pointer == nullptr)
        std::cerr << "Failure: (get_dotnet_load_assembly()) Get delegate failed: " << std::hex << std::showbase << rc << std::endl;

    close_fptr(cxt);
    return (load_assembly_and_get_function_pointer_fn)load_assembly_and_get_function_pointer;
}

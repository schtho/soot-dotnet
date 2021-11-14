#include "inc/soot_dotnet_AssemblyFile.h"
#include "NativeHost.h"

/*
 * Implementation of Java JNI of soot.dotnet.AssemblyFile
 * Created by Thomas Schmeiduch
 */

JNIEXPORT jbyteArray JNICALL Java_soot_dotnet_AssemblyFile_nativeGetAllTypesMsg
        (JNIEnv *env, jobject obj, jstring pathToNativeHost, jbyteArray disassemblerParams) {

    int disParamsArrLen = env->GetArrayLength(disassemblerParams);
    auto* buf = new unsigned char[disParamsArrLen];
    env->GetByteArrayRegion (disassemblerParams, 0, disParamsArrLen, reinterpret_cast<jbyte*>(buf));
    auto* cliDisParamsBytes = new cli_byte_array(disParamsArrLen, buf);

    std::string path_native_host_str = env->GetStringUTFChars(pathToNativeHost, nullptr);
    if (path_native_host_str.empty())
        throw std::invalid_argument( "Path to NativeHost is null!" );

    auto nh = new NativeHost(path_native_host_str);
    cli_byte_array result = nh->AssemblyProvider_getAllTypesMsg(*cliDisParamsBytes);

    if (result.length == 0 || result.byte_arr_ptr == nullptr)
        return nullptr;

    jbyteArray byte_array = env->NewByteArray(result.length);
    env->SetByteArrayRegion(byte_array, 0, result.length, (jbyte*)result.byte_arr_ptr);
    return byte_array;
}

JNIEXPORT jbyteArray JNICALL Java_soot_dotnet_AssemblyFile_nativeGetMethodBodyMsg
        (JNIEnv *env, jobject obj, jstring pathToNativeHost, jbyteArray disassemblerParams) {

    int disParamsArrLen = env->GetArrayLength(disassemblerParams);
    auto* buf = new unsigned char[disParamsArrLen];
    env->GetByteArrayRegion (disassemblerParams, 0, disParamsArrLen, reinterpret_cast<jbyte*>(buf));
    auto* cliDisParamsBytes = new cli_byte_array(disParamsArrLen, buf);

    std::string path_native_host_str = env->GetStringUTFChars(pathToNativeHost, nullptr);
    if (path_native_host_str.empty())
        throw std::invalid_argument( "Path to NativeHost is null!" );

    auto nh = new NativeHost(path_native_host_str);
    cli_byte_array result = nh->AssemblyProvider_GetMethodBodyMsg(*cliDisParamsBytes);

    if (result.length == 0 || result.byte_arr_ptr == nullptr)
        return nullptr;

    jbyteArray byte_array = env->NewByteArray(result.length);
    env->SetByteArrayRegion(byte_array, 0, result.length, (jbyte*)result.byte_arr_ptr);
    return byte_array;
}

JNIEXPORT jbyteArray JNICALL Java_soot_dotnet_AssemblyFile_nativeGetMethodBodyOfPropertyMsg
        (JNIEnv *env, jobject obj, jstring pathToNativeHost, jbyteArray disassemblerParams) {

    int disParamsArrLen = env->GetArrayLength(disassemblerParams);
    auto* buf = new unsigned char[disParamsArrLen];
    env->GetByteArrayRegion (disassemblerParams, 0, disParamsArrLen, reinterpret_cast<jbyte*>(buf));
    auto* cliDisParamsBytes = new cli_byte_array(disParamsArrLen, buf);

    // bool is_setter = (bool)(isSetter == JNI_TRUE);
    std::string path_native_host_str = env->GetStringUTFChars(pathToNativeHost, nullptr);
    if (path_native_host_str.empty())
        throw std::invalid_argument( "Path to NativeHost is null!" );

    auto nh = new NativeHost(path_native_host_str);
    cli_byte_array result = nh->AssemblyProvider_GetMethodBodyOfPropertyMsg(*cliDisParamsBytes);

    if (result.length == 0 || result.byte_arr_ptr == nullptr)
        return nullptr;

    jbyteArray byte_array = env->NewByteArray(result.length);
    env->SetByteArrayRegion(byte_array, 0, result.length, (jbyte*)result.byte_arr_ptr);
    return byte_array;
}

JNIEXPORT jbyteArray JNICALL Java_soot_dotnet_AssemblyFile_nativeGetMethodBodyOfEventMsg
        (JNIEnv *env, jobject obj, jstring pathToNativeHost, jbyteArray disassemblerParams) {

    int disParamsArrLen = env->GetArrayLength(disassemblerParams);
    auto* buf = new unsigned char[disParamsArrLen];
    env->GetByteArrayRegion (disassemblerParams, 0, disParamsArrLen, reinterpret_cast<jbyte*>(buf));
    auto* cliDisParamsBytes = new cli_byte_array(disParamsArrLen, buf);

    std::string path_native_host_str = env->GetStringUTFChars(pathToNativeHost, nullptr);
    if (path_native_host_str.empty())
        throw std::invalid_argument( "Path to NativeHost is null!" );

    auto nh = new NativeHost(path_native_host_str);
    cli_byte_array result = nh->AssemblyProvider_GetMethodBodyOfEventMsg(*cliDisParamsBytes);

    if (result.length == 0 || result.byte_arr_ptr == nullptr)
        return nullptr;

    jbyteArray byte_array = env->NewByteArray(result.length);
    env->SetByteArrayRegion(byte_array, 0, result.length, (jbyte*)result.byte_arr_ptr);
    return byte_array;
}

JNIEXPORT jbyteArray JNICALL Java_soot_dotnet_AssemblyFile_nativeGetAssemblyContentMsg
        (JNIEnv *env, jobject obj, jstring pathToNativeHost, jbyteArray disassemblerParams) {

    int paramsArrLen = env->GetArrayLength(disassemblerParams);
    auto* buf = new unsigned char[paramsArrLen];
    env->GetByteArrayRegion (disassemblerParams, 0, paramsArrLen, reinterpret_cast<jbyte*>(buf));
    auto* cli_proto_params = new cli_byte_array(paramsArrLen, buf);

    std::string path_native_host_str = env->GetStringUTFChars(pathToNativeHost, nullptr);
    if (path_native_host_str.empty())
        throw std::invalid_argument( "Path to NativeHost is null!" );

    // Call Nativehost and Dotnet Code
    auto nh = new NativeHost(path_native_host_str);
    cli_byte_array result = nh->AssemblyProvider_GetAssemblyContent(*cli_proto_params);

    if (result.length == 0 || result.byte_arr_ptr == nullptr)
        return nullptr;

    jbyteArray byte_array = env->NewByteArray(result.length);
    env->SetByteArrayRegion(byte_array, 0, result.length, (jbyte*)result.byte_arr_ptr);
    return byte_array;
}

JNIEXPORT jboolean JNICALL Java_soot_dotnet_AssemblyFile_nativeIsAssembly
        (JNIEnv *env, jobject obj, jstring pathToNativeHost, jstring absolutePathAssembly) {
    const char* assembly_path = env->GetStringUTFChars(absolutePathAssembly, nullptr);

    std::string path_native_host_str = env->GetStringUTFChars(pathToNativeHost, nullptr);
    if (path_native_host_str.empty())
        throw std::invalid_argument( "Path to NativeHost is null!" );

    auto nh = new NativeHost(path_native_host_str);
    bool result = nh->AssemblyProvider_isAssembly(assembly_path);
    return result;
}

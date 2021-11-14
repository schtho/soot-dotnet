//
// Created by Thomas Schmeiduch on 22.03.21.
// Only for debug reason
// uncomment target in CMakeLists.txt to use this
//

#include "Main.h"
#include "NativeHost.h"

int main(int argc, char *argv[])
{
    std::cout << "hallo" << std::endl;

    std::string url = "/Users/user/Documents/Git/UNI/Masterthesis/MT.exe";
    std::string url2 = "/Users/user/Documents/Git/UNI/Masterthesis/NoAssembly.exe";

     auto NH = new NativeHost("/Users/thomasschmeiduch/RiderProjects/Soot.Dotnet/Soot.Dotnet.NativeHost/bin/Debug/libNativeHost.dylib");
     bool lol = NH->AssemblyProvider_isAssembly(url);
     std::cout << (lol ? "tru" : "fal") << std::endl;
     delete NH;

//    auto NH2 = new NativeHost();
//    lol = NH2->AssemblyAnalyzer_isAssembly(url2);
//    std::cout << (lol ? "tru" : "fal") << std::endl;
}
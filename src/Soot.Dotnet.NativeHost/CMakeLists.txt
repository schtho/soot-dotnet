cmake_minimum_required(VERSION 3.17)
project(NativeHost)

set(CMAKE_CXX_STANDARD 17)

set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_SOURCE_DIR}/bin/Debug)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_SOURCE_DIR}/bin/Debug)

file(MAKE_DIRECTORY ${CMAKE_SOURCE_DIR}/bin/Debug)
add_custom_target(build-time-make-directory ALL
        COMMAND ${CMAKE_COMMAND} -E make_directory ${CMAKE_SOURCE_DIR}/bin/Debug)

include_directories(inc)
include_directories(inc.vs)
include_directories($ENV{JAVA_HOME}/include)

if (APPLE)
    include_directories(/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Host.osx-x64/6.0.0/runtimes/osx-x64/native)
    include_directories($ENV{JAVA_HOME}/include/darwin)
    link_directories(/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Host.osx-x64/6.0.0/runtimes/osx-x64/native)
elseif(WIN32)
    # TODO
    # may use VisualStudio VC++ Compiler and the .csproj VS project instead of CMake
elseif(UNIX)
    # TODO
endif()

# only for debug reason
#add_executable(NativeHost inc/coreclr_delegates.h inc/hostfxr.h inc.vs/nethost.h NativeHost.h Settings.cpp inc/Settings.h soot_dotnet_AssemblyFile.cpp NativeHost.cpp inc/cli_string.h inc/cli_byte_array.h Main.cpp Main.h)
add_library(NativeHost SHARED
        inc/coreclr_delegates.h
        inc/hostfxr.h
        inc.vs/nethost.h
        NativeHost.h NativeHost.cpp
        Settings.cpp inc/Settings.h
        soot_dotnet_AssemblyFile.cpp
        inc/cli_string.h inc/cli_byte_array.h)

target_link_libraries(NativeHost
        dl
        nethost
        )

if (APPLE)
    set (CMAKE_SHARED_LINKER_FLAGS "-Wl,-rpath,'@loader_path'")
    add_compile_options(-D OSX)
elseif(UNIX)
    set (CMAKE_SHARED_LINKER_FLAGS "-Wl,-rpath,'$ORIGIN',--disable-new-dtags")
    # TODO
endif()

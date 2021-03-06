// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: DotnetNativeHost.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Soot.Dotnet.Decompiler.Models.Protobuf {

  /// <summary>Holder for reflection information generated from DotnetNativeHost.proto</summary>
  public static partial class DotnetNativeHostReflection {

    #region Descriptor
    /// <summary>File descriptor for DotnetNativeHost.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static DotnetNativeHostReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChZEb3RuZXROYXRpdmVIb3N0LnByb3RvIt8CChFBbmFseXplclBhcmFtc01z",
            "ZxIxChRhbmFseXplcl9tZXRob2RfY2FsbBgBIAEoDjITLkFuYWx5emVyTWV0",
            "aG9kQ2FsbBIjChthc3NlbWJseV9maWxlX2Fic29sdXRlX3BhdGgYAiABKAkS",
            "HAoUdHlwZV9yZWZsZWN0aW9uX25hbWUYAyABKAkSEwoLbWV0aG9kX25hbWUY",
            "BCABKAkSGgoSbWV0aG9kX25hbWVfc3VmZml4GAsgASgJEhcKD21ldGhvZF9w",
            "ZV90b2tlbhgMIAEoBRIVCg1wcm9wZXJ0eV9uYW1lGAYgASgJEhoKEnByb3Bl",
            "cnR5X2lzX3NldHRlchgHIAEoCBISCgpldmVudF9uYW1lGAggASgJEi8KE2V2",
            "ZW50X2FjY2Vzc29yX3R5cGUYCSABKA4yEi5FdmVudEFjY2Vzc29yVHlwZRIS",
            "CgpkZWJ1Z19tb2RlGAogASgIKpoBChJBbmFseXplck1ldGhvZENhbGwSCwoH",
            "Tk9fQ0FMTBAAEhEKDUdFVF9BTExfVFlQRVMQARITCg9HRVRfTUVUSE9EX0JP",
            "RFkQAhIfChtHRVRfTUVUSE9EX0JPRFlfT0ZfUFJPUEVSVFkQAxIcChhHRVRf",
            "TUVUSE9EX0JPRFlfT0ZfRVZFTlQQBBIQCgxHRVRfVFlQRV9ERUYQBSpkChFF",
            "dmVudEFjY2Vzc29yVHlwZRITCg9OT19FVkVOVF9NRVRIT0QQABIQCgxBRERf",
            "QUNDRVNTT1IQARITCg9JTlZPS0VfQUNDRVNTT1IQAhITCg9SRU1PVkVfQUND",
            "RVNTT1IQA0JTChFzb290LmRvdG5ldC5wcm90b0IVUHJvdG9Eb3RuZXROYXRp",
            "dmVIb3N0qgImU29vdC5Eb3RuZXQuRGVjb21waWxlci5Nb2RlbHMuUHJvdG9i",
            "dWZiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall), typeof(global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerParamsMsg), global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerParamsMsg.Parser, new[]{ "AnalyzerMethodCall", "AssemblyFileAbsolutePath", "TypeReflectionName", "MethodName", "MethodNameSuffix", "MethodPeToken", "PropertyName", "PropertyIsSetter", "EventName", "EventAccessorType", "DebugMode" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum AnalyzerMethodCall {
    [pbr::OriginalName("NO_CALL")] NoCall = 0,
    [pbr::OriginalName("GET_ALL_TYPES")] GetAllTypes = 1,
    [pbr::OriginalName("GET_METHOD_BODY")] GetMethodBody = 2,
    [pbr::OriginalName("GET_METHOD_BODY_OF_PROPERTY")] GetMethodBodyOfProperty = 3,
    [pbr::OriginalName("GET_METHOD_BODY_OF_EVENT")] GetMethodBodyOfEvent = 4,
    [pbr::OriginalName("GET_TYPE_DEF")] GetTypeDef = 5,
  }

  public enum EventAccessorType {
    [pbr::OriginalName("NO_EVENT_METHOD")] NoEventMethod = 0,
    [pbr::OriginalName("ADD_ACCESSOR")] AddAccessor = 1,
    [pbr::OriginalName("INVOKE_ACCESSOR")] InvokeAccessor = 2,
    [pbr::OriginalName("REMOVE_ACCESSOR")] RemoveAccessor = 3,
  }

  #endregion

  #region Messages
  public sealed partial class AnalyzerParamsMsg : pb::IMessage<AnalyzerParamsMsg>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<AnalyzerParamsMsg> _parser = new pb::MessageParser<AnalyzerParamsMsg>(() => new AnalyzerParamsMsg());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<AnalyzerParamsMsg> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Soot.Dotnet.Decompiler.Models.Protobuf.DotnetNativeHostReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AnalyzerParamsMsg() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AnalyzerParamsMsg(AnalyzerParamsMsg other) : this() {
      analyzerMethodCall_ = other.analyzerMethodCall_;
      assemblyFileAbsolutePath_ = other.assemblyFileAbsolutePath_;
      typeReflectionName_ = other.typeReflectionName_;
      methodName_ = other.methodName_;
      methodNameSuffix_ = other.methodNameSuffix_;
      methodPeToken_ = other.methodPeToken_;
      propertyName_ = other.propertyName_;
      propertyIsSetter_ = other.propertyIsSetter_;
      eventName_ = other.eventName_;
      eventAccessorType_ = other.eventAccessorType_;
      debugMode_ = other.debugMode_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AnalyzerParamsMsg Clone() {
      return new AnalyzerParamsMsg(this);
    }

    /// <summary>Field number for the "analyzer_method_call" field.</summary>
    public const int AnalyzerMethodCallFieldNumber = 1;
    private global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall analyzerMethodCall_ = global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall.NoCall;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall AnalyzerMethodCall {
      get { return analyzerMethodCall_; }
      set {
        analyzerMethodCall_ = value;
      }
    }

    /// <summary>Field number for the "assembly_file_absolute_path" field.</summary>
    public const int AssemblyFileAbsolutePathFieldNumber = 2;
    private string assemblyFileAbsolutePath_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string AssemblyFileAbsolutePath {
      get { return assemblyFileAbsolutePath_; }
      set {
        assemblyFileAbsolutePath_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "type_reflection_name" field.</summary>
    public const int TypeReflectionNameFieldNumber = 3;
    private string typeReflectionName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string TypeReflectionName {
      get { return typeReflectionName_; }
      set {
        typeReflectionName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "method_name" field.</summary>
    public const int MethodNameFieldNumber = 4;
    private string methodName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string MethodName {
      get { return methodName_; }
      set {
        methodName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "method_name_suffix" field.</summary>
    public const int MethodNameSuffixFieldNumber = 11;
    private string methodNameSuffix_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string MethodNameSuffix {
      get { return methodNameSuffix_; }
      set {
        methodNameSuffix_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "method_pe_token" field.</summary>
    public const int MethodPeTokenFieldNumber = 12;
    private int methodPeToken_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MethodPeToken {
      get { return methodPeToken_; }
      set {
        methodPeToken_ = value;
      }
    }

    /// <summary>Field number for the "property_name" field.</summary>
    public const int PropertyNameFieldNumber = 6;
    private string propertyName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string PropertyName {
      get { return propertyName_; }
      set {
        propertyName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "property_is_setter" field.</summary>
    public const int PropertyIsSetterFieldNumber = 7;
    private bool propertyIsSetter_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool PropertyIsSetter {
      get { return propertyIsSetter_; }
      set {
        propertyIsSetter_ = value;
      }
    }

    /// <summary>Field number for the "event_name" field.</summary>
    public const int EventNameFieldNumber = 8;
    private string eventName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string EventName {
      get { return eventName_; }
      set {
        eventName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "event_accessor_type" field.</summary>
    public const int EventAccessorTypeFieldNumber = 9;
    private global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType eventAccessorType_ = global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType.NoEventMethod;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType EventAccessorType {
      get { return eventAccessorType_; }
      set {
        eventAccessorType_ = value;
      }
    }

    /// <summary>Field number for the "debug_mode" field.</summary>
    public const int DebugModeFieldNumber = 10;
    private bool debugMode_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool DebugMode {
      get { return debugMode_; }
      set {
        debugMode_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as AnalyzerParamsMsg);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(AnalyzerParamsMsg other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (AnalyzerMethodCall != other.AnalyzerMethodCall) return false;
      if (AssemblyFileAbsolutePath != other.AssemblyFileAbsolutePath) return false;
      if (TypeReflectionName != other.TypeReflectionName) return false;
      if (MethodName != other.MethodName) return false;
      if (MethodNameSuffix != other.MethodNameSuffix) return false;
      if (MethodPeToken != other.MethodPeToken) return false;
      if (PropertyName != other.PropertyName) return false;
      if (PropertyIsSetter != other.PropertyIsSetter) return false;
      if (EventName != other.EventName) return false;
      if (EventAccessorType != other.EventAccessorType) return false;
      if (DebugMode != other.DebugMode) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (AnalyzerMethodCall != global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall.NoCall) hash ^= AnalyzerMethodCall.GetHashCode();
      if (AssemblyFileAbsolutePath.Length != 0) hash ^= AssemblyFileAbsolutePath.GetHashCode();
      if (TypeReflectionName.Length != 0) hash ^= TypeReflectionName.GetHashCode();
      if (MethodName.Length != 0) hash ^= MethodName.GetHashCode();
      if (MethodNameSuffix.Length != 0) hash ^= MethodNameSuffix.GetHashCode();
      if (MethodPeToken != 0) hash ^= MethodPeToken.GetHashCode();
      if (PropertyName.Length != 0) hash ^= PropertyName.GetHashCode();
      if (PropertyIsSetter != false) hash ^= PropertyIsSetter.GetHashCode();
      if (EventName.Length != 0) hash ^= EventName.GetHashCode();
      if (EventAccessorType != global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType.NoEventMethod) hash ^= EventAccessorType.GetHashCode();
      if (DebugMode != false) hash ^= DebugMode.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (AnalyzerMethodCall != global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall.NoCall) {
        output.WriteRawTag(8);
        output.WriteEnum((int) AnalyzerMethodCall);
      }
      if (AssemblyFileAbsolutePath.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(AssemblyFileAbsolutePath);
      }
      if (TypeReflectionName.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(TypeReflectionName);
      }
      if (MethodName.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(MethodName);
      }
      if (PropertyName.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(PropertyName);
      }
      if (PropertyIsSetter != false) {
        output.WriteRawTag(56);
        output.WriteBool(PropertyIsSetter);
      }
      if (EventName.Length != 0) {
        output.WriteRawTag(66);
        output.WriteString(EventName);
      }
      if (EventAccessorType != global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType.NoEventMethod) {
        output.WriteRawTag(72);
        output.WriteEnum((int) EventAccessorType);
      }
      if (DebugMode != false) {
        output.WriteRawTag(80);
        output.WriteBool(DebugMode);
      }
      if (MethodNameSuffix.Length != 0) {
        output.WriteRawTag(90);
        output.WriteString(MethodNameSuffix);
      }
      if (MethodPeToken != 0) {
        output.WriteRawTag(96);
        output.WriteInt32(MethodPeToken);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (AnalyzerMethodCall != global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall.NoCall) {
        output.WriteRawTag(8);
        output.WriteEnum((int) AnalyzerMethodCall);
      }
      if (AssemblyFileAbsolutePath.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(AssemblyFileAbsolutePath);
      }
      if (TypeReflectionName.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(TypeReflectionName);
      }
      if (MethodName.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(MethodName);
      }
      if (PropertyName.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(PropertyName);
      }
      if (PropertyIsSetter != false) {
        output.WriteRawTag(56);
        output.WriteBool(PropertyIsSetter);
      }
      if (EventName.Length != 0) {
        output.WriteRawTag(66);
        output.WriteString(EventName);
      }
      if (EventAccessorType != global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType.NoEventMethod) {
        output.WriteRawTag(72);
        output.WriteEnum((int) EventAccessorType);
      }
      if (DebugMode != false) {
        output.WriteRawTag(80);
        output.WriteBool(DebugMode);
      }
      if (MethodNameSuffix.Length != 0) {
        output.WriteRawTag(90);
        output.WriteString(MethodNameSuffix);
      }
      if (MethodPeToken != 0) {
        output.WriteRawTag(96);
        output.WriteInt32(MethodPeToken);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (AnalyzerMethodCall != global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall.NoCall) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) AnalyzerMethodCall);
      }
      if (AssemblyFileAbsolutePath.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AssemblyFileAbsolutePath);
      }
      if (TypeReflectionName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TypeReflectionName);
      }
      if (MethodName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(MethodName);
      }
      if (MethodNameSuffix.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(MethodNameSuffix);
      }
      if (MethodPeToken != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MethodPeToken);
      }
      if (PropertyName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PropertyName);
      }
      if (PropertyIsSetter != false) {
        size += 1 + 1;
      }
      if (EventName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(EventName);
      }
      if (EventAccessorType != global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType.NoEventMethod) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) EventAccessorType);
      }
      if (DebugMode != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(AnalyzerParamsMsg other) {
      if (other == null) {
        return;
      }
      if (other.AnalyzerMethodCall != global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall.NoCall) {
        AnalyzerMethodCall = other.AnalyzerMethodCall;
      }
      if (other.AssemblyFileAbsolutePath.Length != 0) {
        AssemblyFileAbsolutePath = other.AssemblyFileAbsolutePath;
      }
      if (other.TypeReflectionName.Length != 0) {
        TypeReflectionName = other.TypeReflectionName;
      }
      if (other.MethodName.Length != 0) {
        MethodName = other.MethodName;
      }
      if (other.MethodNameSuffix.Length != 0) {
        MethodNameSuffix = other.MethodNameSuffix;
      }
      if (other.MethodPeToken != 0) {
        MethodPeToken = other.MethodPeToken;
      }
      if (other.PropertyName.Length != 0) {
        PropertyName = other.PropertyName;
      }
      if (other.PropertyIsSetter != false) {
        PropertyIsSetter = other.PropertyIsSetter;
      }
      if (other.EventName.Length != 0) {
        EventName = other.EventName;
      }
      if (other.EventAccessorType != global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType.NoEventMethod) {
        EventAccessorType = other.EventAccessorType;
      }
      if (other.DebugMode != false) {
        DebugMode = other.DebugMode;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            AnalyzerMethodCall = (global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall) input.ReadEnum();
            break;
          }
          case 18: {
            AssemblyFileAbsolutePath = input.ReadString();
            break;
          }
          case 26: {
            TypeReflectionName = input.ReadString();
            break;
          }
          case 34: {
            MethodName = input.ReadString();
            break;
          }
          case 50: {
            PropertyName = input.ReadString();
            break;
          }
          case 56: {
            PropertyIsSetter = input.ReadBool();
            break;
          }
          case 66: {
            EventName = input.ReadString();
            break;
          }
          case 72: {
            EventAccessorType = (global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType) input.ReadEnum();
            break;
          }
          case 80: {
            DebugMode = input.ReadBool();
            break;
          }
          case 90: {
            MethodNameSuffix = input.ReadString();
            break;
          }
          case 96: {
            MethodPeToken = input.ReadInt32();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            AnalyzerMethodCall = (global::Soot.Dotnet.Decompiler.Models.Protobuf.AnalyzerMethodCall) input.ReadEnum();
            break;
          }
          case 18: {
            AssemblyFileAbsolutePath = input.ReadString();
            break;
          }
          case 26: {
            TypeReflectionName = input.ReadString();
            break;
          }
          case 34: {
            MethodName = input.ReadString();
            break;
          }
          case 50: {
            PropertyName = input.ReadString();
            break;
          }
          case 56: {
            PropertyIsSetter = input.ReadBool();
            break;
          }
          case 66: {
            EventName = input.ReadString();
            break;
          }
          case 72: {
            EventAccessorType = (global::Soot.Dotnet.Decompiler.Models.Protobuf.EventAccessorType) input.ReadEnum();
            break;
          }
          case 80: {
            DebugMode = input.ReadBool();
            break;
          }
          case 90: {
            MethodNameSuffix = input.ReadString();
            break;
          }
          case 96: {
            MethodPeToken = input.ReadInt32();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code

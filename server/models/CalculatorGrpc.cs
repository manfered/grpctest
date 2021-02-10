// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: calculator.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Calculator {
  public static partial class CalculatorService
  {
    static readonly string __ServiceName = "calculator.CalculatorService";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::Calculator.CalculatorRequest> __Marshaller_calculator_CalculatorRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Calculator.CalculatorRequest.Parser));
    static readonly grpc::Marshaller<global::Calculator.CalculatorResponse> __Marshaller_calculator_CalculatorResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Calculator.CalculatorResponse.Parser));

    static readonly grpc::Method<global::Calculator.CalculatorRequest, global::Calculator.CalculatorResponse> __Method_Calculate = new grpc::Method<global::Calculator.CalculatorRequest, global::Calculator.CalculatorResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Calculate",
        __Marshaller_calculator_CalculatorRequest,
        __Marshaller_calculator_CalculatorResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Calculator.CalculatorReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of CalculatorService</summary>
    [grpc::BindServiceMethod(typeof(CalculatorService), "BindService")]
    public abstract partial class CalculatorServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Calculator.CalculatorResponse> Calculate(global::Calculator.CalculatorRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for CalculatorService</summary>
    public partial class CalculatorServiceClient : grpc::ClientBase<CalculatorServiceClient>
    {
      /// <summary>Creates a new client for CalculatorService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public CalculatorServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for CalculatorService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public CalculatorServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected CalculatorServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected CalculatorServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Calculator.CalculatorResponse Calculate(global::Calculator.CalculatorRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Calculate(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Calculator.CalculatorResponse Calculate(global::Calculator.CalculatorRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Calculate, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Calculator.CalculatorResponse> CalculateAsync(global::Calculator.CalculatorRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CalculateAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Calculator.CalculatorResponse> CalculateAsync(global::Calculator.CalculatorRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Calculate, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override CalculatorServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new CalculatorServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(CalculatorServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Calculate, serviceImpl.Calculate).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, CalculatorServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Calculate, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Calculator.CalculatorRequest, global::Calculator.CalculatorResponse>(serviceImpl.Calculate));
    }

  }
}
#endregion
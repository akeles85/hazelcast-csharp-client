// Copyright (c) 2008-2019, Hazelcast, Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using System.Collections.Generic;
using Hazelcast.Client.Protocol;
using Hazelcast.Client.Protocol.Codec.BuiltIn;
using Hazelcast.Client.Protocol.Codec.Custom;
using Hazelcast.Client.Protocol.Util;
using Hazelcast.IO;
using Hazelcast.IO.Serialization;
using static Hazelcast.Client.Protocol.Codec.BuiltIn.FixedSizeTypesCodec;
using static Hazelcast.Client.Protocol.ClientMessage;
using static Hazelcast.IO.Bits;

namespace Hazelcast.Client.Protocol.Codec
{
    // This file is auto-generated by the Hazelcast Client Protocol Code Generator.
    // To change this file, edit the templates or the protocol
    // definitions on the https://github.com/hazelcast/hazelcast-client-protocol
    // and regenerate it.

    /// <summary>
    /// Puts an entry into this map with a given ttl (time to live) value.Entry will expire and get evicted after the ttl
    /// If ttl is 0, then the entry lives forever.This method returns a clone of the previous value, not the original
    /// (identically equal) value previously put into the map.Time resolution for TTL is seconds. The given TTL value is
    /// rounded to the next closest second value.
    ///</summary>
    internal static class MapPutWithMaxIdleCodec
    {
        //hex: 0x014700
        public const int RequestMessageType = 83712;
        //hex: 0x014701
        public const int ResponseMessageType = 83713;
        private const int RequestThreadIdFieldOffset = PartitionIdFieldOffset + IntSizeInBytes;
        private const int RequestTtlFieldOffset = RequestThreadIdFieldOffset + LongSizeInBytes;
        private const int RequestMaxIdleFieldOffset = RequestTtlFieldOffset + LongSizeInBytes;
        private const int RequestInitialFrameSize = RequestMaxIdleFieldOffset + LongSizeInBytes;
        private const int ResponseInitialFrameSize = ResponseBackupAcksFieldOffset + IntSizeInBytes;

        public class RequestParameters
        {

            /// <summary>
            /// Name of the map.
            ///</summary>
            public string Name;

            /// <summary>
            /// Key for the map entry.
            ///</summary>
            public IData Key;

            /// <summary>
            /// Value for the map entry.
            ///</summary>
            public IData Value;

            /// <summary>
            /// The id of the user thread performing the operation. It is used to guarantee that only the lock holder thread (if a lock exists on the entry) can perform the requested operation.
            ///</summary>
            public long ThreadId;

            /// <summary>
            /// The duration in milliseconds after which this entry shall be deleted. O means infinite.
            ///</summary>
            public long Ttl;

            /// <summary>
            /// The duration of maximum idle for this entry.
            /// Milliseconds of idle, after which this entry shall be deleted. O means infinite.
            ///</summary>
            public long MaxIdle;
        }

        public static ClientMessage EncodeRequest(string name, IData key, IData value, long threadId, long ttl, long maxIdle)
        {
            var clientMessage = CreateForEncode();
            clientMessage.IsRetryable = false;
            clientMessage.AcquiresResource = false;
            clientMessage.OperationName = "Map.PutWithMaxIdle";
            var initialFrame = new Frame(new byte[RequestInitialFrameSize], UnfragmentedMessage);
            EncodeInt(initialFrame.Content, TypeFieldOffset, RequestMessageType);
            EncodeLong(initialFrame.Content, RequestThreadIdFieldOffset, threadId);
            EncodeLong(initialFrame.Content, RequestTtlFieldOffset, ttl);
            EncodeLong(initialFrame.Content, RequestMaxIdleFieldOffset, maxIdle);
            clientMessage.Add(initialFrame);
            StringCodec.Encode(clientMessage, name);
            DataCodec.Encode(clientMessage, key);
            DataCodec.Encode(clientMessage, @value);
            return clientMessage;
        }

        public static RequestParameters DecodeRequest(ClientMessage clientMessage)
        {
            var iterator = clientMessage.GetIterator();
            var request = new RequestParameters();
            var initialFrame = iterator.Next();
            request.ThreadId =  DecodeLong(initialFrame.Content, RequestThreadIdFieldOffset);
            request.Ttl =  DecodeLong(initialFrame.Content, RequestTtlFieldOffset);
            request.MaxIdle =  DecodeLong(initialFrame.Content, RequestMaxIdleFieldOffset);
            request.Name = StringCodec.Decode(iterator);
            request.Key = DataCodec.Decode(iterator);
            request.Value = DataCodec.Decode(iterator);
            return request;
        }

        public class ResponseParameters
        {

            /// <summary>
            /// old value of the entry
            ///</summary>
            public IData Response;
        }

        public static ClientMessage EncodeResponse(IData response)
        {
            var clientMessage = CreateForEncode();
            var initialFrame = new Frame(new byte[ResponseInitialFrameSize], UnfragmentedMessage);
            EncodeInt(initialFrame.Content, TypeFieldOffset, ResponseMessageType);
            clientMessage.Add(initialFrame);

            CodecUtil.EncodeNullable(clientMessage, response, DataCodec.Encode);
            return clientMessage;
        }

        public static ResponseParameters DecodeResponse(ClientMessage clientMessage)
        {
            var iterator = clientMessage.GetIterator();
            var response = new ResponseParameters();
            //empty initial frame
            iterator.Next();
            response.Response = CodecUtil.DecodeNullable(iterator, DataCodec.Decode);
            return response;
        }
    }
}
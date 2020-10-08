﻿// Copyright (c) 2008-2020, Hazelcast, Inc. All Rights Reserved.
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
using Hazelcast.Data;

namespace Hazelcast.DistributedObjects
{
    public sealed class DictionaryEntryLoadedEventArgs<TKey, TValue> : DictionaryEntryEventArgsBase<TKey>
    {
        private readonly Lazy<TValue> _value;
        private readonly Lazy<TValue> _oldValue;

        public DictionaryEntryLoadedEventArgs(MemberInfo member, Lazy<TKey> key, Lazy<TValue> value, Lazy<TValue> oldValue)
            : base(member, key)
        {
            _value = value;
            _oldValue = oldValue;
        }

        /// <summary>
        /// Gets the value that was loaded.
        /// </summary>
        public TValue Value => _value == null ? default : _value.Value;

        /// <summary>
        /// Gets the value before load, if the entry existed.
        /// </summary>
        public TValue OldValue => _oldValue == null ? default : _oldValue.Value;
    }
}
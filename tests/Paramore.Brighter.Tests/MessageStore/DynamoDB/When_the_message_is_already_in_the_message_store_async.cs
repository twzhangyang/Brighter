﻿#region Licence
/* The MIT License (MIT)
Copyright © 2015 Ian Cooper <ian_hammond_cooper@yahoo.co.uk>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

#endregion

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Paramore.Brighter.Tests.MessageStore.DynamoDB
{
    [Trait("Category", "DynamoDB")]
    [Collection("DynamoDB MessageStore")]
    public class DynamoDbMessageStoreMessageAlreadyExistsAsyncTests : BaseDynamoDBMessageStoreTests
    {        
        private readonly Message _messageEarliest;

        private Exception _exception;

        public DynamoDbMessageStoreMessageAlreadyExistsAsyncTests()
        {                                  
            _messageEarliest = new Message(new MessageHeader(Guid.NewGuid(), "test_topic", MessageType.MT_DOCUMENT), new MessageBody("message body"));            
        }

        [Fact]
        public async Task When_the_message_is_already_in_the_message_store_async()
        {
            await _dynamoDbMessageStore.AddAsync(_messageEarliest);

            _exception = await Catch.ExceptionAsync(() => _dynamoDbMessageStore.AddAsync(_messageEarliest));            

            //_should_ignore_the_duplicate_key_and_still_succeed
            _exception.Should().BeNull();
        }
    }
}

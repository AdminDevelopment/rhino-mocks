﻿#region license
// Copyright (c) 2005 - 2007 Ayende Rahien (ayende@ayende.com)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of Ayende Rahien nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion


using System;
using Xunit;

namespace Rhino.Mocks.Tests
{
    public class MockingAbstractClass : IDisposable
	{
		public MockingAbstractClass()
		{
		}

		public void Dispose()
		{
		}

		[Fact]
		public void MockAbsPropertyGetter()
		{
			AbsCls ac = (AbsCls)MockRepository.GenerateStrictMock(typeof(AbsCls));

            ac.Expect(x => x.AbPropGet)
                .Return("n");

			Assert.Equal("n", ac.AbPropGet);
            ac.VerifyAllExpectations();
		}

		[Fact]
		public void MockAbsPropertySetter()
		{
			AbsCls ac = (AbsCls)MockRepository.GenerateStrictMock(typeof(AbsCls));

            ac.Expect(x => x.AbPropSet = "n");
			
			ac.AbPropSet = "n";
            ac.VerifyAllExpectations();
		}


		[Fact]
		public void MockAbsProp()
		{
			AbsCls ac = (AbsCls)MockRepository.GenerateStrictMock(typeof(AbsCls));

            ac.Expect(x => x.AbProp = "n");
            ac.Expect(x => x.AbProp)
                .Return("u");

			ac.AbProp = "n";
			Assert.Equal("u", ac.AbProp);
            ac.VerifyAllExpectations();
		}

		[Fact]
		public void MockAbstractMethod()
		{
			AbsCls ac = (AbsCls)MockRepository.GenerateStrictMock(typeof(AbsCls));

            ac.Expect(x => x.Method())
                .Return(45);
			
			Assert.Equal(45, ac.Method());
            ac.VerifyAllExpectations();
		}

		public abstract class AbsCls
		{
			public abstract string AbPropGet { get; }

			public abstract string AbPropSet { set; }

			public abstract string AbProp { get; set; }

			public abstract int Method();

		}
	}
}

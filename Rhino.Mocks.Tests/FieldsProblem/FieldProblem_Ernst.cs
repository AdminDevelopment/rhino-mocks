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
using Rhino.Mocks.Constraints;
using Rhino.Mocks.Interfaces;

namespace Rhino.Mocks.Tests.FieldsProblem
{
	public class FieldProblem_Ernst
	{
		[Fact]
		public void CallOriginalMethodProblem2()
		{
			MockedClass mock = MockRepository.GenerateStrictMock<MockedClass>();

            mock.Expect(x => x.Method(null))
                .Constraints(Is.Equal("parameter"))
                .CallOriginalMethod(OriginalCallOptions.CreateExpectation);

			mock.Method("parameter");

            mock.VerifyAllExpectations();
		}

		[Fact]
		public void CanUseBackToRecordOnMethodsThatCallToCallOriginalMethod()
		{
            TestClass mock = MockRepository.GenerateStrictMock<TestClass>();

            mock.Expect(x => x.Method())
                .CallOriginalMethod(OriginalCallOptions.NoExpectation);

			mock.Method();
            mock.VerifyAllExpectations();

            mock.BackToRecord(BackToRecordOptions.All);
            mock.Expect(x => x.Method())
                .Throw(new ApplicationException());
            mock.Replay();

            Assert.Throws<ApplicationException>(() => mock.Method());
            mock.VerifyAllExpectations();
		}

		[Fact]
		public void CanUseBackToRecordOnMethodsThatCallPropertyBehavior()
		{
            TestClass mock = MockRepository.GenerateStrictMock<TestClass>();

            mock.Expect(x => x.Id)
                .PropertyBehavior();

			mock.Id = 4;
			int d = mock.Id;

			Assert.Equal(4,d );
            mock.VerifyAllExpectations();

            mock.BackToRecord(BackToRecordOptions.All);
            mock.Expect(x => x.Id)
                .Return(5);
            mock.Replay();

			Assert.Equal(5, mock.Id);
			mock.VerifyAllExpectations();
		}
	}

	public class TestClass
	{
		private int id;


		public virtual int Id
		{
			get { return id; }
			set { id = value; }
		}

		public virtual void Method()
		{
		}
	}

	public class MockedClass
	{
		public virtual void Method(string parameter)
		{
			if (parameter == null)
				throw new ArgumentNullException();

			//Something in this method must be executed
		}
	}
}

using System;
using Xunit;

namespace Rhino.Mocks.Tests.FieldsProblem
{
    
    public class FieldProblem__Alex
    {
        [Fact]
        public void CanMockMethodWithEnvironmentPermissions()
        {
            var withInt = MockRepository.Mock<IDoSomethingWith<int>>();
            var withString = MockRepository.Mock<IDoSomethingWith<string>>();

            var doer = MockRepository.Mock<IDoSomethingTwice>();

            // Fails
            new Doer(doer, withInt, withString);
        }
    }

    public interface IDoSomethingWith<T>
    {
        Action<T> DoSomething
        {
            get;
            set;
        }
    }

    public interface IDoSomethingTwice : IDoSomethingWith<int>, IDoSomethingWith<string>
    {
    }

    public class Doer
    {
        public Doer(IDoSomethingTwice doer, IDoSomethingWith<int> withInt, IDoSomethingWith<string> withString)
        {
            ((IDoSomethingWith<int>)doer).DoSomething += x => withInt.DoSomething(x);
            ((IDoSomethingWith<string>)doer).DoSomething += x => withString.DoSomething(x);
        }
    }
}
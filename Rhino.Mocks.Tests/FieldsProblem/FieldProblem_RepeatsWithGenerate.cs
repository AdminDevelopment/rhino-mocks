using Xunit;
using Rhino.Mocks.Exceptions;

namespace Rhino.Mocks.Tests.FieldsProblem
{
	public class FieldProblem_RepeatsWithGenerate
	{
		[Fact]
        public void RepeatTimes_Fails_When_Called_More_Then_Expected()
        {
            var interfaceMock = MockRepository.GenerateStrictMock<IRepeatsWithGenerate>();

            interfaceMock.Expect(x => x.GetMyIntValue())
                .Repeat.Times(1)
                .Return(4);

            interfaceMock.GetMyIntValue();

            Assert.Throws<ExpectationViolationException>(
                "IRepeatsWithGenerate.GetMyIntValue(); Expected #1, Actual #2.",
                () => interfaceMock.GetMyIntValue());
        }

		[Fact]
        public void RepeatTimes_Works_When_Called_Less_Then_Expected()
        {
		    var interfaceMock = MockRepository.GenerateStrictMock<IRepeatsWithGenerate>();

            interfaceMock.Expect(x => x.GetMyIntValue())
                .Repeat.Times(2)
                .Return(4);
            
		    interfaceMock.GetMyIntValue();

			Assert.Throws<ExpectationViolationException>(
                "IRepeatsWithGenerate.GetMyIntValue(); Expected #2, Actual #1.",
                () => interfaceMock.VerifyAllExpectations());
   
        }
	}

	public interface IRepeatsWithGenerate
	{
	    int GetMyIntValue();
	}
}
using System.Collections.Generic;

namespace IrinaTestingFramework.Contracts
{
    public interface ITestRunner
    {
        List<Test> RunTests(string pathToTestLibrary);
    }
}

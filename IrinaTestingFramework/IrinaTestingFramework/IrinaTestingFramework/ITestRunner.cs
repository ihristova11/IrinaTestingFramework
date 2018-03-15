using System.Collections.Generic;

namespace IrinaTestingFramework
{
    public interface ITestRunner
    {
        List<Test> RunTests(string pathToTestLibrary);
    }
}

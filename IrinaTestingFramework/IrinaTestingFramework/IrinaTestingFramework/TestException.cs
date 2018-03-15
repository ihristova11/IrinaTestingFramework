using System;

namespace IrinaTestingFramework
{
    public class TestException : Exception
    {
        public TestException(string errorMessage)
        : base(errorMessage)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IrinaTestingFramework.Contracts;

namespace IrinaTestingFramework
{
    public class TestRunner : ITestRunner
    {
        public List<Test> RunTests(string pathToTestLibrary)
        {
            var listOfTestResults = new List<Test>();
            var assembly = Assembly.LoadFrom(pathToTestLibrary);
            var types = assembly.GetTypes();
            var testClasses = types.Where(x =>
                x.IsClass && x.IsPublic &&
                x.CustomAttributes
                    .Any(attribute => attribute.AttributeType == typeof(TestClassAttribute)));
            foreach (var testClass in testClasses)
            {
                var methods = testClass.GetMethods();
                var testMethods = methods.Where(x =>
                    x.CustomAttributes
                        .Any(attribute => attribute.AttributeType == typeof(TestClassAttribute)));
                if (testMethods.Any())
                {
                    var instanceOfTestClass = Activator.CreateInstance(testClass);
                    foreach (var testMethod in testMethods)
                    {
                        var testResult = new Test();
                        testResult.Name = $"{testClass.Name}.{testMethod.Name}";
                        try
                        {
                            testMethod.Invoke(instanceOfTestClass, null);
                            testResult.Success = true;
                        }
                        catch (TestException e)
                        {
                            testResult.Success = false;
                            testResult.ErrorMessage = $"Test exception: {e.Message}";
                        }
                        catch (Exception ex)
                        {
                            testResult.Success = false;
                            testResult.ErrorMessage = $"{ex.GetType().Name}:{ex.Message}";
                        }
                        listOfTestResults.Add(testResult);
                    }
                }
            }

            return listOfTestResults;
        }
    }
}

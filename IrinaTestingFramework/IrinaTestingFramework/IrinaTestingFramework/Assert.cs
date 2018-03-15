namespace IrinaTestingFramework
{
    public class Assert
    {
        public static void IsTrue(bool expression, string errorMessage = null)
        {
            if (!expression)
            {
                throw new TestException(errorMessage);
            }

        }

        public static void AreEqual(object expected, object actual, string errorMessage)
        {
            if (!expected.Equals(actual))
            {
                throw new TestException(errorMessage);
            }
        }
    }
}

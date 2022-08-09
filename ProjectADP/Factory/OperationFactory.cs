namespace ProjectADP.Factory
{
    public class OperationFactory : IOperationFactory
    {
        public double CreateOperations(string operation, double leftValue, double rightvalue)
        {
            double result = 0;
            switch (operation)
            {
                case "subtraction":
                    result = leftValue - rightvalue;
                    break;
                case "addition":
                    result = leftValue + rightvalue;
                    break;
                case "multiplication":
                    result = leftValue * rightvalue;
                    break;
                case "division":
                    result = leftValue / rightvalue;
                    break;
                case "remainder":
                    result = leftValue % rightvalue;
                    break;

                default:
                    throw new ApplicationException("Operation not registred");
            }
            return result;
        }

       
    }
}

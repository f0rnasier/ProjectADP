namespace ProjectADP.Factory
{
    public class ErrorFactory : IErrorFactory
    {
        public string MessageError(int err)
        {
            string ErrorMessage="";
            switch (err)
            {
                case 400:
                    ErrorMessage = "You send an Incorrect value in result; No ID specified; Value is invalid";
                    break;
                case 404:
                    ErrorMessage = "The Value not found for specified ID";
                    break;
                case 503:
                    ErrorMessage = "We have an Error communicating with database";
                    break;
                default:
                    throw new ApplicationException("Not Handled Error!");
            }
            return ErrorMessage;
            
        }

    

    }
}

namespace Tracker.ErrorHandling
{
    internal interface IErrorHandlerService
    {
        void DisplayError(ErrorOptions errorOptions);
        void DisplayError(ErrorOptions errorOptions, ErrorDialogChoiceCallback choiceCallback);
    }
}

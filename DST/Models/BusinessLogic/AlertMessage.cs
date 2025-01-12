namespace DST.Models.BusinessLogic
{
    public class AlertMessage
    {
        #region Properties

        public string Message { get; set; }
        public AlertType Type { get; set; }

        #endregion

        #region Constructors

        public AlertMessage(string message, AlertType type)
        {
            Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message.Trim();
            Type = type;
        }

        #endregion
    }
}

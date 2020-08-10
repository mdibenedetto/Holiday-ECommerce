using System;

namespace dream_holiday.Models.ViewModels
{
    /// <summary>
    /// This class is used to handle application error. Is used in development Mode
    /// </summary>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

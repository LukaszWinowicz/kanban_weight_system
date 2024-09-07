using Microsoft.FluentUI.AspNetCore.Components;

namespace BlazorApp.Helpers
{
    public class MessageIntentHelper
    {
        public static MessageIntent GetMessageBarIntent(bool isLoading)
        {
            return isLoading ? MessageIntent.Info : MessageIntent.Error;
        }
    }
}

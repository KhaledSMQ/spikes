using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace WebRecorder
{
    public class UserActionFactory
    {
        private static readonly UserActionFactory Instance = new UserActionFactory();
        private IDictionary<string, Type> UserActionsMap { get; set; }

        public void RegisterUserAction(string type, Type userActionClass)
        {
            UserActionsMap[type] = userActionClass;
        }

        public static UserAction Create(string type)
        {
            var userActionClass = Instance.UserActionsMap[type];
            var userAction = (UserAction)Activator.CreateInstance(userActionClass);
            return userAction;
        }

        public static UserAction Create(string type, string actionParameters)
        {
            var userAction = Create(type);
            if (!string.IsNullOrWhiteSpace(actionParameters))
            {
                var dictionary = ConvertActionParameters(actionParameters);
                userAction.AddParameters(dictionary);
            }
            return userAction;
        }

        private static IDictionary<string, string> ConvertActionParameters(string actionParameters)
        {
            var serializer = new JavaScriptSerializer();
            var dictionary = serializer.Deserialize<Dictionary<string, string>>(actionParameters);
            return dictionary;
        }

        private UserActionFactory()
        {
            UserActionsMap = new Dictionary<string, Type>();
            //TODO: Read this from config or some external source
            RegisterUserAction(UserActionTypes.LinkClick, typeof (LinkClickAction));
            RegisterUserAction(UserActionTypes.Navigate, typeof(NavigateAction));
            RegisterUserAction(UserActionTypes.Submit, typeof(SubmitAction));
            RegisterUserAction(UserActionTypes.TextChange, typeof(TextChangeAction));
        }
    }
}

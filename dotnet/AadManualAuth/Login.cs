namespace AadManualAuth
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EntitlementsApplicationKey { get; set; }

        public Login()
        {
            EntitlementsApplicationKey = "RT";
        }
    }
}

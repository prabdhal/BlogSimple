namespace KeyVaultClient
{
    internal class AuthenticationCallback
    {
        private object keyVaultTokenCallback;

        public AuthenticationCallback(object keyVaultTokenCallback)
        {
            this.keyVaultTokenCallback = keyVaultTokenCallback;
        }
    }
}
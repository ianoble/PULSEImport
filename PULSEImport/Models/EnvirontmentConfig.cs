namespace PULSEImport
{
    public static class EnvironmentConfig
    {
        private static ConfigData _configData;

        public static ConfigData ConfigData
        {
            get { return _configData; }
            set { _configData = value; }
        }

        public static ConfigData GetQAConfig()
        {
            return new ConfigData()
            {
                ClientId = "bAv41PSbPv31TcIDnkoFHzR632Ma", // "e4tU3mJJ4reo1mQXPWbGX3joj_Qa",
                Secret = "E26lV8d4lOxlAciHPytqtQRf9TUa", // "e0TTH8m8KNRAzzvlaI5YLMQmKdEa",
                ApiUrl = "https://api-stg.trimble.com/t/trimble.com/qa/oculus/", // "https://api-stg.trimble.com/t/trimble.com/staging/capi/",
                // https://api-stg.trimble.com/t/trimble.com/oculus/accounts/v5/?name=wester
                // https://api-stg.trimble.com/t/trimble.com/staging/capi/
                PULSEApiUrl = "https://api-stg.trimble.com/t/trimble.com/qa/oculus/",
                OculusTId = "57becd96e4b0d4a289fd8f72" // TODO get source account on Staging
            };
        }

        public static ConfigData GetStagingConfig()
        {
            return new ConfigData()
            {
                ClientId = "oLXcXnpbxq84WLIMKbAgRlHBPq0a", // "e4tU3mJJ4reo1mQXPWbGX3joj_Qa",
                Secret = "PteTFNyWMpyog0RlHn5YkVfsVBoa", // "e0TTH8m8KNRAzzvlaI5YLMQmKdEa",
                ApiUrl = "https://api-stg.trimble.com/t/trimble.com/stg/pulse/", // "https://api-stg.trimble.com/t/trimble.com/staging/capi/",
                // https://api-stg.trimble.com/t/trimble.com/oculus/accounts/v5/?name=wester
                // https://api-stg.trimble.com/t/trimble.com/staging/capi/
                PULSEApiUrl = "https://api-stg.trimble.com/t/trimble.com/na/pulse/",
                OculusTId = "5823032be4b031d66b00e246" // TODO get source account on Staging
            };
        }


        public static ConfigData GetProductionConfig()
        {
            return new ConfigData()
            {
                ClientId = "wPsCetnN3OiJa8focyfXf1LsuK0a",
                Secret = "t8SoVjkFDg_HwI_I4wa9kOfBrzsa",
                ApiUrl = "https://api.trimble.com/t/trimble.com/oculus/",
                PULSEApiUrl = "https://api.trimble.com/t/trimble.com/na/pulse/",
                OculusTId = "58240ee5e4b01e7825f67da6"
            };
        }

        public static ConfigData GetProductionEUConfig()
        {
            return new ConfigData()
            {
                // EU Production
                ClientId = "sgJ5qoYcAFZeeakcs7QSzV60UaAa",
                Secret = "j9_keaO7vMC73t4zWB97TLh8PNAa",
                ApiUrl = "https://api.trimble.com/t/trimble.com/eu/pulse/",
                PULSEApiUrl = "https://api.trimble.com/t/trimble.com/eu/pulse/"

                //"host" : "api.trimble.com",
                //"token_api_path" : "/t/trimble.com/eu/pulse/token/v5",
                //"sensors_api_path" : "/t/trimble.com/eu/pulse/sensors/v5",
                //"accounts_api_path" : "/t/trimble.com/eu/pulse/accounts/v5",
                //"consumer_key" : "sgJ5qoYcAFZeeakcs7QSzV60UaAa",
                //"consumer_secret" : "j9_keaO7vMC73t4zWB97TLh8PNAa",
            };

        }
    }
}

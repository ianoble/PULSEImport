using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using OculusAPI.Models;

namespace PULSEImport.Functions
{
    internal class PulseObjects
    {
        private ConfigData _configData;

        internal PulseObjects(ConfigData configData)
        {
            _configData = configData;
        }

        internal List<Model> LoadDeviceModels()
        {
            Console.WriteLine("LoadDeviceModels");

            var deviceModels = new List<Model>();

            try
            {
                var modelCount = 0;
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var models = QuerySensorModels(pageIndex); // await Task.Run(() => QueryExistingSensors(pageIndex));

                    modelCount = models.metaInfo.totRsltSetCnt;

                    deviceModels.AddRange(models.types.ToList());

                } while (modelCount > deviceModels.Count);

                Console.WriteLine("Device Models: " + deviceModels.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return deviceModels;
        }

        private Models QuerySensorModels(int pageIndex = 1)
        {
            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            var oculusTId = _configData.OculusTId; // "58240ee5e4b01e7825f67da6";

            // Request Accounts 
            var jsonResponse = oculusApiRequest.ExecuteGET(
                _configData.ApiUrl +
                //_configData.PULSEApiUrl +
                //string.Format("types/v1?accountTId={0}&type={1}&targetObjCat={2}", oculusTId, "MODELS", "SENSORS"));
                string.Format("types/v1?type={0}&targetObjCat={1}&pageIndex={2}", "MODELS", "SENSORS", pageIndex));

            var models = JsonConvert.DeserializeObject<OculusAPI.Models.Models>(jsonResponse.ToString());

            return models;
        }

        internal List<Sensor> LoadExistingDevices()
        {
            Console.WriteLine("LoadExistingDevices");

            var accountDevices = new List<Sensor>();

            try
            {
                var deviceCount = 0;
                var pageIndex = 0;

                do
                {
                    pageIndex++;

                    var devices = QueryExistingSensors(pageIndex); // await Task.Run(() => QueryExistingSensors(pageIndex));

                    deviceCount = devices.metaInfo.totRsltSetCnt;

                    accountDevices.AddRange(devices.sensors.ToList());

                } while (deviceCount > accountDevices.Count);

                Console.WriteLine("Devices: " + accountDevices.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            return accountDevices;
        }

        private Sensors QueryExistingSensors(int pageIndex = 1)
        {
            Console.WriteLine("QueryExistingSensors");

            var oculusApiRequest = new OculusAPI.Services.OculusRequest(_configData.AccessToken);

            var jsonResponse =
                oculusApiRequest.ExecuteGET(_configData.PULSEApiUrl +
                                            string.Format("sensors/v5/?accountTId={0}&pageIndex={1}", _configData.AccountTId, pageIndex));

            var devices = JsonConvert.DeserializeObject<OculusAPI.Models.Sensors>(jsonResponse.ToString());

            return devices;
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using custom_speech.Models;

namespace custom_speech
{
    public class ModelHelper
    {
        Dictionary<string, object> ToDictionary(AcousticModel model)
        {
            return ModelToDictionary(model);
        }

        Dictionary<string, object> ToDictionary(LanguageModel model)
        {
            return ModelToDictionary(model);
        }

        private static Dictionary<string, object> ModelToDictionary(SpeechModel model)
        {
            Dictionary<string, object> postParameters = new Dictionary<string, object>();
            foreach (var propertyInfo in model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var param = propertyInfo.GetValue(model);
                var fileName = string.Empty;
                switch (propertyInfo.Name)
                {
                    case "languagedata":
                    case "transcriptions":
                        var textFile = (byte[])param;
                        fileName = propertyInfo.Name;
                        var textFileParameter = new FileParameter(textFile, fileName, "text/plain");
                        postParameters.Add(propertyInfo.Name, textFileParameter);
                        break;
                    case "audiodata":
                        var zipFile = (byte[])param;
                        fileName = propertyInfo.Name;
                        var zipFileParameter = new FileParameter(zipFile, fileName, "application/x-zip-compressed");
                        postParameters.Add(propertyInfo.Name, zipFileParameter);
                        break;
                    case "properties":
                        var jsonProperties = JsonConvert.SerializeObject(param);
                        postParameters.Add(propertyInfo.Name, jsonProperties);
                        break;
                    default: // it's just a string/string key value pair
                        postParameters.Add(propertyInfo.Name, param);
                        break;
                }
            }
            return postParameters;
        }

        public static AcousticModel CreateAcousticModel(SpeechModel model, byte[] zipFile, byte[] transcriptionFile)
        {
            var acousticModel = new AcousticModel()
            {
                name = model.name,
                description = model.description,
                locale = model.locale,
                dataImportKind = "Acoustic",
                properties = model.properties,
                audiodata = zipFile,
                transcriptions = transcriptionFile
            };
            return acousticModel;
        }

        public static LanguageModel CreateLanguageModel(SpeechModel model, byte[] languageFile)
        {
            var languageModel = new LanguageModel()
            {
                name = model.name,
                description = model.description,
                locale = model.locale,
                dataImportKind = "Language",
                properties = model.properties,
                languagedata = languageFile
            };
            return languageModel;
        }

        public void UploadLanguageDataset(string datasetUrl, string speechKey, LanguageModel model)
        {
            var parameters = ToDictionary(model);
            var trainer = new DatasetUpload();
            var response = trainer.MultipartFormDataPost(datasetUrl, speechKey, parameters);

            // Process response
            StreamReader responseReader = new StreamReader(response.GetResponseStream());
            string fullResponse = responseReader.ReadToEnd();
            response.Close();

            Console.WriteLine($"HttpResponse: {fullResponse}");
        }

        public void UploadAcousticDataset(string datasetUrl, string speechKey, AcousticModel model)
        {
            var parameters = ToDictionary(model);
            var trainer = new DatasetUpload();
            var response = trainer.MultipartFormDataPost(datasetUrl, speechKey, parameters);

            // Process response
            StreamReader responseReader = new StreamReader(response.GetResponseStream());
            string fullResponse = responseReader.ReadToEnd();
            response.Close();

            Console.WriteLine($"HttpResponse: {fullResponse}");
        }
    }
}

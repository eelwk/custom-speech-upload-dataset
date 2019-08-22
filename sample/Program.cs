using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using custom_speech;
using custom_speech.Models;

namespace sample
{
    class Program
    {
        private const string SpeechKey = "replaceme";
        // Update with your service region
        private const string DatasetUrl = "https://replaceme.cris.ai/api/speechtotext/v2/datasets/upload";
        private const string PathToLanguageTranscript = @"replaceme\files\LanguageTranscript.txt";
        private const string PathToAudioTranscript = @"replaceme\files\Channel_1_Audio_Transcript.txt";
        private const string PathToZipFile = @"replaceme\files\Channel_1_Audio_-0001.zip";

        static void Main(string[] args)
        {
            Encoding encoding = Encoding.UTF8;
            var languageBytes = encoding.GetBytes(File.ReadAllText(PathToLanguageTranscript, Encoding.UTF8));
            var transcriptBytes = encoding.GetBytes(File.ReadAllText(PathToAudioTranscript, Encoding.UTF8));
            var zipBytes = File.ReadAllBytes(PathToZipFile);

            var modelHelper = new ModelHelper();
            LanguageModel languageModel = GetLanguageModel();
            AcousticModel acousticModel = GetAcousticModel();

            languageModel = ModelHelper.CreateLanguageModel(languageModel, languageBytes);
            acousticModel = ModelHelper.CreateAcousticModel(acousticModel, zipBytes, transcriptBytes);

            modelHelper.UploadLanguageDataset(DatasetUrl, SpeechKey, languageModel);
            modelHelper.UploadAcousticDataset(DatasetUrl, SpeechKey, acousticModel);
        }

        private static LanguageModel GetLanguageModel()
        {
            var properties = new Dictionary<string, string>();
            properties.Add("ProfanityFilterMode", "Masked");
            properties.Add("PunctuationMode", "DictatedAndAutomatic");
            var speechModel = new LanguageModel()
            {
                name = "Test",
                description = "Test Description",
                locale = "en-US",
                dataImportKind = "Language",
                properties = properties
            };
            return speechModel;
        }

        private static AcousticModel GetAcousticModel()
        {
            var properties = new Dictionary<string, string>();
            properties.Add("ProfanityFilterMode", "Masked");
            properties.Add("PunctuationMode", "DictatedAndAutomatic");
            var speechModel = new AcousticModel()
            {
                name = "Test",
                description = "Test Description",
                locale = "en-US",
                dataImportKind = "Acoustic",
                properties = properties
            };
            return speechModel;
        }
    }
}

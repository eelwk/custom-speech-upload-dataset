# custom-speech-upload-dataset

This sample demonstrates how to send a prepared speech model training package per the Azure specifications:
https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/how-to-custom-speech-train-model

to the speech portal in an automated fashion. 

* note that the tricky part is that the post is a multi-part form data post *

You can always import packages manually in your region's speech portal (i.e. https://westus2.cris.ai/Datasets/Create?Kind=Acoustic)

For testing purposes, I have added the following files to the sample:

1. Channel_1_Audio_-0001.zip - acoustic model training
2. Channel_1_Audio_Transcript.txt - acoustic model training
3. LanguageTranscription.txt - language model training

To generate your own package, follow the Azure documentation on preparing a speech training model:

https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/how-to-custom-speech-train-model

# Prerequisites
**1. Set up Azure Speech Services, making sure you select the *S0 (Standard)* tier:**
https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/get-started

Make a note of:
1. the region the Service is located in
2. speech key (Key1 in Cognitive Services - Quick start blade)

**2. Link your new Cognitive Service with the Speech Portal:**
1. Go to http://[region].cris.ai/Subscriptions (example: https://westus2.cris.ai/Subscriptions)
2. Log in
3. Click "Connect existing subscription"
4. Paste in your speech key
5. Click "Add" button

# Run sample
1. Open solution file using Visual Studio (2017 or higher)
2. Restore all packages (open package manager console and run "dotnet restore")
3. Expand sample project and open Program.cs
4. Perform a search for "replaceme"
5. Replace speech key with your speech key
6. Replace Dataset URL with your regional host 
7. Replace PathToLanguageTranscript with where your language transcript file is
8. Replace PathToAudioTranscript with where your audio transcript transcript file is
9. Replace PathToZipFile with where your zip file is
10. Make sure you have your speech portal page open and handy: (i.e. https://westus2.cris.ai/Datasets)
11. Run the sample console application 
12. Open up the speech portal and see that your language and acoustic models have been sent and are processing.

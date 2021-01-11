
sj-jha-twitter project

Credit for all emojis images goes to Twemoji: https://twemoji.twitter.com/

To test:
 1 - Put your Twitter Bearer token in an environment variable named 'TWITTER_API_TOKEN'
 2 - Build and use the "dotnet" CLI to run the server under /server/sj-jha-twitter-server
 3 - Build and run the application under /jha-twitter/client/winforms/sj-jha-twitter-app
 4 - Click "Start" in the WinForms app
 
- OR - 

Set the solution up for dual startup projects: sj-jha-twitter-server -AND- sj-jha-twitter-app


KNOWN WEAKNESSES:
 - Should be profiled for memory leaks. There were some things that I should have been more diligent about disposing
 - More thought needs to be put into resilience in case of failures deep into stream reading/decoding paths
 - Further testing would be ideal


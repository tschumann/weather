# weather

I chose AWS Lambda because AWS SAM has the ability to output a template project with everything set up. Although it's a small project I decided to flesh it out a bit and put the
weather retrieval logic into a separate service - with more time it could be expanded out to get real weather. The small size of the project probably didn't warrant a model class
but it's better practice and less error-prone than passing around a Dictionary.

For the code itself I used AI Overview that Google Search provides - the template project meant that I got all the boiler-plate out of the box so the rest of what I had to do was
expand it out and figure out a few things, which AI might have been able to help with but I don't think it would have been a huge time-saver. For the GitHub workflow I used Copilot
to generate roughly what I needed based on the provided specs, asked it to add caching and tweaked it a bit to fix some issues and get it to do exactly what I wanted.

I didn't have time to figure out proper logger instantiation - normally I'd use dependency injection for the logger but AWS Lambda expects the function class to have a parameterless
constructor so I had to work around that (maybe there is a better way but I couldn't find anything). I'd improve the logger formatting as each logged line is across two lines. I'd
add a bit more unit testing as well - the tests for the HTTP requests cover most of it but there is one untested part of the service. Unknown endpoints return a 403 rather than a
404 as well though I think this has to do with AWS SAM and its mock Lambda environment rather than my code, but I'd fix that too if I could.

I assume that deployment will work as I did not try deploying to a real AWS account.


## Building

```
sam build
```

## Testing locally

```
sam local start-api
```

NOTE: use `curl.exe` to avoid using PowerShell's `curl` built-in
```
curl.exe -v http://127.0.0.1:3000/weather
curl.exe -v http://127.0.0.1:3000/weather?city=
curl.exe -v http://127.0.0.1:3000/weather?city=Sydney
curl.exe -v http://127.0.0.1:3000/weather?city=Beyond
```

## Unit testing

```
./test.ps1
```

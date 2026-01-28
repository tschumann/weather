Set-StrictMode -Version 3.0

$ErrorActionPreference = "Stop"
$PSNativeCommandUserErrorActionPerference = $true

# get the location of this file
$scriptpath = $MyInvocation.MyCommand.Path
# get the directory path to this file
$workingddir = Split-Path $scriptpath
# set the working directory as this file's directory
Push-Location $workingddir

# change into the test directory so we can run the tests - SAM doesn't provide a wrapper around unit tests so we need to go to the directory with the .csproj file to run the tests
Push-Location test\Weather.Test
dotnet test --logger:"console;verbosity=detailed"
# go back to the project root
Pop-Location

#!/bin/bash
set -ev

# upgrade node to latest version
if [ "$CI" ] && [ "$TRAVIS" ]
then 
	source ~/.nvm/nvm.sh; 
	nvm install 6;
	nvm use 6;
fi

# c# tests
dotnet test ./tests/Unit/Domain.Tests/Domain.Tests.csproj
dotnet test ./tests/Unit/Facade.Tests/Facade.Tests.csproj
dotnet test ./tests/Unit/Messaging.Tests/Messaging.Tests.csproj
dotnet test ./tests/Unit/Proxy.Tests/Proxy.Tests.csproj
dotnet test ./tests/Integration/Service.Tests/Service.Tests.csproj

echo $?
if [ $? -eq 1 ]; then
  echo dotnet test fail
  exit 1
fi

# javascript tests
cd ./src/Service.Host
./node_modules/.bin/jest --coverage
echo $?
result=$?
cd ../..

# report to codecov
bash <(curl -s https://codecov.io/bash)

exit $result
FROM mcr.microsoft.com/dotnet/core/runtime:2.2

ARG gitBranch=unspecified
ENV TZ=Europe/London

EXPOSE 5050

COPY bin/release/netcoreapp2.2/publish/ /app/bin/
COPY client/build/ /app/client/build/
COPY views/ /app/views/
     
ENV APP_PATH /app/
ENV GIT_BRANCH $gitBranch

CMD dotnet /app/bin/Linn.Stores.Service.Host.dll

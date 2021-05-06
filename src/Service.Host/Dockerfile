FROM microsoft/dotnet:2.2-runtime

RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*

ARG gitBranch=unspecified
ENV TZ=Europe/London

EXPOSE 5050

COPY bin/release/netcoreapp2.2/publish/ /app/bin/
COPY client/build/ /app/client/build/
COPY views/ /app/views/
     
ENV APP_PATH /app/
ENV GIT_BRANCH $gitBranch

CMD dotnet /app/bin/Linn.Stores.Service.Host.dll

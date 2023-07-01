FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /source
EXPOSE 80
EXPOSE 443

RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs
# COPY *.csproj .
COPY . ./
RUN dotnet restore "MyPastebin.csproj"

RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT [ "dotnet", "MyPastebin.dll" ]

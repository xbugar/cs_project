FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY . .

RUN dotnet tool install --global dotnet-ef

RUN dotnet restore

RUN dotnet publish -c Release -o /out

#RUN dotnet dev-certs https --trust

ENV PATH="$PATH:/root/.dotnet/tools"

CMD ["sh", "-c", "dotnet ef database update && dotnet /out/ExpenseManager.Api.dll"]

# Setup project

## Download and install .NET on Linux WSL

1) Download
```bash
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
```

2) Grant permission
```bash
chmod +x ./dotnet-install.sh
```

3) Install
```bash
./dotnet-install.sh --version latest
```

4) Set env variables
```bash
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools
```


## Setup very basic clean architecture

```bash
dotnet new webapi -n Api -o src/Api
dotnet new classlib -n Application -o src/Application
dotnet new classlib -n Domain -o src/Domain
dotnet new classlib -n Infrastructure -o src/Infrastructure

dotnet new xunit -n UnitTests -o tests/UnitTests
dotnet new xunit -n IntegrationTests -o tests/IntegrationTests
```
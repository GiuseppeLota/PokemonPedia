# PokemonPedia

This .NET core project that expose the following Api:

1) GET /pokemon/translate/{pokemonName}

Parameters:

| parameter name | type | description  |
|---|---|---|
| pokemonName | string | Indicates the pokemon name |

Description:

This Api will retrieve useful pokemon informations for the provided pokemon name with a fun description:

| Fields  |   
|---|
| Name  |
| Description  |
| Habitat  |
| Is_Legendary  |

1) GET /pokemon/{pokemonName}

Parameters:

| parameter name | type | description  |
|---|---|---|
| pokemonName | string | Indicates the pokemon name |

Description:

This Api will retrieve useful pokemon informations for the provided pokemon name with the standard description:

| Fields  |   
|---|
| Name  |
| Description  |
| Habitat  |
| Is_Legendary  |


## Quick start

### Run with dotnet cli

1) Ensure the installation of the .NET SDK. For more info -> [Install .NET CORE](https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=net50)


2) navigate to the root directory of the local copy of the repo and launch the following command:

```
dotnet run --project Sources/PokemonPedia.Api/PokemonPedia.Api.csproj
```

3) open another shell window to perform a quick test:

```
curl http://localhost:5000/pokemon/translate/ditto
```

### Run with docker

1) Install Docker locally. More details here -> [Install Docker](https://docs.docker.com/engine/install/)

2) navigate to the root directory of the project and run:

```
docker build -t pokemonpedia_img .
```

2) Then you can create and run the container with:

```
docker run --name pokemonpedia_container -p 5000:80 pokemonpedia_img
```

### Run Unit Tests

1) To launch Api project tests:

```
dotnet test .\Tests\PokemonPedia.Api.Tests\PokemonPedia.Api.Tests.csproj
```

2) To launch Application project tests:

```
dotnet test .\Tests\PokemonPedia.Application.Tests\PokemonPedia.Application.Tests.csproj
```

3) To launch Infrastructure project tests:

```
dotnet test .\Tests\PokemonPedia.Infrastructure.Tests\PokemonPedia.Infrastructure.Tests.csproj
```


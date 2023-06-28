# OPC UA Server & Client Solution :gear: :computer:

A minimal implementation of OPC UA server and client applications using C#.

## Overview :telescope:

This solution consists of two projects, `Server` and `Client`, which demonstrate the functionality of an OPC UA server and client respectively.

The solution showcases the following features:
- OPC UA server and client communication
- Secure communication using certificates
- Minimal implementation for easy understanding

## Getting Started :rocket:

To use this solution, follow these steps:

### 1. Build the Server

First, build the server project to generate the necessary certificates. At the root, run the following command:

```bash
dotnet build --project OPCUA-Server/
```

### 2. Build the Client

Next, build the client project by running the following command:

```bash
dotnet build --project OPCUA-Client/
```

### 3. Run the Server and then the Client

Start the OPC UA server by running the following command:

```bash
dotnet run --project OPCUA-Server/
```

The server will start and initialize the necessary certificates.


Finally, start the OPC UA client:

```bash
dotnet run --project OPCUA-Client/
```

The client will connect to the OPC UA server and establish communication.

## Project Structure :file_folder:

The solution has the following project structure:

```text
Solution/
├── Server/
│   ├── ...
│   └── Server.csproj
├── Client/
│   ├── ...
│   └── Client.csproj
├── Solution.sln
└── README.md
```

- `Server/`: Contains the OPC UA server implementation.
- `Client/`: Contains the OPC UA client implementation.
- `Solution.sln`: The solution file for JetBrain Rider or other IDEs.
- `README.md`: This file, providing an overview and instructions for the solution.

## Dependencies :books:

The solution has dependencies on the following Nuget packages:
- `Opc.UaFx.Advanced`: OPC UA server library, in the OPCUA-Server project
- `Opc.UaFx.Client`: OPC UA client library, in the OPCUA-Client project

Ensure that these dependencies are correctly retrieved with `dotnet restore`.

## Contributing :raised_hands:

Contributions to enhance this solution are welcome! If you find any issues or have ideas for improvements, feel free to create a pull request or submit an issue.

## License :page_with_curl:

This solution is licensed under the [MIT License](LICENSE.md).

Feel free to modify and add any additional information specific to your solution. Enjoy!

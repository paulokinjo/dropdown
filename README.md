# Backend and Tests Project


## Project Structure

The project is organized into the following main directories:

- `src`: Contains the source code for the backend services.
- `tests`: Contains the test cases for the backend services.
- `clientapp`: Contains the client application. For more information, refer to the [README.md](./clientapp/README.md) file in the `clientapp` directory.

## Technology Stack

This project is built using the following technologies:

- .NET Core
- Entity Framework Core
- xUnit for testing
- Docker for containerization

## Prerequisites

Before you begin, ensure you have met the following requirements:

- You have installed .NET Core SDK.
- You have installed Docker.
- You have a basic understanding of C# and .NET Core.

## Getting Started

To get a local copy up and running, follow these steps:

1. Clone the repository:
    ```sh
    git clone https://github.com/paulokinjo/dropdown.git
    ```
2. Navigate to the project directory:
    ```sh
    cd dropdown
    ```
3. Build the project:
    ```sh
    dotnet build
    ```

## Running the Application

To run the application, use the following command:
```sh
dotnet watch run
```

## Running Tests

To run the tests, use the following command:
```sh
dotnet test
```

## License

Distributed under the MIT License. See `LICENSE` for more information.

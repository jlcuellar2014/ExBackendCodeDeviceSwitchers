# CodeChallenge Backend

## Requirements

Developers are required to use the following technologies, but they are free to incorporate any additional technologies they find necessary:

- dotnet core 6.X or higher
- EF core 6.X or higher
- JWT authentication (Bearer Authentication)

## Problem Description

The goal is to create an API that allows:

- Logging into the application (obtaining JWT for Bearer Authentication)
- Querying devices (with their associated connections)
- Querying switches (with connected devices)
- Connecting a device to a switch (only for authenticated users)
- Disconnecting a device from a switch (only for authenticated users)

Data must be persisted in a local database, at the discretion of the developer. Upon application startup, previous data must be maintained. Data migration capabilities should be respected. All communications will be sent and received using JSON. Comprehensive documentation for endpoints, their uses, and examples is highly desirable.

### Models

The following are the minimum models expected in the database, but developers are free to create additional models as needed.

Device:

- Id
- Hostname
- PortList

Switch:

- Id
- Hostname
- PortList

### Endpoints

The implementation should include, at a minimum, the following endpoints:

- POST /login
- GET /devices
- GET /switches
- POST /switches/{switch_id}/connect-device/{device_id}, with a body indicating the ports to be connected.

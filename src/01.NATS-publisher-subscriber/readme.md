# NATS Publisher & Subscriber Example

This directory contains a **.NET** solution demonstrating a simple **publisher-subscriber** model using **NATS**. It also includes a **Docker Compose** file to run a NATS cluster (three nodes) alongside the .NET publisher and subscriber applications.

---

## Directory Structure

```
NATS.Playground/
└── src/
    └── 01.NATS-publisher-subscriber/
        ├── NatsDemo.Common/
        ├── NatsDemo.Publisher/
        │   ├── Program.cs
        │   ├── Dockerfile
        ├── NatsDemo.Subscriber/
        │   ├── Program.cs
        │   ├── Dockerfile
        ├── NatsDemo.sln
        ├── docker-compose.yml
        ├── .dockerignore
        └── launchSettings.json
```

### Projects

1. **NatsDemo.Common**  
   - Contains shared code (e.g., `NatsConnection`, models, helper classes).

2. **NatsDemo.Publisher**  
   - A console application that publishes messages to a NATS subject (`test.message` by default).
   - Has its own `Dockerfile` for containerization.

3. **NatsDemo.Subscriber**  
   - A console application that subscribes to the same subject and listens for incoming messages.
   - Has its own `Dockerfile` for containerization.

4. **docker-compose.yml**  
   - Defines the NATS cluster (3 nodes) and builds/runs the publisher and subscriber containers.
   - Ensures all containers share the same network (`nats-network`).

---

## How It Works

1. **NATS Cluster**  
   - Three containers: `nats-1`, `nats-2`, `nats-3`  
   - Each runs a NATS server in cluster mode with JetStream enabled (`-js`).  
   - Exposes ports for client connections, monitoring, and clustering.

2. **Publisher**  
   - Connects to `nats-1` by default (using the environment variable `NATS_URL`).
   - Publishes JSON-serialized messages every second to the `test.message` subject.

3. **Subscriber**  
   - Also connects to `nats-1` via the same environment variable.
   - Subscribes to the `test.message` subject and prints any incoming message to the console.

---

## Getting Started

1. **Prerequisites**  
   - [Docker](https://www.docker.com/) installed and running.  
   - [Docker Compose](https://docs.docker.com/compose/) (usually included with Docker Desktop).

2. **Build and Run**  
   - Open a terminal in the `src/01.NATS-publisher-subscriber` directory.
   - Run the following command:
     ```bash
     docker-compose up --build -d
     ```
   - This will:
     - Pull the latest NATS image for `nats-1`, `nats-2`, and `nats-3`.
     - Build Docker images for `NatsDemo.Publisher` and `NatsDemo.Subscriber` from their respective `Dockerfile`s.
     - Start all five containers in detached mode (`-d`).

3. **Check Running Containers**  
   ```bash
   docker ps
   ```

   You should see:

   nats-1, nats-2, nats-3
   nats-publisher
   nats-subscriber

4. View Logs

   Publisher logs:
   ```bash
   docker-compose logs -f publisher
   ```

   Subscriber logs:
   ```bash
   docker-compose logs -f subscriber
   ```

   Nats logs:
   ```bash
   docker-compose logs -f nats-1
   docker-compose logs -f nats-2
   docker-compose logs -f nats-3
   ```

   To stop and remove all containers:
   ```bash
   docker-compose down
   ```

## Customizing 
              
1. Environment Variable
   By default, the publisher and subscriber use NATS_URL=nats://nats-1:4222. 
   If you change the service name or port, update the environment section in the docker-compose.yml accordingly.

2. Subject Name
   By default, the publisher publishes messages to the test.message subject. 
   You can change this by updating the subject name in the Publisher project (Program.cs) and Subscriber project (Program.cs).

## Troubleshooting
1. Connection Refused:
Make sure the NATS_URL matches the service name (nats-1) and port (4222). Also ensure all containers are on the same network.
2. Logs:
If the publisher or subscriber exits unexpectedly, check the logs to see any detailed error messages.
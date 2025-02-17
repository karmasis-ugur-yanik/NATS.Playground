# NATS Playground

A collection of demos and proof-of-concepts (PoCs) showcasing various features and usage patterns of [NATS](https://nats.io/) with .NET. Each demo is organized in its own subdirectory under `src/`.

01. NATS Publisher-Subscriber
  - Description: Demonstrates a basic Publish/Subscribe scenario with NATS, including a 3-node NATS cluster and .NET console apps for publishing and subscribing.
  - Location: src/01.NATS-publisher-subscriber/
  - Quick Start:
      Navigate to the src/01.NATS-publisher-subscriber/ directory.
      Run docker-compose up --build -d to start the NATS cluster, Publisher, and Subscriber.
      Check logs using docker-compose logs -f publisher or docker-compose logs -f subscriber.
  - Future Demos
      plan to add more demos exploring advanced features such as:

      1. JetStream (persistent queue)
      1. Store-and-forward configuration (client will send the message to local NATS server, local NATS server will forward the message to central NATS)
      1. Pub/Sub (in-memory and persistent mode)
      1. Authentication (OAuth 2.0 Device Authorization)
         - Auth Callout(s)

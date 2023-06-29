# Implementing OPC UA

So how do we actually implement OPC UA by developing programs that act as OPC UA clients or servers?

In OPC UA, the client and server are typically loosely coupled, meaning the client does not need to know the schema of the server beforehand. OPC UA provides a standardized information model called the Address Space, which defines the structure and organization of data exposed by the server.

The Address Space represents the data hierarchy and relationships within the server, including objects, variables, methods, and other entities. The server defines the Address Space based on the specific application and data it exposes. The client can dynamically discover and browse the server's Address Space to understand its structure and access the available data.

When a client connects to an OPC UA server, it can perform discovery and browse the server's Address Space to explore the available nodes and their properties. The client can navigate the hierarchy, query information about nodes, and interact with the server based on the discovered structure.

The loose coupling between the client and server allows for flexibility and interoperability. The client can adapt to different servers with varying Address Space structures without prior knowledge of the schema. The standardized OPC UA information model ensures consistency and enables seamless communication between clients and servers from different vendors or implementations.

However, it's worth noting that certain OPC UA server implementations may provide additional features or custom information models that require some level of awareness on the client side. In such cases, the client may need to have some knowledge or configuration specific to that server implementation.

## The server

As we said above, in OPC UA,the data structure in an OPC UA server is based on a hierarchical model known as the Address Space. The Address Space consists of nodes, representing entities such as objects, variables, methods, and more. Each node possesses a unique identifier and can have attributes like name, value, data type, and access level.

To illustrate this, consider a factory with multiple production lines. Each production line consists of various machines, such as SMT machines, reflow ovens, AOI machines, ICT machines, functional test stations, and packaging machines. For example:

```
Factory
├── Production Line 1
│   ├── SMT Machine 1 (Model: Yamaha YSM20)
│   ├── SMT Machine 2 (Model: Panasonic NPM-W2)
│   ├── Reflow Oven (Model: BTU Pyramax)
│   ├── AOI Machine (Model: CyberOptics SE500)
│   ├── ICT Machine (Model: Keysight 3070)
│   ├── Functional Test Station (Model: National Instruments PXI)
│   └── Packaging Machine (Model: Universal Instruments U-flex)
├── Production Line 2
   ├── SMT Machine 1 (Model: Juki FX-3)
   ├── SMT Machine 2 (Model: ASM SIPLACE SX)
   ├── Reflow Oven (Model: Heller 1809 MKIII)
   ├── AOI Machine (Model: KohYoung Zenith)
   ├── ICT Machine (Model: Teradyne TestStation)
   ├── Functional Test Station (Model: Advantest T2000)
   └── Packaging Machine (Model: Fuji AIMEX III)
```

In this example, the factory's Address Space hierarchy can be visualized as a tree-like structure, with the factory as the root node, production lines as intermediate nodes, and the machines as leaf nodes. The hierarchical organization helps logically group and organize the data related to each machine and production line.

## The client

When a client wants to access data from an OPC UA server, it follows a series of steps:

1. Discovery: The client typically performs a discovery process to locate available OPC UA servers on the network. This allows the client to identify the OPC UA server representing the factory.

2. Establishing a Session: Once the client identifies the OPC UA server representing the factory, it establishes a session with the server. This session enables the client to interact with the server and access its data.

3. Browsing the Address Space: The client can browse the server's Address Space to explore the available nodes and their properties. By traversing the hierarchical structure, the client can understand the organization and relationships between different entities within the factory, such as the production lines and machines.

4. Reading and Writing Data: Using the information obtained during the browsing step, the client can read specific values from the nodes of interest by sending read requests to the server.

**Go back to the [README](../README.md) to see an implementation of this example.**

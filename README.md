# No-Code Nodes

A collection of explorations from the AEC Tech Hackathon 2025 Barcelona, focusing on building and surface data processing using n8n workflows and custom nodes.

> **Note**: This is a work in progress and represents our explorations during the hackathon. The code and workflows are experimental and may require adjustments to work in your environment.

## Project Overview

This repository contains our experiments with n8n workflows and custom nodes for processing building and surface data. The project aims to demonstrate how no-code/low-code tools can be used to process and analyze AEC data.

## Project Structure

The repository is organized into three main directories:

### 1. n8n-custom-nodes/

Custom JavaScript functions that operate on data within n8n workflows:

- `prepare_building_sentences.js` - Prepares building data for natural language processing
- `aggregate_building_stats.js` - Aggregates building statistics and metrics
- `aggregate_surface_stats.js` - Aggregates surface statistics and measurements
- `get_building_data.js` - Retrieves building data from various sources
- `get_surface_data.js` - Retrieves surface data and measurements

### 2. n8n-server-functions/

Server-side functions for n8n, including:

- `classify/` - Contains classification functions for building and surface data
- GCP Functions for data processing and analysis

### 3. n8n-graph/

Contains n8n workflow graphs and configurations that demonstrate how to:

- Process building data
- Analyze surface measurements
- Generate reports and visualizations
- Integrate with various data sources

## Prerequisites

- [n8n](https://n8n.io/) either docker deployed or running just on the web
- Node.js (version 14 or higher)
- npm or yarn package manager

## Installation

1. Clone this repository:

```bash
git clone https://github.com/yourusername/no-code-nodes.git
cd no-code-nodes
```

## Usage

1. Create a workspace in n8n
2. Import the workflow graphs from the `n8n-graph/` directory as needed
3. The custom nodes will be available in the n8n workflow editor
4. Run the workflow (might break, we tinkered a lot and by the time you read this, we likely tore down the server functions, but atleast you have the code to replicate it your self)

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support, please open an issue in the GitHub repository.

## AEC Tech Hackathon 2025 Barcelona

This project was developed during the AEC Tech Hackathon 2025 in Barcelona. It represents our team's exploration of using no-code/low-code tools for AEC data processing and analysis. The code and workflows are experimental and may require adjustments to work in your environment.

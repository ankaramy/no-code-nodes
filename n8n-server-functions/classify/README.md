# Building Data Classification GCP Function

This is a Google Cloud Function that processes and classifies building data. It was developed as part of the AEC Tech Hackathon 2025 Barcelona project to demonstrate serverless processing of AEC data.

## Overview

The function receives building data through HTTP requests, processes it using various classification algorithms, and returns structured results. It's designed to be called from n8n workflows to process building data in a serverless environment.

## Files

- `main.py` - The main function code
- `requirements.txt` - Python dependencies
- `deploy.txt` - Deployment instructions
- `cloudbuild.yaml` - Cloud Build configuration
- `.gcloudignore` - Files to ignore during deployment
- `test_data.json` - Sample data for testing

## Setup

1. Ensure you have the Google Cloud SDK installed
2. Set up a Google Cloud project
3. Enable the Cloud Functions API
4. Deploy the function using the instructions in `deploy.txt`

## Usage

The function accepts an array of building descriptions through HTTP POST requests. Each building description should be in the following format:

```json
[
  {
    "sentence": "A building with no specific name."
  },
  {
    "sentence": "A building named Office of the Parliamentary Counsel."
  },
  {
    "sentence": "A church building named St Martin-in-the-Fields."
  }
]
```

Example Python code to call the function:

```python
import requests

url = "YOUR_FUNCTION_URL"
data = [
    {
        "sentence": "A building with no specific name."
    },
    {
        "sentence": "A building named Office of the Parliamentary Counsel."
    }
]
response = requests.post(url, json=data)
```

The function will process each building description and return classification results for each entry.

## Integration with n8n

This function is designed to be called from n8n workflows. You can use the HTTP Request node in n8n to call this function and process the results.

## Development

To modify the function:

1. Update the code in `main.py`
2. Test locally using the test data
3. Deploy using the instructions in `deploy.txt`

## Note

This is a work in progress and was developed during the AEC Tech Hackathon 2025 Barcelona. The function may require adjustments to work in your specific environment.

import functions_framework
from flask import jsonify, request
from sentence_transformers import SentenceTransformer
import numpy as np

model = SentenceTransformer('all-MiniLM-L6-v2')

CATEGORIES = {
    "Civic & Government": "government office public administration court commission",
    "Religious": "church temple mosque chapel religious worship cathedral",
    "Residential": "apartment plinth tower condo residential housing",
    "Commercial": "retail commercial shop store business",
    "Office": "office corporate headquarters consulting firm embassy",
    "Infrastructure": "bridge plinth structural utility building",
    "Mixed-use": "mixed use multiple purposes combined building",
    "Unknown": "unknown unclassified unspecified building"
}

# Precompute category embeddings once
CATEGORY_EMBEDDINGS = {
    cat: model.encode(desc)
    for cat, desc in CATEGORIES.items()
}


@functions_framework.http
def get_category_scores(request):
    request_json = request.get_json(silent=True)

    if request_json is None:
        return jsonify({
            "error": "No JSON data provided",
            "message": "Please send a POST request with an array of objects, each containing a 'sentence' field"
        }), 400

    # Filter out items without "sentence"
    valid_items = [item for item in request_json if "sentence" in item]
    if not valid_items:
        return jsonify({
            "error": "No valid 'sentence' fields found in the input."
        }), 400

    # Extract sentences and compute embeddings in batch
    sentences = [item["sentence"] for item in valid_items]
    sentence_embeddings = model.encode(sentences, batch_size=32)

    results = []

    for item, sent_emb in zip(valid_items, sentence_embeddings):
        scores = {
            cat: float(np.dot(sent_emb, cat_emb) /
                       (np.linalg.norm(sent_emb) * np.linalg.norm(cat_emb)))
            for cat, cat_emb in CATEGORY_EMBEDDINGS.items()
        }
        best_category = max(scores.items(), key=lambda x: x[1])

        results.append({
            "sentence": item["sentence"],
            "classification": {
                "category": best_category[0],
                "confidence": best_category[1],
                "all_scores": scores
            }
        })

    return jsonify({
        "message": "Building classifications completed",
        "results": results
    })


# @functions_framework.http
# def hello_my_friend(request):
#     # Get the JSON data from the request
#     request_json = request.get_json(silent=True)

#     if request_json is None:
#         return jsonify({
#             "error": "No JSON data provided",
#             "message": "Please send a POST request with JSON data containing 'sentence' field"
#         }), 400

#     # Get all keys from the JSON data
#     keys = list(request_json.keys())

#     return jsonify({
#         "message": "Here are the keys from your JSON data",
#         "keys": keys,
#         "original_data": request_json
#     })

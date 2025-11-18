#!/bin/bash

# VibeHome API Test Script
# This script demonstrates how to use the VibeHome API with curl

API_BASE_URL="http://localhost:5000"
API_KEY="test-api-key-12345"

echo "======================================"
echo "VibeHome API Test Script"
echo "======================================"
echo ""

# Test 1: Get all Kids
echo "Test 1: Get all Kids"
echo "GET /odata/Kids"
curl -s -X GET "${API_BASE_URL}/odata/Kids" \
  -H "X-API-Key: ${API_KEY}" \
  -H "Accept: application/json" | jq '.'
echo ""
echo "--------------------------------------"
echo ""

# Test 2: Get a specific Kid
echo "Test 2: Get Kid with ID 1"
echo "GET /odata/Kids(1)"
curl -s -X GET "${API_BASE_URL}/odata/Kids(1)" \
  -H "X-API-Key: ${API_KEY}" \
  -H "Accept: application/json" | jq '.'
echo ""
echo "--------------------------------------"
echo ""

# Test 3: Filter Kids by name
echo "Test 3: Filter Kids (contains 'John')"
echo "GET /odata/Kids?\$filter=contains(Name, 'John')"
curl -s -X GET "${API_BASE_URL}/odata/Kids?\$filter=contains(Name,%20'John')" \
  -H "X-API-Key: ${API_KEY}" \
  -H "Accept: application/json" | jq '.'
echo ""
echo "--------------------------------------"
echo ""

# Test 4: Get Chore Completions with expand
echo "Test 4: Get Chore Completions (top 5, with Kid and ChoreItem)"
echo "GET /odata/ChoreCompletions?\$top=5&\$expand=Kid,ChoreItem"
curl -s -X GET "${API_BASE_URL}/odata/ChoreCompletions?\$top=5&\$expand=Kid,ChoreItem" \
  -H "X-API-Key: ${API_KEY}" \
  -H "Accept: application/json" | jq '.'
echo ""
echo "--------------------------------------"
echo ""

# Test 5: Count Kids
echo "Test 5: Count Kids"
echo "GET /odata/Kids/\$count"
curl -s -X GET "${API_BASE_URL}/odata/Kids/\$count" \
  -H "X-API-Key: ${API_KEY}" \
  -H "Accept: application/json"
echo ""
echo ""
echo "--------------------------------------"
echo ""

# Test 6: Create a new Kid
echo "Test 6: Create a new Kid"
echo "POST /odata/Kids"
curl -s -X POST "${API_BASE_URL}/odata/Kids" \
  -H "X-API-Key: ${API_KEY}" \
  -H "Content-Type: application/json" \
  -H "Accept: application/json" \
  -d '{
    "name": "Test Kid",
    "createdAt": "2025-11-18T00:00:00Z",
    "modifiedAt": "2025-11-18T00:00:00Z",
    "isDeleted": false
  }' | jq '.'
echo ""
echo "--------------------------------------"
echo ""

# Test 7: Get all API Keys
echo "Test 7: Get all API Keys"
echo "GET /odata/ApiKeys"
curl -s -X GET "${API_BASE_URL}/odata/ApiKeys" \
  -H "X-API-Key: ${API_KEY}" \
  -H "Accept: application/json" | jq '.'
echo ""
echo "--------------------------------------"
echo ""

echo "======================================"
echo "Tests completed!"
echo "======================================"

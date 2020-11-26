# random.org random number generator

random.org offers API that you can use to generate real (and cryptographically secure) random numbers. More details about the API can be found here:

https://api.random.org/json-rpc/2/basic

Your task is to test-drive the design and the implementation of the RandomOrgRandomNumberGenerator class so that it can be used to generate a random integer between specified min and max values using the _generateIntegers_ method (consult the documentation provided on link above).

Make sure your class implements both the "happy-path" and the failure scenarios.

For reference, here is a curl request fetching one random integer in range [1, 100]:

```bash
curl --location --request POST 'https://api.random.org/json-rpc/2/invoke' \
--header 'Content-Type: application/json' \
--data-raw '{
    "jsonrpc": "2.0",
    "method": "generateIntegers",
    "params": {
        "apiKey": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "n": 1,
        "min": 1,
        "max": 100,
        "replacement": true
    },
    "id": 42
}'

```

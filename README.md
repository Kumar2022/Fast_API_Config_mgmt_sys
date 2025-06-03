# Configuration Management System

A FastAPI application to manage a Configuration Management system for onboarding organisations from different countries.

## Features

- Create country-specific onboarding requirements
- Retrieve onboarding requirements for a country
- Update onboarding requirements for a country
- Delete onboarding requirements for a country

## Technologies Used

- `FastAPI`
- `PostgreSQL`
- `SQLAlchemy`
- `Pydantic`
- `uvicorn`
- `psycopg2-binary`
- `python-dotenv`

1. Create a virtual environment and activate it:
`python -m venv venv`
`source venv/bin/activate`
- On Windows use `venv\Scripts\activate`

2. Install the dependencies:
`pip install -r requirements.txt`

3.Set up the database:
Update the `"DATABASE_URL"` in the `.env` file with your PostgreSQL credentials.

4.Run the application:
`uvicorn app.main:app --reload`


## USAGE

1. create Configuration
- `curl -X POST "http://127.0.0.1:8000/create_configuration" -H "Content-Type: application/json" -d '{
    "country_code": "IN",
    "requirements": {
        "Business Name": "required",
        "PAN": "required",
        "GSTIN": "required",
        "Address": "required",
        "Contact Number": "required"
    }
}'`


2. Get Configuration
- `curl -X GET "http://127.0.0.1:8000/get_configuration/IN"`


3. Update Configuration
- `curl -X POST "http://127.0.0.1:8000/update_configuration" -H "Content-Type: application/json" -d '{
    "country_code": "IN",
    "requirements": {
        "Business Name": "required",
        "PAN": "required",
        "GSTIN": "required",
        "Address": "required",
        "Contact Number": "required",
        "Email": "required"
    }
}'`


4. Delete Configuration
- `curl -X DELETE "http://127.0.0.1:8000/delete_configuration/IN"`






## C# Libraries

The `csharp` directory contains example libraries including a configuration helper and an in-memory filesystem. See `csharp/README.md` for build and test instructions.

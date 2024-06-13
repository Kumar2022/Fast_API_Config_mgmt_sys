from fastapi import FastAPI, HTTPException, Depends
from sqlalchemy.orm import Session
from . import models, schemas, crud, database

app = FastAPI()

models.Base.metadata.create_all(bind=database.engine)

@app.post("/create_configuration", response_model=schemas.Configuration)
def create_configuration(config: schemas.ConfigurationCreate, db: Session = Depends(database.get_db)):
    db_config = crud.get_configuration(db, country_code=config.country_code)
    if db_config:
        raise HTTPException(status_code=400, detail="Configuration already exists")
    return crud.create_configuration(db=db, config=config)

@app.get("/get_configuration/{country_code}", response_model=schemas.Configuration)
def get_configuration(country_code: str, db: Session = Depends(database.get_db)):
    db_config = crud.get_configuration(db, country_code=country_code)
    if db_config is None:
        raise HTTPException(status_code=404, detail="Configuration not found")
    return db_config

@app.post("/update_configuration", response_model=schemas.Configuration)
def update_configuration(config: schemas.ConfigurationCreate, db: Session = Depends(database.get_db)):
    db_config = crud.update_configuration(db, country_code=config.country_code, requirements=config.requirements)
    if db_config is None:
        raise HTTPException(status_code=404, detail="Configuration not found")
    return db_config

@app.delete("/delete_configuration/{country_code}", response_model=schemas.Configuration)
def delete_configuration(country_code: str, db: Session = Depends(database.get_db)):
    result = crud.delete_configuration(db, country_code=country_code)
    if not result:
        raise HTTPException(status_code=404, detail="Configuration not found")
    return {"detail": "Configuration deleted"}

# Add example data
@app.on_event("startup")
def startup_event():
    with database.SessionLocal() as db:
        # Example data for India
        india_config = schemas.ConfigurationCreate(
            country_code="IN",
            requirements={
                "Business Name": "Flipkart Internet Pvt ltd",
                "PAN": "LOPLI3365J",
                "GSTIN": "09AAACH7409R1ZZ",
                "Address": "Vaishnavi Summit, Ground Floor, 7th Main, 80 Feet Road, 3rd Block, Koramangala Industrial Layout, Bangalore KA IN 560034",
                "Contact Number": "7878789798"
            }
        )
        if not crud.get_configuration(db, "IN"):
            crud.create_configuration(db, india_config)

        # Example data for USA
        usa_config = schemas.ConfigurationCreate(
            country_code="US",
            requirements={
                "Business Name": "The Trump Organisation",
                "Registration Number": "12-3456789",
                "Employer Identification Number (EIN)": "123-45-6789",
                "Address": "456 Oak Street, San Francisco, CA, 94108",
                "Contact Number": "+1-415-555-8765"
            }
        )
        if not crud.get_configuration(db, "US"):
            crud.create_configuration(db, usa_config)
